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

        public int DeviceId { get; private set; }
        public int CameraId { get; private set; }
        public Boolean IsLive { get; private set; }
        public uEye.Defines.DisplayRenderMode RenderMode { get; private set; }
        public Int32 FrameCount { get; private set; }
        public double Fps { get; private set; }
        public string Failed { get; private set; }
        public string SensorName { get; private set; }


        public IDSCamera(PictureBox pictureBox, IMessage message)
        {
            if (CheckRuntimeVersion())
            {
                Message = message;

                PictureBox = pictureBox;
                PictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

                IsLive = false;
                RenderMode = uEye.Defines.DisplayRenderMode.FitToWindow;

                UpdateTimer = new Timer();
                UpdateTimer.Interval = 100;
                UpdateTimer.Tick += UpdateControls;
            }
            else
            {
                Message.Show(".NET Runtime Version 3.5.0 is required", LoggingLevel.Error);
            }
        }

        public void OpenFreeRun()
        {
            var status = Init();
            if (status == uEye.Defines.Status.SUCCESS)
            {
                // Start capture.
                status = Camera.Acquisition.Capture();
                if (status != uEye.Defines.Status.SUCCESS)
                {
                    Message.Show("Starting live video failed",LoggingLevel.Error);
                }
                else
                {
                    // Everything is ok.
                    IsLive = true;
                }
            }

            if (status != uEye.Defines.Status.SUCCESS && Camera.IsOpened)
            {
                Camera.Exit();
            }
        }
        
        public void StopFreeRun()
        {
            var status = Init();
            if (status == uEye.Defines.Status.SUCCESS)
            {
                // Start Freeze.
                status = Camera.Acquisition.Freeze();
                if (status != uEye.Defines.Status.SUCCESS)
                {
                    Message.Show("Starting live video failed",LoggingLevel.Error);
                }
                else
                {
                    // Everything is ok.
                    IsLive = false;
                }
            }

            if (status != uEye.Defines.Status.SUCCESS && Camera.IsOpened)
            {
                Camera.Exit();
            }
        }

        public void Exit()
        {
            UpdateTimer.Stop();
            IsLive = false;
            
            Camera.EventFrame -= FrameEvent;
            Camera.Exit();
            
            PictureBox.Invalidate();
            PictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            RenderMode = uEye.Defines.DisplayRenderMode.FitToWindow;
        }
        
        public void ChooseCamera()
        {
            var chooseForm = new CameraChoose();
            if (chooseForm.ShowDialog() == DialogResult.OK)
            {
                DeviceId = chooseForm.DeviceID;
                CameraId = chooseForm.CameraID;
            }
        }

        private bool CheckRuntimeVersion()
        {
            var versionMin = new Version(3, 5);
            var ok = false;

            foreach (Version version in InstalledDotNetVersions())
            {
                if (version >= versionMin)
                {
                    ok = true;
                    break;
                }
            }

            return ok;
        }

        private System.Collections.ObjectModel.Collection<Version> InstalledDotNetVersions()
        {
            var versions = new System.Collections.ObjectModel.Collection<Version>();
            var NDPKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
            if (NDPKey != null)
            {
                string[] subkeys = NDPKey.GetSubKeyNames();
                foreach (string subkey in subkeys)
                {
                    GetDotNetVersion(NDPKey.OpenSubKey(subkey), subkey, versions);
                    GetDotNetVersion(NDPKey.OpenSubKey(subkey).OpenSubKey("Client"), subkey, versions);
                    GetDotNetVersion(NDPKey.OpenSubKey(subkey).OpenSubKey("Full"), subkey, versions);
                }
            }
            return versions;
        }

        private void GetDotNetVersion(Microsoft.Win32.RegistryKey parentKey,
                                      string subVersionName,
                                      System.Collections.ObjectModel.Collection<Version> versions)
        {
            if (parentKey != null)
            {
                string installed = Convert.ToString(parentKey.GetValue("Install"));
                if (installed == "1")
                {
                    string version = Convert.ToString(parentKey.GetValue("Version"));
                    if (string.IsNullOrEmpty(version))
                    {
                        if (subVersionName.StartsWith("v"))
                            version = subVersionName.Substring(1);
                        else
                            version = subVersionName;
                    }

                    Version ver = new Version(version);

                    if (!versions.Contains(ver))
                        versions.Add(ver);
                }
            }
        }

        public uEye.Defines.Status Init()
        {
            if (Camera == null)
            {
                Camera = new uEye.Camera();
            }

            uEye.Defines.Status status;
            var id = DeviceId | (Int32)uEye.Defines.DeviceEnumeration.UseDeviceID;

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