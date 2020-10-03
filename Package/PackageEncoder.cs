using Package.Attributes;
using Package.DTO;
using Package.Enum;
using Package.Models;
using Package.Parsers;
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
            var assem = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IParser<>)));

            if (bytes.Length != length) {
                throw new ArgumentException(string.Format("Something went wrong with the parsing of attribute {0}",propertyInfo.Name));
            }

            return bytes;
        }
    }
}
