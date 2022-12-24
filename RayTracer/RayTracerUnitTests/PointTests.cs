using NUnit.Framework;
using RayTracer.Implementation;

namespace RayTracerUnitTests;

[TestFixture]
public class PointTests
{
    [Test]
    public void GetXDefault()
    {
        Point pt = new Point();
        Assert.AreEqual(0, pt.X);
    }
    
    [Test]
    public void GetYDefault()
    {
        Point pt = new Point();
        Assert.AreEqual(0, pt.Y);
    }
    
    [Test]
    public void GetZDefault()
    {
        Point pt = new Point();
        Assert.AreEqual(0, pt.Z);
    }
    
    [Test]
    public void GetWDefault()
    {
        Point pt = new Point();
        Assert.AreEqual(1, pt.W);
    }
    
    [Test]
    public void GetX()
    {
        Point pt = new Point(1, 2, 3);
        Assert.AreEqual(1, pt.X);
    }
    
    [Test]
    public void GetY()
    {
        Point pt = new Point(1, 2, 3);
        Assert.AreEqual(2, pt.Y);
    }
    
    [Test]
    public void GetZ()
    {
        Point pt = new Point(1, 2, 3);
        Assert.AreEqual(3, pt.Z);
    }
    
    [Test]
    public void GetW()
    {
        Point pt = new Point(1, 2, 3);
        Assert.AreEqual(1, pt.W);
    }
    
    [Test]
    public void IsPoint()
    {
        Point pt = new Point(1, 2, 3);
        Assert.IsTrue(pt.IsPoint());
    }
    
    [Test]
    public void IsVector()
    {
        Point pt = new Point(1, 2, 3);
        Assert.IsFalse(pt.IsVector());
    }
}