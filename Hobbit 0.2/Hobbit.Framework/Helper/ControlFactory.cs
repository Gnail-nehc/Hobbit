using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.Core;
using Hobbit.Framework.Enum;
using Hobbit.Framework.Interface;

namespace Hobbit.Framework.Helper
{
    public class ControlFactory
    {
        public static T Create<T>(string controlName) where T : IWebControlBase
        {
            return (T)Activator.CreateInstance(typeof(T), controlName);
        }

        public static T Create<T>(FindBy by, string value) where T : IWebControlBase
        {
            return (T)Activator.CreateInstance(typeof(T), by, value);
        }
    }
}
