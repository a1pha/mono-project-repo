namespace RayTracer.Implementation;

public class Tuple
{
    public double X;
    public double Y;
    public double Z;
    public double W;

    public Tuple(double x, double y, double z, double w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public bool IsPoint()
    {
        return W == 1.0;
    }

    public bool IsVector()
    {
        return W == 0.0;
    }
    
    public static Tuple point(double x, double y, double z)
    {
        return new Tuple(x, y, z, 1.0);
    }

    public static Tuple vector(double x, double y, double z)
    {
        return new Tuple(x, y, z, 0.0);
    }

}