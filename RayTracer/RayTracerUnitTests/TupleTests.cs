using NUnit.Framework;
using Tuple = RayTracer.Implementation.Tuple;

namespace RayTracerUnitTests;

[TestFixture]
public class TupleTests
{
    [Test]
    public void GetX()
    {
        Tuple tup = new Tuple(1, 2, 3, 1);
        Assert.AreEqual(1, tup.X);
    }
    
    [Test]
    public void GetY()
    {
        Tuple tup = new Tuple(1, 2, 3, 1);
        Assert.AreEqual(2, tup.Y);
    }
    
    [Test]
    public void GetZ()
    {
        Tuple tup = new Tuple(1, 2, 3, 1);
        Assert.AreEqual(3, tup.Z);
    }
    
    [Test]
    public void GetW()
    {
        Tuple tup = new Tuple(1, 2, 3, 1);
        Assert.AreEqual(1, tup.W);
    }
    
    [Test]
    public void IsPointTrue()
    {
        Tuple point = new Tuple(1, 2, 3, 1);
        Assert.IsTrue(point.IsPoint());
    }

    [Test]
    public void IsPointFalse()
    {
        Tuple vector = new Tuple(1, 2, 3, 0);
        Assert.IsFalse(vector.IsPoint());
    }
    
    [Test]
    public void IsVectorTrue()
    {
        Tuple vector = new Tuple(1, 2, 3, 0);
        Assert.IsTrue(vector.IsVector());
    }
    
    [Test]
    public void IsVectorFalse()
    {
        Tuple point = new Tuple(1, 2, 3, 1);
        Assert.IsFalse(point.IsVector());
    }
    
    [Test]
    public void CreatesPoint()
    {
        Tuple point = Tuple.point(4, -4, 3);
        Assert.AreEqual(4, point.X);
        Assert.AreEqual(-4, point.Y);
        Assert.AreEqual(3, point.Z);
        Assert.AreEqual(1, point.W);
    }

    [Test]
    public void CreatesVector()
    {
        Tuple vector = Tuple.vector(4, -4, 3);
        Assert.AreEqual(4, vector.X);
        Assert.AreEqual(-4, vector.Y);
        Assert.AreEqual(3, vector.Z);
        Assert.AreEqual(0, vector.W);
    }
}