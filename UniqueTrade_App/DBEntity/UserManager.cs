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


  

    public class UserManager
    {
        public static DataTable REGISTRATION(string MODE, string USERNAME, string MPWD, string SPID, string PLACE, string PLAN_TYPE
 , string MembName_F, string MembName_M, string MembName_L, string Memb_Name, string Gender, string Mobile_No
 , string Phone_No, string EMail, string Address1, string Address2, string M_COUNTRY, string State
 , string District, string City, string Pin_Code, string Reg_Amt, string RV_Code, string PIN_ID
 , string pin_no, string REMARK, string client_ip, string ac_name, string ac_no, string bk_name
 , string bk_branch, string bk_ifsc, string bk_card_no, string btc_ac)
        {
            SqlParameter[] sqlpara = new SqlParameter[34];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@USERNAME", USERNAME);
            sqlpara[2] = new SqlParameter("@MPWD", MPWD);
            sqlpara[3] = new SqlParameter("@SPID", SPID);
            sqlpara[4] = new SqlParameter("@PLACE", PLACE);
            sqlpara[5] = new SqlParameter("@PLAN_TYPE", PLAN_TYPE);
            sqlpara[6] = new SqlParameter("@MembName_F", MembName_F);
            sqlpara[7] = new SqlParameter("@MembName_M", MembName_M);
            sqlpara[8] = new SqlParameter("@MembName_L", MembName_L);
            sqlpara[9] = new SqlParameter("@Memb_Name", Memb_Name);
            sqlpara[10] = new SqlParameter("@Gender", Gender);
            sqlpara[11] = new SqlParameter("@Mobile_No", Mobile_No);
            sqlpara[12] = new SqlParameter("@Phone_No", Phone_No);
            sqlpara[13] = new SqlParameter("@EMail", EMail);
            sqlpara[14] = new SqlParameter("@Address1", Address1);
            sqlpara[15] = new SqlParameter("@Address2", Address2);
            sqlpara[16] = new SqlParameter("@M_COUNTRY", M_COUNTRY);
            sqlpara[17] = new SqlParameter("@State", State);
            sqlpara[18] = new SqlParameter("@District", District);
            sqlpara[19] = new SqlParameter("@City", City);
            sqlpara[20] = new SqlParameter("@Pin_Code", Pin_Code);
            sqlpara[21] = new SqlParameter("@Reg_Amt", string.IsNullOrEmpty(Reg_Amt) ? "0" : Reg_Amt);
            sqlpara[22] = new SqlParameter("@RV_Code", RV_Code);
            sqlpara[23] = new SqlParameter("@PIN_ID", PIN_ID);
            sqlpara[24] = new SqlParameter("@pin_no", pin_no);
            sqlpara[25] = new SqlParameter("@REMARK", REMARK);
            sqlpara[26] = new SqlParameter("@client_ip", client_ip);
            sqlpara[27] = new SqlParameter("@ac_name", ac_name);
            sqlpara[28] = new SqlParameter("@ac_no", ac_no);
            sqlpara[29] = new SqlParameter("@bk_name", bk_name);
            sqlpara[30] = new SqlParameter("@bk_branch", bk_branch);
            sqlpara[31] = new SqlParameter("@bk_ifsc", bk_ifsc);
            sqlpara[32] = new SqlParameter("@bk_card_no", bk_card_no);
            sqlpara[33] = new SqlParameter("@btc_ac", btc_ac);

            return DBHelper.GetDataTable("SP_REGISTRATION", sqlpara);
        }



        public static int AddUserfundRequestinbtc(string MODE, string memb_code, string amount, string txn_id,/* string address*/ string BTCAddress, /*string btsAmount,*/ string BTC_Type, string confirms_needed,
 string timeout, string checkout_url, string status_url, string qrcode_url)
        {
            SqlParameter[] sqlpara = new SqlParameter[11];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@memb_code", memb_code);
            sqlpara[2] = new SqlParameter("@amount", amount);
            sqlpara[3] = new SqlParameter("@txn_id", txn_id);
            //sqlpara[4] = new SqlParameter("@address", address);
            sqlpara[4] = new SqlParameter("@BTCAddress", BTCAddress);
            //sqlpara[5] = new SqlParameter("@btsAmount", btsAmount);
            sqlpara[5] = new SqlParameter("@BTC_Type", BTC_Type);
            //sqlpara[7] = new SqlParameter("@BTC_AC", coinAddress);
            sqlpara[6] = new SqlParameter("@confirms_needed", confirms_needed);
            sqlpara[7] = new SqlParameter("@timeout", timeout);
            sqlpara[8] = new SqlParameter("@checkout_url", checkout_url);
            sqlpara[9] = new SqlParameter("@status_url", status_url);
            sqlpara[10] = new SqlParameter("@qrcode_url", qrcode_url);

            return DBHelper.ExecuteNonQuery("SP_TopUpUser", sqlpara);
        }


        public static DataTable GetUserLogin(string username)
        {
            return DBHelper.GetDataTable("select *,(Select Top 1 username from Entry order by memb_code asc) as Admin_username from Entry where tempf='Y' and M_Status='Y' and username='" + username + "'");
        }

        public static DataTable User_LOGIN(string USERNAME)
        {
            SqlParameter[] sqlpara = new SqlParameter[5];
            sqlpara[0] = new SqlParameter("@MODE", "Login");
            sqlpara[1] = new SqlParameter("@USERNAME", USERNAME);
            sqlpara[2] = new SqlParameter("@MPWD", null);
            sqlpara[3] = new SqlParameter("@MEMB_CODE", "0");
            sqlpara[4] = new SqlParameter("@IP", null);
           
            return DBHelper.GetDataTable("SP_LOGIN", sqlpara);
        }

        public static DataTable User_LOGIN_History(string MODE, string USERNAME,string MPWD,string MEMB_CODE,string IP)
        {
            SqlParameter[] sqlpara = new SqlParameter[5];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@USERNAME", USERNAME);
            sqlpara[2] = new SqlParameter("@MPWD", MPWD);
            sqlpara[3] = new SqlParameter("@MEMB_CODE", MEMB_CODE);
            sqlpara[4] = new SqlParameter("@IP", IP);

            return DBHelper.GetDataTable("SP_LOGIN", sqlpara);
        }

        public static DataTable USER_DETAILS(string MODE, string memb_code, string Memb_Name, string MemberName_L, string Gender
       , string dbo, string Mobile_No, string EMail, string Address1
       , string Address2, string M_COUNTRY, string City, string username
       , string mpwd, string RV_Code, string client_ip, string coo_ac
       , string btc_ac, string eth_ac, string ltc_ac)
        {
            SqlParameter[] sqlpara = new SqlParameter[20];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@memb_code", memb_code);
            sqlpara[2] = new SqlParameter("@Memb_Name", Memb_Name);
            sqlpara[3] = new SqlParameter("@MemberName_L", MemberName_L);
            sqlpara[4] = new SqlParameter("@Gender", Gender);
            sqlpara[5] = new SqlParameter("@dbo", dbo);
            sqlpara[6] = new SqlParameter("@Mobile_No", Mobile_No);
            sqlpara[7] = new SqlParameter("@EMail", EMail);
            sqlpara[8] = new SqlParameter("@Address1", Address1);
            sqlpara[9] = new SqlParameter("@Address2", Address2);
            sqlpara[10] = new SqlParameter("@M_COUNTRY", M_COUNTRY);
            sqlpara[11] = new SqlParameter("@City", City);
            sqlpara[12] = new SqlParameter("@username", username);
            sqlpara[13] = new SqlParameter("@mpwd", mpwd);
            sqlpara[14] = new SqlParameter("@RV_Code", RV_Code);
            sqlpara[15] = new SqlParameter("@client_ip", client_ip);
            sqlpara[16] = new SqlParameter("@coo_ac", coo_ac);
            sqlpara[17] = new SqlParameter("@btc_ac", btc_ac);
            sqlpara[18] = new SqlParameter("@eth_ac", eth_ac);
            sqlpara[19] = new SqlParameter("@ltc_ac", ltc_ac);
           // sqlpara[20] = new SqlParameter("@MembName_M", countrycode);


            return DBHelper.GetDataTable("SP_USER_DETAILS", sqlpara);
        }

        public static DataTable Change_Password(string MEMB_CODE, string mpwd)
        {
            SqlParameter[] sqlpara = new SqlParameter[3];
            sqlpara[0] = new SqlParameter("@MODE", "CHANGEPASSWORD");
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);
            sqlpara[2] = new SqlParameter("@mpwd", mpwd);

            return DBHelper.GetDataTable("SP_USER_DETAILS", sqlpara);
        }


        public static DataTable Change_TransPassword(string MEMB_CODE, string RV_Code)
        {
            SqlParameter[] sqlpara = new SqlParameter[3];
            sqlpara[0] = new SqlParameter("@MODE", "CHANGETRANSPASSWORD");
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);
            sqlpara[2] = new SqlParameter("@RV_Code", RV_Code);

            return DBHelper.GetDataTable("SP_USER_DETAILS", sqlpara);
        }

        public static DataTable ADD_ACCOUNT_DETAILS(string MEMB_CODE, string ac_name, string ac_no
        , string bk_name, string bk_branch, string bk_ifsc, string debit_card_no, string transit_no, string ac_type
            ,string PhonePay, string googlepay, string bhimno, string Paytmno)
        {
            SqlParameter[] sqlpara = new SqlParameter[14];
            sqlpara[0] = new SqlParameter("@MODE", "ADDACCOUNT");
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);
            sqlpara[2] = new SqlParameter("@ac_name", ac_name);
            sqlpara[3] = new SqlParameter("@ac_no", ac_no);
            sqlpara[4] = new SqlParameter("@bk_name", bk_name);
            sqlpara[5] = new SqlParameter("@bk_branch", bk_branch);
            sqlpara[6] = new SqlParameter("@bk_ifsc", bk_ifsc);
            sqlpara[7] = new SqlParameter("@debit_card_no", debit_card_no);
            sqlpara[8] = new SqlParameter("@transit_no", transit_no);
            sqlpara[9] = new SqlParameter("@ac_type", ac_type);
            sqlpara[10] = new SqlParameter("@Phone_Pay", PhonePay);
            sqlpara[11] = new SqlParameter("@Google_Pay", googlepay);
            sqlpara[12] = new SqlParameter("@BHIM_ID", bhimno);
            sqlpara[13] = new SqlParameter("@Paytm_no", Paytmno);
            //sqlpara[10] = new SqlParameter("@aadhar_card", aadhar_card);
            //sqlpara[11] = new SqlParameter("@pan_card", pan_card); 

            return DBHelper.GetDataTable("SP_USER_DETAILS", sqlpara);
        }

        public static DataTable GetUserDetailsByMemb_Code(string memb_code)
        {
            return DBHelper.GetDataTable("select * from Entry where tempf='Y' and M_Status='Y' and username='" + memb_code + "'");
        }

        public static DataTable GetUserByEmail(string EMail)
        {
            return DBHelper.GetDataTable("select * from Entry where tempf='Y' and username='" + EMail + "'");
        }

        public static DataTable CheckUsername(string username)
        {
            return DBHelper.GetDataTable("select * from Entry where username='" + username + "'");
        }

       







        public static int AddUserfundRequestinbtc(string MODE, string memb_code, string Usdamount,  string trans_no, string coin_amount, string coin_type, string coinAddress,
            string confirms_needed, string timeout, string checkout_url, string status_url, string qrcode_url)
        {
            SqlParameter[] sqlpara = new SqlParameter[12];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", memb_code);
            sqlpara[2] = new SqlParameter("@amount", Usdamount);           
            sqlpara[3] = new SqlParameter("@txn_id", trans_no);
            sqlpara[4] = new SqlParameter("@BTC_amount", coin_amount);
            sqlpara[5] = new SqlParameter("@BTC_Type", coin_type);
            sqlpara[6] = new SqlParameter("@BTC_AC", coinAddress);
            sqlpara[7] = new SqlParameter("@confirms_needed", confirms_needed);
            sqlpara[8] = new SqlParameter("@timeout", timeout);
            sqlpara[9] = new SqlParameter("@checkout_url", checkout_url);
            sqlpara[10] = new SqlParameter("@status_url", status_url);
            sqlpara[11] = new SqlParameter("@qrcode_url", qrcode_url);

            return DBHelper.ExecuteNonQuery("SP_FUNDREQUEST", sqlpara);
        }

        public static int AddUserfundRequestinCoinpayment(string MODE, string memb_code, string amount, string Attachment, string trans_no, string btc_amount, string btc_type, string btcAddress, string gpayN, string fname, string mnumber, string state, string dstrict, string btcac)
        {
            SqlParameter[] sqlpara = new SqlParameter[14];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", memb_code);
            sqlpara[2] = new SqlParameter("@amount", amount);
            sqlpara[3] = new SqlParameter("@Attachment", Attachment);
            sqlpara[4] = new SqlParameter("@trans_no", trans_no);
            sqlpara[5] = new SqlParameter("@BTC_amount", btc_amount);
            sqlpara[6] = new SqlParameter("@BTC_Type", btc_type);
            sqlpara[7] = new SqlParameter("@BTC_AC", btcAddress);
            sqlpara[8] = new SqlParameter("@Gpay", gpayN);
            sqlpara[9] = new SqlParameter("@fname", fname);
            sqlpara[10] = new SqlParameter("@mnumber", mnumber);
            sqlpara[11] = new SqlParameter("@state", state);
            sqlpara[12] = new SqlParameter("@dstrict", dstrict);
            sqlpara[13] = new SqlParameter("@btcac", btcac);

            return DBHelper.ExecuteNonQuery("SP_FUNDREQUEST", sqlpara);
        }








        public static DataTable GetUserVerify(string username)
        {
            return DBHelper.GetDataTable("select *,(Select Top 1 username from Entry order by memb_code asc) as Admin_username from Entry where tempf='Y' and username='" + username + "'");
        }

        public static int verifyEmail(string username)
        {
            return DBHelper.ExecuteNonQuery("Update Entry set M_Status='Y' where username='" + username + "'");
        }

        public static DataTable GetAccountDetails(string memb_code)
        {
            return DBHelper.GetDataTable("select * from bankdtl where memb_code='" + memb_code + "' order by srno desc");
        }

        public static DataTable GetSponserDetails(string spon_code)
        {
            return DBHelper.GetDataTable("select username as sp_user,memb_name as sp_name,email as sp_Email,m_country as sp_country,mobile_no as sp_mobile_no from Entry where memb_code='" + spon_code + "'");
        }

        public static int UserWithdrawals(string MODE, string SRNO, string commit_no, string memb_code
        , string tdate, string TTIME, string amount, string DONATE_THROUGH
        , string quick, string tflag, string INTPER, string intdays, string plan_type
        , string link_per, string donation_at_reg, string roi_days_type, string address
        , string username, string BTC_amount, string From_Id, string BTC_Type,string remark)
        {
            SqlParameter[] sqlpara = new SqlParameter[22];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@SRNO", SRNO);
            sqlpara[2] = new SqlParameter("@commit_no", commit_no);
            sqlpara[3] = new SqlParameter("@memb_code", memb_code);
            sqlpara[4] = new SqlParameter("@tdate", tdate);
            sqlpara[5] = new SqlParameter("@TTIME", TTIME);
            sqlpara[6] = new SqlParameter("@amount", amount);
            sqlpara[7] = new SqlParameter("@DONATE_THROUGH", DONATE_THROUGH);
            sqlpara[8] = new SqlParameter("@quick", quick);
            sqlpara[9] = new SqlParameter("@tflag", tflag);
            sqlpara[10] = new SqlParameter("@INTPER", INTPER);
            sqlpara[11] = new SqlParameter("@intdays", intdays);
            sqlpara[12] = new SqlParameter("@plan_type", plan_type);
            sqlpara[13] = new SqlParameter("@link_per", link_per);
            sqlpara[14] = new SqlParameter("@donation_at_reg", donation_at_reg);
            sqlpara[15] = new SqlParameter("@roi_days_type", roi_days_type);
            sqlpara[16] = new SqlParameter("@address", address);
            sqlpara[17] = new SqlParameter("@username", username);
            sqlpara[18] = new SqlParameter("@BTC_amount", BTC_amount);
            sqlpara[19] = new SqlParameter("@From_Id", From_Id);
            sqlpara[20] = new SqlParameter("@BTC_Type", BTC_Type);
            sqlpara[21] = new SqlParameter("@Remark", remark);

            return DBHelper.ExecuteNonQuery("SP_FinanceDtl", sqlpara);
        }

        public static int UserDeposite(string SRNO, string memb_code, string amount,string address,string btsAmount,string BTC_Type,string plan_type)
        {
            SqlParameter[] sqlpara = new SqlParameter[7];
            sqlpara[0] = new SqlParameter("@SRNO", SRNO);
            sqlpara[1] = new SqlParameter("@memb_code", memb_code);
            sqlpara[2] = new SqlParameter("@amount", amount);
            sqlpara[3] = new SqlParameter("@address", address);
            sqlpara[4] = new SqlParameter("@btsAmount", btsAmount);
            sqlpara[5] = new SqlParameter("@BTC_Type", BTC_Type);
            sqlpara[6] = new SqlParameter("@plan_type", plan_type);

            return DBHelper.ExecuteNonQuery("SP_Investment", sqlpara);
        }

        public static DataTable DepositeHistory(string memb_code)
        {
            SqlParameter[] sqlpara = new SqlParameter[7];
            sqlpara[0] = new SqlParameter("@SRNO", "1");
            sqlpara[1] = new SqlParameter("@memb_code", memb_code);
            sqlpara[2] = new SqlParameter("@amount", "0");
            sqlpara[3] = new SqlParameter("@address", null);
            sqlpara[4] = new SqlParameter("@btsAmount", "0");
            sqlpara[5] = new SqlParameter("@BTC_Type", null);
            sqlpara[6] = new SqlParameter("@plan_type", null);

            return DBHelper.GetDataTable("SP_Investment", sqlpara);
        }

        public static int AddContactEnquiry(string username, string email, string phone_no, string subject, string message, string Ctype)
        {
            return DBHelper.ExecuteNonQuery("Insert into contact_outer(username,email,phone_no,message,tdate,subject,Ctype) Values('" + username + "','" + email + "', '" + phone_no + "','" + message + "','" + DateTime.Now.ToString() + "','" + subject + "','" + Ctype + "')");
        }


        
        public static int ChangeTXPaasword(string memb_code, string NewTranPass)
        {
            return DBHelper.ExecuteNonQuery("update entry set  rv_code='" + NewTranPass.Trim() + "' where  memb_code=" + memb_code + "");
        }
        

        public static int SP_Contact_Outer(string MODE, string username,string email,string phone_no,string subject,string message)
        {
            SqlParameter[] sqlpara = new SqlParameter[6];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@username", username);
            sqlpara[2] = new SqlParameter("@email", email);
            sqlpara[3] = new SqlParameter("@phone_no", phone_no);
            sqlpara[4] = new SqlParameter("@subject", subject);
            sqlpara[5] = new SqlParameter("@message", message);

            return DBHelper.ExecuteNonQuery("SP_Contact_Outer", sqlpara);
        }


        public static DataTable GetDASHBOARDData(string MODE, string MCODE)
        {
            SqlParameter[] sqlpara = new SqlParameter[4];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MCODE", MCODE);
            sqlpara[2] = new SqlParameter("@TNO", "0");
            sqlpara[3] = new SqlParameter("@ttime", null);

            return DBHelper.GetDataTable("SP_DASHBOARD", sqlpara);
        }

        public static int ConfirmPayment(string MODE, string MCODE,string TNO)
        {
            SqlParameter[] sqlpara = new SqlParameter[4];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MCODE", MCODE);
            sqlpara[2] = new SqlParameter("@TNO", TNO);
            sqlpara[3] = new SqlParameter("@ttime", null);

            return DBHelper.ExecuteNonQuery("SP_DASHBOARD", sqlpara);
        }


        public static DataTable User_Report( string MODE,string MEMB_CODE)
        {
            SqlParameter[] sqlpara = new SqlParameter[2];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);

            return DBHelper.GetDataTable("Sp_Report", sqlpara);
        }

        public static DataTable User_Report1(string MEMB_CODE, string MODE, string USD_Amount)
        {
            SqlParameter[] sqlpara = new SqlParameter[3];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);
            sqlpara[2] = new SqlParameter("@amount", USD_Amount);
            return DBHelper.GetDataTable("Sp_Report", sqlpara);
        }

        public static DataTable User_Active(string MEMB_CODE, string MODE)
        {
            SqlParameter[] sqlpara = new SqlParameter[2];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);

            return DBHelper.GetDataTable("Sp_Report", sqlpara);
        }

        public static DataTable User_psReport(string MEMB_CODE, string MODE)
        {
            SqlParameter[] sqlpara = new SqlParameter[2];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);

            return DBHelper.GetDataTable("Sp_Report", sqlpara);
        }


        public static DataTable User_Report(int MEMB_CODE, string MODE)
        {
            SqlParameter[] sqlpara = new SqlParameter[2];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);

            return DBHelper.GetDataTable("Sp_Report", sqlpara);
        }

        public static int User_TopUp(string MODE, string memb_code, string amount, string address, string btsAmount, string BTC_Type, string plan_type,string From_ID)
        {
            SqlParameter[] sqlpara = new SqlParameter[8];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@memb_code", memb_code);
            sqlpara[2] = new SqlParameter("@amount", amount);
            sqlpara[3] = new SqlParameter("@address", address);
            sqlpara[4] = new SqlParameter("@btsAmount", btsAmount);
            sqlpara[5] = new SqlParameter("@BTC_Type", BTC_Type);
            sqlpara[6] = new SqlParameter("@plan_type", plan_type);
            sqlpara[7] = new SqlParameter("@From_ID", From_ID);

            return DBHelper.ExecuteNonQuery("SP_TopUpUser", sqlpara);
        }
        public static int User_TopUp1(string MODE, string memb_code, string amount, string address, string btsAmount, string BTC_Type, string plan_type, string From_ID)
        {
            SqlParameter[] sqlpara = new SqlParameter[8];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@memb_code", memb_code);
            sqlpara[2] = new SqlParameter("@amount", amount);
            sqlpara[3] = new SqlParameter("@address", address);
            sqlpara[4] = new SqlParameter("@btsAmount", btsAmount);
            sqlpara[5] = new SqlParameter("@BTC_Type", BTC_Type);
            sqlpara[6] = new SqlParameter("@plan_type", plan_type);
            sqlpara[7] = new SqlParameter("@From_ID", From_ID);

            return DBHelper.ExecuteNonQuery("SP_TopUpUser", sqlpara);
        }


        public static DataTable User_TopUp_History(string MODE, string From_ID)
        {
            SqlParameter[] sqlpara = new SqlParameter[8];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@memb_code", From_ID);
            sqlpara[2] = new SqlParameter("@amount", "0");
            sqlpara[3] = new SqlParameter("@address", null);
            sqlpara[4] = new SqlParameter("@btsAmount", "0");
            sqlpara[5] = new SqlParameter("@BTC_Type", null);
            sqlpara[6] = new SqlParameter("@plan_type", null);
            sqlpara[7] = new SqlParameter("@From_ID", From_ID);

            return DBHelper.GetDataTable("SP_TopUpUser", sqlpara);
        }


        public static int Generate_ROI(string memb_code)
        {
            SqlParameter[] sqlpara = new SqlParameter[1];
            sqlpara[0] = new SqlParameter("@memb_code", memb_code);
            //sqlpara[1] = new SqlParameter("@commit_no", commit_no);
            //sqlpara[2] = new SqlParameter("@amount", amount);

            return DBHelper.ExecuteNonQuery("SP_Generate_ROI", sqlpara);
        }

        public static int AddUserfund(string memb_code, string amount,string From_Id,string MODE)
        {
            SqlParameter[] sqlpara = new SqlParameter[4];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@memb_code", memb_code);
            sqlpara[2] = new SqlParameter("@amount", amount);
            sqlpara[3] = new SqlParameter("@From_Id", From_Id);

            return DBHelper.ExecuteNonQuery("SP_FinanceDtl", sqlpara);
        }


        public static DataTable BINARY_TREEVIEW(int MCODE)
        {
            SqlParameter[] sqlpara = new SqlParameter[1];
            sqlpara[0] = new SqlParameter("@MCODE", MCODE);

            return DBHelper.GetDataTable("BINARY_TREEVIEW_1", sqlpara);
        }


        public static DataTable GetRateByDate(string memb_code)
        {
            SqlParameter[] sqlpara = new SqlParameter[1];

            sqlpara[0] = new SqlParameter("@memb_code", memb_code);
            return DataLayer.DBHelper.GetDataTable("SP_GETRATEBYDATE", sqlpara);
        }

        public static int BINARY_CLOSING()
        {
            return DBHelper.ExecuteNonQuery("BINARY_CLOSING");
        }

        public static int BOOSTER_CLOSING()
        {
            return DBHelper.ExecuteNonQuery("SP_BOOSTORROI");
        }


        public static DataTable GetFirstUser()
        {
            return DBHelper.GetDataTable("select Top 1 Memb_Name,EMAIL,username from Entry order by memb_code asc");
        }

        public static DataTable GetUserDetailsByUsername(string USERNAME)
        {
            return DBHelper.GetDataTable("select * from Entry where username='" + USERNAME + "'");
        }

        public static DataTable GetCountry()
        {
            return DBHelper.GetDataTable("select Top 1 Memb_Name,EMAIL,username from Entry order by memb_code asc");
        }

        public static DataTable GetUserCountry(string CountryName)
        {
            return DBHelper.GetDataTable("SELECT Country,code FROM country_code_list where Country='" + CountryName + "'");
        }

        public static DataTable GetLatestFranchisee()
        {
            return DBHelper.GetDataTable("select * from Franchisee where status='A' order by ttime desc");
        }

        //-----------------------IMPS-------------------//

        public static DataTable User_Report_Account(string MODE, string bk_name)
        {
            SqlParameter[] sqlpara = new SqlParameter[2];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@BANKNAME", bk_name);

            return DBHelper.GetDataTable("Sp_Report", sqlpara);
        }



        public static DataTable ADD_ACCOUNT_DETAILSSS(string MODE, string MEMB_CODE, string ac_name, string ac_no
     , string bk_name, string bk_branch, string bk_ifsc, string debit_card_no, string transit_no, string ac_type, string Mobile_No, string srno
         , string beneficiaryid, string beneficiaryid_corporate, string agent_id, string Pin_Code, string Bank_Address)
        {
            SqlParameter[] sqlpara = new SqlParameter[17];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);
            sqlpara[2] = new SqlParameter("@ac_name", ac_name);
            sqlpara[3] = new SqlParameter("@ac_no", ac_no);
            sqlpara[4] = new SqlParameter("@bk_name", bk_name);
            sqlpara[5] = new SqlParameter("@bk_branch", bk_branch);
            sqlpara[6] = new SqlParameter("@bk_ifsc", bk_ifsc);
            sqlpara[7] = new SqlParameter("@debit_card_no", debit_card_no);
            sqlpara[8] = new SqlParameter("@transit_no", transit_no);
            sqlpara[9] = new SqlParameter("@ac_type", ac_type);
            sqlpara[10] = new SqlParameter("@Mobile_No", Mobile_No);
            sqlpara[11] = new SqlParameter("@srno", srno);
            sqlpara[12] = new SqlParameter("@beneficiaryid", beneficiaryid);
            sqlpara[13] = new SqlParameter("@beneficiaryid_corporate", beneficiaryid_corporate);
            sqlpara[14] = new SqlParameter("@agent_id", agent_id);
            sqlpara[15] = new SqlParameter("@Pin_Code", Pin_Code);
            sqlpara[16] = new SqlParameter("@Bank_Address", Bank_Address);

            return DBHelper.GetDataTable("SP_USER_DETAILS", sqlpara);
        }

        //public static DataTable User_Report(string MODE, string memb_code)
        //{
        //    SqlParameter[] sqlpara = new SqlParameter[2];
        //    sqlpara[0] = new SqlParameter("@MODE", MODE);
        //    sqlpara[1] = new SqlParameter("@memb_code",memb_code);



        //    return DBHelper.GetDataTable("Sp_Report", sqlpara);
        //}


        public static DataTable User_Report_Srno(string MEMB_CODE, string MODE, string SRNO)
        {
            SqlParameter[] sqlpara = new SqlParameter[3];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);
            sqlpara[2] = new SqlParameter("@SRNO", SRNO);

            return DBHelper.GetDataTable("Sp_Report", sqlpara);
        }

        public static DataTable User_Report_Mobile_Acc_No(string MEMB_CODE, string MODE, string Mobile_No, string ac_no)
        {
            SqlParameter[] sqlpara = new SqlParameter[4];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);
            sqlpara[2] = new SqlParameter("@Mobile_No", Mobile_No);
            sqlpara[3] = new SqlParameter("@ac_no", ac_no);

            return DBHelper.GetDataTable("Sp_Report", sqlpara);
        }

        public static DataTable Search_Registration(string MODE, string ErrorMsg, string with_srno)
        {
            SqlParameter[] sqlpara = new SqlParameter[3];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@ErrorMsg", ErrorMsg);
            sqlpara[2] = new SqlParameter("@with_srno", with_srno);

            return DBHelper.GetDataTable("Sp_Search_Registration", sqlpara);
        }



        public static DataTable ADD_ACCOUNT_DETAILS(string MEMB_CODE, string ac_name, string ac_no
, string bk_name, string bk_branch, string bk_ifsc, string debit_card_no, string transit_no, string ac_type
    , string PhonePay, string googlepay, string bhimno, string Paytmno, string aadhar_card, string pan_card,
    string Mobile_No, string beneficiaryid, string beneficiaryid_corporate, string agent_id, string Pin_Code, string Bank_Address)
        {
            SqlParameter[] sqlpara = new SqlParameter[22];
            sqlpara[0] = new SqlParameter("@MODE", "ADDACCOUNT");
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);
            sqlpara[2] = new SqlParameter("@ac_name", ac_name);
            sqlpara[3] = new SqlParameter("@ac_no", ac_no);
            sqlpara[4] = new SqlParameter("@bk_name", bk_name);
            sqlpara[5] = new SqlParameter("@bk_branch", bk_branch);
            sqlpara[6] = new SqlParameter("@bk_ifsc", bk_ifsc);
            sqlpara[7] = new SqlParameter("@debit_card_no", debit_card_no);
            sqlpara[8] = new SqlParameter("@transit_no", transit_no);
            sqlpara[9] = new SqlParameter("@ac_type", ac_type);
            sqlpara[10] = new SqlParameter("@phonepay_no", PhonePay);
            sqlpara[11] = new SqlParameter("@gpay_no", googlepay);
            sqlpara[12] = new SqlParameter("@BHIM_ID", bhimno);
            sqlpara[13] = new SqlParameter("@paytm_no", Paytmno);
            sqlpara[14] = new SqlParameter("@aadhar_card", aadhar_card);
            sqlpara[15] = new SqlParameter("@pan_card", pan_card);
            sqlpara[16] = new SqlParameter("@Mobile_No", Mobile_No);
            sqlpara[17] = new SqlParameter("@beneficiaryid", beneficiaryid);
            sqlpara[18] = new SqlParameter("@beneficiaryid_corporate", beneficiaryid_corporate);
            sqlpara[19] = new SqlParameter("@agent_id", agent_id);
            sqlpara[20] = new SqlParameter("@Pin_Code", Pin_Code);
            sqlpara[21] = new SqlParameter("@Bank_Address", Bank_Address);

            return DBHelper.GetDataTable("SP_USER_DETAILS", sqlpara);
        }




        public static DataTable UserssWithdrawalsIMPS(string srno, string memb_code, string amount, string remark
    , string req_type, string cardno, string account_name, string bankac, string bankname
    , string bankbranch, string micr_code, string ifs_code, string pan_no, string Wallet, string transaction_no, string bank_id
    , string request_mobile_no, string reqt_order_no
    , string api_type, string FUNDTRANSNO, string TRNXNO)
        {
            SqlParameter[] sqlpara = new SqlParameter[21];
            sqlpara[0] = new SqlParameter("@srno", srno);
            sqlpara[1] = new SqlParameter("@memb_code", memb_code);
            sqlpara[2] = new SqlParameter("@amount", amount);
            sqlpara[3] = new SqlParameter("@remark", remark);
            sqlpara[4] = new SqlParameter("@req_type", req_type);
            sqlpara[5] = new SqlParameter("@cardno", cardno);
            sqlpara[6] = new SqlParameter("@account_name", account_name);
            sqlpara[7] = new SqlParameter("@bankac", bankac);
            sqlpara[8] = new SqlParameter("@bankname", bankname);
            sqlpara[9] = new SqlParameter("@bankbranch", bankbranch);
            sqlpara[10] = new SqlParameter("@micr_code", micr_code);
            sqlpara[11] = new SqlParameter("@ifs_code", ifs_code);
            sqlpara[12] = new SqlParameter("@pan_no", pan_no);
            sqlpara[13] = new SqlParameter("@Wallet", Wallet);

            sqlpara[14] = new SqlParameter("@transaction_no", transaction_no);
            sqlpara[15] = new SqlParameter("@bank_id", bank_id);
            sqlpara[16] = new SqlParameter("@request_mobile_no", request_mobile_no);
            sqlpara[17] = new SqlParameter("@reqt_order_no", reqt_order_no);
            sqlpara[18] = new SqlParameter("@api_type", api_type);
            sqlpara[19] = new SqlParameter("@FUNDTRANSNO", FUNDTRANSNO);
            sqlpara[20] = new SqlParameter("@TRNXNO", TRNXNO);

            //return DBHelper.GetDataTable("SP_FinanceDtl", sqlpara);
            return DBHelper.GetDataTable("SP_Withdrawals_IMPS", sqlpara);
        }


        public static DataTable User_Report_Mobile(string MEMB_CODE, string MODE, string Mobile_No)
        {
            SqlParameter[] sqlpara = new SqlParameter[3];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);
            sqlpara[2] = new SqlParameter("@Mobile_No", Mobile_No);

            return DBHelper.GetDataTable("Sp_Report", sqlpara);
        }


        public static DataTable USER_REPORT(string MODE, string Memb_Code)
        {
            SqlParameter[] sqlpara = new SqlParameter[2];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@Memb_Code", Memb_Code);

            return DBHelper.GetDataTable("SP_REPORT", sqlpara);
        }


        public static DataTable User_Report_Mobile_Srno(string MEMB_CODE, string MODE, string Mobile_No, string Srno)
        {
            SqlParameter[] sqlpara = new SqlParameter[4];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", MEMB_CODE);
            sqlpara[2] = new SqlParameter("@Mobile_No", Mobile_No);
            sqlpara[3] = new SqlParameter("@Srno", Srno);

            return DBHelper.GetDataTable("Sp_Report", sqlpara);
        }


        //////----------CALL BACK-----------------///////////

        public static DataTable Get_DataRequest(string MODE, string refnumber)
        {
            SqlParameter[] sqlpara = new SqlParameter[2];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@ClientRefNo", refnumber);

            return DBHelper.GetDataTable("SP_CALLBACK", sqlpara);
        }

        public static DataTable Update_request(string MODE, string content, string srno, string refnumber)
        {
            SqlParameter[] sqlpara = new SqlParameter[5];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@DoopmeRemark", content);
            sqlpara[2] = new SqlParameter("@srno", srno);
            sqlpara[3] = new SqlParameter("@ClientRefNo", refnumber);

            return DBHelper.GetDataTable("SP_CALLBACK", sqlpara);
        }



        public static DataTable Pindetails(string Mode, string PinID, string Memb_Code, string Pin, string Commit_No, string from_Id, string amount)
        {
            SqlParameter[] sqlpara = new SqlParameter[7];
            sqlpara[0] = new SqlParameter("@Mode", Mode);
            sqlpara[1] = new SqlParameter("@PinID", PinID);
            sqlpara[2] = new SqlParameter("@Memb_Code", Memb_Code);
            sqlpara[3] = new SqlParameter("@Pin", Pin);
            sqlpara[4] = new SqlParameter("@Commit_No", Commit_No);
            sqlpara[5] = new SqlParameter("@from_Id", from_Id);
            sqlpara[6] = new SqlParameter("@amount", amount);

            return DBHelper.GetDataTable("SP_Pindetails", sqlpara);
        }


        public static DataTable PinRequestdetails(string Mode, string PinID, string Memb_Code, string Pin
            , string Commit_No, string from_Id, string amount, string Pin_Quantity, string fileAttn1)
        {
            SqlParameter[] sqlpara = new SqlParameter[9];
            sqlpara[0] = new SqlParameter("@Mode", Mode);
            sqlpara[1] = new SqlParameter("@PinID", PinID);
            sqlpara[2] = new SqlParameter("@Memb_Code", Memb_Code);
            sqlpara[3] = new SqlParameter("@Pin", Pin);
            sqlpara[4] = new SqlParameter("@Commit_No", Commit_No);
            sqlpara[5] = new SqlParameter("@from_Id", from_Id);
            sqlpara[6] = new SqlParameter("@amount", amount);
            sqlpara[7] = new SqlParameter("@Pin_Quantity", Pin_Quantity);
            sqlpara[8] = new SqlParameter("@pin_slip", fileAttn1);

            return DBHelper.GetDataTable("SP_Pindetails", sqlpara);
        }

        //public static int User_TopUp(string MODE, string memb_code, string amount, string address, string btsAmount
        //    , string BTC_Type, string plan_type, string From_ID, string PinID)
        //{
        //    SqlParameter[] sqlpara = new SqlParameter[9];
        //    sqlpara[0] = new SqlParameter("@MODE", MODE);
        //    sqlpara[1] = new SqlParameter("@memb_code", memb_code);
        //    sqlpara[2] = new SqlParameter("@amount", amount);
        //    sqlpara[3] = new SqlParameter("@address", address);
        //    sqlpara[4] = new SqlParameter("@btsAmount", btsAmount);
        //    sqlpara[5] = new SqlParameter("@BTC_Type", BTC_Type);
        //    sqlpara[6] = new SqlParameter("@plan_type", plan_type);
        //    sqlpara[7] = new SqlParameter("@From_ID", From_ID);
        //    sqlpara[8] = new SqlParameter("@PInID", PinID);

        //    return DBHelper.ExecuteNonQuery("SP_TopUpUser", sqlpara);
        //}

        public static int AddUserfundRequestinmetamask(string MODE, string memb_code, string amount, string btc_amount, string btc_type, string btcAddress, string txthash)
        {
            SqlParameter[] sqlpara = new SqlParameter[7];
            sqlpara[0] = new SqlParameter("@MODE", MODE);
            sqlpara[1] = new SqlParameter("@MEMB_CODE", memb_code);
            sqlpara[2] = new SqlParameter("@amount", amount);
            sqlpara[3] = new SqlParameter("@BTC_amount", btc_amount);
            sqlpara[4] = new SqlParameter("@BTC_Type", btc_type);
            sqlpara[5] = new SqlParameter("@address", btcAddress);
            sqlpara[6] = new SqlParameter("@txthash", txthash);

            return DBHelper.ExecuteNonQuery("SP_FinanceDtl", sqlpara);
        }

        //-----------------button-------------------//
        public static DataTable Generate_ROI_HOURS(string memb_code , string amount)
        {
            SqlParameter[] sqlpara = new SqlParameter[2];
            sqlpara[0] = new SqlParameter("@memb_code", memb_code);
            sqlpara[1] = new SqlParameter("@amount", amount);

            return DBHelper.GetDataTable("SP_Generate_ROI", sqlpara);
        }

        public static DataTable Generate_ROI_HOURS1(string memb_code, string amount)
        {
            SqlParameter[] sqlpara = new SqlParameter[2];
            sqlpara[0] = new SqlParameter("@memb_code", memb_code);
            sqlpara[1] = new SqlParameter("@amount", amount);

            return DBHelper.GetDataTable("SP_Generate_ROI", sqlpara);
        }

        public static DataTable Generate_ROI_HOURS2(string memb_code, string amount)
        {
            SqlParameter[] sqlpara = new SqlParameter[2];
            sqlpara[0] = new SqlParameter("@memb_code", memb_code);
            sqlpara[1] = new SqlParameter("@amount", amount);

            return DBHelper.GetDataTable("SP_Generate_ROI", sqlpara);
        }

        public static DataTable Generate_Rental_Income(string MODE, string memb_code, string amount)
        {
            SqlParameter[] sqlpara = new SqlParameter[3];
            sqlpara[0] = new SqlParameter("@MODE", MODE); 
            sqlpara[1] = new SqlParameter("@memb_code", memb_code);
            sqlpara[2] = new SqlParameter("@amount", amount);

            return DBHelper.GetDataTable("Sp_SetRentalIncome", sqlpara);
        }
    }


   
}