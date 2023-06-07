using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace app.lab10
{
    public class Rsa
    {
        public static void Main()
        {
            try
            {
                
                string originalData = "Senchenya Vladislav Igorevich";
                originalData = "Senchenya Vladislav Igorevich;alskdjfaopwimvbopasidfhgvnapmalskdjfaopwimvbopasidfhgvnapmseoxasoidjfmpbvoaeiwrhncpasoiedrjfgnapboseirjmcpalskdjfaopwimvbopasidfhgvnapmseoxasoidjfmpbvoaeiwrhncpasoiedrjfgnapboseirjmcpfaosidjfphganbshudffaosidjfphganbshudfsealskdjfaopwimvbopasidfhgvnapmseoxasoidjfmpbvoaeiwrhncpasoiedrjfgnapboseirjmcpfaosidjfphganbshudfoxasoidjfmpbvoaeiwrhncpasoiedrjfgnapboseirjmcpfaosidjfphganbshudf";

                Console.WriteLine($"Original message: {originalData.Length}");
                
                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(originalData);

                var watch = new Stopwatch();
            

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(4096))
                {
                    // Получение открытого и закрытого ключей RSA
                    RSAParameters publicKey = RSA.ExportParameters(false);
                    RSAParameters privateKey = RSA.ExportParameters(true);
                    
                    // Шифрование данных с использованием открытого ключа
                    watch.Start();
                    byte[] encryptedData = RSAEncrypt(dataToEncrypt, publicKey);
                    watch.Stop();
                    Console.WriteLine($"RSA encryption time: {watch.ElapsedMilliseconds} ms");
                    // Расшифрование данных с использованием закрытого ключа
                    watch.Reset();
                    watch.Start();
                    byte[] decryptedData = RSADecrypt(encryptedData, privateKey);
                    watch.Stop();
                    Console.WriteLine($"RSA decryption time: {watch.ElapsedMilliseconds} ms");

                    string decryptedString = Encoding.UTF8.GetString(decryptedData);

                    // Вывод результатов
                    Console.WriteLine("Original Data: {0}", originalData);
                    Console.WriteLine("Encrypted Data: {0}", Convert.ToBase64String(encryptedData));
                    Console.WriteLine("Decrypted Data: {0}", decryptedString);
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Main2()
        {
            try
            {
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                string originalData = "Senchenya Vladislav Igorevich";
                byte[] dataToEncrypt = ByteConverter.GetBytes(originalData);

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    // Получение открытого и закрытого ключей RSA
                    RSAParameters publicKey = RSA.ExportParameters(false);
                    RSAParameters privateKey = RSA.ExportParameters(true);

                    // Шифрование данных с использованием открытого ключа
                    byte[] encryptedData = RSAEncrypt(dataToEncrypt, publicKey);

                    // Кодирование зашифрованных данных в Base64 или ASCII
                    string encryptedString = Convert.ToBase64String(encryptedData);
                    //string encryptedString = ASCIIEncoding.ASCII.GetString(encryptedData);

                    // Декодирование зашифрованных данных из Base64 или ASCII
                    byte[] decodedData = Convert.FromBase64String(encryptedString);
                    //byte[] decodedData = ASCIIEncoding.ASCII.GetBytes(encryptedString);

                    // Расшифрование данных с использованием закрытого ключа
                    byte[] decryptedData = RSADecrypt(decodedData, privateKey);

                    string decryptedString = ByteConverter.GetString(decryptedData);

                    Console.WriteLine("Original Data: {0}", originalData);
                    Console.WriteLine("Encrypted Data (Base64): {0}", encryptedString);
                    Console.WriteLine("Decrypted Data: {0}", decryptedString);
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Main3()
        {
            try
            {
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                string originalData = "Senchenya Vladislav Igorevich";
                byte[] dataToEncrypt = ByteConverter.GetBytes(originalData);

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    // Получение открытого и закрытого ключей RSA
                    RSAParameters publicKey = RSA.ExportParameters(false);
                    RSAParameters privateKey = RSA.ExportParameters(true);

                    // Шифрование данных с использованием открытого ключа
                    byte[] encryptedData = RSAEncrypt(dataToEncrypt, publicKey);

                    //string encryptedString = Convert.ToBase64String(encryptedData);
                    string encryptedString = ASCIIEncoding.ASCII.GetString(encryptedData);

                    // Декодирование зашифрованных данных из Base64 или ASCII
                    //byte[] decodedData = Convert.FromBase64String(encryptedString);
                    byte[] decodedData = ASCIIEncoding.ASCII.GetBytes(encryptedString);

                    // Расшифрование данных с использованием закрытого ключа
                    byte[] decryptedData = RSADecrypt(decodedData, privateKey);

                    string decryptedString = ByteConverter.GetString(decryptedData);

                    Console.WriteLine("Original Data: {0}", originalData);
                    Console.WriteLine("Encrypted Data (ASCII): {0}", encryptedString);
                    Console.WriteLine("Decrypted Data: {0}", decryptedString);
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static byte[] RSAEncrypt(byte[] dataToEncrypt, RSAParameters publicKey)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(publicKey);
                return RSA.Encrypt(dataToEncrypt, true);
            }
        }

        static byte[] RSADecrypt(byte[] dataToDecrypt, RSAParameters privateKey)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(privateKey);
                return RSA.Decrypt(dataToDecrypt, true);
            }
        }
    }
}