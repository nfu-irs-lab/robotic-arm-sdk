# Robotic Arm SDK

[![dotnet](https://github.com/nfu-irs-lab/robotic-arm-sdk/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/nfu-irs-lab/robotic-arm-sdk/actions/workflows/dotnet.yml?query=branch%3Amain)
[![GitHub release](https://img.shields.io/github/release/nfu-irs-lab/robotic-arm-sdk.svg)](https://github.com/nfu-irs-lab/robotic-arm-sdk/releases)
[![GitHub issues](https://img.shields.io/github/issues/nfu-irs-lab/robotic-arm-sdk.svg)](https://github.com/nfu-irs-lab/robotic-arm-sdk/issues)

機械手臂 SDK。目前主要支援：
- HIWIN 機械手臂
- HIWIN 電動夾爪
- TM Robot 機械手臂
- CoppeliaSim 機械手臂（部分功能）

更多的功能在 [Extras RASDK](https://github.com/nfu-irs-lab/extras-robotic-arm-sdk)

> 支援的環境為 .NET Framework 4.7.2 (64-bit)  
> Forked from [HRTK 1.4.0](https://github.com/nfu-irs-lab/hiwinrobot-toolkit/releases/tag/v1.4.0)

# 各 Project 功能

- [RASDK.Arm](/RASDK.Arm)：機械手臂函式庫。
- [RASDK.Arm.TestForms](/RASDK.Arm.TestForms)：「RASDK.Arm」的測試用視窗程式。不會封裝進 NuGet。
- [RASDK.Basic](/RASDK.Basic)：基本功能函式庫。
- [RASDK.Basic.TestForms](/RASDK.Basic.TestForms)：「RASDK.Basic」的測試用視窗程式。不會封裝進 NuGet。
- [RASDK.Gripper](/RASDK.Gripper)：夾爪函式庫。

# 使用方法

## 安裝 NuGet Package

1. 先在 [releases](https://github.com/nfu-irs-lab/robotic-arm-sdk/releases) 頁面中下載 NuGet Package，其檔案名稱爲「NFUIRSL.RASDK.x.x.x.nupkg」。
2. 將「NFUIRSL.RASDK.x.x.x.nupkg」放到一個自己方便管理的路徑下。如：`C:\Users\MYUSERNAME\Documents\References`。
3. 將上述的路徑加入到 Visual Studio 中的套件來源。可參考[ Microsoft 官方說明 ](https://docs.microsoft.com/zh-tw/nuget/consume-packages/install-use-packages-visual-studio#package-sources)。
4. 在 Visual Studio 爲目標專案或解決方案增加 NuGet Package「NFUIRSL.RASDK」。可參考[ Microsoft 官方說明 ](https://docs.microsoft.com/zh-tw/nuget/consume-packages/install-use-packages-visual-studio)。

## 機械手臂（RASDK.Arm）

```csharp
// 實體化。
var logHandler = new RASDK.Basic.EmptyLog(); // EmptyLog()：不產生 Log 檔。
var messageHandler = new RASDK.Basic.Message.GeneralMessage(logHandler); // GeneralMessage()：一般的訊息處理器。
var arm = new RASDK.Arm.Hiwin.RoboticArm(messageHandler, "192.168.100.123"); // 以 HIWIN 手臂爲例。

arm.Connect();    // 連線。
arm.Disconnect(); // 斷線。
var connected = arm.Connected;  // 判斷連線。

arm.Homing();                                // 復歸，回原點。
arm.MoveAbsolute(-20, 400, 350, 180, 0, 90); // 絕對運動。
arm.MoveRelative(0, 15, -0.5, 0, 0, 0);      // 相對運動。
arm.Jog("+X");                               // 吋動。
arm.Abort();                                 // 停止動作。

arm.Speed = 25;             // 設定速度。
arm.Acceleration = 20;      // 設定加速度。
var speed = arm.Speed;      // 取得速度。
var acc = arm.Acceleration; // 取得加速度。

var position = arm.GetNowPosition(); // 取得目前座標位置。
```
