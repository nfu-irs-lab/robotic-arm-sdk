using Basic;
using RASDK.Arm.Action;
using RASDK.Arm.Type;

namespace RASDK.Arm
{
    /// <summary>
    /// 機械手臂控制介面。
    /// </summary>
    public interface IArm : IDevice
    {
        /// <summary>
        /// 整體加速度比例。<br/>
        /// 正常數值爲 1 ~ 100，-1 代表取得數值時出錯。
        /// </summary>
        int Acceleration { get; set; }

        /// <summary>
        /// 手臂ID。
        /// </summary>
        int Id { get; }

        /// <summary>
        /// 手臂IP。
        /// </summary>
        string Ip { get; }

        /// <summary>
        /// 整體速度比例。<br/>
        /// 正常數值爲 1 ~ 100，-1 代表取得數值時出錯。
        /// </summary>
        int Speed { get; set; }

        /// <summary>
        /// 進行手臂動作。
        /// </summary>
        /// <param name="armAction"></param>
        void Do(IArmAction armAction);

        /// <summary>
        /// 取得手臂目前的位置座標數值。<br/>
        /// 須選擇是笛卡爾還是關節座標。預設爲笛卡爾座標。
        /// </summary>
        /// <param name="coordinateType"></param>
        /// <returns>目前的手臂位置座標數值。</returns>
        double[] GetPosition(CoordinateType coordinateType = CoordinateType.Descartes);
    }
}