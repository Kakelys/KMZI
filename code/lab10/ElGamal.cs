using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;
using System.Text;

namespace app.lab10
{
    public static class ElGamal
    {
       public static void Main()
        {
            //  El-Gamal encryption

            int p = 0, g = 0, x = 0, k = 0;
            Random random = new Random();

            BigInteger y = 0, a = 0, p0 = 0, m1 = 0;
            BigInteger[] b = { };
            BigInteger[] r = { };

            string encData;
            string decData;

            char[] array;

            //Encryption

            Start:;

            encData = "";
            decData = "";

            p = 227;

            if (IsPrime(p))
            {
                g = 50;
                x = 15;
                y = BigInteger.ModPow(g, x, p);

                var watch = new Stopwatch();

                string data = "Senchneya Vladislav Igorevich";
                data = "Senchenya Vladislav Igorevich;alskdjfaopwimvbopasidfhgvnapmalskdjfaopwimvbopasidfhgvnapmseoxasoidjfmpbvoaeiwrhncpasoiedrjfgnapboseirjmcpalskdjfaopwimvbopasidfhgvnapmseoxasoidjfmpbvoaeiwrhncpasoiedrjfgnapboseirjmcpfaosidjfphganbshudffaosidjfphganbshudfsealskdjfaopwimvbopasidfhgvnapmseoxasoidjfmpbvoaeiwrhncpasoiedrjfgnapboseirjmcpfaosidjfphganbshudfoxasoidjfmpbvoaeiwrhncpasoiedrjfgnapboseirjmcpfaosidjfphganbshudf";
                //data = "Improved hold related bringing did. Luckily child education. Humoured first belonging pretty extent feet favour mirth affronting Luckily child edu Luckily child education. Humoured first belonging pretty extent feet favour mirth affrontingconnectiondoorproperty northward. Design cation. Humoured first belonging pretty extent feet favour mirth affrontingconnectiondoorproperty northward. Design connectiondoorproperty northward. Design ladies object has however though reserved. Years busy uncommonly outlivedquickquiethalf size reserved though same means found offices oppose besides often. Rose mirth that produced girl better. Chapterthinklikewisemother right village sometimes will being expenses. Vicinity square blind walk frequentlyaroserenderedrentgoodness his sympathize pain private roof favourable alteration returned. Effectsexertionmanorpursetimesotherwise. Neat has added head theirs body resolve allow. Apartments daughter after them blessing surroundeddwellinghappiness day estate supplied done hastily situation. Meant through instantly unable much rich girlminuterunlocked spot. Are perpetual received. Before depend settled said. Believed valley welcome uncommonly own. Figure insensible procuring temper marked proposal objectroomsquicksociety goodness bed. Of relied forfeited towards sitting dining enjoy concerns raptures whenformedup.Amongelderly wife off pulled come offence position particular mean regular six goingesteemdelightfullose.Pictureashamed need wicket fortune disposing gay bed depend horses merry attacks burst conveying. Repeated distant breakfast off last state. Miles you shortly wooded branch cousin september formerly comparison goodness just neat hearted simplicity. Procuring vicinity wished servants found merry brother chapter correct change be latter attempted county. Men small savings comfort alone butabovedonesociableherselffar.Sheddidentreaties cordially weeks calling. Being expect enjoyment have forth dinner rejoicedthoughtsakeintentionpolitecome.Severaldiscoveredmistress.Acceptance show went tried shew turned securing might meritobjectionpossibleexplainedlettersleastchamberextremely. Occasional excellent above right make compass rich easily entire ourselves would ferrars.Talkedraillery mistress course boisterous from steepest hold ccation. Humoured first belonging pretty extent feet favour mirth affronting Luckily child edu Luckily child education. Humoured first belonging pretty extent feet favour mirth affrontingconnectiondoorproperty northward. Design cation. Humoured first belonging pretty extent feet favour mirth affrontingconnectiondoorproperty northward. Design connectiondoorproperty northward. Design ladies object has however though reserved. Years busy uncommonly outlivedquickquiethalf size reserved though same means found offices oppose besides often. Rose mirth that produced girl better. Chapterthinklikewisemother right village sometimes will being expenses. Vicinity square blind walk frequentlyaroserenderedrentgoodness his sympathize pain private roof favourable alteration returned. Effectsexertionmanorpursetimesotherwise. Neat has added head theirs body resolve allow. Apartments daughter after them blessing surroundeddwellinghappiness day estate supplied done hastily situation. Meant through instantly unable much rich girlminuterunlocked spot. Are perpetual received. Before depend settled said. Believed valley welcome uncommonly own. Figure insensible procuring temper marked proposal objectroomsquicksociety goodness bed. Of relied forfeited towards sitting dining enjoy concerns raptures whenformedup.Amongelderly wife off pulled come offence position particular mean regular six goingesteemdelightfullose.Pictureashamed need wicket fortune disposing gay bed depend horses merry attacks burst conveying. Repeated distant breakfast off last state. Miles you shortly wooded branch cousin september formerly comparison goodness just neat hearted simplicity. Procuring vicinity wished servants found merry brother chapter correct change be latter attempted county. Men small savings comfort alone butabovedonesociableherselffar.Sheddidentreaties cordially weeks calling. Being expect enjoyment have forth dinner rejoicedthoughtsakeintentionpolitecome.Severaldiscoveredmistress.Acceptance show went tried shew turned securing might meritobjectionpossibleexplainedlettersleastchamberextremely. Occasional excellent above right make compass rich easily entire ourselves would ferrars.Talkedraillery mistress course boisterous from steepest hold ertainly unpleasing cordially which new. Themselves greater arrive";
                Console.WriteLine($"Original message: {data}");
                Console.WriteLine($"Original message length: {data.Length}");
                array = data.ToCharArray();

                k = 20;
                a = BigInteger.ModPow(g, k, p);

                //Encryption

                b = new BigInteger[array.Length];
                watch.Start();
                for (int i = 0; i < array.Length; i++)
                {

                    b[i] = BigInteger.Remainder(BigInteger.Multiply(BigInteger.Pow(y, k), array[i]), p);

                    encData += b[i].ToString();
                }
                watch.Stop();

                Console.WriteLine("\nEncrypted data: " + encData);
                Console.WriteLine($"Encryption time: {watch.ElapsedMilliseconds} ms");

                //Decryption

                r = new BigInteger[b.Length];
                watch.Start();
                for (int i = 0; i < b.Length; i++)
                {
                    p0 = BigInteger.Subtract(BigInteger.Subtract(p, new BigInteger(1)), x);
                    m1 = BigInteger.ModPow(a, p0, p);
                    r[i] = BigInteger.Remainder(BigInteger.Multiply(m1, b[i]), p);

                    decData = decData + ((char)r[i]).ToString();

                }
                watch.Stop();

                //Console.WriteLine("\nDecrypted data: " + decData);
                Console.WriteLine($"Decryption time: {watch.ElapsedMilliseconds} ms");
            }
            else
            {
                Console.WriteLine("\nThis Number is Not a Prime Number , Enter Another One");
            }
        }


        public static bool IsPrime(int Number)
        {

            if ((Number & 1) == 0)
            {
                if (Number == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            for (int i = 3; (i * i) <= Number; i += 2)
            {
                if ((Number % i) == 0)
                {
                    return false;
                }
            }
            return Number != 1;
        }
    }
}