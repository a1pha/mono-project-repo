using NUnit.Framework;
using RayTracer.Implementation;

namespace RayTracerUnitTests;

[TestFixture]
public class PixelTests
{
    [Test]
    public void CreatePixel()
    {
        Color black = new Color(0, 0, 0);
        Pixel px = new Pixel(0, 0, black);
        Assert.That(px.X, Is.EqualTo(0));
        Assert.That(px.Y, Is.EqualTo(0));
        Assert.That(px.Color, Is.EqualTo(black));
    }
    
}