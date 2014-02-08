using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using Hobbit.Framework.Interface;
using Hobbit.Framework.Struct;
using Hobbit.Framework.Core;

namespace Hobbit.Framework.DataLoadService
{
    public class ExcelDataLoadService : IDataLoadService
    {
        public void LoadData(string xlsFileName, string prjId, string tcId, out IDataModel globalData, out IDataModel testData, out DataTable controlInfo)
        {
            globalData = null; testData = null; controlInfo = null;
            string connectionString = "Provider=Microsoft.Jet.OleDb.4.0; Data Source="
            + xlsFileName + "; Extended Properties='Excel 8.0;IMEX=1';";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var tb = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                using (OleDbCommand command = new OleDbCommand())
                {
                    foreach (DataRow row in tb.Rows)
                    {
                        var dt = new DataTable();
                        string conditionColumn = XlsGlobalDataStruct.ProjectId, comparedValue = prjId;//"'" + prjId + "'";
                        string sheetname = row["TABLE_NAME"].ToString();
                        command.Connection = connection;
                        if (sheetname.Contains(XlsGlobalDataStruct.TableOrSheet)
                            || sheetname.Contains(XlsControlInfoStruct.TableOrSheet))
                        {
                            command.CommandText = "SELECT * FROM [" + sheetname + "] where " + conditionColumn + " = " + comparedValue;
                        }
                        else if (sheetname.Contains(XlsTestDataStruct.TableOrSheet))
                        {
                            string andConditionString = XlsTestDataStruct.TestCaseId + " = " + tcId;//'" + tcId + "'";
                            command.CommandText = "SELECT * FROM [" + sheetname + "] where " + conditionColumn + " = " + comparedValue
                                + " AND " + andConditionString;
                        }
                        else
                            throw new System.Exception("All sheets should call " + XlsGlobalDataStruct.TableOrSheet + ", " +
                                XlsTestDataStruct.TableOrSheet + ", or " + XlsControlInfoStruct.TableOrSheet + ".Please check sheet name again!");
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                        {
                            adapter.SelectCommand = command;
                            adapter.Fill(dt);
                            dt.TableName = sheetname.Replace("$", "");
                            if (sheetname.Contains(XlsGlobalDataStruct.TableOrSheet))
                            {
                                var dr = FillDataRow(dt);
                                globalData = new DataModel(dr);
                            }
                            else if (sheetname.Contains(XlsTestDataStruct.TableOrSheet))
                            {
                                var dr = FillDataRow(dt);
                                testData = new DataModel(dr);
                            }
                            else if (sheetname.Contains(XlsControlInfoStruct.TableOrSheet))
                            {
                                controlInfo = dt;
                            }
                        }
                    }
                }
            }
        }

        private DataRow FillDataRow(DataTable dt)
        {
            var table = new DataTable();
            table.Clear();
            DataRow row = table.NewRow();
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 2; i < dr.ItemArray.Length; i = i + 2)
                {
                    string name = dr.ItemArray[i].ToString();
                    string value = dr.ItemArray[i + 1].ToString();
                    if (!string.IsNullOrEmpty(name))
                    {
                        table.Columns.Add(name);
                        row[name] = value;
                    }
                }
            }
            return row;
        }
    }
}
