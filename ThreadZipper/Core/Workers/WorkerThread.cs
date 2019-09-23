namespace ThreadZipper.Core.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using ThreadZipper.Common;

    public class WorkerThread
    {
        private ThreadSafeQueue<DataBlock> _readedQueue;
        private ThreadSafeQueue<DataBlock> _zippedQueue;
        private IWorker _worker;
        private Thread _thread;

        public WorkerThread(ThreadSafeQueue<DataBlock> queue1, ThreadSafeQueue<DataBlock> queue2)
        {
            _readedQueue = queue1;
            _zippedQueue = queue2;
            _thread = new Thread(() => {
                while (DoWork() != WorkResult.EndedRead)
                    break;
            });
        }

        public void Start()
        {
            _thread.Start();
        }

        public void Stop()
        {
            _thread.Abort();
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
