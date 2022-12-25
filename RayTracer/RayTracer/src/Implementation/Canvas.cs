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
}