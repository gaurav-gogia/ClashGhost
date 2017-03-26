using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace ClashGhost
{
    class HashKarenge
    {
        internal static string CreateHash(string privateKey, string publicKey, string password)
        {
            string hashedMessage = null;

            var toBeHashed = privateKey + publicKey + password;

            hashedMessage = ComputeMD5(toBeHashed);

            for (int i = 0; i < 9; i++)
                hashedMessage = ComputeMD5(toBeHashed);

            return hashedMessage;
        }

        private static string ComputeMD5(string s)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(s, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            var res = CryptographicBuffer.EncodeToHexString(hashed);

            return res;
        }
    }
}
