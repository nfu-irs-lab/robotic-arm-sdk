using System;
using NUnit.Framework;

namespace NFUIRSL.HRTK.Vision.UnitTests
{
    [TestFixture]
    public class VisionPositioningUnitTests
    {
        [Test]
        public void ImageToArm_InputRealData_OutputRealData()
        {
            // Arrange.
            var vp = MakeVisionPositioning();
            var imagePoint = new double[] { 217, 198 };
            var exp = new double[] { 0, 0 };
            var allowableError = 1;

            // Act.
            var actual = vp.ImageToArm(imagePoint[0], imagePoint[1]);

            // Assert.
            var errorX = Math.Abs(actual[0] - exp[0]);
            var errorY = Math.Abs(actual[1] - exp[1]);
            Assert.IsTrue(errorX < allowableError);
            Assert.IsTrue(errorY < allowableError);
        }

        private VisionPositioning MakeVisionPositioning()
        {
            return new VisionPositioning(_intrinsicMatrix, _armTranslationVector);
        }

        private static readonly double[,] _intrinsicMatrix =
        {
            { 5098.50139522781, 0, 0 },
            { -5.81726849512637, 5097.97821577839, 0 },
            { 1536.59830718425, 1123.13936955248, 1 }
        };

        private static readonly double[] _armTranslationVector =
            { 0, 0, 0 };
    }
}