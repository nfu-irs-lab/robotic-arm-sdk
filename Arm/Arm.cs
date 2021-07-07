using Basic.Message;

namespace Arm
{
    public class Arm
    {
        public Arm()
        {
            ArmActionFactory armActionFactory = new HiwinArmActionFactory("127.0.0.1", new EmptyMessage());
            armActionFactory.GetConnect();
        }
    }
}