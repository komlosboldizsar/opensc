using System.Threading.Tasks;

namespace OpenSC.Library.TaskSchedulerQueue
{

    public abstract class QueuedTask<TResult>
    {
        protected internal virtual bool IsValid => true;
        private TaskCompletionSource<TResult> taskCompletionSource = new();
        public async Task Wait() => await taskCompletionSource.Task;
        public void Cancel() => taskCompletionSource.SetCanceled();
        public void Ready(TResult result)
        {
            taskCompletionSource.SetResult(result);
            _ready(result);
        }
        protected virtual void _ready(TResult result) { }
    }

    public abstract class QueuedTask : QueuedTask<NullResult>
    {
        public void Ready() => base.Ready(null);
    }

}
