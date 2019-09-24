namespace ThreadZipperCore.Core
{
    using System;
    using System.Collections.Generic;
    using ThreadZipperCore.Common;
    using ThreadZipperCore.Core.Common;
    using ThreadZipperCore.Core.Workers;

    public class ArchiveManager
    {
        private Dictionary<string, WorkMode> _workModes;
        private string _inputFilePath;
        private string _outputFilePath;

        private ThreadSafeQueue<DataBlock> _readedQueue;
        private ThreadSafeQueue<DataBlock> _zippedQueue;
        private WorkerThread[] _workers;

        public event EventHandler<OperationResultEventArgs> Ended;

        public ArchiveManager(string cmd, string src, string trg)
        {
            _workModes = new Dictionary<string, WorkMode>
            {
                { "Compress", WorkMode.Compress },
                { "Decompress", WorkMode.Decompress }
            };
            _inputFilePath = src;
            _outputFilePath = trg;

            _readedQueue = new ThreadSafeQueue<DataBlock>();
            _zippedQueue = new ThreadSafeQueue<DataBlock>();
        }

        public void Start()
        {
            var procCount = Environment.ProcessorCount;
            _workers = new WorkerThread[procCount];

            for(int i = 0; i < procCount; i++)
            {
                _workers[i] = new WorkerThread(_readedQueue, _zippedQueue);
                _workers[i].Error += ExceptionHandler;
            }
            foreach (var worker in _workers)
            {
                worker.Start();
            }
        } 

        private void ExceptionHandler(object obj, ErrorExecutionArgs args)
        {
            Stop();
            Ended?.Invoke(this, new OperationResultEventArgs());
        }

        public void Stop()
        {
            foreach (var worker in _workers)
            {
                worker.Stop();
                worker.Error -= ExceptionHandler;
            }
        }
        
    }


 }
