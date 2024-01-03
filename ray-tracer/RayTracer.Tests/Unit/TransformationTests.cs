using System.Drawing;
using NUnit.Framework;
using RayTracer.Implementation;
using Tuple = RayTracer.Implementation.Tuple;

namespace RayTracer.Tests.Unit;

[TestFixture]
public class TransformationTests{

    public static object[] ShearCases =
    {
        new object[] { new double[] {1, 0, 0, 0, 0, 0}, new Tuple(5, 3, 4, 1) },
        new object[] { new double[] {0, 1, 0, 0, 0, 0}, new Tuple(6, 3, 4, 1) },
        new object[] { new double[] {0, 0, 1, 0, 0, 0}, new Tuple(2, 5, 4, 1) },
        new object[] { new double[] {0, 0, 0, 1, 0, 0}, new Tuple(2, 7, 4, 1) },
        new object[] { new double[] {0, 0, 0, 0, 1, 0}, new Tuple(2, 3, 6, 1) },
        new object[] { new double[] {0, 0, 0, 0, 0, 1}, new Tuple(2, 3, 7, 1) },
    };

    [Test]
    public void TranslatePoint(){
        Matrix transform = Transformation.Translation(5, -3, 2);
        Tuple p = new Tuple(-3, 4, 5, 1);
        Tuple expected = new Tuple(2, 1, 7, 1);
        Assert.That(transform * p, Is.EqualTo(expected));
    }

    [Test]
    public void InverseTranslatePoint(){
        Matrix transform = Transformation.Translation(5, -3, 2);
        Matrix inverse = transform.Inverse();
        Tuple p = new Tuple(-3, 4, 5, 1);
        Tuple expected = new Tuple(-8, 7, 3, 1);
        Assert.That(inverse * p, Is.EqualTo(expected));

    }

    [Test]
    public void TranslateVector(){
        Matrix transform = Transformation.Translation(5, -3, 2);
        Tuple vector = new Tuple(-3, 4, 5, 0);
        Assert.That(transform * vector, Is.EqualTo(vector));
    }

    [Test]
    public void ScalingPoint(){
        Matrix transform = Transformation.Scaling(2, 3, 4);
        Tuple p = new Tuple(-4, 6, 8, 1);
        Tuple expected = new Tuple(-8, 18, 32, 1);
        Assert.That(transform * p, Is.EqualTo(expected));
    }

    [Test]
    public void ScalingVector(){
        Matrix transform = Transformation.Scaling(2, 3, 4);
        Tuple vector = new Tuple(-4, 6, 8, 0);
        Tuple expected = new Tuple(-8, 18, 32, 0);
        Assert.That(transform * vector, Is.EqualTo(expected));
    }

    [Test]
    public void InverseScalingVector(){
        Matrix transform = Transformation.Scaling(2, 3, 4);
        Matrix inverse = transform.Inverse();
        Tuple vector = new Tuple(-4, 6, 8, 0);
        Tuple expected = new Tuple(-2, 2, 2, 0);
        Assert.That(inverse * vector, Is.EqualTo(expected));
    }

    [Test]
    public void ReflectionPoint(){
        Matrix transform = Transformation.Scaling(-1, 1, 1);
        Tuple p = new Tuple(2, 3, 4, 1);
        Tuple expected = new Tuple(-2, 3, 4, 1);
        Assert.That(transform * p, Is.EqualTo(expected));
    }

    [Test]
    public void RotationXPoint(){
        Matrix transformHalfQuarter = Transformation.RotationX(Math.PI / 4);
        Matrix transformQuarter = Transformation.RotationX(Math.PI / 2);
        Tuple p = new Tuple(0, 1, 0, 1);
        Tuple expectedHalfQuarter = new Tuple(0, Math.Sqrt(2)/2, Math.Sqrt(2)/2, 1);
        Tuple expectedQuarter = new Tuple(0, 0, 1, 1);
        Assert.That(transformHalfQuarter * p, Is.EqualTo(expectedHalfQuarter));
        Assert.That(transformQuarter * p, Is.EqualTo(expectedQuarter));
    }

    [Test]
    public void RotationXInversePoint(){
        Matrix transformHalfQuarter = Transformation.RotationX(Math.PI / 4);
        Tuple p = new Tuple(0, 1, 0, 1);
        Tuple expectedHalfQuarter = new Tuple(0, Math.Sqrt(2)/2, -Math.Sqrt(2)/2, 1);
        Assert.That(transformHalfQuarter.Inverse() * p, Is.EqualTo(expectedHalfQuarter));
    }

    [Test]
    public void RotationYPoint(){
        Matrix transformHalfQuarter = Transformation.RotationY(Math.PI / 4);
        Matrix transformQuarter = Transformation.RotationY(Math.PI / 2);
        Tuple p = new Tuple(0, 0, 1, 1);
        Tuple expectedHalfQuarter = new Tuple(Math.Sqrt(2)/2, 0, Math.Sqrt(2)/2, 1);
        Tuple expectedQuarter = new Tuple(1, 0, 0, 1);
        Assert.That(transformHalfQuarter * p, Is.EqualTo(expectedHalfQuarter));
        Assert.That(transformQuarter * p, Is.EqualTo(expectedQuarter));
    }

    [Test]
    public void RotationZPoint(){
        Matrix transformHalfQuarter = Transformation.RotationZ(Math.PI / 4);
        Matrix transformQuarter = Transformation.RotationZ(Math.PI / 2);
        Tuple p = new Tuple(0, 1, 0, 1);
        Tuple expectedHalfQuarter = new Tuple(-Math.Sqrt(2)/2, Math.Sqrt(2)/2, 0, 1);
        Tuple expectedQuarter = new Tuple(-1, 0, 0, 1);
        Assert.That(transformHalfQuarter * p, Is.EqualTo(expectedHalfQuarter));
        Assert.That(transformQuarter * p, Is.EqualTo(expectedQuarter));
    }

    [TestCaseSource(nameof(ShearCases))]
    public void ShearingPoint(double[] shear, Tuple expected){
        Matrix transform = Transformation.Shear(shear[0], shear[1], shear[2], shear[3], shear[4], shear[5]);
        Tuple p = new Tuple(2, 3, 4, 1);
        Assert.That(transform * p, Is.EqualTo(expected));
    }

    [Test]
    public void ComprehensiveTransform(){
        Tuple p = new Tuple(1, 0, 1, 1);
        Matrix A = Transformation.RotationX(Math.PI / 2);
        Matrix B = Transformation.Scaling(5, 5, 5);
        Matrix C = Transformation.Translation(10, 5, 7);

        Tuple p2 = A * p;
        Tuple expectedP2 = new Tuple(1, -1, 0, 1);

        Tuple p3 = B * p2;
        Tuple expectedP3 = new Tuple(5, -5, 0, 1);

        Tuple p4 = C * p3;
        Tuple expectedP4 = new Tuple(15, 0, 7, 1);

        Assert.That(p2, Is.EqualTo(expectedP2));
        Assert.That(p3, Is.EqualTo(expectedP3));
        Assert.That(p4, Is.EqualTo(expectedP4));
    }

    [Test]
    public void ComprehensiveInverseTransform(){
        Tuple p = new Tuple(1, 0, 1, 1);
        Matrix A = Transformation.RotationX(Math.PI / 2);
        Matrix B = Transformation.Scaling(5, 5, 5);
        Matrix C = Transformation.Translation(10, 5, 7);
        Matrix T = C * B * A;

        Tuple expected = new Tuple(15, 0, 7, 1);

        Assert.That(T * p, Is.EqualTo(expected));
    }
}