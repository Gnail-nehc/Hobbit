using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hobbit.Framework.Interface
{
    public interface IParameterizableControl
    {
        DataTable ControlInfo { get; set; }
    }
}
