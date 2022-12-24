namespace RayTracer.Implementation;

public class Point : Tuple
{
    public Point()
    {
        X = 0;
        Y = 0;
        Z = 0;
        W = 1;
    }

    public Point(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
        W = 1;
    }
    public override bool IsPoint()
    {
        return true;
    }

    public override bool IsVector()
    {
        return false;
    }
}