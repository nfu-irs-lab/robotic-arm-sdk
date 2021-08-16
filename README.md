# Robotic Arm SDK

[![GitHub release](https://img.shields.io/github/release/nfu-irs-lab/robotic-arm-sdk.svg)](https://github.com/nfu-irs-lab/robotic-arm-sdk/releases)
[![GitHub repo size](https://img.shields.io/github/repo-size/nfu-irs-lab/robotic-arm-sdk)](https://github.com/nfu-irs-lab/robotic-arm-sdk)
[![GitHub issues](https://img.shields.io/github/issues/nfu-irs-lab/robotic-arm-sdk.svg)](https://github.com/nfu-irs-lab/robotic-arm-sdk/issues)

機械手臂 SDK。目前主要支援：
- HIWIN 機械手臂
- HIWIN 電動夾爪
- TM Robot 機械手臂
- IDS 工業攝影機

支援的環境為 64-bit .NET Framework 4.7.2

> Forked from [HRTK 1.4.0](https://github.com/nfu-irs-lab/hiwinrobot-toolkit/releases/tag/v1.4.0)

# 各 Project 功能

- [RASDK.Arm](/RASDK.Arm)：機械手臂函式庫。
- [RASDK.Arm.TestForms](/RASDK.Arm.TestForms)：「RASDK.Arm」的測試用視窗程式。不會封裝進 NuGet。
- [RASDK.Basic](/RASDK.Basic)：基本功能函式庫。
- [RASDK.Basic.TestForms](/RASDK.Basic.TestForms)：「RASDK.Basic」的測試用視窗程式。不會封裝進 NuGet。
- [RASDK.Gripper](/RASDK.Gripper)：夾爪函式庫。
- [RASDK.UI](/RASDK.UI)：使用者介面函式庫。
- [RASDK.Vision](/RASDK.Vision)：影像及視覺相關函式庫。
- [RASDK.Vision.TestForms](/RASDK.Vision.TestForms)：「RASDK.Vision」的測試用視窗程式。不會封裝進 NuGet。

# 使用方法

## 安裝 NuGet Package

1. 先在 [releases](https://github.com/nfu-irs-lab/robotic-arm-sdk/releases) 頁面中下載 NuGet Package，其檔案名稱爲「NFUIRSL.RASDK.x.x.x.nupkg」。
2. 將「NFUIRSL.RASDK.x.x.x.nupkg」放到一個自己方便管理的路徑下。如：`C:\Users\MYUSERNAME\Documents\References`。
3. 將上述的路徑加入到 Visual Studio 中的套件來源。可參考[ Microsoft 官方說明 ](https://docs.microsoft.com/zh-tw/nuget/consume-packages/install-use-packages-visual-studio#package-sources)。
4. 在 Visual Studio 爲目標專案或解決方案增加 NuGet Package「NFUIRSL.RASDK」。可參考[ Microsoft 官方說明 ](https://docs.microsoft.com/zh-tw/nuget/consume-packages/install-use-packages-visual-studio)。

## 基本使用

```csharp
// 實體化。
ILogHandler logHandler = new RASDK.Basic.EmptyLog();
IMessage message = new RASDK.Basic.Message.GeneralMessage(logHandler);
var arm = new RASDK.Arm.Hiwin.RoboticArm("192.168.100.123", message); // 以 HIWIN 手臂爲例。

arm.Connection.Open();  // 連線。
arm.Connection.Close(); // 斷線。
var connected = arm.Connection.IsOped;  // 判斷連線。

arm.Motion.Homing();                           // 復歸，回原點。
arm.Motion.Absolute(20, 400, 350, 180, 0, 90); // 絕對運動。
arm.Motion.Relative(0, 15, 0, 0, 0, 0);        // 相對運動。
arm.Motion.Jog("+X");                          // 吋動。
arm.Motion.Abort();                            // 停止動作。

arm.Speed = 25;             // 設定速度。
arm.Acceleration = 20;      // 設定加速度。
var speed = arm.Speed;      // 取得速度。
var acc = arm.Acceleration; // 取得加速度。

var nowPosition = arm.GetNowPosition(); // 取得目前座標位置。
```
