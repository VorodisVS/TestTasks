namespace ThreadZipper
{
    using System;
    using Commands;

    class Program
    {
        static void Main(string[] args)
        {


            CommandManager cmd = new CommandManager(
                (key) => Console.WriteLine($"Unknown command {key}"),
                (info) => Console.WriteLine(info));

            cmd.AddAction("1", new ActionItem(() => Console.WriteLine(), "summa"));
            cmd.AddAction("2", new ActionItem(() => Console.WriteLine(), "summa2"));
            cmd.AddAction("3", new ActionItem(() => Console.WriteLine(), "summa3"));

            cmd.DoAction("help");
            Console.Read();
        }
    }
}
