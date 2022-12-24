namespace RayTracer.Implementation;

public abstract class Tuple
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public double W { get; init; }

    public abstract bool IsPoint();

    public abstract bool IsVector();

}