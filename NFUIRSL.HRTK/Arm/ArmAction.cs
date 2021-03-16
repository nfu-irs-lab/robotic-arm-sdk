namespace NFUIRSL.HRTK
{
    public interface IArmAction
    {
        string Message { get; }
    }

    public abstract class ArmMotion : IArmAction
    {
        public string Message { get; }
    }
}