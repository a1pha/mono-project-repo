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
        can.WritePixel(2,3, col);
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
        can.WritePixel(-1,-3, col);
        can.WritePixel(10,20, col);
        can.WritePixel(100,200, col);
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
        String ppmstring = can.ToPPM();
        using (var reader = new StringReader(ppmstring))
        {
            Assert.That(reader.ReadLine(), Is.EqualTo("P3"));
            Assert.That(reader.ReadLine(), Is.EqualTo("10 20"));
            Assert.That(reader.ReadLine(), Is.EqualTo("255"));
        }
    }

    [Test]
    public void CanvasToPPMNoOverflow1()
    {
        Canvas can = new Canvas(5, 3);
        Color col1 = new Color(1.5, 0, 0);
        Color col2 = new Color(0, 0.5, 0);
        Color col3 = new Color(-0.5, 0, 1);
        can.WritePixel(0,0,col1);
        can.WritePixel(2,1,col2);
        can.WritePixel(4,2,col3);
        String ppmstring = can.ToPPM();
        String expected =
            "P3\n5 3\n255\n255 0 0 0 0 0 0 0 0 0 0 0 0 0 0\n0 0 0 0 0 0 0 128 0 0 0 0 0 0 0\n0 0 0 0 0 0 0 0 0 0 0 0 0 0 255\n";
        using (var reader = new StringReader(ppmstring))
        {
            String actual = reader.ReadToEnd();
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
    
    [Test]
    public void CanvasToPPMNoOverflow2()
    {
        Canvas can = new Canvas(10, 2);
        Color col = new Color(1, 0.8, 0.6);
        can.FillWithColor(col);
        String ppmstring = can.ToPPM();
        String expected =
            "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n" +
            "153 255 204 153 255 204 153 255 204 153 255 204 153\n" +
            "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n" +
            "153 255 204 153 255 204 153 255 204 153 255 204 153";
        using (var reader = new StringReader(ppmstring))
        {
            String actual = reader.ReadToEnd();
            string[] lines = actual.Split('\n');
            string output = string.Join("\n", lines[3..7]);
            Console.Write(output);
            Assert.That(output, Is.EqualTo(expected));
        }
    }

    [Test]
    public void CanvasToPPMEndsNln()
    {
        Canvas can = new Canvas(5, 3);
        String ppm = can.ToPPM();
        Assert.That(ppm.Last(), Is.EqualTo('\n'));
    }

}