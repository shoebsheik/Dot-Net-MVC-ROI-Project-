using UniqueTrade_App.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueTrade_App.DBEntity
{
    public class SupportTicketManager
    {
        public static int AddUpdate_Ticket(string MODE, string To_Id,string From_Id, string Message, string Subject)
        {
            SqlParameter[] sqlpara = new SqlParameter[7];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@TTIME", null);
            sqlpara[2] = new SqlParameter("@To_Id", To_Id);
            sqlpara[3] = new SqlParameter("@From_Id", From_Id);
            sqlpara[4] = new SqlParameter("@Message", Message);
            sqlpara[5] = new SqlParameter("@Subject", Subject);
            sqlpara[6] = new SqlParameter("@status", "0");
           
            return DBHelper.ExecuteNonQuery("SP_Support", sqlpara);
        }

        public static DataTable GetTicket(string MODE, string From_Id)
        {
            SqlParameter[] sqlpara = new SqlParameter[7];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@TTIME",null);
            sqlpara[2] = new SqlParameter("@To_Id", "0");
            sqlpara[3] = new SqlParameter("@From_Id", From_Id);

            return DBHelper.GetDataTable("SP_Support", sqlpara);
        }
    }
}
