using System;
using System.Threading;
using viviLib.ExceptionHandling;
    
namespace viviLib.ScheduledTask
{
    /// <summary>
    /// ScheduledTask 的摘要说明。
    /// </summary>
    public abstract class ScheduledTask
    {
        private ScheduledTaskConfiguration _config;
        /// <summary>
        /// 执行线程。
        /// </summary>
        protected Thread ScheduleThread;

        /// <summary>
        /// 执行。
        /// </summary>
        /// <param name="config">配置。</param>
        public void Execute(ScheduledTaskConfiguration config)
        {
            this.Config = config;
            ThreadStart start = new ThreadStart(this.ScheduleCallback);
            this.ScheduleThread = new Thread(start);
            this.ScheduleThread.Start();
        }
       

        /// <summary>
        /// 执行计划任务。
        /// </summary>
        protected void ExecuteTask()
        {
            if (this.Config != null)
            {
                for (int i = 0; i < this.Config.Executes.Count; i++)
                {
                    try
                    {
// ReSharper disable once AssignNullToNotNullAttribute
                        ((IScheduledTaskExecute) Activator.CreateInstance(Type.GetType(this.Config.Executes[i]), true)).Execute();
                    }
                    catch (Exception exception)
                    {
                        ExceptionHandler.HandleException(exception);
                    }
                }
            }
        }

        /// <summary>
        /// 执行定时操作事件。
        /// </summary>
        protected abstract void ScheduleCallback();
        /// <summary>
        /// 停止。
        /// </summary>
        public void Stop()
        {
            if (this.ScheduleThread != null)
            {
                this.ScheduleThread.Abort();
            }
        }

        /// <summary>
        /// 配置信息。
        /// </summary>
        protected ScheduledTaskConfiguration Config
        {
            get
            {
                return this._config;
            }
            set
            {
                this._config = value;
            }
        }
    }
}

