using Package.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Package.DTO {
    public class BaseDTO {
        [SerializePlace(0)]
        [Length(1)]
        public byte[] MessageType { get; set; }

        [SerializePlace(1)]
        [Length(8)]
        public byte[] Time { get; set; }

        [SerializePlace(2)]
        [Length(5)]
        public byte[] Identifier { get; set; }

        public byte[] Flatten() {
            IEnumerable<PropertyInfo> props = GetType().GetRuntimeProperties();
            List<byte[]> res = new List<byte[]>();

            foreach (PropertyInfo prop in props) {
                bool serialiazable = prop.GetCustomAttribute<NonSerializedAttribute>() == null;

                if (serialiazable) {
                    var at = prop.GetCustomAttributes(typeof(SerializePlace)).ToList();
                    int? place = prop.GetCustomAttribute<SerializePlace>()?.GetPlace();

                    if (place == null)
                        throw new FormatException("Every attribute without a non serialized attribute should provide a serialize place");

                    if (res.ElementAtOrDefault(place ?? 0) != null)
                        throw new FormatException("This place in the list is already filled do you have a duplicate serialize place?");

                    byte[] val = (byte[])prop.GetValue(this);
                    res.Insert(place ?? 0, val);
                }
            }

            return res.SelectMany(b => b).ToArray();
        }
    }
}
