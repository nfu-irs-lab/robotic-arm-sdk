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
        private IMessage Message;
        private const int CnNumberOfSeqBuffers = 3;
        private PictureBox PictureBox;
        private Timer UpdateTimer;

        public Boolean IsLive { get; private set; }
        public uEye.Defines.DisplayRenderMode RenderMode { get; private set; }
        public Int32 FrameCount { get; private set; }
        public double Fps { get; private set; }
        public string Failed { get; private set; }
        public string SensorName { get; private set; }


        public IDSCamera(PictureBox pictureBox, IMessage message)
        {
            Message = message;
            
            PictureBox = pictureBox;
            PictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

            Camera = new uEye.Camera();
            IsLive = false;
            RenderMode = uEye.Defines.DisplayRenderMode.FitToWindow;

            UpdateTimer = new Timer();
            UpdateTimer.Interval = 100;
            UpdateTimer.Tick += UpdateControls;
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
        {
            var camera = sender as uEye.Camera;
            if (camera.IsOpened)
            {
                uEye.Defines.DisplayMode mode;
                camera.Display.Mode.Get(out mode);

                // Only display in DiB mode.
                if (mode == uEye.Defines.DisplayMode.DiB)
                {
                    Int32 memId;
                    var status = camera.Memory.GetLast(out memId);
                    if ((status == uEye.Defines.Status.SUCCESS) && 0 < memId)
                    {
                        if (camera.Memory.Lock(memId) == uEye.Defines.Status.SUCCESS)
                        {
                            camera.Display.Render(memId, RenderMode);
                            camera.Memory.Unlock(memId);
                        }
                    }
                }
                ++FrameCount;
            }
        }

        private void UpdateControls(object sender, EventArgs e)
        {
            Camera.Timing.Framerate.GetCurrentFps(out var frameRate);
            Fps = frameRate;

            Camera.Information.GetCaptureStatus(out var captureStatus);
            if (null != captureStatus)
            {
                Failed = "" + captureStatus.Total;
            }
        }
    }
}