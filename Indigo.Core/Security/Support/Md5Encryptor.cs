using Indigo.Infrastructure.Util;

namespace Indigo.Security.Support
{
    public sealed class Md5Encryptor : IEncryptor
    {
        public string Encrypt(string raw)
        {
            return DigestUtils.Md5Hex(raw);
        }
    }
}
