using NUnit.Framework;
using RayTracer.Implementation;
using Tuple = RayTracer.Implementation.Tuple;

namespace RayTracer.Tests.Unit;

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
        Matrix mat2 = new Matrix(3, 3, "-3 -5 0 .999 -2 -7 0 1 1");
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

    [Test]
    public void Submatrix3x3Test1()
    {
        Matrix identity = Matrix.IdentityMatrix(3);
        Matrix submatrix = identity.Submatrix( 0, 0);
        Matrix expected = new Matrix(2, 2, "1 0 0 1");
        Assert.That(submatrix, Is.EqualTo(expected));
    }
    
    [Test]
    public void Submatrix3x3Test2()
    {
        Matrix identity = Matrix.IdentityMatrix(3);
        Matrix submatrix = identity.Submatrix( 1, 1);
        Matrix expected = new Matrix(2, 2, "1 0 0 1");
        Assert.That(submatrix, Is.EqualTo(expected));
    }

    [Test]
    public void Minor3x3()
    {
        Matrix mat = new Matrix(3, 3, "3 5 0 2 -1 -7 6 -1 5");
        Assert.That(mat.Minor(1, 0), Is.EqualTo(25));
    }
    
    [Test]
    public void Cofactor3x3()
    {
        Matrix mat = new Matrix(3, 3, "3 5 0 2 -1 -7 6 -1 5");
        Assert.That(mat.Minor(0, 0), Is.EqualTo(-12));
        Assert.That(mat.Cofactor(0, 0), Is.EqualTo(-12));
        Assert.That(mat.Minor(1, 0), Is.EqualTo(25));
        Assert.That(mat.Cofactor(1, 0), Is.EqualTo(-25));
    }

    [Test]
    public void Determinant3x3()
    {
        Matrix mat = new Matrix(3, 3, "1 2 6 -5 8 -4 2 6 4");
        Assert.That(mat.Cofactor(0,0), Is.EqualTo(56));
        Assert.That(mat.Cofactor(0,1), Is.EqualTo(12));
        Assert.That(mat.Cofactor(0,2), Is.EqualTo(-46));
        Assert.That(mat.Determinant(), Is.EqualTo(-196));
    }
    
    [Test]
    public void Determinant4x4()
    {
        Matrix mat = new Matrix(4, 4, "-2 -8 3 5 -3 1 7 3 1 2 -9 6 -6 7 7 -9");
        Assert.That(mat.Cofactor(0,0), Is.EqualTo(690));
        Assert.That(mat.Cofactor(0,1), Is.EqualTo(447));
        Assert.That(mat.Cofactor(0,2), Is.EqualTo(210));
        Assert.That(mat.Cofactor(0,3), Is.EqualTo(51));
        Assert.That(mat.Determinant(), Is.EqualTo(-4071));
    }

    [Test]
    public void IsInvertibleTrue()
    {
        Matrix mat = new Matrix(4,4,"6 4 4 4 5 5 7 6 4 -9 3 -7 9 1 7 -6");
        Assert.That(mat.IsInvertible(), Is.True);
    }

    [Test]
    public void IsIntertibleFalse()
    {
        Matrix mat = new Matrix(4,4,"-4 2 -2 -3 9 6 2 6 0 -5 1 -5 0 0 0 0");
        Assert.That(mat.IsInvertible(), Is.False);   
    }

    [Test]
    public void MatrixInverse1()
    {
        Matrix mat = new Matrix(4, 4, "-5 2 6 -8" +
                                      " 1 -5 1 8" +
                                      " 7 7 -6 -7" +
                                      " 1 -3 7 4");
        Matrix inverse = mat.Inverse();
        Matrix expectedinverse = new Matrix(4, 4, "0.21805 0.45113 0.24060 -0.04511 " +
                                                  "-0.80827 -1.45677 -0.44361 0.52068 " +
                                                  "-0.07895 -0.22368 -0.05263 0.19737 " +
                                                  "-0.52256 -0.81391 -0.30075 0.30639");
        Assert.That(mat.Determinant(), Is.EqualTo(532));
        Assert.That(mat.Cofactor(2,3), Is.EqualTo(-160));
        Assert.That(inverse[3,2], Is.EqualTo(-160.0/532));
        Assert.That(mat.Cofactor(3,2), Is.EqualTo(105));
        Assert.That(inverse[2,3], Is.EqualTo(105.0/532));
        Assert.That(inverse, Is.EqualTo(expectedinverse));
    }
    
    [Test]
    public void MatrixInverse2()
    {
        Matrix mat = new Matrix(4, 4, "8 -5 9 2 7 5 6 1 -6 0 9 6 -3 0 -9 -4");
        Matrix expmatinv = new Matrix(4, 4, "-0.15385 -0.15385 -0.28205 -0.53846 " +
                                            "-0.07692 0.12308 0.02564 0.03077 " +
                                            "0.35897 0.35897 0.43590 0.92308 " +
                                            "-0.69231 -0.69231 -0.76923 -1.92308");
        Assert.That(mat.Inverse(), Is.EqualTo(expmatinv));
    }
    
    [Test]
    public void MatrixInverse3()
    {
        Matrix mat = new Matrix(4, 4, "9 3 0 9 -5 -2 -6 -3 -4 9 6 4 -7 6 6 2");
        Matrix expmatinv = new Matrix(4, 4, "-0.04074 -0.07778 0.14444 -0.22222 " +
                                            "-0.07778 0.03333 0.36667 -0.33333 " +
                                            "-0.02901 -0.14630 -0.10926 0.12963 " +
                                            "0.17778 0.06667 -0.26667 0.33333");
        Assert.That(mat.Inverse(), Is.EqualTo(expmatinv));
    }

    [Test]
    public void MatrixInverse4()
    {
        Matrix A = new Matrix(4, 4, "3 -9 7 3 3 -8 2 -9 -4 4 4 1 -6 5 -1 1");
        Matrix B = new Matrix(4, 4, "8 2 2 2 3 -1 7 0 7 0 5 4 6 -2 0 5");
        Matrix C = A * B;
        Assert.That(C * B.Inverse(), Is.EqualTo(A));
    }
}