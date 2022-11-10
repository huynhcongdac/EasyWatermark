using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace EasyWatermark.Storage
{
    public class JsonDataManager<T> : IDataManager<T>
    {
        private string _fileNameToSave;

        public JsonDataManager(string fileNameToSave)
        {
            _fileNameToSave = fileNameToSave;
        }

        public virtual T Load()
        {
            if (!File.Exists(_fileNameToSave))
            {
                return default(T);
            }
            var json = string.Empty;
            while (true)
            {
                try
                {
                    using (var sd = new StreamReader(_fileNameToSave))
                    {
                        json = sd.ReadToEnd();
                    }
                    break;
                }
                catch
                {
                    Thread.Sleep(500); 
                }
            }
            if (IsProtectData)
            {
                if (string.IsNullOrEmpty(DataProtectionPassword))
                {
                    throw new Exception("Data protection password cannot empty");
                }
                json = DecryptString(json, DataProtectionPassword);
            }
            var obj = JsonConvert.DeserializeObject<T>(json);

            return obj;
        }

        public virtual void Update(T item)
        {
            var json = JsonConvert.SerializeObject(item);
            if (IsProtectData)
            {
                if (string.IsNullOrEmpty(DataProtectionPassword))
                {
                    throw new Exception("Data protection password cannot empty");
                }
                json = EncryptString(json, DataProtectionPassword);
            }
            while (true)
            {
                try
                {
                    using (var sw = new StreamWriter(_fileNameToSave))
                    {
                        sw.Write(json);
                    }
                    break;
                }
                catch (IOException)
                {
                    Thread.Sleep(500);
                }
            }
        }

        private const int Keysize = 256;

        private const int DerivationIterations = 1000;

        public bool IsProtectData { get; set; }

        public string DataProtectionPassword { get; set; }

        private string EncryptString(string plainText, string passPhrase)
        {
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public string DecryptString(string cipherText, string passPhrase)
        {
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
