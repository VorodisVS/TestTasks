namespace ThreadZipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ErrorEventArgs : EventArgs
    {
        Exception Exception { get; }

        public ErrorEventArgs(Exception ex)
        {
            Exception = ex;
        }
    }
}
