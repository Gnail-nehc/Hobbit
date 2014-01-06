using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hobbit.Framework.Interface
{
    public interface ILogger
    {
        void LogInfo(string message, params object[] args);

        void LogError(string message, params object[] args);

        void LogWarning(string message, params object[] args);

        void ScenarioStart(int scenarioId, string scenarioTitle);

        void ScenarioEnd(int scenarioId);
    }
}
