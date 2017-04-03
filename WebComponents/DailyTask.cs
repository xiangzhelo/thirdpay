using System;
using System.Threading;
using viviLib.ExceptionHandling;
using viviLib.ScheduledTask;

namespace viviapi.WebComponents.ScheduledTask
{
    ///<summary>
    /// DailyTask 的摘要说明。
    /// </summary>
    public class DailyTask : viviLib.ScheduledTask.ScheduledTask
    {
        private DateTime lastExecuteTime = DateTime.MinValue;

        /// <summary>
        /// 执行计划任务。
        /// </summary>
        protected override void ScheduleCallback()
        {
            while (true)
            {
                if (this.lastExecuteTime.AddDays(1.0) < DateTime.Now)
                {
                    while (ScheduledTasks.TaskExecuting)
                    {
                        Thread.Sleep(1000);
                    }
                    try
                    {
                        ScheduledTasks.TaskExecuting = true;
                        base.ExecuteTask();
                        this.lastExecuteTime = DateTime.Today;
                        ScheduledTaskLog.WriteLog(base.Config);
                    }
                    catch (Exception exception)
                    {
                        ExceptionHandler.HandleException(exception);
                    }
                    finally
                    {
                        ScheduledTasks.TaskExecuting = false;
                    }
                }
                Thread.Sleep((int)(base.Config.ThreadSleepSecond * 1000));
            }
        }
    }
}

