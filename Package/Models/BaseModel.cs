using System;
using Package.Enum;

namespace Package.Models {
    public class BaseModel {
        private string _identifier;

        public string Identifier{
            get { return _identifier; }
            set {
                if (!string.IsNullOrWhiteSpace(value) && value.Length != 5)
                    throw new ArgumentException("If the identifier is not null it has to be five characters");
                _identifier = value; 
            }
        }

        public MessageTypeEnum MessageType { get; set; }
        public DateTime Time { get; set; }

        public BaseModel(MessageTypeEnum mte, string identifier = "", DateTime? dt = null) {
            Time = dt ?? DateTime.UtcNow;
            MessageType = mte;
            Identifier = identifier;
        }
    }
}
