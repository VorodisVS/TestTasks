namespace ThreadZipper.Core
{
    public interface IWorker
    {
        WorkResult Work();
    }

    public class ReaderWorkerStub : IWorker
    {
        public WorkResult Work()
        {
            return WorkResult.Readed;
        }
    }

    public class ZipperWorkerStub : IWorker
    {
        public WorkResult Work()
        {
            return WorkResult.Compressed;
        }
    }

    public class WriterWorkerStub : IWorker
    {
        public WorkResult Work()
        {
            return WorkResult.Writed;
        }
    }
}
