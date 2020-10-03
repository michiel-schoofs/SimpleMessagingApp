using System;

namespace Package.Parsers {
    public class DateTimeParser : IParser<DateTime> {
        public DateTime Decode(byte[] stream) {
            long value = BitConverter.ToInt64(stream, 0);
            return new DateTime(value);
        }

        public byte[] Encode(DateTime value) {
            return BitConverter.GetBytes(value.ToBinary());
        }
    }
}
