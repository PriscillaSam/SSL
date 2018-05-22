using S.S.L.Domain.Interfaces.Utilities;
using System.Linq;
using System.Text;

namespace S.S.L.Infrastructure.Utilities
{
    public class MD5Encryption : IEncryption
    {
        public string Encrypt(string plainText)
        {
            return
                System.Security
                .Cryptography.MD5.Create()
                 .ComputeHash(Encoding.ASCII.GetBytes(plainText))
                  .Select(x => x.ToString("x2"))
                   .Aggregate((ag, s) => ag + s);
        }
    }
}
