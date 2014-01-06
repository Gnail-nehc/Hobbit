using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hobbit.Framework.Interface
{
    public interface IDataLoadService
    {
        void LoadData(string dataSourceName, string prjId, string tcId, out IDataModel globalData, out IDataModel testData, out DataTable controlInfo);

    }
}
