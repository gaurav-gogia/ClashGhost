using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

namespace ClashGhost
{
    class DataSecurity
    {
        internal static string EncryptThisData(string text)
        {
            string result = null;
            try
            {
                if (!String.IsNullOrEmpty(text))
                {
                    byte[] plaintextBytes = Encoding.Unicode.GetBytes(text);

                    SymmetricAlgorithm symmetricAlgorithm = Aes.Create();
                    symmetricAlgorithm.Key = new byte[]{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plaintextBytes, 0, plaintextBytes.Length);
                        }

                        result = Encoding.Unicode.GetString(memoryStream.ToArray());
                    }
                }
                
            }
            catch(Exception e) { Debug.WriteLine(e); }

            return result;
        }

        internal static string DecryptThisCipher(string text)
        {
            string result = null;

            try
            {
                if (!String.IsNullOrEmpty(text))
                {
                    byte[] encryptedBytes = Encoding.Unicode.GetBytes(text);

                    SymmetricAlgorithm symmetricAlgorithm = Aes.Create();
                    symmetricAlgorithm.Key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
                    using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            byte[] decryptedBytes = new byte[encryptedBytes.Length];
                            cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                            result = Encoding.Unicode.GetString(decryptedBytes);
                        }
                    }
                }                
            }
            catch(Exception e) { Debug.WriteLine(e); }

            return result;
        }
    }
}
