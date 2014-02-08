using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hobbit.Framework.Interface
{
    public interface IDataModel
    {
        string this[string key] { get; }
    }
}
