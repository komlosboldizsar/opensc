using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace OpenSC.Library.TaskSchedulerQueue
{

    public class TaskQueue<TQueuedTask, TTaskResult>
        where TQueuedTask : QueuedTask<TTaskResult>
    {

        private TQueuedTask lastDequeuedTask;
        private BufferBlock<TQueuedTask> fifo;
        private CancellationTokenSource cancellationTokenSource;
        private Task requestSchedulerTask;

        public delegate void TaskHandlerDelegate(TQueuedTask task);
        private TaskHandlerDelegate doTaskMethod;
        private TaskHandlerDelegate invalidTaskHandlerMethod;

        public TaskQueue(TaskHandlerDelegate doTaskMethod, TaskHandlerDelegate invalidTaskHandlerMethod)
        {
            this.doTaskMethod = doTaskMethod;
            this.invalidTaskHandlerMethod = invalidTaskHandlerMethod;
        }

        public bool Running => (requestSchedulerTask != null);

        public void Start()
        {
            if (requestSchedulerTask != null)
                return;
            fifo = new();
            cancellationTokenSource = new();
            requestSchedulerTask = Task.Run(schedulerTaskMethod);
        }

        public void Stop() => Task.Run(_stop);

        private async Task _stop()
        {
            try
            {
                cancellationTokenSource.Cancel();
                lastDequeuedTask?.Cancel();
                await requestSchedulerTask;
                requestSchedulerTask.Dispose();
                cancellationTokenSource.Dispose();
            }
            catch (ObjectDisposedException)
            { }
            finally
            {
                requestSchedulerTask = null;
                cancellationTokenSource = null;
            }
        }

        private async Task schedulerTaskMethod()
        {
            try
            {
                while (true)
                {
                    if (lastDequeuedTask != null)
                        await lastDequeuedTask.Wait();
                    TQueuedTask task = await fifo.ReceiveAsync(cancellationTokenSource.Token);
                    if ((task != null) && task.IsValid)
                    {
                        lastDequeuedTask = task;
                        doTaskMethod(task);
                    }
                    else
                    {
                        invalidTaskHandlerMethod(task);
                    }
                }
            }
            catch (OperationCanceledException)
            { }
        }

        public void Enqueue(TQueuedTask item)
        {
            if (fifo == null)
                return;
            if (cancellationTokenSource.IsCancellationRequested)
                return;
            fifo.Post(item);
        }

        public void LastDequeuedTaskReady(TTaskResult result) => lastDequeuedTask?.Ready(result);

    }

    public class TaskQueue<TQueuedTask> : TaskQueue<TQueuedTask, NullResult>
       where TQueuedTask : QueuedTask
    {

        public TaskQueue(TaskHandlerDelegate doTaskMethod, TaskHandlerDelegate invalidTaskHandlerMethod)
            : base(doTaskMethod, invalidTaskHandlerMethod) { }

        public void LastDequeuedTaskReady() => base.LastDequeuedTaskReady(null);

    }

}
