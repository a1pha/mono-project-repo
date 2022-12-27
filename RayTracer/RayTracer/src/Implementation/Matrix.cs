namespace RayTracer.Implementation;

public class Matrix
{
    public long Rows { get; }
    public long Cols { get; }
    public double[,] Array { get; }
    
    // Generic rxc, empty 
    public Matrix(long r, long c)
    {
        Rows = r;
        Cols = c;
        Array = new double[r, c];
    }
    
    // Generic rxc, input provided 
    public Matrix(long r, long c, String data)
    {
        String[] inputData = data.Split(" ");
        if (inputData.Length != r * c)
        {
            throw new ArgumentException(
                "Initialization data does not contain the right number of space separated values");
        }

        Rows = r;
        Cols = c;
        Array = new double[r, c];
        double curr = 0;
        long counter = 0;
        foreach (string val in inputData)
        {
            if (!Double.TryParse(val, out curr))
            {
                throw new ArgumentException("Bad value {0} in initialization data.", val);
            }
            Array[counter / Cols, counter % Cols] = curr;
            counter++;
        }
    }

    public Matrix(Tuple tup)
    {
        Rows = 4;
        Cols = 1;
        Array = new double[4, 1];
        Array[0, 0] = tup.X;
        Array[1, 0] = tup.Y;
        Array[2, 0] = tup.Z;
        Array[3, 0] = tup.W;
    }
    
    public double this[long index1, long index2]
    {
        get { return Array[index1, index2]; }
        set { Array[index1, index2] = value; }
    }
    
    private static bool CompareDoubleEpsilon(double a, double b)
        => Math.Abs(a - b) < 1e-9 ;

    public bool Equals(Matrix other)
    {
        if (this.Cols != other.Cols || this.Rows != other.Rows) return false;
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                if (!CompareDoubleEpsilon(this[i, j], other[i, j])) return false;
            }
        }
        return true;
    }

    public override bool Equals(Object obj)
    {
        if (obj == null)
            return false;

        Matrix mat = obj as Matrix;
        if (mat == null)
            return false;
        else
            return Equals(mat);
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.Cols != b.Rows)
        {
            throw new InvalidOperationException("Incompatible matrices for multiplication");
        }
        Matrix ret = new Matrix(a.Rows, b.Cols);
        double currSum;
        for (int i = 0; i < ret.Rows; i++)
        {
            for (int j = 0; j < ret.Cols; j++)
            {
                currSum = 0;
                for (int k = 0; k < a.Cols; k++)
                {
                    currSum += a[i, k] * b[k, j];
                }
                ret[i, j] = currSum;
            }
        }
        return ret;
    }

    public static Tuple operator *(Matrix mat, Tuple tup)
    {
        Matrix tupmat = new Matrix(tup);
        Matrix result = mat * tupmat;
        Tuple tupresult = new Tuple(result[0, 0], result[1, 0], result[2, 0], result[3, 0]);
        return tupresult;
    }

    public static Matrix identityMatrix(long rank)
    {
        Matrix mat = new Matrix(rank, rank);
        for (int i = 0; i < rank; i++)
        {
            mat.Array[i, i] = 1;
        }
        return mat;
    }

}