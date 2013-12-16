using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zhaopin.IbatisFactory.Common
{
    public class Common
    {
        public static string GetDataType(string sqltype)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(sqltype))
            {
                switch (sqltype)
                {
                    case "int":
                        result = "int";
                        break;
                    case "datetime":
                        result = "DateTime";
                        break;
                    case "nvarchar":
                        result = "string"; 
                        break;
                    case "varchar":
                        result = "string";
                        break;
                    case "text":
                        result = "string";
                        break;
                    case "bigint":
                        result = "long";
                        break;
                    case "float":
                        result = "decimal";
                        break;
                    case "nchar":
                        result = "string";
                        break;
                    case "date":
                        result = "DateTime";
                        break;  
                    default:
                        result = "string";
                        break;
                }
            }

            return result;
        }
    }
}
