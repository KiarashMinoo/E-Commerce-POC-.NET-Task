using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers
{
    public static class CipheringHelper
    {
        public static string Decrypt(this string cipherText, string key, string salt)
        {
            var aes = Aes.Create();
            int ByteCount = 0;
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            using (ICryptoTransform decryptor = aes.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(salt)))
            {
                using MemoryStream MemStream = new(cipherTextBytes);
                using CryptoStream CryptoStream = new(MemStream, decryptor, CryptoStreamMode.Read);

                ByteCount = CryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                MemStream.Close();
                CryptoStream.Close();
            }

            return Encoding.UTF8.GetString(plainTextBytes, 0, ByteCount);
        }

        public static string Encrypt(this string plainText, string key, string salt)
        {
            var aes = Aes.Create();
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[]? cipherTextBytes = null;
            using (ICryptoTransform encryptor = aes.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(salt)))
            {
                using MemoryStream MemStream = new();
                using CryptoStream CryptoStream = new(MemStream, encryptor, CryptoStreamMode.Write);
                CryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                CryptoStream.FlushFinalBlock();
                cipherTextBytes = MemStream.ToArray();
                MemStream.Close();
                CryptoStream.Close();
            }

            return Convert.ToBase64String(cipherTextBytes);
        }
    }
}
