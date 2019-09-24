namespace ThreadZipperCore.Core.Workers
{
    using System;
    using System.Threading;
    using ThreadZipperCore.Common;
    using ThreadZipperCore.Core.Common;

    public class WorkerThread
    {
        private ThreadSafeQueue<DataBlock> _readedQueue;
        private ThreadSafeQueue<DataBlock> _zippedQueue;
        private IWorker _worker;
        private Thread _thread;

        public event EventHandler<ErrorExecutionArgs> Error;

        public WorkerThread(ThreadSafeQueue<DataBlock> queue1, ThreadSafeQueue<DataBlock> queue2)
        {
            _readedQueue = queue1;
            _zippedQueue = queue2;
            _thread = new Thread(() => 
            {
                try
                {
                    while (DoWork() != WorkResult.EndedRead)
                        break;
                }
                catch(Exception ex)
                {
                    Error?.Invoke(this, new ErrorExecutionArgs());
                }
            });
        }

        public void Start()
        {           
            _thread.Start();
        }

        public void Stop()
        {
           
            _thread.Join();
        }

        private WorkResult DoWork()
        {
            var mode = GetMode();
            _worker = GetWorker(mode);
            return _worker.Work();
        }

        private IWorker GetWorker(WorkerMode mode)
        {
            switch (mode)
            {
                case WorkerMode.Reader:
                    return new ReaderWorkerStub();
                    break;
                case WorkerMode.Zipper:
                    return new ZipperWorkerStub();
                    break;
                case WorkerMode.Writter:
                    return new WriterWorkerStub();
                    break;
                default:
                    return null;
                    break;
            }
        }

        private WorkerMode GetMode()
        {
            if (_zippedQueue.Count > 0)
                return WorkerMode.Writter;
            if (_readedQueue.Count > 0)
                return WorkerMode.Zipper;
            return WorkerMode.Reader;
        }
    }

}
