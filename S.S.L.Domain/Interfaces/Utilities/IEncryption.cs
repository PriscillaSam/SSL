using System.Threading.Tasks;

namespace S.S.L.Domain.Interfaces.Utilities
{
    public interface IEncryption
    {
        string Encrypt(string plainText);
    }
}
