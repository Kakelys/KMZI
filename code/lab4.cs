using System;
using System.Diagnostics;

namespace app
{
    //task: 
    // 1 На основе аффинной системы подстаново Цезаря; a = 6, b = 7 
    // 2 Таблица Трисемуса, ключевое слово – security
    public static class lab4
    {
        private static string _alphabet = "abcdefghijklmnopqrstuvwxyz";

        public static void func1(string str, int a, int b)
        {
            string res = "";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < str.Length; i++)
            {
                if(str[i] == ',' || str[i] == '`' || str[i] == ' ' || str[i] == '.' || str[i] == '!' || str[i] == ':')
                {
                    res += str[i];
                    continue;    
                }

                var index = ((a * (_alphabet.IndexOf(char.ToLower(str[i])) + 1) + b) % 26)- 1;
                var ch = _alphabet[index == -1 ? 25 : index];
                res += char.IsUpper(str[i]) ? char.ToUpper(ch) : ch;
            }
            stopwatch.Stop();
            Console.WriteLine($"Cezar shifr time: {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("Cezar shifr");
            Console.WriteLine(res);
            Console.WriteLine("\n\n\n");
            //Entropy.forAlphabet(res, "cezar_shifr.xlsx");
            func1Inverse(res, ModInverse(a,26), b);
        }

        public static void func1Inverse(string str, int a, int b)
        {
            string res = "";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < str.Length; i++)
            {
                if(str[i] == ',' || str[i] == '`' || str[i] == ' ' || str[i] == '.' || str[i] == '!' || str[i] == ':')
                {
                    res += str[i];
                    continue;    
                }

                var index = (a*(_alphabet.IndexOf(char.ToLower(str[i]))+1+26-b)%26) - 1;
                var ch = _alphabet[index == -1 ? 25 : index];
                res += char.IsUpper(str[i]) ? char.ToUpper(ch) : ch;
            }
            stopwatch.Stop();
            Console.WriteLine($"Cezar deshifr time: {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("Cezar deshifr");
            System.Console.WriteLine(res);
            Console.WriteLine("\n\n\n");
            //Entropy.forAlphabet(res, "cezar_deshifr.xlsx");
        }

        public static void func2(string str, string key)
        {
            int n = 5, m = 5;

            var table = new char[n,m];
            var alph = _alphabet;
            alph = alph.Remove(alph.IndexOf('j'), 1);
            str = str.Replace('j','i');
            str = str.ToLower();
            // fill table
            for(var i = 0; i < n; i++)
            {
                for(var j = 0; j < m; j++)
                {
                    if(key.Length > 0)
                    {
                        Console.Write(key[0]);
                        table[i,j] = key[0];
                        alph = alph.Remove(alph.IndexOf(key[0]), 1);
                        key = key.Remove(0,1);
                    }
                    else
                    {
                        Console.Write(alph[0]);
                        table[i,j] = alph[0];
                        alph = alph.Remove(alph.IndexOf(alph[0]), 1);
                    }
                }
                Console.WriteLine();
            }

            var res = "";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for(var i =0; i < str.Length; i++)
            {
                for(var j = 0; j < n; j++)
                {
                    for(var k = 0; k < m; k++)
                    {
                        if(str[i] == table[j,k])
                        {
                            res += table[j == m - 1 ? 0 : j+1, k];
                        }
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Tric shifr time: {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("Tric shifr");
            Console.WriteLine(res);
            Console.WriteLine("\n\n\n");
            //Entropy.forAlphabet(res, "tric_shifr.xlsx");
            func2Inverse(res, key, table);
        }

        public static void func2Inverse(string str, string key, char[,] table)
        {
            int n = 5, m = 5;

            var res = "";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for(var i =0; i < str.Length; i++)
            {
                for(var j = 0; j < n; j++)
                {
                    for(var k = 0; k < m; k++)
                    {
                        if(str[i] == table[j,k])
                        {
                            res += table[j == 0 ? m-1 : j-1, k];
                        }
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Tric deshifr time: {stopwatch.ElapsedMilliseconds} ms");

            Console.WriteLine("Tric deshifr");
            Console.WriteLine(res);
            Console.WriteLine("\n\n\n");
            //Entropy.forAlphabet(res, "tric_deshifr.xlsx");
        }

        public static int ModInverse(int a, int m)
        {
            int m0 = m;
            int y = 0, x = 1;

            if (m == 1)
                return 0;

            while (a > 1 && m > 0)
            {
                // q - частное, t - остаток
                int q = a / m;
                int t = m;

                m = a % m;
                a = t;
                t = y;

                y = x - q * y;
                x = t;
            }

            if (x < 0)
                x += m0;

            return x;
        }
    }
}