using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Lab9
{
    public static class Lab9Code
    {
        public static List<int> Generate(int length)
        {
            var sequence = new List<int>();
            var sum = 0;
            for (int i = 0; i < length; i++)
            {
                var next = sum + 1;
                sequence.Add(next);
                sum += next;
            }
            return sequence;
        }

        public static List<int> GenNorm(List<int> d, int a, int n, int z)
        {
            var e = new List<int>();
            for (int i = 0; i < z; i++)
            {
                e.Add((d[i] * a) % n);
            }
            return e;
        }

        public static List<int> Encrypt(List<int> e, string msg, int z)
        {
            var res = new List<int>();
            for(int i = 0; i < msg.Length; i++)
            {
                var sum = 0;
                var s = "0" + GetBytes(msg[i].ToString());

                for(int j = 0; j < s.Length; j++)
                {
                    if(s[j] == '1')
                    {
                        sum += e[j];
                    }
                }

                res.Add(sum);
            }

            return res;
        }

        public static string Decode(List<int> d, int ch, int z)
        {
            var tmp = "";

            for (int i = z; i > 0; i--)
            {
                if (ch >= d[i-1])
                {
                    tmp += '1';
                    ch = ch - d[i-1];
                }
                else
                {
                    tmp += '0';
                }
            }

            var res = "";
            for (int i = tmp.Length-1; i >-1; i--)
            {
                res+= tmp[i];
            }

            return res;
        }

        public static int InverseNumber(int a, int n)
        {
            for (int i = 0; i < 999999; i++)
            {
                if (((a * i) % n) == 1) return (i);
            }
            return 0;
        }

        public static string GetBytes(string str)
        {
            string res = "";
            for (int i = 0; i < str.Length; i++)
            {
                res += Convert.ToString((int)str[i], 2);
            }
            return res;
        }
    }
}