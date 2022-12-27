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

}