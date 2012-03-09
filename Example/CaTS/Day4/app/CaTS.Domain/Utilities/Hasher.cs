using System.Security.Cryptography;
using System.Text;

namespace CaTS.Domain.Utilities
{
    public class Hasher
    {
        public static string Hash(string value) {
            var addSalt = string.Concat(HASH_SALT, value);
            var sha1Hashser = new SHA1CryptoServiceProvider();
            var hashedBytes = sha1Hashser.ComputeHash(Encoding.Unicode.GetBytes(addSalt));
            return new UnicodeEncoding().GetString(hashedBytes);
        }

        private const string HASH_SALT = "I^>cI'}7hgIdKlCLY2%:";
    }
}
