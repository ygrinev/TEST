using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ProxyAPI.Helpers
{
    public class CryptoHelper
    {
        public static string EncryptDecrypt(string input, string key, bool encrypt)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            byte[] bytes = Convert.FromBase64String(input);
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.KeySize = 128;
                    aes.BlockSize = 128;
                    aes.Padding = PaddingMode.Zeros;

                    aes.Key = Convert.FromBase64String(key);
                    aes.IV = Enumerable.Repeat((byte)0, 16).ToArray();

                    using (var cryptor = encrypt ? aes.CreateEncryptor(aes.Key, aes.IV) : aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        using (var ms = new MemoryStream())
                        using (var cryptoStream = new CryptoStream(ms, cryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(bytes, 0, bytes.Length);
                            cryptoStream.FlushFinalBlock();

                            return Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
            catch(Exception)
            { 
                throw;
            }
        }
    }
}
