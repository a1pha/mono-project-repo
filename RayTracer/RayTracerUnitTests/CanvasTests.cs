using NUnit.Framework;
using RayTracer.Implementation;

namespace RayTracerUnitTests;

[TestFixture]
public class CanvasTests
{
    [Test]
    public void CreateCanvas()
    {
        Canvas can = new Canvas(100, 200);
        Assert.That(can.Array.Rank, Is.EqualTo(2));
        Assert.That(can.Array.GetLength(0), Is.EqualTo(100));
        Assert.That(can.Array.GetLength(1), Is.EqualTo(200));
    }
    
}