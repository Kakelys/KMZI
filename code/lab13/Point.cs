using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace app.lab13
{
    public class Point
    {
        public BigInteger X;
        public BigInteger Y;


        public override string ToString()
        {
            return $"(X:{X}, Y:{Y})";
        }
    }
}