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
    
    [Test]
    public void AreEqualTrueExact()
    {
        Tuple tup1 = new Tuple(1, 2, 3, 1);
        Tuple tup2 = new Tuple(1, 2, 3, 1);
        Assert.AreEqual(tup1, tup2);
    }
    
    [Test]
    public void AreEqualTrueNotExact()
    {
        Tuple tup1 = new Tuple(1, 2, 3, 1);
        Tuple tup2 = new Tuple(1.0-Double.Epsilon, 2.0-Double.Epsilon, 3.0-Double.Epsilon, 1);
        Assert.AreEqual(tup1, tup2);
    }
    
    [Test]
    public void AreEqualFalseNotExact()
    {
        Tuple tup1 = new Tuple(1, 2, 3, 1);
        Tuple tup2 = new Tuple(0.999999, 1.999999, 2.999999, 1);
        Assert.AreNotEqual(tup1, tup2);
    }
    
    [Test]
    public void AreEqualFalse()
    {
        Tuple tup1 = new Tuple(1, 2, 3, 1);
        Tuple tup2 = new Tuple(1, 5, 3, 1);
        Assert.AreNotEqual(tup1, tup2);
    }

    [Test]
    public void TupleAdditionBasic1()
    {
        Tuple tup1 = new Tuple(1, 2, 3, 1);
        Tuple tup2 = new Tuple(3, 2, 1, 0);
        Tuple sum = tup1 + tup2;
        Tuple expected = new Tuple(4, 4, 4, 1);
        Assert.AreEqual(expected, sum);
    }
    
    [Test]
    public void TupleAdditionBasic2()
    {
        Tuple tup1 = new Tuple(1, 2, 3, 0);
        Tuple tup2 = new Tuple(3, 2, 1, 0);
        Tuple sum = tup1 + tup2;
        Tuple expected = new Tuple(4, 4, 4, 0);
        Assert.AreEqual(expected, sum);
    }
    
    [Test]
    public void TupleSubtractionBasic1()
    {
        Tuple tup1 = Tuple.point(3, 2, 1);
        Tuple tup2 = Tuple.point(5, 6, 7);
        Tuple diff = tup1 - tup2;
        Tuple expected = Tuple.vector(-2, -4, -6);
        Assert.AreEqual(expected, diff);
    }
    
    [Test]
    public void TupleSubtractionBasic2()
    {
        Tuple tup1 = Tuple.point(3, 2, 1);
        Tuple tup2 = Tuple.vector(5, 6, 7);
        Tuple diff = tup1 - tup2;
        Tuple expected = Tuple.point(-2, -4, -6);
        Assert.AreEqual(expected, diff);
    }
    
    [Test]
    public void TupleSubtractionBasic3()
    {
        Tuple tup1 = Tuple.vector(3, 2, 1);
        Tuple tup2 = Tuple.vector(5, 6, 7);
        Tuple diff = tup1 - tup2;
        Tuple expected = Tuple.vector(-2, -4, -6);
        Assert.AreEqual(expected, diff);
    }
    
    [Test]
    public void TupleSubtractFromZero()
    {
        Tuple zero = Tuple.vector(0, 0, 0);
        Tuple tup2 = Tuple.vector(5, -6, 7);
        Tuple diff = zero - tup2;
        Tuple expected = Tuple.vector(-5, 6, -7);
        Assert.AreEqual(expected, diff);
    }
    
    [Test]
    public void NegatingTuple()
    {
        Tuple actual = new Tuple(-1, 2, -3, 4);
        Tuple expected = new Tuple(1, -2, 3, -4);
        Assert.AreEqual(expected, -actual);
    }
    
    [Test]
    public void ScalarMultiplication1()
    {
        Tuple actual = new Tuple(-1, 2, -3, 4);
        Tuple expected = new Tuple(-2, 4, -6, 8);
        Assert.AreEqual(expected, actual*2);
    }
    
    [Test]
    public void ScalarMultiplication2()
    {
        Tuple actual = new Tuple(-1, 2, -3, 4);
        Tuple expected = new Tuple(-0.5, 1.0, -1.5, 2.0);
        Assert.AreEqual(expected, 0.5*actual);
    }

    [Test] 
    public void ScalarDivision()
    {
        Tuple actual = new Tuple(1, -2, -3, -4);
        Tuple expected = new Tuple(0.5, -1, -1.5, -2);
        Assert.AreEqual(expected, actual/2);
    }
    
    [Test] 
    public void Magnitude1()
    {
        Tuple i = Tuple.vector(1,0,0);
        Assert.AreEqual(1, i.Magnitude());
    }
    
    [Test] 
    public void Magnitude2()
    {
        Tuple j = Tuple.vector(0,1,0);
        Assert.AreEqual(1, j.Magnitude());
    }
    
    [Test] 
    public void Magnitude3()
    {
        Tuple k = Tuple.vector(0,0,1);
        Assert.AreEqual(1, k.Magnitude());
    }
    
    [Test] 
    public void Magnitude4()
    {
        Tuple pos = Tuple.vector(1,2,3);
        Assert.AreEqual(Double.Sqrt(14), pos.Magnitude());
    }
    
    [Test] 
    public void Magnitude5()
    {
        Tuple neg = Tuple.vector(-1,-2,-3);
        Assert.AreEqual(Double.Sqrt(14), neg.Magnitude());
    }
    
    [Test] 
    public void Normalize1()
    {
        Tuple orig = Tuple.vector(4,0,0);
        Tuple norm = Tuple.vector(1,0,0);
        Assert.AreEqual(norm, orig.Normalize());
    }
    
    [Test] 
    public void Normalize2()
    {
        Tuple orig = Tuple.vector(1,2,3);
        Tuple norm = Tuple.vector(1.0/Double.Sqrt(14),2.0/Double.Sqrt(14),3/Double.Sqrt(14));
        Assert.AreEqual(norm, orig.Normalize());
    }
    
    [Test] 
    public void Magnitude6()
    {
        Tuple orig = Tuple.vector(1,2,3);
        Tuple norm = orig.Normalize();
        Assert.AreEqual(1, norm.Magnitude());
    }
    
    [Test] 
    public void DotProduct()
    {
        Tuple a = Tuple.vector(1,2,3);
        Tuple b = Tuple.vector(2,3,4);
        Assert.AreEqual(20, Tuple.DotProduct(a, b));
    }
    
    [Test] 
    public void CrossProduct1()
    {
        Tuple a = Tuple.vector(1,2,3);
        Tuple b = Tuple.vector(2,3,4);
        Tuple res = Tuple.vector(-1, 2, -1);
        Assert.AreEqual(res, Tuple.CrossProduct(a,b));
    }
    
    [Test] 
    public void CrossProduct2()
    {
        Tuple a = Tuple.vector(1,2,3);
        Tuple b = Tuple.vector(2,3,4);
        Tuple res = Tuple.vector(1, -2, 1);
        Assert.AreEqual(res, Tuple.CrossProduct(b,a));
    }

}