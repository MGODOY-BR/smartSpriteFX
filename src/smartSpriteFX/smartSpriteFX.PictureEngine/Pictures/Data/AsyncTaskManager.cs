using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.Data
{
    /// <summary>
    /// Controls the asynchronous tasks state
    /// </summary>
    internal class AsyncTaskManager
    {
        /// <summary>
        /// It´s a list of recent async Insert/Update asynchronous task list
        /// </summary>
        private List<Task> _taskList = new List<Task>();

        /// <summary>
        /// Register a list of task
        /// </summary>
        /// <param name="task"></param>
        public void Register(Task task)
        {
            _taskList.Add(task);
        }

        /// <summary>
        /// Clears the insert/update task
        /// </summary>
        public void Clear()
        {
            _taskList.Clear();
        }

        /// <summary>
        /// Threads the exceptions happened
        /// </summary>
        public void ThreatException()
        {
            Exception exception = null;
            foreach (var taskItem in _taskList)
            {
                exception = taskItem.Exception;
                if(exception != null)
                {
                    break;
                }

                while (!taskItem.IsCompleted && !taskItem.IsFaulted)
                {
                    Thread.Sleep(10);
                }
            }

            this.Clear();
            if (exception != null)
            {
                throw exception;
            }
        }
    }
}
