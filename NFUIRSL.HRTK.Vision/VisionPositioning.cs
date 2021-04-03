using System;
using MathNet.Numerics.LinearAlgebra;

namespace NFUIRSL.HRTK.Vision
{
    public class VisionPositioning
    {
        private readonly Matrix<double> _armRotationMatrix;
        private readonly Vector<double> _armTranslationVector;
        private readonly Matrix<double> _cameraIntrinsicMatrix;
        private readonly Matrix<double> _cameraRotationMatrix;
        private readonly Vector<double> _cameraTranslationVector;

        public VisionPositioning(double[,] cameraIntrinsicMatrix,
                                 double[] armTranslationVector = null,
                                 double[,] armRotationMatrix = null,
                                 double[] cameraTranslationVector = null,
                                 double[,] cameraRotationMatrix = null)
        {
            _cameraIntrinsicMatrix = Matrix<double>.Build.DenseOfArray(cameraIntrinsicMatrix);

            _armTranslationVector = armTranslationVector == null
                ? Vector<double>.Build.Dense(3, 0)
                : Vector<double>.Build.DenseOfArray(armTranslationVector);

            _armRotationMatrix = armRotationMatrix == null
                ? Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } })
                : Matrix<double>.Build.DenseOfArray(armRotationMatrix);

            _cameraTranslationVector = armTranslationVector == null
                ? Vector<double>.Build.Dense(3, 0)
                : Vector<double>.Build.DenseOfArray(cameraTranslationVector);

            _cameraRotationMatrix = armRotationMatrix == null
                ? Matrix<double>.Build.DenseOfArray(new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } })
                : Matrix<double>.Build.DenseOfArray(cameraRotationMatrix);
        }

        public double[] ImageToArm(double pixelX, double pixelY)
        {
            var imagePoint = Matrix<double>.Build.DenseOfRowArrays(new[] { pixelX, pixelY, 1 });

            var transformation = _armRotationMatrix.SubMatrix(0, 2, 0, 3);
            transformation = transformation.InsertRow(2, _armTranslationVector);
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