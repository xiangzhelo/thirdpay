namespace viviLib.ScheduledTask
{
    using System;

    /// <summary>
    /// ScheduledTaskExecuteBase 的摘要说明。
    /// </summary>
    public abstract class ScheduledTaskExecuteBase : IScheduledTaskExecute
    {
        /// <summary>
        /// 执行。
        /// </summary>
        public void Execute()
        {
            DateTime now = DateTime.Now;
            this.ExecuteTask();
            ScheduledTaskLog.WriteExecuteLog(base.GetType(), now, DateTime.Now);
        }

        /// <summary>
        /// 执行任务。
        /// </summary>
        protected abstract void ExecuteTask();
    }
}

