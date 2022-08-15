namespace RASDK.Arm.Hiwin
{
    /// <summary>
    /// HIWIN 機器人控制例外錯誤。
    /// </summary>
    public class HiwinRobotControlException : System.Exception
    {
        /// <summary>
        /// HIWIN 機器人控制例外錯誤。
        /// </summary>
        public HiwinRobotControlException() : base()
        { }

        /// <summary>
        /// HIWIN 機器人控制例外錯誤。
        /// </summary>
        public HiwinRobotControlException(string message) : base(message)
        { }

        /// <summary>
        /// HIWIN 機器人控制例外錯誤。
        /// </summary>
        public HiwinRobotControlException(int code) : base($"HIWIN robot error code: {code:0000}.")
        { ErrorCode = code; }

        /// <summary>
        /// HIWIN 機器人控制例外錯誤。
        /// </summary>
        public HiwinRobotControlException(int code, string message)
            : base($"HIWIN robot error code: {code:0000}. {message}.")
        { ErrorCode = code; }

        /// <summary>
        /// HIWIN 機器人控制例外錯誤。
        /// </summary>
        public HiwinRobotControlException(string message, System.Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// HIWIN 錯誤代碼。-99999 代表該例外錯誤沒有被指定錯誤代碼。
        /// </summary>
        public int ErrorCode { get; protected set; } = -99999;
    }
}