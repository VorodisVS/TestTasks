namespace ThreadZipper.Core
{
    public enum WorkResult
    {
        Readed,
        Compressed,
        Decompressed,
        Writed,
        EndedRead,
    }

    public enum WorkerMode
    {
        Reader,
        Zipper,
        Writter
    }

    public enum WorkMode
    {
        Compress,
        Decompress
    }
}
