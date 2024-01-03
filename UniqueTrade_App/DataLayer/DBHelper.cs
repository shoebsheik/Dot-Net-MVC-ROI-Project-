using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;

namespace UniqueTrade_App.DataLayer
{
    public class DBHelper
    {
        private static string GetConnectionString()
        {
            string ip = WebConfigurationManager.AppSettings["senderIP"].ToString();
            string userid = WebConfigurationManager.AppSettings["senderuser"].ToString();
            string pwd = WebConfigurationManager.AppSettings["senderPass"].ToString();
            string dbname = WebConfigurationManager.AppSettings["senderdbname"].ToString();

            string connection = "Data Source={0};Initial Catalog={1};Integrated Security=False;User Id={2};Password={3}";
            string connection_main = string.Format(connection, BeechtreeED.Crypto.Decrypt_web(ip, "IP"), BeechtreeED.Crypto.Decrypt_web(dbname, "DBNAME"), BeechtreeED.Crypto.Decrypt_web(userid, "USER"), BeechtreeED.Crypto.Decrypt_web(pwd, "PWD"));

            string a = BeechtreeED.Crypto.Encrypt_web(userid, "USER");
            string b = BeechtreeED.Crypto.Encrypt_web(pwd, "PWD");
            string c = BeechtreeED.Crypto.Encrypt_web(dbname, "DBNAME");

            //return ConfigurationManager.ConnectionStrings["Global Crowd_Conn"].ToString();
            return connection_main;
        }
        private static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());

        }
        private static SqlCommand GetCommand(string cmdTxt, SqlConnection connection, params SqlParameter[] commandParameters)
        {
            SqlCommand command = new SqlCommand(cmdTxt, connection);
            if (cmdTxt.ToLower().StartsWith("usp_") || cmdTxt.ToLower().StartsWith("pr") || cmdTxt.ToLower().StartsWith("sp_") || cmdTxt.ToLower().StartsWith("pi") || cmdTxt.ToLower().StartsWith("bi"))
                command.CommandType = CommandType.StoredProcedure;
            if (commandParameters != null)
                AttachParameters(command, commandParameters);
            return command;
        }

        public static DataTable GetDataTable(string cmdTxt, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = GetCommand(cmdTxt, connection, commandParameters);
                command.CommandTimeout = 100;
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);
                return dt;
            }
        }

        public static int GetScaler(string cmdTxt, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = GetCommand(cmdTxt, connection, commandParameters);
                int var = Convert.ToInt16(command.ExecuteScalar());
                connection.Close();
                return var;
            }
        }

        public static bool GetScaler_Boolean(string cmdTxt, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = GetCommand(cmdTxt, connection, commandParameters);
                bool var = Convert.ToBoolean(command.ExecuteScalar());
                connection.Close();
                return var;
            }
        }

        public static string GetScaler_String(string cmdTxt, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = GetCommand(cmdTxt, connection, commandParameters);
                string var = command.ExecuteScalar().ToString();
                connection.Close();
                return var;
            }
        }

        public static SqlDataReader GetDataReader(string cmdTxt, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = GetCommand(cmdTxt, connection, commandParameters);
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
        }

        public static DataSet GetDataSet(string cmdTxt, params SqlParameter[] commandParameters)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = GetCommand(cmdTxt, connection, commandParameters);
            command.CommandTimeout = 180;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet(command.CommandText);
            da.Fill(ds);
            return ds;
        }
        public static int ExecuteNonQuery(string cmdTxt, params SqlParameter[] commandParameters)
        {
            SqlConnection connection = GetConnection();
            connection.Open();
            SqlCommand command = GetCommand(cmdTxt, connection, commandParameters);
            command.CommandTimeout = 3000000;
            int rVal = command.ExecuteNonQuery();
            connection.Close();
            return rVal;
        }



        public static bool IsDataAvailable(string cmdTxt)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = GetCommand(cmdTxt, connection, null);
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);
                return dt.Rows.Count > 0;
            }
        }

        //public static SelectList ToSelectList(string SQL, string valueField, string textField)
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    DataTable dt = GetDataTable(SQL, null);
        //    if(dt.Rows.Count > 0)
        //    { 
        //            list.Add(new SelectListItem()
        //            {
        //                Text = R.Read<string>(textField).ToString(), //row[textField].ToString(),
        //                Value = R.Read<string>(valueField).ToString()
        //            });
        //        }
        //    }
        //    return new SelectList(list, "Value", "Text");
        //}

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
    }
}
