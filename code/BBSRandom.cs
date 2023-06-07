using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app
{
    public class BBSRandom
    {
        private readonly int p, q;
        private int x;

        public BBSRandom(int p, int q, int x)
        {
            this.p = p;
            this.q = q;
            this.x = x;
        }

        private int NextBit()
        {
            x = (x * x) % (p * q);
            return x % 2;
        }

        public int Next(int minValue, int maxValue)
        {
            int range = maxValue - minValue;
            int bitsNeeded = (int)Math.Ceiling(Math.Log(range, 2));
            int result = 0;

            for (int i = 0; i < bitsNeeded; i++)
            {
                int bit = NextBit();
                result = (result << 1) | bit;
            }

            return minValue + result % range;
        }
    }
}