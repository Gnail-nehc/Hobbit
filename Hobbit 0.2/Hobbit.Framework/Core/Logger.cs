using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Hobbit.Framework.Interface;

namespace Hobbit.Framework.Core
{
    public class Logger : ILogger
    {
        private TestContext _testContext;
        private volatile static ILogger _instance = null;
        private static readonly object lockHelper = new object();

        public static ILogger CreateInstance(TestContext _testContext)
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new Logger(_testContext);
                }
            }
            return _instance;
        }


        private Logger(TestContext testContext)
        {
            try
            {
                _testContext = testContext;
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void LogInfo(string message, params object[] args)
        {
            WriteLine(LogType.Info, message, args);
        }

        public void LogError(string message, params object[] args)
        {
            WriteLine(LogType.Error, message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            WriteLine(LogType.Warning, message, args);
        }

        public void ScenarioStart(int scenarioId, string scenarioTitle)
        {
            if (null != _testContext)
                _testContext.WriteLine("\r\nSTART SCENARIO {0} - {1}.", scenarioId, scenarioTitle);
        }

        public void ScenarioEnd(int scenarioId)
        {
            if (null != _testContext)
                _testContext.WriteLine("\r\nSCENARIO {0} END.", scenarioId);
        }

        private void WriteLine(LogType logType, string message, params object[] args)
        {
            if (null != _testContext)
                _testContext.WriteLine("\r\n" + logType.ToString().ToUpper() + ": {0}", string.Format(message, args));
        }

        private enum LogType
        {
            Info,
            Error,
            Warning,
        }
    }
}
