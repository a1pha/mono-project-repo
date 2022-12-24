namespace RayTracer.Implementation;

public class Vector : Tuple
{
    public Vector()
    {
        X = 0;
        Y = 0;
        Z = 0;
        W = 0;
    }
    
    public Vector(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
        W = 0;
    }
    public override bool IsPoint()
    {
        return false;
    }

    public override bool IsVector()
    {
        return true;
    }
}