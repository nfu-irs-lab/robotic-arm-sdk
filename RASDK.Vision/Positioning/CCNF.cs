using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace RASDK.Vision.Positioning
{
    /// <summary>
    /// RASDK.Vision positioning by Camera Calibration with Negative Feedback.<br/>
    /// 負回授相機標定視覺定位法。
    /// </summary>
    public class CCNF : IVisionPositioning
    {
        /// <summary>
        /// 誤差逼近算法。
        /// </summary>
        public delegate void Approximation(double errorX,
                                           double errorY,
                                           ref double valueX,
                                           ref double valueY);

        /// <summary>
        /// 座標轉換算法。
        /// </summary>
        public delegate void TransferFunctionOfVirtualCheckBoardToArm(double vX,
                                                                      double vY,
                                                                      out double armX,
                                                                      out double armY);

        private Approximation _approximation;
        private TransferFunctionOfVirtualCheckBoardToArm _transferFunctionOfVirtualCheckBoardToArm;

        private readonly CameraParameter _cameraParameter;
        private double _allowableError;

        public double AllowableError
        {
            get => _allowableError;
            set => _allowableError = value;
        }

        /// <summary>
        /// RASDK.Vision positioning by Camera Calibration with Negative Feedback.<br/>
        /// 負回授相機標定視覺定位法。
        /// </summary>
        /// <remarks>
        /// 此方法的運作方式爲先給定一個預測虛擬定位板座標作，將其透過相機標定法來算出對應的預測像素座標。<br/>
        /// 如果預測像素座標與實際像素座標的差距大於容許誤差，就調整預測虛擬定位板座標，再重複上述步驟。<br/>
        /// 如果預測像素座標與實際像素座標的差距小於等於容許誤差，就視目前的預測虛擬定位板座標爲正確的，再將其透過一組變換來轉換成手臂座標。<br/>
        /// 虛擬定位板座標是一個以相機成像平面投影到實物平面的假想平面座標系。其原點在鏡頭中心，也就是主點的投影位置。
        /// </remarks>
        public CCNF(CameraParameter cameraParameter,
                    TransferFunctionOfVirtualCheckBoardToArm tf,
                    Approximation approximation = null)
        {
            _allowableError = 3;
            _cameraParameter = cameraParameter;

            _transferFunctionOfVirtualCheckBoardToArm = tf ?? BasicTransferFunctionOfVirtualCheckBoardToArm;
            _approximation = approximation ?? BasicApproximation;
        }

        public void ImageToArm(int pixelX, int pixelY, out double armX, out double armY)
        {
            // 給定一個預測虛擬定位板座標。
            double virtualCheckBoardX = 0;
            double virtualCheckBoardY = 0;

            while (true)
            {
                // 將預測虛擬定位板座標透過相機標定法來算出對應的預測像素座標。
                var forecastPixel = CvInvoke.ProjectPoints(
                    new[] { new MCvPoint3D32f((float)virtualCheckBoardX, (float)virtualCheckBoardY, 0) },
                    new VectorOfDouble(_cameraParameter.RotationVectors),
                    new VectorOfDouble(_cameraParameter.TranslationVectors),
                    new Matrix<double>(_cameraParameter.IntrinsicMatrix),
                    new VectorOfDouble(_cameraParameter.DistortionCoefficients));

                // 計算預測像素座標與實際像素座標的差距。
                double errorX = pixelX - forecastPixel[0].X;
                double errorY = pixelY - forecastPixel[0].Y;

                // 判定差距是否大於容許誤差。
                if (Math.Abs(errorX) > _allowableError || Math.Abs(errorY) > _allowableError)
                {
                    // 差距大於容許誤差，調整預測虛擬定位板座標。
                    _approximation(errorX, errorY, ref virtualCheckBoardX, ref virtualCheckBoardY);
                }
                else
                {
                    // 差距小於等於容許誤差，跳出無限迴圈。
                    break;
                }
            }

            // 將虛擬定位板座標轉換成手臂座標。
            _transferFunctionOfVirtualCheckBoardToArm(virtualCheckBoardX,
                                                      virtualCheckBoardY,
                                                      out armX,
                                                      out armY);
        }

        private void BasicTransferFunctionOfVirtualCheckBoardToArm(double vX, double vY, out double armX, out double armY)
        {
            armX = vX;
            armY = vY;
        }

        private void BasicApproximation(double errorX, double errorY, ref double vX, ref double vY)
        {
            if (errorX > 0)
                vX++;
            else if (errorX < 0)
                vX--;

            if (errorY > 0)
                vY++;
            else if (errorY < 0)
                vY--;
        }
    }
}