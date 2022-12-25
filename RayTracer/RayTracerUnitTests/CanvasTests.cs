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

    [Test]
    public void WritePixelCorrect()
    {
        Canvas can = new Canvas(10, 20);
        Color col = new Color(0.5, 0.3, 0.1);
        Color black = new Color(0, 0, 0);
        Canvas.WritePixel(can, 2, 3, col);
        for (int i = 0; i < can.Array.GetLength(0); i++)
        {
            for (int j = 0; j < can.Array.GetLength(1); j++)
            {
                if (i == 2 && j == 3)
                {
                    Assert.That(can.Array[i,j].Color, Is.EqualTo(col));
                }
                else
                {
                    Assert.That(can.Array[i,j].Color, Is.EqualTo(black));
                }
            }
        }
    }
    
    [Test]
    public void WritePixelIncorrect()
    {
        Canvas can = new Canvas(10, 20);
        Color col = new Color(0.5, 0.3, 0.1);
        Color black = new Color(0, 0, 0);
        Canvas.WritePixel(can, -1, -3, col);
        Canvas.WritePixel(can, 10, 20, col);
        Canvas.WritePixel(can, 100, 200, col);
        for (int i = 0; i < can.Array.GetLength(0); i++)
        {
            for (int j = 0; j < can.Array.GetLength(1); j++)
            {
                Assert.That(can.Array[i,j].Color, Is.EqualTo(black));
            }
        }
    }
    
}