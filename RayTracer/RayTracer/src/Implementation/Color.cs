namespace RayTracer.Implementation;

public class Color : Tuple
{
    public Color(double red, double green, double blue) : base(red, green, blue, 0)
    {
    }

    public double Red => X;
    public double Green => Y;
    public double Blue => Z;
    
    public static Color operator +(Color a, Color b)
        => new Color(a.Red+b.Red, a.Green+b.Green, a.Blue+b.Blue);
   
    public static Color operator -(Color a, Color b)
        => new Color(a.Red-b.Red, a.Green-b.Green, a.Blue-b.Blue);

    public static Color operator *(Color a, double b)
        => new Color(b * a.Red, b * a.Green, b * a.Blue);

    public static Color operator *(double b, Color a)
        => new Color(b * a.Red, b * a.Green, b * a.Blue);
    
    public static Color operator *(Color a, Color b)
        => new Color(b.Red * a.Red, b.Green * a.Green, b.Blue * a.Blue);
}