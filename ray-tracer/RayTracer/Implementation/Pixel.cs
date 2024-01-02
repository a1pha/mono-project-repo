namespace RayTracer.Implementation;

public class Pixel
{
    public Color Color;

    public Pixel()
    {
        Color = new Color(0, 0, 0);
    }

    public Pixel(Color col)
    {
        Color = col;
    }
}