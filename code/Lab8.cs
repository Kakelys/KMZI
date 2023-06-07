using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    public static class Lab8
    {
        // 1 варик BBS, генерация ПСП (псевдослучайные последовательности)
        // n=256(задание *****), p,q 23,19 23%4=3, 19%4=3 n=437


        //13 varik, алгоритм RC4
        // n = 8(2^n(кол-во перестановок)); 61, 60, 22, 23, 60, 61
        public static void Main() 
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("BBS");
            var bbs = new BBSRandom(23,19,3); 

            for (int i = 0; i < 10; i++) 
            {
                Console.WriteLine(bbs.Next(0, 256));
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            Console.WriteLine("RC4");
            
            int[] key = { 61, 60, 22, 23, 60, 61 };
            var msg = "Peace of trash";
            byte[] input = Encoding.UTF8.GetBytes(msg);    
            Console.WriteLine("Input text: Peace of trash");
            Console.WriteLine(msg.Length);
            stopwatch.Start();

            byte[] encrypted = RC4.Encrypt(input, key);

            stopwatch.Stop();
            

            string hexString = BitConverter.ToString(encrypted);//.Replace("-", "");
            Console.WriteLine(hexString);
            Console.WriteLine($"Time to encrypt: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}