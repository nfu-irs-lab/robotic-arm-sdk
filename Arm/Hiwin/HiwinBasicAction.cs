using Basic.Message;

namespace Arm.Hiwin
{
    public abstract class HiwinBasicAction
    {
        protected int _id;
        protected readonly IMessage _message;
        
        public abstract int Do();
        
        public HiwinBasicAction(int id, IMessage message)
        {
            _id = id;
            _message = message;
        }
    }
}