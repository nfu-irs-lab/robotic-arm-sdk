using System;
using NUnit.Framework;

namespace NFUIRSL.HRTK.Vision.UnitTests
{
    // [TestFixture]
    // public class VisionPositioningUnitTests
    // {
    //     // [TestCase(217,198,-120.299, 520.597)]
    //     [TestCase(2869, 1863, 102.524, 386.361)]
    //     // [TestCase(0,0,0, 0)]
    //     public void ImageToArm_InputRealData_OutputRealData(int pixX, int pixY, double armX, double armY)
    //     {
    //         // Arrange.
    //         var vp = MakeVisionPositioning();
    //         var exp = new[] { armX, armY };
    //         var allowableError = 1;
    //
    //         // Act.
    //         var actual = vp.ImageToArm(pixX, pixY);
    //
    //         // Assert.
    //         var errorX = Math.Abs(actual[0] - exp[0]);
    //         var errorY = Math.Abs(actual[1] - exp[1]);
    //         Assert.IsTrue(errorX < allowableError);
    //         Assert.IsTrue(errorY < allowableError);
    //     }
    //
    //     private VisionPositioning MakeVisionPositioning()
    //     {
    //         return new VisionPositioning(_intrinsicMatrix, _armTranslationVector);
    //     }
    //
    //     private static readonly double[,] _intrinsicMatrix =
    //     {
    //         { 5098.50139522781, 0, 0 },
    //         { -5.81726849512637, 5097.97821577839, 0 },
    //         { 1536.59830718425, 1123.13936955248, 1 }
    //     };
    //
    //     // XXX
    //     private static readonly double[] _armTranslationVector =
    //         // { -120.299, -520.597 , 0 };
    //         // { 120.299, -520.597 , 0 };
    //         // { 0, -520.597 ,  -120.299};
    //         { 0, -520.597, 120.299 };
    // }
}