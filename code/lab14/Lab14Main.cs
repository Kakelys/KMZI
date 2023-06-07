using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;

namespace app.lab14
{
    public class Lab14Main
    {
        public static void Main() 
        {
            var inp = @"C:\course\leet\app\lab14\pic.png";
            var res = @"C:\course\leet\app\lab14\res.png";
            Steganography.EmbedMessage(inp, "Super cool message for embed", res);
            Console.WriteLine("Embed completed");
            var resText = Steganography.ExtractMessage(res);
            Console.WriteLine($"Res text: {resText}");

            Steg2.EmbedMessage(inp, "Some message", @"C:\course\leet\app\lab14\res2.png");
            Console.WriteLine($"Res text: {Steg2.ExtractMessage(@"C:\course\leet\app\lab14\res2.png")}");

            /*
            RandomLSB.EmbedMessage(inp, "Super cool message for embed",  @"C:\course\leet\app\lab14\res2.png");
            Console.WriteLine($"Res2: {RandomLSB.ExtractMessage(@"C:\course\leet\app\lab14\res2.png")}");
            */

            SaveLastBit(inp, @"C:\course\leet\app\lab14\in_low.png");
            SaveLastBit(res, @"C:\course\leet\app\lab14\res_low.png");
        }

        public static void SaveLastBit(string imagePath, string outputPath)
        {
            // Открываем исходное изображение
            Bitmap originalImage = new Bitmap(imagePath);

            // Создаем новое изображение с теми же размерами
            Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height);

            // Проходим по каждому пикселю и извлекаем самый младший бит из каждого цветового канала
            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    Color pixel = originalImage.GetPixel(x, y);

                    // Извлекаем младший бит из каждого цветового канала
                    byte red = (byte)(pixel.R & 0x01);
                    byte green = (byte)(pixel.G & 0x01);
                    byte blue = (byte)(pixel.B & 0x01);

                    // Формируем новый цвет на основе извлеченных битов
                    Color newPixel = Color.FromArgb(red * 255, green * 255, blue * 255);

                    // Устанавливаем новый пиксель в новое изображение
                    newImage.SetPixel(x, y, newPixel);
                }
            }

            // Сохраняем новое изображение в формате PNG
            newImage.Save(outputPath, ImageFormat.Png);
        }
    }
}