using System.Text;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace app.lab14
{
    public class Steg2
    {
        public static void EmbedMessage(string containerPath, string message, string outputImagePath)
        {
            // загрузить контейнер
            Bitmap container = new Bitmap(containerPath);

            // конвертировать сообщение в битовый массив
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            BitArray messageBits = new BitArray(messageBytes);

            // проверить, есть ли достаточно места в контейнере для размещения сообщения
            int maxMessageBits = (container.Width * container.Height) * 3; // максимальное количество битов сообщения, которое можно разместить в контейнере
            if (messageBits.Length > maxMessageBits)
            {
                throw new Exception("Сообщение слишком большое для данного контейнера");
            }

            // разместить битовый поток сообщения в контейнере
            int messageBitIndex = 0;
            for (int y = 0; y < container.Width; y++)
            {
                for (int x = 0; x < container.Height; x++)
                {
                    Color pixel = container.GetPixel(x, y);
                    if (messageBitIndex < messageBits.Length)
                    {
                        // получить биты цветового значения пикселя
                        BitArray redBits = new BitArray(new byte[] { pixel.R });
                        BitArray greenBits = new BitArray(new byte[] { pixel.G });
                        BitArray blueBits = new BitArray(new byte[] { pixel.B });

                        // заменить младший значащий бит в каждом цветовом значении на бит из сообщения
                        if (messageBits[messageBitIndex])
                        {
                            redBits[0] = true;
                        }
                        else
                        {
                            redBits[0] = false;
                        }

                        messageBitIndex++;

                        if (messageBitIndex < messageBits.Length)
                        {
                            if (messageBits[messageBitIndex])
                            {
                                greenBits[0] = true;
                            }
                            else
                            {
                                greenBits[0] = false;
                            }

                            messageBitIndex++;
                        }

                        if (messageBitIndex < messageBits.Length)
                        {
                            if (messageBits[messageBitIndex])
                            {
                                blueBits[0] = true;
                            }
                            else
                            {
                                blueBits[0] = false;
                            }

                            messageBitIndex++;
                        }

                        // создать новый цветовой пиксель с измененными младшими значащими битами
                        byte[] newRedBytes = new byte[1];
                        byte[] newGreenBytes = new byte[1];
                        byte[] newBlueBytes = new byte[1];
                        redBits.CopyTo(newRedBytes, 0);
                        greenBits.CopyTo(newGreenBytes, 0);
                        blueBits.CopyTo(newBlueBytes, 0);
                        Color newPixel = Color.FromArgb(newRedBytes[0], newGreenBytes[0], newBlueBytes[0]);

                        // установить новый цветовой пиксель в контейнере
                        container.SetPixel(y, x, newPixel);
                    }
                    else
                    {
                        // если все биты сообщения уже размещены, то выйти из цикла
                        break;
                    }
                }

                if (messageBitIndex >= messageBits.Length)
                {
                    // если все биты сообщения уже размещены, то выйти из внешнего цикла
                    break;
                }
            }

            // сохранить измененный контейнер в новый файл
            container.Save(outputImagePath, ImageFormat.Png);
        }

        public static string ExtractMessage(string imagePath)
        {
            // загрузить контейнер
            Bitmap container = new Bitmap(imagePath);

            // создать битовый массив для сообщения
            int messageBitsLength = (container.Width * container.Height) * 3;
            BitArray messageBits = new BitArray(messageBitsLength);

            // извлечь битовый поток сообщения из контейнера
            int messageBitIndex = 0;
            for (int y = 0; y < container.Width; y++)
            {
                for (int x = 0; x < container.Height; x++)
                {
                    Color pixel = container.GetPixel(y, x);
                    BitArray redBits = new BitArray(new byte[] { pixel.R });
                    BitArray greenBits = new BitArray(new byte[] { pixel.G });
                    BitArray blueBits = new BitArray(new byte[] { pixel.B });

                    // извлечь младшие значащие биты из цветового значения пикселя
                    bool redBit = redBits[0];
                    messageBits[messageBitIndex] = redBit;
                    messageBitIndex++;

                    if (messageBitIndex < messageBitsLength)
                    {
                        bool greenBit = greenBits[0];
                        messageBits[messageBitIndex] = greenBit;
                        messageBitIndex++;
                    }

                    if (messageBitIndex < messageBitsLength)
                    {
                        bool blueBit = blueBits[0];
                        messageBits[messageBitIndex] = blueBit;
                        messageBitIndex++;
                    }

                    if (messageBitIndex >= messageBitsLength)
                    {
                        // если все биты сообщения уже извлечены, то выйти из цикла
                        break;
                    }
                }

                if (messageBitIndex >= messageBitsLength)
                {
                    // если все биты сообщения уже извлечены, то выйти из внешнего цикла
                    break;
                }
            }

            // конвертировать битовый массив в текстовое сообщение
            byte[] messageBytes = new byte[(messageBits.Length + 7) / 8];
            messageBits.CopyTo(messageBytes, 0);
            string message = System.Text.Encoding.UTF8.GetString(messageBytes);

            var sb = new StringBuilder("");
            foreach (var ch in message)
            {
                if(((int)ch)>255)
                    break;
                sb.Append(ch);
            }

            return sb.ToString();
        }
    }
}