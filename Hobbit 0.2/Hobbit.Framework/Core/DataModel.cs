using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hobbit.Framework.Interface;

namespace Hobbit.Framework.Core
{
    public class DataModel : IDataModel
    {
        private DataRow _dr;
        public DataModel(DataRow dr)
        {
            _dr = dr;
        }
        public string this[string key]
        {
            get
            {
                return _dr[key] as string;
            }
        }



    }
}
