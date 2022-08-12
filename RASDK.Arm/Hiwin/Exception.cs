namespace RASDK.Arm.Hiwin
{
    public class HiwinRobotControlException : System.Exception
    {
        public HiwinRobotControlException() : base()
        { }

        public HiwinRobotControlException(string message) : base(message)
        { }

        public HiwinRobotControlException(int code) : base($"HIWIN robot error code: {code}.")
        { }

        public HiwinRobotControlException(int code, string message)
            : base($"HIWIN robot error code: {code:0000}. {message}.")
        { }

        public HiwinRobotControlException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}