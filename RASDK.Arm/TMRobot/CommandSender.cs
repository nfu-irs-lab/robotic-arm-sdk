using System.Text;
using AELTA_test;

namespace RASDK.Arm.TMRobot
{
    public class CommandSender
    {
        private readonly SocketClientObject _socketClientObject;

        public CommandSender(SocketClientObject socketClientObject)
        {
            _socketClientObject = socketClientObject;
        }

        public void Send(string command)
        {
            string s = string.Empty;
            s = SocketClientObject.DataToPacket("$TMSCT", command);
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            if (_socketClientObject != null)
            {
                _socketClientObject.WriteSyncData(bytes);
            }
        }
    }
}