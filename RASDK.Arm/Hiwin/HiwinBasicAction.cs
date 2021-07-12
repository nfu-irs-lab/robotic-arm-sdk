using Basic.Message;

namespace RASDK.Arm.Hiwin
{
    public abstract class HiwinBasicAction
    {
        protected int _id;
        protected readonly IMessage _message;

        public HiwinBasicAction(int id, IMessage message)
        {
            _id = id;
            _message = message;
        }
    }
}