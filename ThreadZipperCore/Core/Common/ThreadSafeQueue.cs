namespace ThreadZipperCore.Common
{  
    using System.Collections.Generic;

    public class ThreadSafeQueue<T> : Queue<T>
    {
        private Queue<T> _queue;
        private object _locker;

        public int Count => _queue.Count;

        public ThreadSafeQueue()
        {
            _queue = new Queue<T>();
            _locker = new object();
        }

        public void Enqueue(T item)
        {
            lock (_locker)
            {                
                _queue.Enqueue(item);
            }
        }

        public T Dequeue()
        {
            lock (_locker)
            {
                return _queue.Dequeue();
            }
        }
    }
}
