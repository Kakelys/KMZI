using System.Diagnostics;
using System.Text;

namespace app.Lab9
{
    public static class Lab9Main
    {
        public static void Main() 
        {
            var a = 31;
            var n = 11111;
            var z = 10;
            Console.WriteLine("Private key");
            var d = Lab9Code.Generate(10);
            d.ForEach(x => Console.Write($"{x} "));
            //ar d = new int[]{ 2, 3, 6, 13, 27, 52, 105, 210, 420, 840, 1680 };
            Console.WriteLine("\nPublic key");
            var e = Lab9.Lab9Code.GenNorm(d.ToList(), a, n, z);
            e.ForEach(x => Console.Write($"{x} "));

            var msg = "Vladislav";
            
            Console.WriteLine($"\nMessage: {msg}, length: {msg.Length}");
            Console.WriteLine("\nEncrypted message");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var c = Lab9Code.Encrypt(e, msg, z);
            stopwatch.Stop();
            //c.ForEach(x => Console.Write($"{x} "));

            Console.WriteLine($"Encrypt time {stopwatch.ElapsedMilliseconds} ms, z = {z}");

            int ai = Lab9Code.InverseNumber(a, n);
            Console.WriteLine($"\nInverse number: {ai}");
            var s = new List<int>();

            foreach (var ch in c)
            {
                s.Add((ch * ai) % n);
            }

            Console.WriteLine("\nDecrypted message\n");
            //s.ForEach(x => Console.Write($"{x} "));

            string res = "";
            stopwatch.Start();
            foreach (int ch in s)
            {
                string tmp = Lab9Code.Decode(d, ch, z);
                res += tmp;
            }
            stopwatch.Stop();
            Console.WriteLine($"Decrypt time {stopwatch.ElapsedMilliseconds} ms, z = {z}");
            var str = Enumerable.Range(0, res.Length / 8)
                .Select(i => Convert.ToByte(res.Substring(i * 8, 8), 2))
                .ToArray();

            //Console.WriteLine(Encoding.UTF8.GetString(str));
        }
    }
}