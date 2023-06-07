

using System.Numerics;

namespace app.lab13
{
    public class Lab13Main
    {
        public static void Main() 
        {
            First();
            CurveCrypt.Main();
        }

        static void First() 
        {
            var P = new Point { X = 7, Y = 14 };
            var Q = new Point { X = 4, Y = 2 };
            var R = new Point { X = 5, Y = 6 };
            var k = new BigInteger(10);
            var l = new BigInteger(15);

            Console.WriteLine($"P: {P}");
            Console.WriteLine($"Q: {Q}");
            Console.WriteLine($"R: {R}");

            var kP = PointHelper.Multiply(k, P);
            var QplusP = PointHelper.Add(Q, P);
            var klQminusR = PointHelper.MultiplyAddSubtract(k, P, l, Q, R);
            var PminusQplusR = PointHelper.AddSubtract(P, Q, R);

            Console.WriteLine($"kP: {kP}");
            Console.WriteLine($"Q+P: {QplusP}");
            Console.WriteLine($"kP + lQ - R: {klQminusR}");
            Console.WriteLine($"P - Q + R: {PminusQplusR}");
        }
    }
}