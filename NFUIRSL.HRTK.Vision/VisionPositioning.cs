using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace NFUIRSL.HRTK.Vision
{
    public interface IVisionPositioning
    {
        /// <summary>
        /// Get point of arm by pixel of image.
        /// </summary>
        /// <param name="pixelX"></param>
        /// <param name="pixelY"></param>
        /// <param name="armX"></param>
        /// <param name="armY"></param>
        void ImageToArm(int pixelX, int pixelY, out double armX, out double armY);
    }

    /// <summary>
    /// Digit-by-digit calculation by Checking Forecast Result with Camera Calibration.<br/>
    /// 相機標定驗算預測結果逼近算法。
    /// </summary>
    public class DCFRCC : IVisionPositioning
    {
        private readonly double _allowableError;
        private readonly CameraParameter _cameraParameter;

        /// <summary>
        /// Digit-by-digit calculation by Checking Forecast Result with Camera Calibration.<br/>
        /// 相機標定驗算預測結果逼近算法。
        /// </summary>
        public DCFRCC(CameraParameter cameraParameter, double allowableError)
        {
            _cameraParameter = cameraParameter;
            _allowableError = allowableError;
        }

        public void ImageToArm(int pixelX, int pixelY, out double armX, out double armY)
        {
            double forecastArmX = 0;
            double forecastArmY = 0;

            while (true)
            {
                var forecastPixel = CvInvoke.
                    ProjectPoints(new[] { new MCvPoint3D32f((float)forecastArmX, (float)forecastArmY, 0) },
                                  new VectorOfDouble(_cameraParameter.RotationVectors),
                                  new VectorOfDouble(_cameraParameter.TranslationVectors),
                                  new Emgu.CV.Matrix<double>(_cameraParameter.IntrinsicMatrix),
                                  new VectorOfDouble(_cameraParameter.DistortionCoefficients));

                double errorX = pixelX - forecastPixel[0].X;
                double errorY = pixelY - forecastPixel[0].Y;

                if (Math.Abs(errorX) > _allowableError || Math.Abs(errorY) > _allowableError)
                {
                    CalOffset(errorX, errorY, ref forecastArmX, ref forecastArmY);
                }
                else
                {
                    break;
                }
            }

            armX = forecastArmX;
            armY = forecastArmY;
        }

        private void CalOffset(double errorX, double errorY, ref double armX, ref double armY)
        {
            if (errorX > 0)
                armX++;
            else if (errorX < 0)
                armX--;

            if (errorY > 0)
                armY++;
            else if (errorY < 0)
                armY--;
        }
    }

    // public class VisionPositioning
    // {
    //     private readonly Matrix<double> _armRotationMatrix;
    //     private readonly Vector<double> _armTranslationVector;
    //     private readonly Matrix<double> _cameraIntrinsicMatrix;
    //     private readonly Matrix<double> _cameraRotationMatrix;
    //     private readonly Vector<double> _cameraTranslationVector;
    //
    //     public VisionPositioning(double[,] cameraIntrinsicMatrix,
    //                              double[] armTranslationVector = null,
    //                              double[,] armRotationMatrix = null,
    //                              double[] cameraTranslationVector = null,
    //                              double[,] cameraRotationMatrix = null)
    //     {
    //         // XXX
    //         _cameraIntrinsicMatrix = Matrix<double>.Build.DenseOfArray(cameraIntrinsicMatrix);
    //         // _cameraIntrinsicMatrix = _cameraIntrinsicMatrix.Transpose();
    //
    //         _armTranslationVector = armTranslationVector == null
    //             ? Vector<double>.Build.Dense(3, 0)
    //             : Vector<double>.Build.DenseOfArray(armTranslationVector);
    //
    //         // XXX
    //         _armRotationMatrix = armRotationMatrix == null
    //             // ? Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } })
    //             ? Matrix<double>.Build.DenseOfArray(new double[,] { { 0, 0, 1 }, { 0, 1, 0 }, { 1, 0, 0 } })
    //             : Matrix<double>.Build.DenseOfArray(armRotationMatrix);
    //
    //         _cameraTranslationVector = cameraTranslationVector == null
    //             ? Vector<double>.Build.Dense(3, 0)
    //             : Vector<double>.Build.DenseOfArray(cameraTranslationVector);
    //
    //         _cameraRotationMatrix = cameraRotationMatrix == null
    //             ? Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } })
    //             : Matrix<double>.Build.DenseOfArray(cameraRotationMatrix);
    //     }
    //
    //     public double[] ImageToArm(double pixelX, double pixelY)
    //     {
    //         // var imagePoint = Matrix<double>.Build.DenseOfRowArrays(new[] { pixelX, pixelY, 1 });
    //         var imagePoint = Matrix<double>.Build.DenseOfRowArrays(new[] { 1, pixelY, pixelX });
    //
    //         var transformation = _armRotationMatrix.SubMatrix(0, 2, 0, 3);
    //         transformation = transformation.InsertRow(2, _armTranslationVector);
    //         transformation = transformation.Multiply(_cameraIntrinsicMatrix);
    //         var result = imagePoint.Multiply(transformation.Inverse());
    //
    //         return new[]
    //         {
    //             (result[0, 0] / result[0, 2]),
    //             (result[0, 1] / result[0, 2])
    //         };
    //     }
    // }
}