using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace BO;

static class Tools
{
    public static string ToStringProperty<T>(this T t)
    {
        string str = " ";
        foreach (PropertyInfo item in t.GetType().GetProperties())
            if(item.PropertyType == typeof(IEnumerable))
            {
                foreach(var i in (IEnumerable)item)
                {
                    str += (i + " ");
                }
            }
            else
            {
                str += "\n" + item.Name + ": " + item.GetValue(t, null);
            }
            
        return str;
    }
}
