namespace OpenSC.Library.TaskSchedulerQueue
{
    public class ImmediatelyReadyQueuedTask : QueuedTask
    {
        public ImmediatelyReadyQueuedTask() => Ready();
    }
}
