using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace app
{
    public static class Lab5
    {
        public static string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public static void Main() {
            
            var str = "Incommode no delay domestic seven what longer sake manners prudent suffer like believing see shewing. Form doors assure depart summer exertion least settle young required followed. Ladies any rest servants reasonable four conveying greatest whose amongst right feebly excuse husband leave. Moment sensible in sang hunted reasonable more as. Unreserved depend know hearted acceptance diminution resembled pianoforte. Abroad paid offices almost delivered could week beauty relation entirely particular eyes consider agreement formal shy secure. Looked frankness asked abroad now. Believing cold lovers horses suppose if equally marianne uncommonly elinor than receivedintroduced favour. Unpacked promotion improving discovery worthy at afford hastily happen wife diverted thought lain. Anyconsisted this books discovered. Last enough enabled turned. Direction felt several earnestly parish side contained hundred returned fruit doubtful miss little rather rapturous. Attention played his inquietude water likely reached two half. Announcing cause blessing formed. Sir ﻿no sister spring neglected opinions tiled perpetual matters enough. Projection period wound lain several real. Paid required middleton fine upon them shy doubtful waiting prepare unknown unlocked delivered furniture rooms again. Children weeks preference. Shot then every held advantages green. Decay depend invitation are so. Father arose beyond determine explain consisted screened earnestly husbands court marianne prospect. John than assistance add length ladies principles unaffected required forbade others civility. Introduced attempt wholly prevailed music point denote think regular imprudence went resolve graceful desirous mirth. Excited building assure sons unfeelingcomparison wound sending quick gave noise myself small domestic humanity justice. Provision well settle. All spoil concerns. Abroad inquietude feebly consider advantages endeavor sending added. Then delight resolution never ladyship extremely garden brought mistake earnestly seven wicket joy moment repair marry known. Departure acceptance humanity linen attempted hopes marianne supplied general produced girl before missed performed. Cold tall lived resolved those ten so. Misery evening questions roof often property wicket returned. Really their proposal county of preserved lively concealed. Afraid concluded sending asked mother proposal received attended ready calling written settling occasional since none principle. Window wishing downs next expense detract so cheerful concluded wishing table opinion green remember. Procuring promotion engaged danger temper trifling settling prospect shy hastened calling form. Rent poor exposed felicity genius norland find learn oppose mrs graceful so listening ham pleasant. Make answered full prepare projection call four additions. Attempt considered truth  hold worth smiling occasion regret lain eldest wandered dear dull. Principle should unreserved feebly reserved lasting shall. Literature collected exercise dinner hopetedious. Shade uncommonly front occasional future contempt quick are week cousin earnestly decisively. Good produced books cheerful pressed cold pulled pretended hearted inquietude while. Miss frequently speedilylattefrontexcusethrown required considered replied books waiting desirous consider melancholy. Had cheerful house months children name beCommanded quiet unpleasant assure sake unpleasing like country believing contrasted. Is affronting eat started restinnatesupposing dashwoods within defer inquiry. Jointure favour judgment waiting rejoiced dwelling. ";

            Console.WriteLine($"Original text length: {str.Length}");

            //Lab5.RouteEncrypt(str, "celeste");

            Lab5.RouteEncrypt("Hello world with route encryption", "celeste");

            /*
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var encrypted = Lab5.Encrypt("some random text writen in monday midnight", "vlad", "senchlya");
            stopwatch.Stop();
            Console.WriteLine($"Multiple permutation encrypt time: {stopwatch.ElapsedMilliseconds} ms");
            Entropy.forAlphabet(encrypted, "multiple_encr.xlsx");

            Console.WriteLine(encrypted);

            stopwatch = new Stopwatch();
            stopwatch.Start();
            var decrypted = Lab5.Decrypt(encrypted, "vlad", "senchlya");
            stopwatch.Stop();
            Console.WriteLine($"Multiple permutation decrypt time: {stopwatch.ElapsedMilliseconds} ms");

            Entropy.forAlphabet(encrypted, "multiple_decr.xlsx");

            Console.WriteLine(decrypted);

            */
        }

        public static void RouteEncrypt(string str, string key) {
            Console.WriteLine($"Text for encryption: {str}\n");
            var keyLength = key.Length;
            var columns = str.Length % keyLength > 0 ? str.Length / keyLength + 1 : str.Length / keyLength;
            var table = new char[columns,keyLength];

            Console.WriteLine($"Key length: {keyLength}");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for(int i = 0, strIndex = 0; i < columns; i++) {
                for(var j = 0 ; j < keyLength; j++) {
                    table[i,j] = strIndex < str.Length ? str[strIndex++] : '_';
                }
            }
            Console.WriteLine("Table for encryption: ");
            Display(table);
            Console.WriteLine("\n\n");

            var res = "";

            bool up = true;
            for(int i = 0;;i++){
                if(res.Length >= str.Length){
                    break;
                }

                for(var j = up ? columns-1 : 0;j >= 0 && j < columns; j = up ? j-1 : j+1){
                    res+=table[j,i];
                }

                up = !up;
            }
            stopwatch.Stop();
            Console.WriteLine($"Route encrypt time: {stopwatch.ElapsedMilliseconds} ms");
            Entropy.ForAlphabet(res, "route_encr.xlsx");
            Console.WriteLine($"\n\nEncrypted text: {res}");

            RouteDecrypt(res, key);
        }

        public static void RouteDecrypt(string str, string key) {
            var rows = key.Length;
            var columns = str.Length % rows > 0 ? str.Length / rows + 1 : str.Length / rows;
            var table = new char[rows, columns];

            //for output
            //var line = "";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //table with encrypted text
            var right = true;
            for(int i = 0, strIndex = 0; strIndex < str.Length; i++) {
                for(var j = right ? 0 : columns-1; j >= 0 && j < columns; j = right ? j+1 : j-1) {
                    table[i,j] = str[strIndex++];
                    //line = right ? line + table[i,j] : table[i,j] + line;
                }
                //Console.WriteLine(line);
                //line = "";
                right = !right;
            }
            Console.Write($"\n\nTable for decryption: \n");
            Display(table);
            Console.WriteLine("\n\n");

            var res = "";
            //decryption
            for(int i = columns - 1; i >= 0; i--) {
                for(var j = 0; j < rows; j++) {
                    res += table[j, i];
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Route decrypt time: {stopwatch.ElapsedMilliseconds} ms");

            Entropy.ForAlphabet(res, "route_decr.xlsx");
            Console.WriteLine($"\n\nDecrypted text: {res}");
        }

        public static string Encrypt(string str, string key1, string key2)
        {
            int rows = key1.Length;
            int cols = key2.Length;
            int length = rows * cols;

            while (str.Length < length)
            {
                str += '_';
            }
            Console.WriteLine($"Text for encryption: {str}\n");

            char[,] matrix = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = str[i * cols + j];
                }
            }
            Display(matrix);
            Console.WriteLine();

            // Переставляем строчки
            string sortedKey1 = ""; key1.OrderBy(c => c).ToList().ForEach(c => sortedKey1 += c);
            char[,] sortedMatrix1 = new char[rows, cols];
            for (int i = 0, lastIndex = -2, validIndex = 0; i < rows; i++)
            {
                int index = key1.IndexOf(sortedKey1[i]);
                if (lastIndex == index)
                {
                    validIndex = index + 1;
                    index = key1.IndexOf(sortedKey1[i], validIndex);
                }
                lastIndex = index;
                for (int j = 0; j < cols; j++)
                {
                    sortedMatrix1[i, j] = matrix[index, j];
                }
            }
            Display(sortedMatrix1);
            Console.WriteLine();

            // Переставляемым столбцы
            string sortedKey2 = ""; key2.OrderBy(c => c).ToList().ForEach(c => sortedKey2 += c);
            char[,] sortedMatrix2 = new char[rows, cols];
            for (int i = 0, lastIndex = -2, validIndex = 0; i < cols; i++)
            {
                int index = key2.IndexOf(sortedKey2[i]);
                if(lastIndex == index)
                {
                    validIndex = index + 1;
                    index = key2.IndexOf(sortedKey2[i], validIndex);
                }
                lastIndex = index;
                for (int j = 0; j < rows; j++)
                {
                    sortedMatrix2[j, i] = sortedMatrix1[j, index];
                }
            }
            Display(sortedMatrix2);
            Console.WriteLine();

            // Формируем зашифрованное сообщение из матрицы
            string encryptedText = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    encryptedText += sortedMatrix2[i, j];
                }
            }

            return encryptedText;
        }

        public static string Decrypt(string encryptedText, string key1, string key2)
        {

            int rows = key1.Length;
            int cols = key2.Length;


            char[,] matrix = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = encryptedText[i * cols + j];
                }
            }
            Display(matrix);
            Console.WriteLine();

            string sortedKey2 = ""; key2.OrderBy(c => c).ToList().ForEach(c => sortedKey2 += c);
            char[,] unsortedMatrix2 = new char[rows, cols];
            for (int i = 0, lastIndex = -2, validIndex = 0; i < cols; i++)
            {
                int index = sortedKey2.LastIndexOf(key2[i]);
                if (index == lastIndex)
                {
                    validIndex = index + 1;
                    index = sortedKey2.LastIndexOf(key2[i], validIndex);
                }
                lastIndex = index;
                for (int j = 0; j < rows; j++)
                {
                    unsortedMatrix2[j, i] = matrix[j, index];
                }
            }
            Display(unsortedMatrix2);
            Console.WriteLine();

            string sortedKey1 = ""; key1.OrderBy(c => c).ToList().ForEach(c => sortedKey1 += c);
            char[,] unsortedMatrix1 = new char[rows, cols];
            for (int i = 0, lastIndex = -2, validIndex = 0; i < rows; i++)
            {
                int index = sortedKey1.LastIndexOf(key1[i]);
                if (index == lastIndex)
                {
                    validIndex = index + 1;
                    index = sortedKey2.LastIndexOf(key2[i], validIndex);
                }
                lastIndex = index;
                for (int j = 0; j < cols; j++)
                {
                    unsortedMatrix1[i, j] = unsortedMatrix2[index, j];
                }
            }
            Display(unsortedMatrix1);


            string decryptedText = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    decryptedText += unsortedMatrix1[i, j];
                }
            }

            return decryptedText.TrimEnd('_');
        }

        public static void Display(char[,] arr){
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}