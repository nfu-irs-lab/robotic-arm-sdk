using MathNet.Numerics.LinearAlgebra;

namespace NFUIRSL.HRTK.Vision
{
    public class VisionPositioning
    {
        private Matrix<double> _armRotationMatrix;
        private Vector<double> _armTranslationVextor;
        private Matrix<double> _cameraIntrinsicMatrix;
        private Matrix<double> _cameraRotationMatrix;
        private Vector<double> _cameraTranslationVextor;

        public double[] ImageToArm(double pixelX, double pixelY)
        {
            var imagePoint = Matrix<double>.Build.DenseOfRowArrays(new[] { pixelX, pixelY, 1 });

            var transformation = _armRotationMatrix.SubMatrix(0, 2, 0, 3);
            transformation = transformation.InsertRow(2, _armTranslationVextor);
            transformation = transformation.Multiply(_cameraIntrinsicMatrix);
            var result = imagePoint.Multiply(transformation.Inverse());

            return new[]
            {
                (result[0, 0] / result[0, 2]),
                (result[0, 1] / result[0, 2])
            };
        }
    }
}