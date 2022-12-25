using NUnit.Framework;
using RayTracer.Implementation;

namespace RayTracerUnitTests;

[TestFixture]
public class PixelTests
{
    [Test]
    public void CreatePixelDefault()
    {
        Color black = new Color(0, 0, 0);
        Pixel px = new Pixel();
        Assert.That(px.Color, Is.EqualTo(black));
    }
    
    [Test]
    public void CreatePixelWithColor()
    {
        Color cl = new Color(10, 20, 30);
        Pixel px = new Pixel(cl);
        Assert.That(px.Color, Is.EqualTo(cl));
    }
    
}