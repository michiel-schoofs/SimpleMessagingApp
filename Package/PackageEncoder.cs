using Package.Attributes;
using Package.DTO;
using Package.Enum;
using Package.Models;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Package {
    public static class PackageEncoder<T,Y> where T: BaseDTO, new() where Y : BaseModel {

        public static T Parse(Y model) {
            T result = new T();

            PropertyInfo[] modelProperties = model.GetType().GetProperties();
            PropertyInfo[] dtoProperties = result.GetType().GetProperties();

            foreach (PropertyInfo property in modelProperties) {
                PropertyInfo corresponding = dtoProperties.FirstOrDefault(p => p.Name.Equals(property.Name));
                
                if (corresponding == null) {
                    throw new ArgumentException(string.Format("No corresponding dto property found for: {0}",property.Name));
                }

                Length lengthAttribute = corresponding.GetCustomAttribute<Length>();

                if (lengthAttribute == null) {
                    throw new Exception(string.Format("No length attribute provided for dto property {0}",corresponding.Name));
                }

                corresponding.SetValue(result, ParsePropertyInfo(property, model, lengthAttribute.GetLength()));
            }

            return result;
        }

        private static byte[] ParsePropertyInfo(PropertyInfo propertyInfo, Y model, int length) {
            byte[] bytes = new byte[length];

            switch (propertyInfo.PropertyType) {
                case var type when (type == typeof(DateTime)):
                    DateTime val_dt = (DateTime)propertyInfo.GetValue(model);
                    bytes = BitConverter.GetBytes(val_dt.ToBinary());
                    break;
                case var type when (type == typeof(string)):
                    string val_str = (string)propertyInfo.GetValue(model);
                    bytes = UTF8Encoding.ASCII.GetBytes(val_str);
                    break;
                case var type when (type == typeof(MessageTypeEnum)):
                    MessageTypeEnum enum_val = (MessageTypeEnum)propertyInfo.GetValue(model);
                    bytes = new byte[] { (byte)enum_val };
                    break;
            }

            if (bytes.Length != length) {
                throw new ArgumentException(string.Format("Something went wrong with the parsing of attribute {0}",propertyInfo.Name));
            }

            return bytes;
        }
    }
}
