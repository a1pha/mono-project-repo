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
        => W == 1.0;

    public bool IsVector()
        => W == 0.0;

    public static Tuple point(double x, double y, double z)
        => new Tuple(x, y, z, 1.0);
  

    public static Tuple vector(double x, double y, double z)
        => new Tuple(x, y, z, 0.0);
    

   private static bool CompareDoubleEpsilon(double a, double b, double epsilon)
       => Math.Abs(a - b) < epsilon;
    
   public static bool AreEqual(Tuple a, Tuple b, double epsilon = 0.000001) 
       => CompareDoubleEpsilon(a.X, b.X, epsilon) 
          && CompareDoubleEpsilon(a.Y, b.Y, epsilon) 
          && CompareDoubleEpsilon(a.Z, b.Z, epsilon) 
          && a.W == b.W;

   public static Tuple operator +(Tuple a)
       => a;

   public static Tuple operator -(Tuple a)
       => new Tuple(-a.X, -a.Y, -a.Z, -a.W);
   
   public static Tuple operator +(Tuple a, Tuple b)
       => new Tuple(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
   
   public static Tuple operator -(Tuple a, Tuple b)
       => new Tuple(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);

   public static Tuple operator *(Tuple a, double b)
       => new Tuple(b * a.X, b * a.Y, b * a.Z, b * a.W);

   public static Tuple operator *(double b, Tuple a)
       => new Tuple(b * a.X, b * a.Y, b * a.Z, b * a.W);
   
   public static Tuple operator /(Tuple a, double b)
       => new Tuple(a.X/b, a.Y/b, a.Z/b, a.W/b);
}