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
- [RASDK.Gripper](/RASDK.Gripper)：夾爪函式庫。
- [RASDK.UI](/RASDK.UI)：使用者介面函式庫。
- [RASDK.Vision](/RASDK.Vision)：影像及視覺相關函式庫。
