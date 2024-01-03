namespace RayTracer.Implementation;

public class Transformation
{
    public static Matrix Translation(double x, double y, double z)
    {
        Matrix transform = Matrix.IdentityMatrix(4);
        transform.Array[0,3] = x;
        transform.Array[1,3] = y;
        transform.Array[2,3] = z;

        return transform;
    }

    public static Matrix Scaling(double x, double y, double z){

        Matrix transform = Matrix.IdentityMatrix(4);
        transform.Array[0,0] = x;
        transform.Array[1,1] = y;
        transform.Array[2,2] = z;

        return transform;
    }

    public static Matrix RotationX(double radians){
        Matrix transform = Matrix.IdentityMatrix(4);
        transform.Array[1, 1] = Math.Cos(radians);
        transform.Array[1, 2] = -Math.Sin(radians);
        transform.Array[2, 1] = Math.Sin(radians);
        transform.Array[2, 2] = Math.Cos(radians);

        return transform;
    }

    public static Matrix RotationY(double radians){
        Matrix transform = Matrix.IdentityMatrix(4);
        transform.Array[0, 0] = Math.Cos(radians);
        transform.Array[2, 0] = -Math.Sin(radians);
        transform.Array[0, 2] = Math.Sin(radians);
        transform.Array[2, 2] = Math.Cos(radians);

        return transform;
    }

    public static Matrix RotationZ(double radians){
        Matrix transform = Matrix.IdentityMatrix(4);
        transform.Array[0, 0] = Math.Cos(radians);
        transform.Array[0, 1] = -Math.Sin(radians);
        transform.Array[1, 0] = Math.Sin(radians);
        transform.Array[1, 1] = Math.Cos(radians);

        return transform;
    }

    public static Matrix Shear(double xy, double xz, double yx, double yz, double zx, double zy)
    {
        Matrix transform = Matrix.IdentityMatrix(4);
        transform.Array[0, 1] = xy;
        transform.Array[0, 2] = xz;
        transform.Array[1, 0] = yx;
        transform.Array[1, 2] = yz;
        transform.Array[2, 0] = zx;
        transform.Array[2, 1] = zy;

        return transform;
    }
}