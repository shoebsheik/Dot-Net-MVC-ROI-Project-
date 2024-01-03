using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UniqueTrade_App.DBEntity;
using System.Web.Configuration;
using System.Web;

namespace UniqueTrade_App.CommanFunction
{
    [Serializable()]
    public class UserInfo
    {

        #region Public Varaible
        public string UserID = "";
        public string User_Name = "";
        public string EmailID = "";
        public string userType = "";
        public bool IsActive = false;
        public bool Isvalid_Email = false;
        public bool IsInst_Valid = false;


        public string memb_codeEncrypt = "";

        public string mposition = "";
        public string memb_code = "";
        public string memb_name = "";
        public string username = "";
        public string M_COUNTRY = "";
        public string spon_code = "";
        public string plac_code = "";
        public string mpwd = "";
        public string RV_Code = "";

        public string Address1 = string.Empty;
        public string Pin_Code = string.Empty;
        public string MembName_L = string.Empty;
        public string dbo1 = string.Empty;
        public string dbo = "";
        public string Gender = "";
        public string Mobile_No = "";
        public string Aadhar_No = "";
        
        public string City = "";
        public string Address2 = "";


        public string accStatus = "";
        public string btc_ac = "";
        public string eth_ac = "";
        public string ltc_ac = "";
       

        public string ac_name = "";
        public string ac_no = "";
        public string bk_name = "";
        public string bk_branch = "";
        public string bk_ifsc = "";
        public string debit_card_no = "";
        public string transit_no = "";
        public string ac_type = "";
        public string Phone_Pay = "";
        public string Google_Pay = "";
        public string BHIM_ID = "";
        public string Paytm_no = "";

        public string M_Status = "";
        public string Tempf_Status = "";
        public string Countrycode = "";
        public string last_log_in = "";


        public string sp_name = "";
        public string sp_Email = "";
        public string sp_country = "";
        public string sp_mobile_no = "";
        public string sp_user = "";


        public string level_No = "";
        public string level_text = "";

        public string joining_date = "";
        public string activation_date = "";
        public string authrised = string.Empty;

        public string left_referral = "";
        public string Newslist = "";
        public string right_referral = "";

        public string actioname = "";
        public string controllerName = "";
        public string languageCode = "";

        public string aadhar_card = "";
        public string pan_card = "";
        public string PinID = "";

        public string Website = "";

        #endregion Public Varaible

        private bool _isAuthenticated = false;

        public bool IsAuthenticated { get { return _isAuthenticated; } }

        public UserInfo(string userid, string usertype)
        {
            if (IsSuperUser(userid))
            {
                SetSuperValues(userid);
            }
            else
            {
                //DataTable dt = UserManager.GetUserLogin(userid);
                DataTable dt = UserManager.User_LOGIN(userid);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    SetValues(row);
                }
            }
        }

        public UserInfo(string userid, string password, string UserStatus, string usertype)
        {
            if (IsSuperUser(userid, password))
            {
                SetSuperValues(userid);
            }
            else
            {
                if (string.Equals(usertype, "U"))
                {
                    DataTable dt = UserManager.User_LOGIN(userid);
                    if (dt.Rows.Count > 0)
                    {
                        if (string.Equals(password, dt.Rows[0]["mpwd"].ToString()))
                        {
                            DataRow row = dt.Rows[0];
                            SetValues(row);
                        }
                        else if (!string.Equals(password, dt.Rows[0]["mpwd"].ToString()))
                        {
                            UserID = dt.Rows[0]["memb_code"].ToString();
                        }
                    }
                }
            }
        }

        private void SetSuperValues(string userid)
        {
            UserID = "0";
            _isAuthenticated = true;
            User_Name = "Super Admin";
            EmailID = "mavrodibox@gmail.com";
            userType = "S";
            IsActive = true;
            //_emailDrafts = EmailDraftManager.Get(UserID);
        }

        private bool IsSuperUser(string userid, string pwd)
        {
            if (userid.Equals(SuperUser.UserID) && pwd.Equals(SuperUser.Pwd))//SuperUser
                return true;
            else return false;
        }

        private bool IsSuperUser(string userid)
        {
            if (userid.Equals(SuperUser.UserID))
                return true;
            else return false;
        }

        private void SetValues(DataRow row)
        {
            if (row != null)
            {
                UserID = row["memb_code"].ToString();

                mposition = row["mposition"] != null && row["mposition"] != DBNull.Value ? row["mposition"].ToString().Trim() : "";
                memb_code = row["memb_code"] != null && row["memb_code"] != DBNull.Value ? row["memb_code"].ToString().Trim() : "";
                memb_name = row["memb_name"] != null && row["memb_name"] != DBNull.Value ? row["memb_name"].ToString().Trim() : "";
                username = row["username"] != null && row["username"] != DBNull.Value ? row["username"].ToString().Trim() : "";
                M_COUNTRY = row["M_COUNTRY"] != null && row["M_COUNTRY"] != DBNull.Value ? row["M_COUNTRY"].ToString().Trim() : "";
                Countrycode = row["MembName_M"] != null && row["MembName_M"] != DBNull.Value ? row["MembName_M"].ToString().Trim() : "";
                spon_code = row["spon_code"] != null && row["spon_code"] != DBNull.Value ? row["spon_code"].ToString().Trim() : "";
                plac_code = row["plac_code"] != null && row["plac_code"] != DBNull.Value ? row["plac_code"].ToString().Trim() : "";
                mpwd = row["mpwd"] != null && row["mpwd"] != DBNull.Value ? row["mpwd"].ToString() : "";
                RV_Code = row["RV_Code"] != null && row["RV_Code"] != DBNull.Value ? row["RV_Code"].ToString() : "";

                memb_codeEncrypt = Crypto.Encrypt(memb_code, System.Text.Encoding.Unicode);

                left_referral = "" + HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Home/SignUp?REGUSER="+ username;
                // left_referral = "" + WebConfigurationManager.AppSettings["domainName"] + "/"+username+"L";

                //right_referral = "" + WebConfigurationManager.AppSettings["domainName"] + "/" + username + "R";
                Website = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                userType = "U";
                User_Name = row["username"] != null && row["username"] != DBNull.Value ? row["username"].ToString().Trim() : "";
                EmailID = row["EMail"] != null && row["EMail"] != DBNull.Value ? row["EMail"].ToString().Trim() : "";
                _isAuthenticated = true;

                Gender = row["Gender"] != null && row["Gender"] != DBNull.Value ? row["Gender"].ToString().Trim() : "";
                Mobile_No = row["Mobile_No"] != null && row["Mobile_No"] != DBNull.Value ? row["Mobile_No"].ToString().Trim() : "";

                //level_text = row["Level_Text"] != null && row["Level_Text"] != DBNull.Value ? row["Level_Text"].ToString().Trim() : "";

                authrised = row["authrised"] != null && row["authrised"] != DBNull.Value ? row["authrised"].ToString().Trim() : "";

                Address1 = row["Address1"] != null && row["Address1"] != DBNull.Value ? row["Address1"].ToString().Trim() : "";
                Pin_Code = row["Pin_Code"] != null && row["Pin_Code"] != DBNull.Value ? row["Pin_Code"].ToString().Trim() : "";
                MembName_L = row["MembName_L"] != null && row["MembName_L"] != DBNull.Value ? row["MembName_L"].ToString().Trim() : "";

                dbo = row["dboS"] != null && row["dboS"] != DBNull.Value ? row["dboS"].ToString().Trim() : "";
                dbo1 = row["dboE"] != null && row["dboE"] != DBNull.Value ? row["dboE"].ToString().Trim() : "";

                Address2 = row["Address2"] != null && row["Address2"] != DBNull.Value ? row["Address2"].ToString().Trim() : "";
                City = row["City"] != null && row["City"] != DBNull.Value ? row["City"].ToString().Trim() : "";

                M_Status = row["M_Status"] != null && row["M_Status"] != DBNull.Value ? row["M_Status"].ToString().Trim() : "";
                Tempf_Status = row["tempf"] != null && row["tempf"] != DBNull.Value ? row["tempf"].ToString().Trim() : "";

                if (!string.IsNullOrEmpty(row["last_log_in"].ToString()))
                {
                    last_log_in = Convert.ToDateTime(row["last_log_in"].ToString()).ToString("hh:mm");
                }
                if (!string.IsNullOrEmpty(row["Reg_date"].ToString().Trim()))
                {
                    joining_date = Convert.ToDateTime(row["Reg_date"].ToString().Trim()).ToString("dd MMMM yyyy");
                }
                if (!string.IsNullOrEmpty(row["auth_date"].ToString().Trim()))
                {
                    activation_date = Convert.ToDateTime(row["auth_date"].ToString().Trim()).ToString("dd MMMM yyyy");
                }
                else
                {
                    activation_date = null;
                }

                DataTable dt = UserManager.GetAccountDetails(memb_code);
                if (dt.Rows.Count > 0)
                {
                    accStatus = "1";
                    btc_ac = dt.Rows[0]["btc_ac"].ToString().Trim();
                    eth_ac = dt.Rows[0]["eth_ac"].ToString().Trim();
                    ltc_ac = dt.Rows[0]["ltc_ac"].ToString().Trim();

                    ac_name = dt.Rows[0]["ac_name"].ToString().Trim();
                    ac_no = dt.Rows[0]["ac_no"].ToString().Trim();
                    bk_name = dt.Rows[0]["bk_name"].ToString().Trim();
                    bk_branch = dt.Rows[0]["bk_branch"].ToString().Trim();
                    bk_ifsc = dt.Rows[0]["bk_ifsc"].ToString().Trim();
                    debit_card_no = dt.Rows[0]["debit_card_no"].ToString().Trim();
                    ac_type = dt.Rows[0]["ac_type"].ToString().Trim();
                    Phone_Pay = dt.Rows[0]["Phone_Pay"].ToString().Trim();
                    Google_Pay = dt.Rows[0]["Google_Pay"].ToString().Trim();
                    BHIM_ID = dt.Rows[0]["BHIM_ID"].ToString().Trim();
                    Paytm_no = dt.Rows[0]["Paytm_no"].ToString().Trim();
                }
                else
                {
                    accStatus = "0";
                }


                DataTable dtSp = UserManager.GetSponserDetails(spon_code);
                if (dtSp.Rows.Count > 0)
                {
                    sp_country = dtSp.Rows[0]["sp_country"].ToString().Trim();
                    sp_Email = dtSp.Rows[0]["sp_Email"].ToString().Trim();
                    sp_mobile_no = dtSp.Rows[0]["sp_mobile_no"].ToString().Trim();
                    sp_name = dtSp.Rows[0]["sp_name"].ToString().Trim();
                    sp_user = dtSp.Rows[0]["sp_user"].ToString().Trim();
                }
            }
        }

        private void SetValuesAdmin(DataRow row)
        {
            if (row != null)
            {
                UserID = row["srno"].ToString();
                username = row["user_id"] != null && row["user_id"] != DBNull.Value ? row["user_id"].ToString().Trim() : "";
                mpwd = row["pwd"] != null && row["pwd"] != DBNull.Value ? row["pwd"].ToString() : "";

                userType = "A";
            }
        }
    }

    internal class SuperUser
    {
        internal const string UserID = "supermavrodibox";
        internal const string Pwd = "super@007";
    }
}