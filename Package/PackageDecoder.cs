using Package.Attributes;
using Package.DTO;
using Package.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Package {
    public static class PackageDecoder<T,Y>  where T : BaseDTO,new() where Y: BaseModel, new() {
        public static Y Decode(byte[] stream) {
            Y result = new Y();
            T dto = Fill(stream);

            return result;
        }


        private static T Fill(byte[] stream) {
            T result = new T();
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

        private static List<PropertyInfo> GetOrderedList(T dto) {
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
