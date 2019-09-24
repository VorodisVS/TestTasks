namespace Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using ThreadZipperCore.Core;

    [TestFixture]
    class ArchiveManagerTests
    {

        [Test]
        public void StartStopTests()
        {
            bool isEnded = false;
            ArchiveManager archiveManager = new ArchiveManager("compress", "", "");
            archiveManager.Ended += (obj, args) =>
            {
                isEnded = true;
            };
            archiveManager.Start();
            Thread.Sleep(50);
            //Assert.IsFalse(isEnded);
            archiveManager.Stop();
           //// Assert.IsTrue(isEnded);
        }
    }
}
