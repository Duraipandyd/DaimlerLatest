using Daimler.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Daimler.Services
{
    public class CommonFunction
    {
        public System.Data.DataTable ExecuteSelectSQL(String SQLString)
        {

            try
            {

                System.Data.DataTable dtblDataTable = new System.Data.DataTable();

                using (DaimlerContext context = new DaimlerContext())
                {
                    DbConnection conn = context.Database.GetDbConnection();
                    DbCommand cmd = conn.CreateCommand();

                    cmd.CommandText = SQLString;

                    conn.Open();

                    try
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = (SqlCommand)cmd;
                        dataAdapter.Fill(dtblDataTable);
                    }
                    catch (Exception ex)
                    {

                    }

                    conn.Close();

                    if (dtblDataTable.Rows.Count == 0) return null;

                }

                return dtblDataTable;

            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public static object NullToZero(object value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public static Double NothingToDoubleZero(Object value)
        {
            if (!(value is DBNull))
            {
                return Convert.ToDouble(value);
            }
            else
            {
                return 0.0;
            }
        }

        public static decimal NullToDecimalZero(object value)
        {
            try
            {
                if (!(value is DBNull))
                {
                    return Convert.ToDecimal(value);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int NullToIntZero(object value)
        {
            try
            {
                if (!(value is DBNull))
                {
                    return Convert.ToInt32(value);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string NullToEmpty(object value)
        {
            try
            {
                if (value == null)
                {
                    return "";
                }

                return value.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static double NullToDoubleZero(object prmobject)
        {
            try
            {
                if (DBNull.Value.Equals(prmobject))
                {
                    return 0.0;
                }
                else
                {
                    return Convert.ToDouble(prmobject);
                }


            }
            catch (Exception)
            {
                return 0.0;
            }

        }
    }
}
