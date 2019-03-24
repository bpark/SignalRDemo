using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SignalRDemo.Scheduler
{
    public class PeriodicHostedService : IHostedService, IDisposable
    {
        
        private readonly List<TaskWrapper> _scheduledTasks = new List<TaskWrapper>();

        public PeriodicHostedService(IEnumerable<IPeriodicTask> periodicTasks)
        {
            foreach (var periodicTask in periodicTasks)
            {
                _scheduledTasks.Add(new TaskWrapper()
                {
                    PeriodicTask = periodicTask
                });
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduledTasks.ForEach(task => task.Start());
            return Task.CompletedTask;
        }

        private void ExecuteTasks(object state)
        {
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _scheduledTasks.ForEach(task => task.Stop());
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _scheduledTasks.ForEach(task => task.Dispose());
        }
        
        private class TaskWrapper
        {
            public IPeriodicTask PeriodicTask { private get; set; }
            private Timer _timer;

            public void Start()
            {
                _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(PeriodicTask.Interval));
            }

            public void Stop()
            {
                _timer?.Change(Timeout.Infinite, 0);    
            }

            public void Dispose()
            {
                _timer?.Dispose();
            }

            private void ExecuteTask(object state)
            {
                PeriodicTask.Execute();
            }
        }
    }
}