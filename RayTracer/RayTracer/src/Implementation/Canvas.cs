namespace RayTracer.Implementation;

public class Canvas
{
    public int L;
    public int W;
    public Pixel[,] Array;

    public Canvas(int l, int w)
    {
        L = l;
        W = w;
        Array = new Pixel[l,w];
        for (int i = 0; i < l; i++)
        {
            for (int j = 0; j < w; j++)
            {
                Array[i, j] = new Pixel();
            }
        }
    }

    public static void WritePixel(Canvas can, int x, int y, Color c)
    {
        if (x < 0 || x >= can.L || y < 0 || y >= can.W)
        {
            return;
        }
        can.Array[x, y].Color = c;
    }

    private static string PPMHeader(Canvas can)
    {
        StringWriter stringWriter = new StringWriter();
        stringWriter.WriteLine("P3");
        stringWriter.WriteLine("{0} {1}", can.L, can.W);
        stringWriter.WriteLine("255");
        stringWriter.Close();
        return stringWriter.ToString();
    }
    
    public static string CanvasToPPM(Canvas can)
    {
        StringWriter stringWriter = new StringWriter();
        // Write header
        stringWriter.Write(Canvas.PPMHeader(can));
        // Write file
        for (int i = 0; i < can.L; i++)
        {
            for (int j = 0; j < can.W; j++)
            {
                if (j > 0 && j % 6 == 0)
                {
                    stringWriter.WriteLine();
                }
                stringWriter.Write("{0} {1} {2} ",
                    (int) can.Array[i,j].Color.Red*255,
                    (int) can.Array[i,j].Color.Green*255,
                    (int) can.Array[i,j].Color.Blue*255);
            }
            stringWriter.WriteLine();
        }

        stringWriter.Close();
        return stringWriter.ToString();
    }
}