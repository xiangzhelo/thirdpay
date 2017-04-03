using System;
using System.Collections.Generic;
using viviLib.ExceptionHandling;
using viviLib.ScheduledTask;

namespace viviapi.WebComponents.ScheduledTask
{
    /// <summary>
    /// 执行计划任务类。
    /// </summary>
    public class ScheduledTasks
    {
        private viviLib.ScheduledTask.ScheduledTask[] _scheduledTasks;
        /// <summary>
        /// 是否正在运行。
        /// </summary>
        public static bool TaskExecuting = false;

        /// <summary>
        /// 执行计划任务。
        /// </summary>
        public void Start()
        {
            List<viviLib.ScheduledTask.ScheduledTaskConfiguration> configs = ScheduledTaskConfigurationSectionHandler.GetConfigs();
            if (configs != null)
            {
                this._scheduledTasks = new viviLib.ScheduledTask.ScheduledTask[configs.Count];
                for (int i = 0; i < configs.Count; i++)
                {
                    try
                    {
                        var scheduledTaskType = configs[i].ScheduledTaskType;
                        if (scheduledTaskType != null)
                        {
// ReSharper disable once AssignNullToNotNullAttribute
                            var task = Activator.CreateInstance(Type.GetType(scheduledTaskType)) as viviLib.ScheduledTask.ScheduledTask;
                            if (task != null)
                            {
                                task.Execute(configs[i]);

                                this._scheduledTasks[i] = task;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        ExceptionHandler.HandleException(exception);
                    }
                }
            }
        }

        /// <summary>
        /// 停止计划任务的执行。
        /// </summary>
        public void Stop()
        {
            if (this._scheduledTasks != null)
            {
                for (int i = 0; i < this._scheduledTasks.Length; i++)
                {
                    if (this._scheduledTasks[i] != null)
                    {
                        this._scheduledTasks[i].Stop();
                    }
                }
            }
        }
    }
}

