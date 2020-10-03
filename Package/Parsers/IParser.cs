namespace Package.Parsers {
    internal interface IParser<T> {
        byte[] Encode(T value);
        T Decode(byte[] stream);
    }
}
