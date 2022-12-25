using NUnit.Framework;
using RayTracer.Implementation;

namespace RayTracerUnitTests;

[TestFixture]
public class CanvasTests
{
    [Test]
    public void CreateCanvas()
    {
        Canvas can = new Canvas(10, 20);
        Assert.That(can.Array.Rank, Is.EqualTo(2));
        Assert.That(can.Array.GetLength(0), Is.EqualTo(10));
        Assert.That(can.Array.GetLength(1), Is.EqualTo(20));
        Color black = new Color(0, 0, 0);
         for (int i = 0; i < can.Array.GetLength(0); i++)
         {
             for (int j = 0; j < can.Array.GetLength(1); j++)
             {
                 Assert.That(can.Array[i,j].Color, Is.EqualTo(black));
             }
         }
    }
    
}