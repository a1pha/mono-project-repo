using NUnit.Framework;
using RayTracer.Implementation;

namespace RayTracerUnitTests;

[TestFixture]
public class VectorTests
{
    [Test]
    public void GetXDefault()
    {
        Vector pt = new Vector();
        Assert.AreEqual(0, pt.X);
    }
    
    [Test]
    public void GetYDefault()
    {
        Vector pt = new Vector();
        Assert.AreEqual(0, pt.Y);
    }
    
    [Test]
    public void GetZDefault()
    {
        Vector pt = new Vector();
        Assert.AreEqual(0, pt.Z);
    }
    
    [Test]
    public void GetWDefault()
    {
        Vector pt = new Vector();
        Assert.AreEqual(0, pt.W);
    }
    
    [Test]
    public void GetX()
    {
        Vector pt = new Vector(1, 2, 3);
        Assert.AreEqual(1, pt.X);
    }
    
    [Test]
    public void GetY()
    {
        Vector pt = new Vector(1, 2, 3);
        Assert.AreEqual(2, pt.Y);
    }
    
    [Test]
    public void GetZ()
    {
        Vector pt = new Vector(1, 2, 3);
        Assert.AreEqual(3, pt.Z);
    }
    
    [Test]
    public void GetW()
    {
        Vector pt = new Vector(1, 2, 3);
        Assert.AreEqual(0, pt.W);
    }
    
    [Test]
    public void IsPoint()
    {
        Vector pt = new Vector(1, 2, 3);
        Assert.IsFalse(pt.IsPoint());
    }
    
    [Test]
    public void IsVector()
    {
        Vector pt = new Vector(1, 2, 3);
        Assert.IsTrue(pt.IsVector());
    }
}