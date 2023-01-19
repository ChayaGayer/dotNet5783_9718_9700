using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;



internal static class Config
{
    static string s_config = "configuration";

    internal static int GetNextOrderNumber()
    {
        int i= XMLTools.ToIntNullable(XMLTools.LoadListFromXMLElement(s_config), "NextOrderNumber") ?? throw new NullReferenceException();

        return XMLTools.ToIntNullable(XMLTools.LoadListFromXMLElement(s_config), "NextOrderNumber") ?? throw new NullReferenceException();
        //return (int)XMLTools.LoadListFromXMLElement(s_config).Element("NextOrderNumber");

    }
    internal static void SaveNextOrderNumber(int num)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderNumber").SetValue(num.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);

    }

    internal static int GetNextOrderItemNumber()
    {
        return (int)XMLTools.LoadListFromXMLElement(s_config).Element("NextOrderItemNumber");
    }
    internal static void SaveNextOrderItemNumber(int num)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderItemNumber").SetValue(num.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);

    }
}

