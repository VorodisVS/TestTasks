namespace ThreadZipperCore.Commands
{
    using System;

    public class ErrorEventArgs : EventArgs
    {
        Exception Exception { get; }

        public ErrorEventArgs(Exception ex)
        {
            Exception = ex;
        }
    }
}
