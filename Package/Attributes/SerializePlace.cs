using System;

namespace Package.Attributes {
    [AttributeUsage(AttributeTargets.Property)]
    internal class SerializePlace : Attribute {
        private int _place;

        public SerializePlace(int place) {
            if (place < 0) 
                throw new ArgumentException("The place where this object needs to be serialized can't be smaller than 0");

            _place = place;
        }

        public int GetPlace() {
            return _place;
        }
    }
}
