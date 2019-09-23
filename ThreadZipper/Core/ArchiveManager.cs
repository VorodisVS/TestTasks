namespace ThreadZipper.Core
{
    using System.Collections.Generic;
    using ThreadZipper.Common;

    public class ArchiveManager
    {
        private Dictionary<string, WorkMode> _workModes;
        private string _inputFilePath;
        private string _outputFilePath;

        private ThreadSafeQueue<DataBlock> _readedQueue;
        private ThreadSafeQueue<DataBlock> _zippedQueue;

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

        }
    }


 }
