namespace RayTracer.Implementation;

public class Pixel
{
    public int X;
    public int Y;
    public Color Color;

    public Pixel(int x, int y, Color col)
    {
        X = x;
        Y = y;
        Color = col;
    }
}