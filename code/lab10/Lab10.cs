using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace app.lab10
{
    public class Lab10 // 8 в методе
    {
        //8-10,9-11,10-12,11-13,12-14,13-15
        public static void Main() 
        {
            //Task1();
            
            Rsa.Main();
            Console.WriteLine("\n");
            //Rsa.Main2();
            //Rsa.Main3();
            Console.WriteLine("\n");
            ElGamal.Main();
        }

        public static void Task1() 
        {
            BigInteger a = new BigInteger(20);
            var x = 100000;
            BigInteger n;
            string N = "1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            BigInteger.TryParse(N,out n);

            Stopwatch stopwatch = new Stopwatch();
            
            for(var i =0; i < 10; i++) {
                stopwatch.Reset();
                stopwatch.Start();
                FyncY(a, x, n);
                stopwatch.Stop();
                Console.WriteLine($"x: {x} time:{stopwatch.ElapsedMilliseconds}");

                x += 100000;
            }
        }

        public static BigInteger FyncY(BigInteger a, int x, BigInteger n)
        {
            return BigInteger.Pow(a, x) % n; 
        }

        
    }
}