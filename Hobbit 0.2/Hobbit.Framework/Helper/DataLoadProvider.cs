using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using Hobbit.Framework.DataLoadService;
using Hobbit.Framework.Enum;
using Hobbit.Framework.Interface;

namespace Hobbit.Framework.Helper
{
    public class DataLoadProvider
    {
        public static void LoadData(LoadType loadType, string dataSourceName, string prjId, string tcId, out IDataModel globalData, out IDataModel testData, out DataTable controlInfo)
        {
            switch (loadType)
            { 
                case LoadType.Excel:
                    Load<ExcelDataLoadService>(dataSourceName, prjId, tcId, out globalData, out testData, out controlInfo);
                    break;
                case LoadType.DB:
                    Load<DBDataLoadService>(dataSourceName, prjId, tcId, out globalData, out testData, out controlInfo);
                    break;
                case LoadType.Xml:
                    Load<XmlDataLoadService>(dataSourceName, prjId, tcId, out globalData, out testData, out controlInfo);
                    break;
                default:
                    globalData = null;testData=null;controlInfo=null;
                    break;
            }
        }

        private static void Load<T>(string dataSourceName, string prjId, string tcId, out IDataModel globalData, out IDataModel testData, out DataTable controlInfo) where T : IDataLoadService
        {
            var service = (T)Activator.CreateInstance(typeof(T));
            service.LoadData(dataSourceName, prjId, tcId, out globalData, out testData, out controlInfo);
        }
    }


}
