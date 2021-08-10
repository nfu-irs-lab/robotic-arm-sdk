namespace RASDK.Arm
{
    public interface IConnection
    {
        void Open();

        void Close();

        bool IsOpen { get; }
    }
}