using System;
using System.Collections.Generic;
using System.Linq;

namespace app
{
    public class Lab6
    {
        // VI IV II B 1-2-2

        public static string alphabet = "abcdefghijklmnopqrstuvwxyz";

        // it's VI rotor
        static string leftR = "jpgvoumfyqbenhzrdkasxlictw";
        static int leftP = 1; 
        static int leftCurr = 0;

        // it's IV rotor
        static string middleR = "esovpzjayquirhxlnfthkdcmwb";
        static int middleP = 2;
        static int middleCurr = 0;

        // it's II rotor
        static string rightR = "ajdksiruxblhwtmcqgznpyfvoe";
        static int rightP = 2;
        static int rightCurr = 0;

        // it's B reflector
        static Dictionary<string, string> reflector = new ()
        {
            {"a", "y"},
            {"y", "a"}, // 1
            {"b", "r"},
            {"r", "b"}, // 2
            {"c", "u"},
            {"u", "c"}, // 3
            {"d", "h"},
            {"h", "d"}, // 4
            {"e", "q"},
            {"q", "e"}, // 5
            {"f", "s"},
            {"s", "f"}, // 6
            {"g", "l"},
            {"l", "g"}, // 7
            {"i", "p"},
            {"p", "i"}, // 8
            {"j", "x"},
            {"x", "j"}, // 9
            {"k", "n"},
            {"n", "k"}, // 10
            {"m", "o"},
            {"o", "m"}, // 11
            {"t", "z"},
            {"z", "t"}, // 12
            {"v", "w"},
            {"w", "v"}, // 13
        };

        public static void Main()
        {
            var text = "osu";
            var encrypted = Encrypt(text.ToLower());
            Console.WriteLine(encrypted);
            Console.WriteLine("\n\n");

            var decrypted = Decrypt(encrypted);
            Console.WriteLine(decrypted);
        }

        static string Encrypt(string text)
        {
            var res = "";
            rightCurr = 0;
            middleCurr = 0;
            leftCurr = 0;

            for(var i = 0; i < text.Length; i++)
            {
                var letter = text[i].ToString();
                Console.Write($"{letter}-");
                letter = EncryptLetterForward(letter, true);
                letter = Reflect(letter);
                Console.Write($"{letter}(R)-");
                letter = EncryptLetterBackward(letter, false);
                rotate();
                Console.WriteLine();
                res += letter;
            }
            
            return res;
        }

        static string Decrypt(string text)
        {
            var res = "";
            rightCurr = 0;
            middleCurr = 0;
            leftCurr = 0;
            
            for(var i = 0; i < text.Length; i++)
            {
                var letter = text[i].ToString();
                Console.Write($"{letter}-");
                letter = EncryptLetterForward(letter, true);
                letter = Reflect(letter);
                Console.Write($"{letter}(R)-");
                letter = EncryptLetterBackward(letter, false);
                rotateBack();
                Console.WriteLine();
                res += letter;
            }

            return res;
        }

        static void rotate() {
            if(middleP == 0 || rightP == 0 || leftP == 0)
                throw new Exception("Hey bro, this code work only with rotate positions > 0");

            rightCurr = (rightCurr + rightP) % alphabet.Length;
            middleCurr = (middleCurr + middleP) % alphabet.Length;
            leftCurr =  (leftCurr + leftP) % alphabet.Length;
        }

        static void rotateBack() {
            if(middleP == 0 || rightP == 0 || leftP == 0)
                throw new Exception("Hey bro, this code work only with rotate positions > 0");

            rightCurr = (rightCurr - rightP + alphabet.Length) % alphabet.Length;
            middleCurr = (middleCurr - middleP + alphabet.Length) % alphabet.Length;
            leftCurr = (leftCurr - leftP + alphabet.Length) % alphabet.Length;
        }

        static string EncryptLetterForward(string letter, bool isForward)
        {
            // by right
            letter = EncryptByRightRotor(letter, isForward);
            Console.Write($"{letter}(r)-");
            // by middle
            letter = EncryptByMiddleRotor(letter, isForward);
            Console.Write($"{letter}(m)-");
            // by left
            letter = EncryptByLeftRotor(letter, isForward);
            Console.Write($"{letter}(l)-");

            return letter;   
        }

        static string Reflect(string letter)
        {
            var reflected = reflector.FirstOrDefault(x => x.Key == letter).Value;
            if(string.IsNullOrEmpty(reflected))
                throw new Exception("Invalid letter in Reflector");

            return reflected;
        }

        static string EncryptLetterBackward(string letter, bool isForward)
        {
            // by left
            letter = EncryptByLeftRotor(letter, isForward);
            Console.Write($"{letter}(l)-");
            // by middle
            letter = EncryptByMiddleRotor(letter, isForward);
            Console.Write($"{letter}(m)-");
            // by right
            letter = EncryptByRightRotor(letter, isForward);
            Console.Write($"{letter}(r)-");

            return letter;
        }

        static string EncryptByRightRotor(string letter, bool isForward)
        {
            var letterIndexAlphabet = 0;
            var letterIndexRoter = 0;
            // by right
            if(isForward){
                letterIndexAlphabet = alphabet.IndexOf(letter);
                letterIndexRoter = (letterIndexAlphabet+rightCurr) % alphabet.Length;
                letter = rightR[letterIndexRoter].ToString();
            } 
            else 
            {
                letterIndexRoter = rightR.IndexOf(letter);
                letterIndexAlphabet = (letterIndexRoter+rightCurr) % alphabet.Length;
                letter = alphabet[letterIndexAlphabet].ToString();
            }

            return letter;
        }

        static string EncryptByMiddleRotor(string letter, bool isForward)
        {
            var letterIndexAlphabet = 0;
            var letterIndexRoter = 0;
            // by middle
            if(isForward)
            {
                letterIndexAlphabet = alphabet.IndexOf(letter);
                letterIndexRoter = (letterIndexAlphabet+middleCurr) % alphabet.Length;
                letter = middleR[letterIndexRoter].ToString();
            }
            else 
            {
                letterIndexRoter = middleR.IndexOf(letter);
                letterIndexAlphabet = (letterIndexRoter+middleCurr) % alphabet.Length;;
                letter = alphabet[letterIndexAlphabet].ToString();
            }

            return letter;
        }

        static string EncryptByLeftRotor(string letter, bool isForward)
        {
            var letterIndexAlphabet = 0;
            var letterIndexRoter = 0;
            // by left
            if(isForward)
            {
                letterIndexAlphabet = alphabet.IndexOf(letter);
                letterIndexRoter = (letterIndexAlphabet+leftCurr) % alphabet.Length;
                letter = leftR[letterIndexRoter].ToString();
            }
            else
            {
                letterIndexRoter = leftR.IndexOf(letter);
                letterIndexAlphabet = (letterIndexRoter+leftCurr) % alphabet.Length;;
                letter = alphabet[letterIndexAlphabet].ToString();
            }

            return letter;
        }
    }
}