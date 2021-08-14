﻿using System.Runtime.CompilerServices;
using RASDK.Basic;
using RASDK.Basic.Message;

namespace RASDK.Arm.Hiwin
{
    internal static class ReturnCodeCheck
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSuccessful(int code, IMessage message = null, int ignoreCode = 0, int successCode = 0)
        {
            bool successful;
            if ((code == ignoreCode) || (code == successCode))
            {
                successful = true;
            }
            else
            {
                successful = false;
                message?.Show($"上銀手臂錯誤。\r\n錯誤代碼：{code}", LoggingLevel.Error);
            }
            return successful;
        }
    }
}