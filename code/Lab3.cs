using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app
{
    public class Lab3
    {
        public static int gcd(int a, int b)
        {
            while(b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static bool IsPrime(int n)
        {
            if(n == 1)
                return false;
            
            if(n == 2)
                return true;

            if(n % 2 == 0)
                return false;
                
            for(var i = 3; i <= Math.Sqrt(n); i+=2)
            {
                if(n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static void SimpleGCD(int number)
        {
            Console.WriteLine($"Number {number} to simple numbers");
            Console.WriteLine(1);
            var i = 2;
            int pow = 0;
            int count = 0;

            while(true)
            {
                if(number % i == 0)
                {
                    if(pow == i){count++;}
                    else
                    {
                        if(count > 0)
                            Console.WriteLine($"{pow}^{count}");
                        count = 1;
                    }
                    pow = i;

                    number = number / i;
                }
                else
                {
                    i++;
                }
                if(number == 1)
                    break;
            }

            if(count > 0)
                Console.WriteLine($"{pow}^{count}");
        }
    }
}


