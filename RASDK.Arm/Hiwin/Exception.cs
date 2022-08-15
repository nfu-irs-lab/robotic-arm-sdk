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
        { }

        /// <summary>
        /// HIWIN 機器人控制例外錯誤。
        /// </summary>
        public HiwinRobotControlException(int code, string message)
            : base($"HIWIN robot error code: {code:0000}. {message}.")
        { }

        /// <summary>
        /// HIWIN 機器人控制例外錯誤。
        /// </summary>
        public HiwinRobotControlException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}