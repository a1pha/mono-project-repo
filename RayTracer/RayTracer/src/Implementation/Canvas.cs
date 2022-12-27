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
    
    public static string CanvasToPPM(Canvas can)
    {
        StringWriter stringWriter = new StringWriter();
        stringWriter.Write(PPMHeader(can));

        for (int i = 0; i < can.H; i++)
        {
            stringWriter.WriteLine();
            int charactersWritten = 0;
            for (int j = 0; j < can.W; j++)
            {
                int R = (int)Math.Round(can.Array[i, j].Color.Red * 255, MidpointRounding.AwayFromZero);
                int G = (int)Math.Round(can.Array[i, j].Color.Green * 255, MidpointRounding.AwayFromZero);
                int B = (int)Math.Round(can.Array[i, j].Color.Blue * 255, MidpointRounding.AwayFromZero);
                R = Clamp(R, 0, 255);
                G = Clamp(G, 0, 255);
                B = Clamp(B, 0, 255);

                string pixel = $"{R} {G} {B}";
                if (charactersWritten + pixel.Length > 70)
                {
                    stringWriter.WriteLine();
                    charactersWritten = 0;
                }
                else if (charactersWritten != 0)
                {
                    stringWriter.Write(" ");
                    charactersWritten++;
                }
                stringWriter.Write(pixel);
                charactersWritten += pixel.Length;
            }
        }
        stringWriter.Close();
        return stringWriter.ToString();
    }

    private static int Clamp(int value, int min, int max)
    {
        return Math.Min(Math.Max(value, min), max);
    }
}