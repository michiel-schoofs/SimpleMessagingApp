using Package.Enum;

namespace Package.Parsers {
    public class MessageTypeEnumParser : IParser<MessageTypeEnum> {
        public MessageTypeEnum Decode(byte[] stream) {
            byte value = stream[0];
            return (MessageTypeEnum) System.Enum.Parse(typeof(MessageTypeEnum), value.ToString());
        }

        public byte[] Encode(MessageTypeEnum value) {
            return new byte[] { (byte)value };
        }
    }
}
