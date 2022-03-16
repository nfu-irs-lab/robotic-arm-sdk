using System.Runtime.CompilerServices;
using RASDK.Basic;
using RASDK.Basic.Message;

namespace RASDK.Arm.Hiwin
{
    internal static class ReturnCodeCheck
    {
        /// <summary>
        /// 檢查指令是否成果。
        /// </summary>
        /// <param name="code">執行指令回傳的代碼。</param>
        /// <param name="message">訊息處理器。</param>
        /// <param name="ignoreCode">要忽略的錯誤代碼。</param>
        /// <param name="successCode">代表成功的代碼。</param>
        /// <returns></returns>
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