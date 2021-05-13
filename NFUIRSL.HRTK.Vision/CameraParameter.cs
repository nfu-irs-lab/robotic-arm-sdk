using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFUIRSL.HRTK.Vision
{
    public class CameraParameter
    {
        /// <summary>
        /// X of principle point.
        /// </summary>
        public double Cx;

        /// <summary>
        /// Y of principle point.
        /// </summary>
        public double Cy;

        /// <summary>
        /// X of focal length.
        /// </summary>
        public double Fx;

        /// <summary>
        /// Y of focal length.
        /// </summary>
        public double Fy;

        /// <summary>
        /// Skew parameter.
        /// </summary>
        public double Skew;

        /// <summary>
        /// Radial Distortion.
        /// </summary>
        public double K1 = 0;

        /// <summary>
        /// Radial Distortion.
        /// </summary>
        public double K2 = 0;

        /// <summary>
        /// Radial Distortion.
        /// </summary>
        public double K3 = 0;

        /// <summary>
        /// Tangential Distortion.
        /// </summary>
        public double P1 = 0;

        /// <summary>
        /// Tangential Distortion.
        /// </summary>
        public double P2 = 0;

        public double[] RotationVectors = new double[3];
        public double[] TranslationVectors = new double[3];

        /// <summary>
        /// The upper triangular 3*3 matrix of camera intrinsic parameter.
        /// </summary>
        public double[,] IntrinsicMatrix
        {
            get
            {
                return new double[,]
                {
                    { Fx, Skew, Cx },
                    { 0, Fy, Cy },
                    { 0, 0, 1 },
                };
            }

            set
            {
                Fx = value[0, 0];
                Skew = value[0, 1];
                Cx = value[0, 2];
                Fy = value[1, 1];
                Cy = value[1, 2];
            }
        }

        public double[] DistortionCoefficients
        {
            get { return new double[] { K1, K2, P1, P2 }; }
        }

        public CameraParameter(double cx,
                               double cy,
                               double fx,
                               double fy,
                               double skew,
                               double[] rotationVectors,
                               double[] translationVectors)
        {
            Cx = cx;
            Cy = cy;
            Fx = fx;
            Fy = fy;
            Skew = skew;
            Array.Copy(rotationVectors, RotationVectors, rotationVectors.Length);
            Array.Copy(translationVectors, TranslationVectors, translationVectors.Length);
        }
    }
}