using NUnit.Framework;
using RayTracer.Implementation;

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
    
}