using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace RayTracer.Implementation;

public class Canvas
{
    public int W;
    public int H;
    public Pixel[,] Array;

    public Canvas(int w, int h)
    {
        W = w;
        H = h;
        Array = new Pixel[H,W];
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                Array[i, j] = new Pixel();
            }
        }
    }

    public static void WritePixel(Canvas can, int x, int y, Color c)
    {
        if (x < 0 || x >= can.W || y < 0 || y >= can.H)
        {
            return;
        }
        can.Array[y, x].Color = c;
    }

    private static string PPMHeader(Canvas can)
    {
        StringWriter stringWriter = new StringWriter();
        stringWriter.Write("P3\n{0} {1}\n255", can.W, can.H);
        stringWriter.Close();
        return stringWriter.ToString();
    }
    
    private static void WritePixelToPPM(StringBuilder buffer, Color color, ref int charactersWrittenForPixel)
    {
        int R = (int) Math.Round(color.Red * 255, MidpointRounding.AwayFromZero);
        int G = (int) Math.Round(color.Green * 255, MidpointRounding.AwayFromZero);
        int B = (int) Math.Round(color.Blue * 255, MidpointRounding.AwayFromZero);
        R = Math.Min(Math.Max(R, 0), 255);
        G = Math.Min(Math.Max(G, 0), 255);
        B = Math.Min(Math.Max(B, 0), 255);

        string[] strings = new string[] { R.ToString(), G.ToString(), B.ToString() };

        foreach (string s in strings)
        {
            if (charactersWrittenForPixel + 1 + s.Length > 70)
            {
                buffer.AppendLine();
                charactersWrittenForPixel = 0;
            }
            else if (charactersWrittenForPixel != 0)
            {
                buffer.Append(" ");
                charactersWrittenForPixel++;
            }

            buffer.Append(s);
            charactersWrittenForPixel += s.Length;
        }
    }

    public static string CanvasToPPM(Canvas can)
    {
        StringWriter stringWriter = new StringWriter();
        // Write header
        stringWriter.WriteLine(PPMHeader(can));
        StringBuilder buffer = new StringBuilder();
        for (int i = 0; i < can.H; i++)
        {
            if (buffer.Length > 0)
            {
                buffer.AppendLine();
            }

            int charactersWrittenForPixel = 0;
            for (int j = 0; j < can.W; j++)
            {
                WritePixelToPPM(buffer, can.Array[i, j].Color, ref charactersWrittenForPixel);
            }
        }

        stringWriter.Write(buffer.ToString());
        stringWriter.Close();
        return stringWriter.ToString();
    }

    public void FillWithColor(Color col)
    {
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                Array[i, j].Color = col;
            }
        }
    }

    private static int Clamp(int value, int min, int max)
    {
        return Math.Min(Math.Max(value, min), max);
    }
}