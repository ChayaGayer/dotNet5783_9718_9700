using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace BO;
/// <summary>
/// ovveride of the ToString
/// </summary>
public static class Tools
{
    public static string ToStringProperty<T>(this T t, string str = "") 
    {
        foreach (PropertyInfo item in t.GetType().GetProperties())
        {
            if (item.GetValue(t, null) is IEnumerable<object>)
            {
                IEnumerable<object> list = (IEnumerable<object>)item.GetValue(obj: t, null);
                string s = String.Join(" ", list);
                str += "\n" + item.Name + ": " + s;
            }
            else
                str += "\n" + item.Name + ": " + item.GetValue(t, null);
        }
       

        return str;
       
    }
}
