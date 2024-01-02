using System;
using System.Text;

namespace RayTracer.Implementation
{
    public class Canvas
    {
        public int Width { get; }
        public int Height { get; }
        public Pixel[,] Array { get; }

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
            Array = new Pixel[Height, Width];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Array[y, x] = new Pixel();
                }
            }
        }

        public void WritePixel(int x, int y, Color color)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return;
            }
            Array[y, x].Color = color;
        }

        public void FillWithColor(Color color)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Array[y, x].Color = color;
                }
            }
        }

        public string ToPPM()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"P3\n{Width} {Height}\n255");

            for (int y = 0; y < Height; y++)
            {
                var charactersWrittenForPixel = 0;
                for (int x = 0; x < Width; x++)
                {
                    WritePixelToPPM(sb, Array[y, x].Color, ref charactersWrittenForPixel);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private static void WritePixelToPPM(StringBuilder buffer, Color color, ref int charactersWrittenForPixel)
        {
            int r = Clamp((int) Math.Round(color.Red * 255, MidpointRounding.AwayFromZero), 0, 255);
            int g = Clamp((int) Math.Round(color.Green * 255, MidpointRounding.AwayFromZero), 0, 255);
            int b = Clamp((int) Math.Round(color.Blue * 255, MidpointRounding.AwayFromZero), 0, 255);

            var values = new[] { r.ToString(), g.ToString(), b.ToString() };

            foreach (var value in values)
            {
                if (charactersWrittenForPixel + 1 + value.Length > 70)
                {
                    buffer.AppendLine();
                    charactersWrittenForPixel = 0;
                }
                else if (charactersWrittenForPixel != 0)
                {
                    buffer.Append(" ");
                    charactersWrittenForPixel++;
                }

                buffer.Append(value);
                charactersWrittenForPixel += value.Length;
            }
        }

        private static int Clamp(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }
    }
}

