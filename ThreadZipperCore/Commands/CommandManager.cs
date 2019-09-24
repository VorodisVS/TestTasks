namespace ThreadZipperCore.Commands
{
    using System;
    using System.Collections.Generic;

    public class CommandManager
    {
        private Dictionary<string, ActionItem> _actions;
        private Action<string> _defaultAction;
        private Action<string> _helpAction;
        private const string HELP_COMMAND = "help";

        public event EventHandler<ErrorEventArgs> Error;

        public CommandManager(Action<string> defaultAction, Action<string> helpAction)
        {
            _defaultAction = defaultAction;
            _helpAction = helpAction;
            _actions = new Dictionary<string, ActionItem>();
        }

        public void AddAction(string key, ActionItem action)
        {
            _actions.Add(key, action);
        }

        public void DoAction(string key)
        {
            try
            {
                if (key.Equals(HELP_COMMAND))
                {
                    _helpAction?.Invoke(GetCommandsWithDescriptions());
                    return;
                }
                if (_actions.TryGetValue(key, out var action))
                {
                    action.Action.Invoke();
                    return;
                }
                _defaultAction.Invoke(key);

            }
            catch (Exception ex)
            {
                Error?.Invoke(this, new ErrorEventArgs(ex));
            }
        }

        private string GetCommandsWithDescriptions()
        {
            string help = "Aviable commands: \n";
            foreach (KeyValuePair<string, ActionItem> a in _actions)
            {
                help += $"{a.Key} - {a.Value.Info} \n";
            }

            return help;
        }
    }
}
