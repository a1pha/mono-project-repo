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

   private static bool CompareDoubleEpsilon(double a, double b, double epsilon)
    {
        return Math.Abs(a - b) < epsilon;
    } 
   public static bool AreEqual(Tuple a, Tuple b, double epsilon = 0.000001)
   {
       return CompareDoubleEpsilon(a.X, b.X, epsilon) && CompareDoubleEpsilon(a.Y, b.Y, epsilon)
                                                      && CompareDoubleEpsilon(a.Z, b.Z, epsilon)
                                                      && a.W == b.W;
   }

}