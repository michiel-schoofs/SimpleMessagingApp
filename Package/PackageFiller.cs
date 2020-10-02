using Package.Attributes;
using Package.DTO;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Package {
    public static class PackageFiller<Y> where Y : BaseDTO, new(){
        public static Y Fill(byte[] stream) {
            Y result = new Y();
            List<PropertyInfo> ordered = GetOrderedList(result);
            int totalLength = 0;

            foreach (PropertyInfo pi in ordered) {
                Length length = pi.GetCustomAttribute<Length>();

                if (length == null)
                    throw new ArgumentException(string.Format("You haven't provided a length attribute for property {0}", pi.Name));

                byte[] property_result = new byte[length.GetLength()];
                Array.Copy(stream, totalLength, property_result, 0, length.GetLength());
                totalLength += length.GetLength();

                pi.SetValue(result, property_result);
            }

            return result;
        }

        private static List<PropertyInfo> GetOrderedList(Y dto) {
            List<PropertyInfo> properties = new List<PropertyInfo>();

            foreach (PropertyInfo pi in dto.GetType().GetProperties()) {
                SerializePlace place = pi.GetCustomAttribute<SerializePlace>();

                if (place == null)
                    throw new ArgumentException(string.Format("You have no provided a SerializePlace attribute for property {0}", pi.Name));

                properties.Insert(place.GetPlace(), pi);
            }

            return properties;
        }
    }
}