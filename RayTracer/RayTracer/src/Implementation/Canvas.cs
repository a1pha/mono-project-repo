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
        // Write header
        stringWriter.Write(PPMHeader(can));
        // Write file
        for (int i = 0; i < can.H; i++)
        {
            stringWriter.WriteLine();
            for (int j = 0; j < can.W; j++)
            {
                if (j > 0 && j % 6 == 0)
                {
                    stringWriter.WriteLine();
                }
                else if (j !=  0)
                {
                    stringWriter.Write(" ");
                }

                int R = (int) Math.Round(can.Array[i, j].Color.Red * 255, MidpointRounding.AwayFromZero);
                int G = (int) Math.Round(can.Array[i, j].Color.Green * 255, MidpointRounding.AwayFromZero);
                int B = (int) Math.Round(can.Array[i, j].Color.Blue * 255, MidpointRounding.AwayFromZero);
                R = Math.Min(Math.Max(R, 0), 255);
                G = Math.Min(Math.Max(G, 0), 255);
                B = Math.Min(Math.Max(B, 0), 255);
                
                stringWriter.Write("{0} {1} {2}", R, G, B);
            }
        }

        stringWriter.Close();
        return stringWriter.ToString();
    }
}