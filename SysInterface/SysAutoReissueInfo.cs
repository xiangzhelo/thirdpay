using System.Threading;

namespace viviapi.SysInterface
{
    /// <summary>
    /// 网银自动补发(共5次)
    /// 5s
    /// 10s
    /// 60s
    /// 10分
    /// 30分
    /// </summary>
    public class SysAutoReissueInfo
    {
        public Timer TimerReissue;

        public SysAutoReissueInfo()
        {
            InputCharset = "GB2312";
            NotifiedTimes = 1;
        }

        public string InterfaceVersion { get; set; }
        public string InputCharset { get; set; }
        public string OrderId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte OrderType { get; set; }
        /// <summary>
        /// 已执行次数
        /// </summary>
        public int NotifiedTimes { get; set; }
        public string NotifyUrl { get; set; }
    }
}
