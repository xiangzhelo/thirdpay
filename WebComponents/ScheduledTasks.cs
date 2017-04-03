using System;
using System.Collections.Generic;
using viviLib.ExceptionHandling;
using viviLib.ScheduledTask;

namespace viviapi.WebComponents.ScheduledTask
{
    /// <summary>
    /// ִ�мƻ������ࡣ
    /// </summary>
    public class ScheduledTasks
    {
        private viviLib.ScheduledTask.ScheduledTask[] _scheduledTasks;
        /// <summary>
        /// �Ƿ��������С�
        /// </summary>
        public static bool TaskExecuting = false;

        /// <summary>
        /// ִ�мƻ�����
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
        /// ֹͣ�ƻ������ִ�С�
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

