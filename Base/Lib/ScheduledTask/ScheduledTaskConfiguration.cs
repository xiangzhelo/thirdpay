using System;
using System.Collections.Generic;

namespace viviLib.ScheduledTask
{
    /// <summary>
    /// ScheduledTaskConfiguration 的摘要说明。
    /// </summary>
    [Serializable]
    public class ScheduledTaskConfiguration
    {
        private List<String> _excutes = new List<String>();
        private string _scheduleTaskType = string.Empty;
        private int _threadSleepSecond = 60;

        /// <summary>
        /// 执行类。
        /// </summary>
        public List<String> Executes
        {
            get
            {
                return this._excutes;
            }
        }

        /// <summary>
        /// 任务类型。
        /// </summary>
        public string ScheduledTaskType
        {
            get
            {
                return this._scheduleTaskType;
            }
            set
            {
                this._scheduleTaskType = value;
            }
        }

        /// <summary>
        /// 每次执行后线程停止的睡眠的时间。
        /// </summary>
        public int ThreadSleepSecond
        {
            get
            {
                return this._threadSleepSecond;
            }
            set
            {
                this._threadSleepSecond = value;
            }
        }
    }
}

