using RASDK.Basic.Message;

namespace RASDK.Arm.Hiwin
{
    public abstract class BasicAction
    {
        protected int _id;
        protected readonly IMessage _message;

        public BasicAction(int id, IMessage message)
        {
            _id = id;
            _message = message;
        }
    }
}