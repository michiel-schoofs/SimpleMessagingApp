using Package;
using Package.DTO;
using Package.Models;
using System;

namespace Server {
    class Program {
        static void Main(string[] args) {
            BaseDTO base_dto = PackageEncoder<BaseDTO, BaseModel>.Parse(new BaseModel(Package.Enum.MessageTypeEnum.Hello, "jklmo", DateTime.Now.AddHours(-1)));
            var bytes = base_dto.Flatten();
        }
    }
}
