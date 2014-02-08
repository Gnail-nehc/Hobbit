using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hobbit.Framework.Exception
{
    public class FailToLoadControlException : System.Exception
    {
        public FailToLoadControlException(string controlName)
            : base(string.Format("Fail to load control! Name: [{0}].Please check Data Source first.", controlName))
        {

        }
    }
}
