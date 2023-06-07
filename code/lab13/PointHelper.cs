using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace app.lab13
{
    public static class PointHelper
    {
        public static BigInteger a = 5;
        public static BigInteger b = 5;
        public static BigInteger p = 17;

        // kP
        public static Point Multiply(BigInteger k, Point P)
        {
            var result = new Point();
            var current = new Point { X = P.X, Y = P.Y };

            for (int i = 0; i < k.ToByteArray().Length * 8; i++)
            {
                if (k.IsBitSet(i))
                {
                    result = Add(result, current);
                }

                current = Double(current);
            }

            return result;
        }

        public static bool IsBitSet(this BigInteger n, int index)
        {
            return (n & (BigInteger.One << index)) != BigInteger.Zero;
        }

        private static Point Double(Point P)
        {
            if (P.Y == 0)
            {
                return new Point { X = 0, Y = 0 };
            }

            var slope = (3 * P.X * P.X + a) * BigInteger.ModPow(2 * P.Y, p - 2, p);
            var x = (slope * slope - 2 * P.X) % p;
            var y = (slope * (P.X - x) - P.Y) % p;

            return new Point { X = x < 0 ? x + p : x, Y = y < 0 ? y + p : y };
        }

        // P + Q
        public static Point Add(Point P, Point Q)
        {
            if (P.X == 0 && P.Y == 0)
            {
                return Q;
            }

            if (Q.X == 0 && Q.Y == 0)
            {
                return P;
            }

            if (P.X == Q.X && P.Y != Q.Y)
            {
                return new Point { X = 0, Y = 0 };
            }

            if (P.X == Q.X)
            {
                return Double(P);
            }

            var slope = (Q.Y - P.Y) * BigInteger.ModPow(Q.X - P.X, p - 2, p);
            var x = (slope * slope - P.X - Q.X) % p;
            var y = (slope * (P.X - x) - P.Y) % p;

            return new Point { X = x < 0 ? x + p : x, Y = y < 0 ? y + p : y };
        }

        // kP + lQ - R
        public static Point MultiplyAddSubtract(BigInteger k, Point P, BigInteger l, Point Q, Point R)
        {
            var kp = Multiply(k, P);
            var lq = Multiply(l, Q);
            var klq = Add(kp, lq);

            return Subtract(klq, R);
        }

        public static Point Subtract(Point P, Point Q)
        {
            var negativeQ = new Point { X = Q.X, Y = -Q.Y };

            return Add(P, negativeQ);
        }

        // P + Q - R
        public static Point AddSubtract(Point P, Point Q, Point R)
        {
            var negativeQ = new Point { X = Q.X, Y = -Q.Y };
            var pq = Add(P, negativeQ);

            return Add(pq, R);
        }
    }
}