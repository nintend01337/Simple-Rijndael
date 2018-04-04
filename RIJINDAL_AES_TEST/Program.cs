using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace RIJINDAL_AES_TEST
{
    class Program
    {
        public static void Encrypt(string input)
        {
            string salt = "XGl0k7JSF1nHafce";
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] coded = null;

            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.IV = Encoding.ASCII.GetBytes(salt);
            aes.GenerateKey();
            ICryptoTransform crypto = aes.CreateEncryptor(aes.Key,aes.IV);
            coded = crypto.TransformFinalBlock(bytes, 0, bytes.Length);

            string output = Encoding.UTF8.GetString(coded);
            File.WriteAllBytes("coded.txt",coded);
            File.WriteAllBytes("key.txt", aes.Key);
        }

        public static void Decrypt(byte [] input, byte [] key)
        {
            string salt = "XGl0k7JSF1nHafce";
            byte[] bytes = input;
            byte[] encoded ;
            RijndaelManaged aes = new RijndaelManaged();
            aes.Key = key;
            aes.IV = Encoding.ASCII.GetBytes(salt);
            aes.Padding = PaddingMode.None;
            ICryptoTransform decrypto = aes.CreateDecryptor(aes.Key,aes.IV);

            encoded = decrypto.TransformFinalBlock(input, 0, input.Length);
            File.WriteAllText("decoded.txt", Encoding.UTF8.GetString(encoded));
        }


        static void Main(string[] args)
        {
            int     KeyCode = 0;
            string   input = null;
            byte    []key = null;
            byte    [] raw;
            Console.WriteLine(string.Format("Choose Operation :  \n \n 1.Encrypt using AES \n 2.Decrypt using AES \n"));


            KeyCode = Convert.ToInt32(Console.ReadLine());

            switch (KeyCode)
            {
                    case 1:

                        input = File.ReadAllText("text.txt", Encoding.UTF8);
                        Encrypt(input);

                    break;

                    case 2:

                    raw = File.ReadAllBytes("coded.txt");
                    key = File.ReadAllBytes("key.txt");
                    Decrypt(raw, key);
                    break;

                default:
                    Console.WriteLine("Invalid Operation");
                    break;
            }
            
            Console.WriteLine("done...");
            Console.ReadLine();
        }
    }
}
