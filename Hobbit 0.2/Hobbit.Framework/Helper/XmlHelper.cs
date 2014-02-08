using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Hobbit.Framework.Helper
{
    public class XmlHelper
    {
        public static XElement LoadXml(string filePath)
        {
            try
            {
                return XElement.Load(filePath);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static IEnumerable<XElement> GetElementsByCondition(XElement searchedElement, string nodeName, string attributeName, string attributeValue)
        {
            try
            {
                return from elm in searchedElement.Descendants(nodeName)
                       where (string)elm.Attribute(attributeName) == attributeValue
                       select elm;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
