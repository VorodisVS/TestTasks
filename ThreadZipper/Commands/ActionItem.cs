namespace ThreadZipper.Commands
{
    using System;

    public struct ActionItem
    {
        public string Info { get; }
        public Action Action { get; }

        public ActionItem(Action action, string info)
        {
            Action = action;
            Info = info;
        }
    }
}
