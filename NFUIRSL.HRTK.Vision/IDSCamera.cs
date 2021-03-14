using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFUIRSL.HRTK.Vision
{
    public class IDSCamera
    {
        private uEye.Camera Camera;
        private IMessage Message = null;
        private const int CnNumberOfSeqBuffers = 3;
        private Boolean IsLive;
        private Int32 FrameCount;
        private Timer UpdateTimer = new Timer();
        private uEye.Defines.DisplayRenderMode RenderMode;
        private string SensorName;
        private PictureBox PictureBox;

        public IDSCamera(PictureBox pictureBox, IMessage message)
        {
            PictureBox = pictureBox;
            Message = message;
        }

        public uEye.Defines.Status Init(int deviceId)
        {
            uEye.Defines.Status status;
            var id = deviceId | (Int32)uEye.Defines.DeviceEnumeration.UseDeviceID;

            status = Camera.Init(id, PictureBox.Handle);
            if (status != uEye.Defines.Status.SUCCESS)
            {
                Message.Show("Initializing the camera failed");
                return status;
            }

            status = MemoryHelper.AllocImageMems(Camera, CnNumberOfSeqBuffers);
            if (status != uEye.Defines.Status.SUCCESS)
            {
                Message.Show("Allocating memory failed");
                return status;
            }

            status = MemoryHelper.InitSequence(Camera);
            if (status != uEye.Defines.Status.SUCCESS)
            {
                Message.Show("Add to sequence failed");
                return status;
            }

            Camera.EventFrame += FrameEvent;
            FrameCount = 0;
            UpdateTimer.Start();
            uEye.Types.SensorInfo sensorInfo;
            Camera.Information.GetSensorInfo(out sensorInfo);
            SensorName = sensorInfo.SensorName;
            PictureBox.SizeMode = PictureBoxSizeMode.Normal;
            return status;
        }

        private void FrameEvent(object sender, EventArgs e)
        { }
    }
}