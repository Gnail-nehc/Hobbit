using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hobbit.Framework.WebControl;
using Hobbit.Framework.Interface;

namespace Hobbit.Framework.Core
{
    public abstract class TestModel
    {
        public IDataModel GlobalData { get; set; }

        public IDataModel TestData { get; set; }

        public TestContext TestContext { get; set; }

        public ILogger Log
        {
            get
            {
                return Logger.CreateInstance(TestContext);
            }
        }

        [TestInitialize]
        public abstract void Init();

        [TestCleanup]
        public abstract void End();
    }
}
