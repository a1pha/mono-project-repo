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
        Assert.That(can.Array.GetLength(0), Is.EqualTo(20));
        Assert.That(can.Array.GetLength(1), Is.EqualTo(10));
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
                if (i == 3 && j == 2)
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

    [Test]
    public void CanvasToPPMHeader()
    {
        Canvas can = new Canvas(10, 20);
        String ppmstring = Canvas.CanvasToPPM(can);
        using (var reader = new StringReader(ppmstring))
        {
            Assert.That(reader.ReadLine(), Is.EqualTo("P3"));
            Assert.That(reader.ReadLine(), Is.EqualTo("10 20"));
            Assert.That(reader.ReadLine(), Is.EqualTo("255"));
        }
    }

    [Test]
    public void CanvasToPPMNoOverflow()
    {
        Canvas can = new Canvas(5, 3);
        Color col1 = new Color(1.5, 0, 0);
        Color col2 = new Color(0, 0.5, 0);
        Color col3 = new Color(-0.5, 0, 1);
        Canvas.WritePixel(can, 0, 0, col1);
        Canvas.WritePixel(can, 2, 1, col2);
        Canvas.WritePixel(can, 4, 2, col3);
        String ppmstring = Canvas.CanvasToPPM(can);
        String expected =
            "P3\n5 3\n255\n255 0 0 0 0 0 0 0 0 0 0 0 0 0 0\n0 0 0 0 0 0 0 128 0 0 0 0 0 0 0\n0 0 0 0 0 0 0 0 0 0 0 0 0 0 255";
        using (var reader = new StringReader(ppmstring))
        {
            String actual = reader.ReadToEnd();
            Console.Write(actual);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
    
}