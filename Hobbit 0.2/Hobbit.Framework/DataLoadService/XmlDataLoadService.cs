using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hobbit.Framework.Interface;
using Hobbit.Framework.Helper;
using System.Xml.Linq;
using Hobbit.Framework.Struct;
using System.Data;
using System.IO;
using Hobbit.Framework.Core;

namespace Hobbit.Framework.DataLoadService
{
    public class XmlDataLoadService : IDataLoadService
    {
        public void LoadData(string xmlFileName, string prjId, string tcId, out IDataModel globalData, out IDataModel testData, out System.Data.DataTable controlInfo)
        {
            XElement root = XmlHelper.LoadXml(xmlFileName);
            var prj = XmlHelper.GetElementsByCondition(root, XmlTemplateStruct.ProjectNode, XmlTemplateStruct.ProjectIdProperty, prjId).ToList()[0];
            if (null != prj)
            {
                var target = prj.Element(XmlTemplateStruct.GlobalDataNode);
                globalData = new DataModel(TransformNode2DataTable(XmlTemplateStruct.GlobalDataNode, target).Rows[0]);

                target = prj.Element(XmlTemplateStruct.TestDataNode);
                var collection = XmlHelper.GetElementsByCondition(target, XmlTemplateStruct.TestDataChildNode, XmlTemplateStruct.TestCaseIdProperty, tcId);
                if (null != collection)
                {
                    testData = new DataModel(TransformNode2DataTable(XmlTemplateStruct.TestDataChildNode, collection).Rows[0]);
                }
                else
                    throw new System.Exception("Not found node " + XmlTemplateStruct.TestDataChildNode + " under " + XmlTemplateStruct.TestDataNode + " which " + XmlTemplateStruct.TestCaseIdProperty + " = " + tcId);

                target = prj.Element(XmlTemplateStruct.ControlInfoNode);
                collection = target.Elements(XmlTemplateStruct.ControlInfoChildNode);
                controlInfo = TransformNode2DataTable(XmlTemplateStruct.ControlInfoChildNode, collection);
            }
            else
                throw new System.Exception("Not found node " + XmlTemplateStruct.ProjectNode + " which " + XmlTemplateStruct.ProjectIdProperty + " = " + prjId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="element">The contents of the element.</param>
        /// <returns></returns>
        private DataTable TransformNode2DataTable(string nodeName, object element)
        {
            var ds = new DataSet();
            string xmlStr = new XElement(nodeName, element).ToString();
            ds.ReadXml(new StringReader(xmlStr));
            return ds.Tables[0];
        }

    }
}
