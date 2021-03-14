using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFUIRSL.HRTK.Vision
{
    public enum CaptureMode
    {
        FreeRun,
        Stop,
        Snapshot
    }

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

        public bool AutoShutter
        {
            get
            {
                Camera.AutoFeatures.Software.Shutter.GetEnable(out bool enable);
                return enable;
            }

            set { Camera.AutoFeatures.Software.Shutter.SetEnable(value); }
        }

        public bool AutoWhiteBalance
        {
            get
            {
                Camera.AutoFeatures.Software.WhiteBalance.GetEnable(out bool enable);
                return enable;
            }

            set { Camera.AutoFeatures.Software.WhiteBalance.SetEnable(value); }
        }

        public bool AutoGain
        {
            get
            {
                Camera.AutoFeatures.Software.Gain.GetEnable(out bool enable);
                return enable;
            }

            set { Camera.AutoFeatures.Software.Gain.SetEnable(value); }
        }

        public IDSCamera(PictureBox pictureBox, IMessage message)
        {
            if (CheckRuntimeVersion())
            {
                Message = message;

                PictureBox = pictureBox;
                PictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

                DeviceId = 1;
                CameraId = 1;
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

        ~IDSCamera()
        {
            Exit();
        }

        public void Open(CaptureMode captureMode = CaptureMode.FreeRun, bool autoFeatures = true)
        {
            var status = Init();
            if (status == uEye.Defines.Status.SUCCESS)
            {
                status = ChangeCaptureMode(captureMode);
            }

            if (status != uEye.Defines.Status.SUCCESS && Camera.IsOpened)
            {
                Camera.Exit();
            }
            else
            {
                AutoGain = autoFeatures;
                AutoShutter = autoFeatures;
                AutoWhiteBalance = autoFeatures;
            }
        }

        public uEye.Defines.Status ChangeCaptureMode(CaptureMode captureMode)
        {
            Func<uEye.Defines.Status> func;
            bool expectIsLive;

            switch (captureMode)
            {
                case CaptureMode.FreeRun:
                    func = Camera.Acquisition.Capture;
                    expectIsLive = true;
                    break;

                case CaptureMode.Stop:
                    func = Camera.Acquisition.Stop;
                    expectIsLive = false;
                    break;

                case CaptureMode.Snapshot:
                    func = Camera.Acquisition.Freeze;
                    expectIsLive = false;
                    break;

                default:
                    return uEye.Defines.Status.NO_SUCCESS;
            }

            var status = func();
            if (status != uEye.Defines.Status.SUCCESS)
            {
                Message.Show("Starting live video failed", LoggingLevel.Error);
            }
            else
            {
                // Everything is ok.
                IsLive = expectIsLive;
            }
            return status;
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

        public void ShowSettingForm()
        {
            if (Camera != null)
            {
                var settingForm = new SettingsForm(Camera);
                settingForm.SizeControl.AOIChanged += OnDisplayChanged;
                settingForm.FormatControl.DisplayChanged += OnDisplayChanged;
                settingForm.ShowDialog();
            }
            else
            {
                Message.Show("Camera never initialization.", LoggingLevel.Warn);
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

        private uEye.Defines.Status Init()
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
        
        private void OnDisplayChanged(object sender, EventArgs e)
        {
            uEye.Defines.DisplayMode displayMode;
            Camera.Display.Mode.Get(out displayMode);

            // set scaling options
            if (displayMode != uEye.Defines.DisplayMode.DiB)
            {
                if (RenderMode == uEye.Defines.DisplayRenderMode.DownScale_1_2)
                {
                    RenderMode = uEye.Defines.DisplayRenderMode.Normal;

                    PictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                    // get image size
                    System.Drawing.Rectangle rect;
                    Camera.Size.AOI.Get(out rect);

                    PictureBox.Width = rect.Width;
                    PictureBox.Height = rect.Height;
                }
                else
                {
                    Camera.DirectRenderer.SetScaling(RenderMode == uEye.Defines.DisplayRenderMode.FitToWindow);
                }
            }
            else
            {
                if (RenderMode != uEye.Defines.DisplayRenderMode.FitToWindow)
                {
                    PictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                    // get image size
                    System.Drawing.Rectangle rect;
                    Camera.Size.AOI.Get(out rect);

                    if (RenderMode != uEye.Defines.DisplayRenderMode.Normal)
                    {

                        PictureBox.Width = rect.Width / 2;
                        PictureBox.Height = rect.Height / 2;
                    }
                    else
                    {
                        PictureBox.Width = rect.Width;
                        PictureBox.Height = rect.Height;
                    }
                }
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