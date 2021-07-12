using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASDK.Vision.Positioning
{
    /// <summary>
    /// RASDK.Vision positioning interface.
    /// </summary>
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
}