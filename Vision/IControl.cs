using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vision
{
    public class IControl : UserControl
    {
        protected uEye.Camera m_Camera;

        public IControl()
        {
        }

        public IControl(uEye.Camera camera)
        {
            m_Camera = camera;
        }

        public virtual void OnControlFocusActive()
        {
        }

        public virtual void OnControlFocusLost()
        {
        }

        public void SetCameraObject(uEye.Camera camera)
        {
            m_Camera = camera;
        }
    }
}