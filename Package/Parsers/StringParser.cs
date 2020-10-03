using System.Text;

namespace Package.Parsers {
    public class StringParser : IParser<string> {
        public string Decode(byte[] stream) {
            return UTF8Encoding.ASCII.GetString(stream);
        }

        public byte[] Encode(string value) {
            return UTF8Encoding.ASCII.GetBytes(value);
        }
    }
}
