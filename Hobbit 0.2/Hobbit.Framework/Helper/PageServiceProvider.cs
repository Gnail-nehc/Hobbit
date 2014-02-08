using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.Interface;

namespace Hobbit.Framework.Helper
{
    public class PageServiceProvider
    {
        public static T GetService<T>() where T : IPageService
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
