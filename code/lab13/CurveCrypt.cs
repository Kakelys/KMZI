using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;

namespace app.lab13
{
    public class CurveCrypt
    {
        private static ECDomainParameters curve;

        static CurveCrypt()
        {
            BigInteger p = new BigInteger("61");
            BigInteger a = new BigInteger("1"); 
            BigInteger b = new BigInteger("1"); 
            BigInteger x = new BigInteger("0"); // Координата x точки G
            BigInteger y = new BigInteger("1"); // Координата y точки G
            BigInteger n = new BigInteger("15"); // значение порядка группы точек
            BigInteger h = BigInteger.One; // Кофактор (в данном случае равен 1)
           
            try {
                ECCurve curveParams = new FpCurve(p, a, b, n, h);
                ECPoint g = curveParams.CreatePoint(x, y);
                curve = new ECDomainParameters(curveParams, g, n, h);
                return;
            } catch (Exception ex) 
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine(".");
            }
            
            X9ECParameters curveParamss = SecNamedCurves.GetByName("secp256r1"); // Выбор эллиптической кривой (в данном случае secp256r1)
            Console.WriteLine("Curve: " + curveParamss.G.XCoord);
            curve = new ECDomainParameters(curveParamss.Curve, curveParamss.G, curveParamss.N, curveParamss.H);
        }

        public static byte[] Encrypt(string plaintext, ECPrivateKeyParameters privateKey, ECPublicKeyParameters publicKey)
        {
            byte[] plaintextBytes = System.Text.Encoding.UTF8.GetBytes(plaintext);

            IBasicAgreement agreement = AgreementUtilities.GetBasicAgreement("ECDH");
            agreement.Init(privateKey);
            BigInteger sharedSecret = agreement.CalculateAgreement(publicKey);

            byte[] keyBytes = sharedSecret.ToByteArray();
            if (keyBytes.Length != 32) // Если размер общего ключа не равен 32 байтам, обрезаем его или дополняем нулями
            {
                Array.Resize(ref keyBytes, 32);
            }

            byte[] encryptedBytes;
            var cipher = CipherUtilities.GetCipher("AES/ECB/PKCS7Padding");
            
            KeyParameter key = new KeyParameter(keyBytes);
            cipher.Init(true, key);
            encryptedBytes = cipher.DoFinal(plaintextBytes);
            

            return encryptedBytes;
        }

        public static string Decrypt(byte[] encryptedBytes, ECPrivateKeyParameters privateKey, ECPublicKeyParameters publicKey)
        {
            IBasicAgreement agreement = AgreementUtilities.GetBasicAgreement("ECDH");
            agreement.Init(privateKey);
            BigInteger sharedSecret = agreement.CalculateAgreement(publicKey);

            byte[] keyBytes = sharedSecret.ToByteArray();
            if (keyBytes.Length != 32)
            {
                Array.Resize(ref keyBytes, 32);
            }

            byte[] decryptedBytes;
            var cipher = CipherUtilities.GetCipher("AES/ECB/PKCS7Padding");
            
            KeyParameter key = new KeyParameter(keyBytes);
            cipher.Init(false, key);
            decryptedBytes = cipher.DoFinal(encryptedBytes);
            

            string plaintext = System.Text.Encoding.UTF8.GetString(decryptedBytes);
            return plaintext;
        }

        public static void Main()
        {
            // Генерация ключей
            ECKeyPairGenerator keyGen = new ECKeyPairGenerator();
            SecureRandom random = new SecureRandom();
            keyGen.Init(new ECKeyGenerationParameters(curve, random));
            AsymmetricCipherKeyPair keyPair = keyGen.GenerateKeyPair();
            ECPrivateKeyParameters privateKey = (ECPrivateKeyParameters)keyPair.Private;
            ECPublicKeyParameters publicKey = (ECPublicKeyParameters)keyPair.Public;

            // Шифрование и расшифрование текста
            string plaintext = "Hello, world!";
            byte[] encryptedBytes = Encrypt(plaintext, privateKey, publicKey);
            string decryptedText = Decrypt(encryptedBytes, privateKey, publicKey);

            Console.WriteLine("Plaintext: " + plaintext);
            Console.WriteLine("Encrypted bytes: " + BitConverter.ToString(encryptedBytes));
            Console.WriteLine("Decrypted text: " + decryptedText);
        }
    }
}