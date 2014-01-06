using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.Interface;
using System.Diagnostics;
using Hobbit.Framework.Helper;
using Hobbit.Framework.Core;
using Hobbit.Framework.Exception;
using Hobbit.Framework.Struct;

namespace Hobbit.Framework.DataLoadService
{
    public class DBDataLoadService : IDataLoadService
    {
        public void LoadData(string connectionString, string prjId, string tcId, out IDataModel globalData, out IDataModel testData, out DataTable controlInfo)
        {
            LoadData(connectionString, prjId, tcId, out globalData, out testData);
            LoadControlInfo(connectionString, prjId, out controlInfo);
        }

        private void LoadControlInfo(string connectionString, string prjId, out DataTable dt)
        {
            try
            {
                string sql = "select " + 
                    SqlFunc4ControlInfo.UIType + "," +
                    SqlFunc4ControlInfo.UIProperty + "," + 
                    SqlFunc4ControlInfo.PropValue + "," +
                    SqlFunc4ControlInfo.UIName + 
                    " from " + SqlFunc4ControlInfo.FuncInDB + "(" + prjId + ")";
                dt = SqlHelper.GetDataTableBySql(connectionString, sql);
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("Fail to load controls in project [{0}].", prjId);
                throw ex;
            }
        }

        private void LoadData(string connectionString, string prjId, string tcId, out IDataModel global, out IDataModel local)
        {
            string sql = "select " +
                SqlFunc4Data.Name + "," + SqlFunc4Data.Value + "," + SqlFunc4Data.IsGlobal +
                " from " + SqlFunc4Data.FuncInDB + "(" + prjId + "," + tcId + ")";
            DataTable dt = SqlHelper.GetDataTableBySql(connectionString, sql);
            global = ConvertDataTabel2DataModel(dt, true);
            local = ConvertDataTabel2DataModel(dt, false);
        }

        private IDataModel ConvertDataTabel2DataModel(DataTable dt, bool isGlobal)
        {
            try
            {
                var query = from row in dt.AsEnumerable()
                            where row.Field<bool>(SqlFunc4Data.IsGlobal) == isGlobal
                            select row;
                var table = new DataTable();
                table.Clear();
                DataRow dataRow = table.NewRow();
                if (query.Count() != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string name = dr.ItemArray[0].ToString();
                        table.Columns.Add(name);
                        dataRow[name] = dr.ItemArray[1];
                    }
                }
                return new DataModel(dataRow);
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
