using NUnit.Framework;
using RayTracer.Implementation;
using Tuple = RayTracer.Implementation.Tuple;

namespace RayTracerUnitTests;

[TestFixture]
public class MatrixTests
{
    [Test]
    public void ConstructEmptyMatrix2x2()
    {
        Matrix mat = new Matrix(2, 2);
        Assert.That(mat[0,0], Is.EqualTo(0));
        Assert.That(mat[0,1], Is.EqualTo(0));
        Assert.That(mat[1,0], Is.EqualTo(0));
        Assert.That(mat[1,1], Is.EqualTo(0));
    }
    
    [Test]
    public void ConstructEmptyMatrix3x3()
    {
        Matrix mat = new Matrix(3, 3);
        Assert.That(mat[0,0], Is.EqualTo(0));
        Assert.That(mat[2,1], Is.EqualTo(0));
        Assert.That(mat[0,2], Is.EqualTo(0));
        Assert.That(mat[2,2], Is.EqualTo(0));
    }
    [Test]
    public void ConstructCustomMatrix2x2()
    {
        Matrix mat = new Matrix(2, 2, "-3 -5 1 -2");
        Assert.That(mat[0,0], Is.EqualTo(-3));
        Assert.That(mat[0,1], Is.EqualTo(-5));
        Assert.That(mat[1,0], Is.EqualTo(1));
        Assert.That(mat[1,1], Is.EqualTo(-2));
    }
    
    [Test]
    public void ConstructCustomMatrix3x3()
    {
        Matrix mat = new Matrix(3, 3, "-3 -5 0 1 -2 -7 0 1 1");
        Assert.That(mat[0,0], Is.EqualTo(-3));
        Assert.That(mat[1,1], Is.EqualTo(-2));
        Assert.That(mat[2,2], Is.EqualTo(1));
    }
    
    [Test]
    public void ConstructCustomMatrix4x4()
    {
        Matrix mat = new Matrix(4, 4, "1 2 3 4 5.5 6.5 7.5 8.5 9 10 11 12 13.5 14.5 15.5 16.5");
        Assert.That(mat[0,0], Is.EqualTo(1));
        Assert.That(mat[0,3], Is.EqualTo(4));
        Assert.That(mat[1,0], Is.EqualTo(5.5));
        Assert.That(mat[1,2], Is.EqualTo(7.5));
        Assert.That(mat[2,2], Is.EqualTo(11));
        Assert.That(mat[3,0], Is.EqualTo(13.5));
        Assert.That(mat[3,2], Is.EqualTo(15.5));
    }
    
    [Test]
    public void ConstructMatrix2x2BadInput1()
    {
        Assert.Throws<ArgumentException>(
            delegate { Matrix mat = new Matrix(2, 2, "-3 -5 1"); });
    }
    
    [Test]
    public void ConstructMatrix2x2BadInput2()
    {
        Assert.Throws<ArgumentException>(
            delegate { Matrix mat = new Matrix(2, 2, ""); });
    }
    
    [Test]
    public void ConstructMatrix2x2BadInput3()
    {
        Assert.Throws<ArgumentException>(
            delegate { Matrix mat = new Matrix(2, 2, "-3 -5 1 1 1"); });
    }
    
    [Test]
    public void ConstructMatrix3x3BadInput1()
    {
        Assert.Throws<ArgumentException>(
            delegate { Matrix mat = new Matrix(2, 2, "-3 -5 1 1 2 3 4 5 6"); });
    }
    
    [Test]
    public void ConstructMatrix3x3BadInput2()
    {
        Assert.Throws<ArgumentException>(
            delegate { Matrix mat = new Matrix(2, 2, ""); });
    }
    
    [Test]
    public void ConstructMatrix3x3BadInput3()
    {
        Assert.Throws<ArgumentException>(
            delegate { Matrix mat = new Matrix(2, 2, "-3 -5 1 1 2 3 4 5 6 1 12"); });
    }
    
    [Test]
    public void EqualsTrue()
    {
        Matrix mat1 = new Matrix(3, 3, "-3 -5 0 1 -2 -7 0 1 1");
        Matrix mat2 = new Matrix(3, 3, "-3 -5 0 1 -2 -7 0 1 1");
        Assert.That(mat1, Is.EqualTo(mat2));
    }
    
    [Test]
    public void EqualsFalse()
    {
        Matrix mat1 = new Matrix(3, 3, "-3 -5 0 0 -2 -7 0 1 1");
        Matrix mat2 = new Matrix(3, 3, "-3 -5 0 1 -2 -7 0 1 1");
        Assert.That(mat1, Is.Not.EqualTo(mat2));
    }
    
    [Test]
    public void EqualsTrueNotExact()
    {
        Matrix mat1 = new Matrix(3, 3, "-3 -5 0 1 -2 -7 0 1 1");
        Matrix mat2 = new Matrix(3, 3, "-3 -5 0 .999999999 -2 -7 0 1 1");
        Assert.That(mat1, Is.EqualTo(mat2));
    }
    
    [Test]
    public void EqualsFalseNotExact()
    {
        Matrix mat1 = new Matrix(3, 3, "-3 -5 0 1 -2 -7 0 1 1");
        Matrix mat2 = new Matrix(3, 3, "-3 -5 0 .99999999 -2 -7 0 1 1");
        Assert.That(mat1, Is.Not.EqualTo(mat2));
    }

    [Test]
    public void Multiply4x4()
    {
        Matrix mat1 = new Matrix(4, 4, "1 2 3 4 4 3 2 1 -1 -2 -3 -4 -4 -3 -2 -1");
        Matrix mat2 = new Matrix(4, 4, "0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15");
        Matrix expected = new Matrix(4, 4, "80 90 100 110 40 50 60 70 -80 -90 -100 -110 -40 -50 -60 -70");
        Assert.That(mat1*mat2, Is.EqualTo(expected));
    }
    
    [Test]
    public void Multiply3x3()
    {
        Matrix mat1 = new Matrix(3, 3, "1 2 3 4 5 6 7 8 9");
        Matrix mat2 = new Matrix(3, 3, "9 8 7 6 5 4 3 2 1");
        Matrix expected = new Matrix(3, 3, "30 24 18 84 69 54 138 114 90");
        Assert.That(mat1*mat2, Is.EqualTo(expected));
    }
    
    [Test]
    public void MultiplyDimensionMismatch()
    {
        Matrix mat1 = new Matrix(4, 4, "1 2 3 4 4 3 2 1 -1 -2 -3 -4 -4 -3 -2 -1");
        Matrix mat2 = new Matrix(2, 2);
        Assert.Throws<InvalidOperationException>(
            delegate
            {
                Matrix result = mat1 * mat2; });
    }

    [Test]
    public void MatrixByTuple()
    {
        Matrix mat = new Matrix(4, 4, "1 2 3 4 2 4 4 2 8 6 4 1 0 0 0 1");
        Tuple tup = new Tuple(1, 2, 3, 1);
        Tuple expected = new Tuple(18, 24, 33,1);
        Assert.That(mat*tup, Is.EqualTo(expected));
    }

    [Test]
    public void Identity()
    {
        Matrix mat = Matrix.IdentityMatrix(4);
        Matrix exp = new Matrix(4, 4, "1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1");
        Assert.That(mat, Is.EqualTo(exp));
    }

    [Test]
    public void MultiplyByIdentity()
    {
        Matrix mat = new Matrix(4, 4, "1 2 3 4 2 4 4 2 8 6 4 1 0 0 0 1");
        Matrix identity = Matrix.IdentityMatrix(4);
        Assert.That(mat*identity, Is.EqualTo(mat));
    }

    [Test]
    public void Transpose()
    {
        Matrix mat = new Matrix(4, 4, "1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16");
        Matrix expected = new Matrix(4, 4, "1 5 9 13 2 6 10 14 3 7 11 15 4 8 12 16");
        Assert.That(mat.Transpose(), Is.EqualTo(expected));
    }

    [Test]
    public void TransposeIdentity()
    {
        Assert.That(Matrix.IdentityMatrix(4).Transpose(), Is.EqualTo(Matrix.IdentityMatrix(4)));
    }

    [Test]
    public void Determinant2x2()
    {
        Matrix mat = new Matrix(2, 2, "1 2 3 4");
        Assert.That(mat.Determinant, Is.EqualTo(-2));
    }
}