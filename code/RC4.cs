using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app
{
    public static class RC4
    {
        public static byte[] Encrypt(byte[] input, int[] key)
        {
            byte[] output = new byte[input.Length];
            byte[] S = InitializeS(key);
            int i = 0;
            int j = 0;
            for (int k = 0; k < input.Length; k++)
            {
                i = (i + 1) % 256;
                j = (j + S[i]) % 256;
                Swap(S, i, j);
                int t = (S[i] + S[j]) % 256;
                output[k] = (byte)(input[k] ^ S[t]);
            }
            return output;
        }

        private static byte[] InitializeS(int[] key)
        {
            byte[] S = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % key.Length]) % 256;
                Swap(S, i, j);
            }
            return S;
        }

        private static void Swap(byte[] S, int i, int j)
        {
            byte temp = S[i];
            S[i] = S[j];
            S[j] = temp;
        }
    }
}