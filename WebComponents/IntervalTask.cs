using System;
using System.Threading;
using viviLib.ExceptionHandling;
using viviLib.ScheduledTask;

namespace viviapi.WebComponents.ScheduledTask
{
  

    /// <summary>
    /// IntervalTask 的摘要说明。
    /// </summary>
    public class IntervalTask : viviLib.ScheduledTask.ScheduledTask
    {
        /// <summary>
        /// 执行计划任务。
        /// </summary>
        protected override void ScheduleCallback()
        {
            while (true)
            {
                while (ScheduledTasks.TaskExecuting)
                {
                    Thread.Sleep(1000);
                }
                try
                {
                    ScheduledTasks.TaskExecuting = true;
                    base.ExecuteTask();

                    ScheduledTaskLog.WriteLog(Config);
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
                finally
                {
                    ScheduledTasks.TaskExecuting = false;
                }
                Thread.Sleep((int)(Config.ThreadSleepSecond * 1000));
            }
        }
    }
}

