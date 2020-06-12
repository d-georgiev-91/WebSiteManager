using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebSiteManager.Services.Security
{
    public class AesPasswordEncryptor : IPasswordEncryptor
    {
        private readonly IEncryptionKeyProvider _encryptionKeyProvider;

        public AesPasswordEncryptor(IEncryptionKeyProvider encryptionKeyProvider)
        {
            _encryptionKeyProvider = encryptionKeyProvider;
        }

        public string Encrypt(string password)
        {
            string encData = null;
            var keys = GetHashKeys();

            try
            {
                encData = EncryptStringToBytes_Aes(password, keys[0], keys[1]);
            }
            catch (SystemException ex) 
                when (ex is CryptographicException || 
                      ex is ArgumentException)
            {
                Debug.WriteLine(ex);
            }

            return encData;
        }

        public string Decrypt(string encryptedPassword)
        {
            string decData = null;
            var keys = GetHashKeys();

            try
            {
                decData = DecryptStringFromBytes_Aes(encryptedPassword, keys[0], keys[1]);
            }

            catch (SystemException ex)
                when (ex is CryptographicException ||
                      ex is ArgumentException)
            {
                Debug.WriteLine(ex);
            }

            return decData;
        }

        private byte[][] GetHashKeys()
        {
            var key = _encryptionKeyProvider.GetKey();
            var result = new byte[2][];
            var enc = Encoding.UTF8;

            var sha2 = new SHA256CryptoServiceProvider();

            var rawKey = enc.GetBytes(key);
            var rawIV = enc.GetBytes(key);

            var hashKey = sha2.ComputeHash(rawKey);
            var hashIV = sha2.ComputeHash(rawIV);

            Array.Resize(ref hashIV, 16);

            result[0] = hashKey;
            result[1] = hashIV;

            return result;
        }

        //source: https://msdn.microsoft.com/de-de/library/system.security.cryptography.aes(v=vs.110).aspx
        private string EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            using var aesAlg = new AesManaged { Key = key, IV = iv };

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using var msEncrypt = new MemoryStream();
            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }
            
            var encrypted = msEncrypt.ToArray();

            return Convert.ToBase64String(encrypted);
        }

        //source: https://msdn.microsoft.com/de-de/library/system.security.cryptography.aes(v=vs.110).aspx
        private string DecryptStringFromBytes_Aes(string cipherText, byte[] key, byte[] iv)
        {
            var cipherTextBytes = Convert.FromBase64String(cipherText);

            if (cipherTextBytes == null || cipherTextBytes.Length <= 0)
            {
                throw new ArgumentNullException(nameof(cipherText));
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException(nameof(iv));
            }

            using var aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using var msDecrypt = new MemoryStream(cipherTextBytes);
            using var csDecrypt =
                new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            var plaintext = srDecrypt.ReadToEnd();

            return plaintext;
        }
    }
}