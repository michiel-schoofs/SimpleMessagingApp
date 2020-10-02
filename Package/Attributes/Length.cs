using System;

namespace Package.Attributes {
    [AttributeUsage(AttributeTargets.Property)]
    internal class Length : Attribute{
        private int _length;

        public Length(int length) {
            if (length < 0)
                throw new ArgumentException("You cannot have a negative length.");

            _length = length;
        }

        public int GetLength() {
            return _length;
        }
    }
}
