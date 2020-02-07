using BanklyDemo.Core.Common;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BanklyDemo.DomainServices.Crypto
{
    public class CryptoService: ICryptoService
    {
        public string CreateUniqueKey(int length = 32)
        {
            var bytes = new byte[length];
            new RNGCryptoServiceProvider().GetBytes(bytes);

            return ToHexString(bytes);
        }

        public string Decrypt(string cipherText, string key)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");

            string plainText = null;
            var fragments = cipherText.Split('.');

            if (fragments.Length < 2)
                throw new ArgumentException("cipherText not formatted correctly");

            var encryptedText = FromHexString(fragments[1]);
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.ASCII.GetBytes(key);
                aesAlg.IV = FromHexString(fragments[0]);
                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(encryptedText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return plainText;
        }

        public string Encrypt(string plainText, string key)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");

            byte[] encrypted;
            byte[] IV;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.ASCII.GetBytes(key);
                IV = aesAlg.IV;
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return $"{ToHexString(IV)}.{ToHexString(encrypted)}";
        }

        public string GenerateSalt(int maxLenght)
        {
            var salt = new byte[maxLenght];

            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public string Hash(string text, string salt = null, int iterations = 1)
        {
            if (salt != null)
            {
                text += salt;
            }

            using (var sha = SHA256Managed.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var hash = sha.ComputeHash(bytes);

                for (int i = 1; i < iterations; i++)
                {
                    hash = sha.ComputeHash(hash);
                }

                return ToHexString(hash);
            }
        }

        private static string ToHexString(byte[] byteArray)
        {
            var sb = new StringBuilder();

            foreach (var value in byteArray)
            {
                sb.Append(value.ToString("x2"));
            }

            return sb.ToString();
        }

        private static byte[] FromHexString(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
