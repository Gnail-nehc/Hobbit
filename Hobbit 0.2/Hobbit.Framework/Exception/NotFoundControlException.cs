using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hobbit.Framework.Exception
{
    public class NotFoundControlException : System.Exception
    {
        public NotFoundControlException(string controlName, string error)
            : base(string.Format("Not found control! Name: [{0}]."+error, controlName))
        {

        }

    }
}
