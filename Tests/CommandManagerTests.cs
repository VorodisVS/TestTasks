namespace Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using ThreadZipperCore.Commands;

    [TestFixture]
    public class CommandManagerTests
    { 
        [Test]
        public void CommadTest()
        {
            string[] testCommands = new string[]
            {
                "test1", "test2", "test3", "test4", "test5", "test6"
            };
            List<string> awaitingCommands = new List<string>();
            CommandManager cmdManager = new CommandManager((s) => awaitingCommands.Add(s), (h) => awaitingCommands.Add(h));

            foreach(var s in testCommands)
                cmdManager.AddAction(s, new ActionItem(() => awaitingCommands.Add(s), s));
            foreach (var s in testCommands)
                cmdManager.DoAction(s);

            CollectionAssert.AreEqual(testCommands, awaitingCommands); 
        }


        [Test]
        public void ErrorTest()
        {
            bool errorOccured = false;
            CommandManager cmdManager = new CommandManager(s => string.IsNullOrEmpty(s), h => string.IsNullOrEmpty(h));
            cmdManager.Error += (obj, e) =>
            {
                errorOccured = true;
            };

            cmdManager.AddAction("test", new ActionItem(() => throw new System.Exception(), "info"));
            Assert.DoesNotThrow(() => cmdManager.DoAction("test"));
            Assert.IsTrue(errorOccured);
        }
    }
}