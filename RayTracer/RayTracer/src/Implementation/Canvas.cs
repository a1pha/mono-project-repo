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
    }
}