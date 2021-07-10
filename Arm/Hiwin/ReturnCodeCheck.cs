using System.Runtime.CompilerServices;

namespace Arm.Hiwin
{
    public static class ReturnCodeCheck
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSuccessful(int code, int ignoreCode = 0, int successCode = 0)
        {
            return (code == ignoreCode) || (code == successCode);
        }
    }
}