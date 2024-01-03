using System;
using System.Data;
using UniqueTrade_App.CommanFunction;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Configuration;
using UniqueTrade_App.CommonFunction;
using UniqueTrade_App.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace UniqueTrade_App.CommanFunction
{
    public class BasePage : Controller
    {
        protected UserInfo userInfo = null;

        public void Page_PreInit(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(userInfo.UserID))
                RedirectToAction("Index", "Home");
        }

        public BasePage()
        {
            userInfo = Common.CurrentUserInfo;

            if (Common.CurrentPageName != "changeculture" && userInfo != null)
            {
                userInfo.actioname = Common.CurrentPageName;
                userInfo.controllerName = Common.CurrentControllerName;
            }

            if (userInfo == null)
            {
                return;
            }
        }

        public string GetSmallDateValue(string strDt)
        {
            if (String.IsNullOrEmpty(strDt))
                return "";
            DateTime userSelectedDate = Convert.ToDateTime(strDt);
            return userSelectedDate.Date.ToString("dd/MM/yyyy");
        }

        public string GetSmallDateValue2(string strDt)
        {
            if (String.IsNullOrEmpty(strDt))
                return "";
            DateTime userSelectedDate = Convert.ToDateTime(strDt);
            return userSelectedDate.Date.ToString("MM/dd/yyyy");
        }

        public string GetSmallDateValue3(string strDt)
        {
            if (String.IsNullOrEmpty(strDt))
                return "";
            DateTime userSelectedDate = Convert.ToDateTime(strDt);
            return userSelectedDate.Date.ToString("yyyy-MM-dd");
        }

        public string getRandomPassword()
        {

            string allowedChars = "";

            // allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";

            // allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";

            allowedChars = "1,2,3,4,5,6,7,8,9,0";

            char[] sep = { ',' };

            string[] arr = allowedChars.Split(sep);

            string passwordString = "";

            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < 6; i++)
            {

                temp = arr[rand.Next(0, arr.Length)];

                passwordString += temp;

            }

            return passwordString;

        }

        public string converttimetoint(string inputtime)
        {
            string outputtime = "10.30 am";

            string[] mystring = inputtime.Split(' ');

            if (String.Equals(mystring[1].ToString(), "PM") || String.Equals(mystring[1].ToString(), "pm"))
            {
                string[] substring = mystring[0].Split(':');
                outputtime = (12 + int.Parse(substring[0])).ToString() + substring[1];
            }
            else
            {
                string[] substring = mystring[0].Split(':');
                outputtime = substring[0] + substring[1];
            }

            return outputtime;
        }

        public string CreateRandomPassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }



        public string CreateRandomPin(int length)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public BTCAddressModel GetBTCAddress(string currancy)
        {
            BTCAddressModel AM = new BTCAddressModel();
            try
            {
                //HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ////string url = "https://api.paybear.io/v2/" + Currency + "/payment/http%3A%2F%2Fputsreq.com%2FUv8u7ofxXDWVoaVawDWd/?token=" + WebConfigurationManager.AppSettings["Coinpayment_Private_Key"];
                //string url = "https://api.paybear.io/v2/" + currancy + "/payment/http%3A%2F%2Ftario.ontario.network%2F?token=" + WebConfigurationManager.AppSettings["Coinpayment_Private_Key"];
                //HttpResponseMessage response = client.GetAsync(url).Result;
                //if (response.IsSuccessStatusCode)
                //{
                //    string JSON = response.Content.ReadAsStringAsync().Result;
                //    var obj = JsonConvert.DeserializeObject<RootObject1>(JSON);
                //    Data1 objdata = obj.data;
                //    Data1 objDATA = new Data1();
                //    AM.BTCAddress = objdata.address;
                //    AM.result = "ok";
                //}
                //return _Address;
                //int dt = 0;
                CoinPayments cp = new CoinPayments(WebConfigurationManager.AppSettings["Coinpayment_Private_Key"], WebConfigurationManager.AppSettings["Coinpayment_Public_Key"]);
                Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
                var datacp = dict;
                if (currancy == "BTC")
                {
                    datacp = cp.CallAPI("BTC");

                }
                else if (currancy == "ETH")
                {
                    datacp = cp.CallAPI("ETH");
                }
                else if (currancy == "TRX")
                {
                    datacp = cp.CallAPI("TRX");
                }
                var error = datacp["error"];
                if (error == "ok")
                {
                    var address = datacp["result"]["address"];
                    var pubkey = WebConfigurationManager.AppSettings["Coinpayment_Public_Key"].ToString();
                    var msg = "ok";
                    AM.BTCAddress = address;
                    AM.pubkey = pubkey;
                    AM.result = msg;
                }

                //HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //string url = "http://api.logicaltechconsultants.com/Home/GetAddress?pub_key=" + WebConfigurationManager.AppSettings["Coinpayment_Public_Key"] + "&privte_key=" + WebConfigurationManager.AppSettings["Coinpayment_Private_Key"] + "&currency=" + currancy + "";
                //HttpResponseMessage response = client.GetAsync(url).Result;
                //if (response.IsSuccessStatusCode)
                //{
                //    string JSON = response.Content.ReadAsStringAsync().Result;
                //    JSON = JSON.Replace(@"""", "");
                //    if (!string.IsNullOrEmpty(JSON))
                //    {
                //        var address = JSON;
                //        var pubkey = WebConfigurationManager.AppSettings["Coinpayment_Public_Key"].ToString();
                //        var msg = "ok";
                //        AM.BTCAddress = JSON;
                //        AM.pubkey = pubkey;
                //        AM.result = msg;
                //    }
                //}

            }
            catch (Exception ex)
            {

            }
            return AM;
        }

        public  BTCAddressModel GetCoinpaymentAddress(string currancy)
        {
            BTCAddressModel AM = new BTCAddressModel();
            try
            {
                CoinPayments cp = new CoinPayments(WebConfigurationManager.AppSettings["Coinpayment_Private_Key"], WebConfigurationManager.AppSettings["Coinpayment_Public_Key"]);
                Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
                var datacp = dict;
                //if (currancy == "BTC")
                //{
                //    datacp = cp.CallAPI("BTC");

                //}
                //else if (currancy == "ETH")
                //{
                //    datacp = cp.CallAPI("ETH");
                //}
                //else if (currancy == "TRX")
                //{
                //    datacp = cp.CallAPI("TRX");
                //}
               // datacp = cp.CallAPI(currancy);
                datacp = cp.CallAPIFOR_CALLBACK_ADDRESS(currancy);

                var error = datacp["error"];
                if (error == "ok")
                {
                    var address = datacp["result"]["address"];
                    var pubkey = WebConfigurationManager.AppSettings["Coinpayment_Public_Key"].ToString();
                    var msg = "ok";
                    AM.BTCAddress = address;
                    AM.pubkey = pubkey;
                    AM.result = msg;
                }              

            }
            catch (Exception ex)
            {

            }
            return AM;
        }

        public BTCAddressModel GetCreateTransaction(string currancy, string currancyamount)
        {
            BTCAddressModel AM = new BTCAddressModel();
            try
            {
                CoinPayments cp = new CoinPayments(WebConfigurationManager.AppSettings["Coinpayment_Private_Key"], WebConfigurationManager.AppSettings["Coinpayment_Public_Key"]);
                Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
                var datacp = dict;
                //if (currancy == "BTC")
                //{
                //    datacp = cp.CallAPI("BTC");

                //}
                //else if (currancy == "ETH")
                //{
                //    datacp = cp.CallAPI("ETH");
                //}
                //else if (currancy == "TRX")
                //{
                //    datacp = cp.CallAPI("TRX");
                //}
                // datacp = cp.CallAPI(currancy);
                datacp = cp.create_transaction(currancy, userInfo.EmailID, userInfo.username, currancyamount);

                var error = datacp["error"];
                if (error == "ok")
                {
                    var address = datacp["result"]["address"];
                    var amount = datacp["result"]["amount"];
                    var txn_id = datacp["result"]["txn_id"];
                    var confirms_needed = datacp["result"]["confirms_needed"];
                    var timeout = datacp["result"]["timeout"];
                    var checkout_url = datacp["result"]["checkout_url"];
                    var status_url = datacp["result"]["status_url"];
                    var qrcode_url = datacp["result"]["qrcode_url"];
                    var pubkey = WebConfigurationManager.AppSettings["Coinpayment_Public_Key"].ToString();
                    var msg = "ok";
                    AM.BTCAddress = address;
                    AM.pubkey = pubkey;
                    AM.result = msg;
                    AM.txn_id = txn_id;
                    AM.confirms_needed = confirms_needed;
                    AM.timeout = timeout.ToString();
                    AM.checkout_url = checkout_url;
                    AM.status_url = status_url;
                    AM.qrcode_url = qrcode_url;
                }

            }
            catch (Exception ex)
            {

            }
            return AM;
        }

        public BTCAddressModel Gettransaction_confirm(string transaction_no)
        {
            BTCAddressModel AM = new BTCAddressModel();
            try
            {
                CoinPayments cp = new CoinPayments(WebConfigurationManager.AppSettings["Coinpayment_Private_Key"], WebConfigurationManager.AppSettings["Coinpayment_Public_Key"]);
                Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
                var datacp = dict;
               
                datacp = cp.transaction_confirm(transaction_no);

                var error = datacp["error"];
                if (error == "ok")
                {
                    var payment_address = datacp["result"][transaction_no]["payment_address"];
                    var time_created = datacp["result"][transaction_no]["time_created"];
                    var time_expires = datacp["result"][transaction_no]["time_expires"];
                    var status = datacp["result"][transaction_no]["status"];
                    var status_text = datacp["result"][transaction_no]["status_text"];
                    var type = datacp["result"][transaction_no]["type"];
                    var coin = datacp["result"][transaction_no]["coin"];
                    var amount = datacp["result"][transaction_no]["amount"];
                    var amountf = datacp["result"][transaction_no]["amountf"];
                    var received = datacp["result"][transaction_no]["received"];                   
                    var receivedf = datacp["result"][transaction_no]["receivedf"];
                    var recv_confirms = datacp["result"][transaction_no]["recv_confirms"];
                    
                    //var amount = datacp["result"]["amount"];
                    //var txn_id = datacp["result"]["txn_id"];
                    //var confirms_needed = datacp["result"]["confirms_needed"];
                    //var timeout = datacp["result"]["timeout"];
                    //var checkout_url = datacp["result"]["checkout_url"];
                    //var status_url = datacp["result"]["status_url"];
                    //var qrcode_url = datacp["result"]["qrcode_url"];
                    var pubkey = WebConfigurationManager.AppSettings["Coinpayment_Public_Key"].ToString();
                    var msg = "ok";

                    AM.result = msg;
                    AM.payment_address = payment_address;
                    AM.time_created = time_created;
                    AM.time_expires = time_expires;
                    AM.status = status.ToString();
                    AM.status_text = status_text;
                    AM.type = type;
                    AM.coin = coin;
                    AM.amount = amount.ToString();
                    AM.amountf = amountf;
                    AM.received = received.ToString();
                    AM.receivedf = receivedf;
                    AM.recv_confirms = recv_confirms.ToString();
                    
                   
                }

            }
            catch (Exception ex)
            {

            }
            return AM;
        }
        public string GenerateRandomOTP(int iOTPLength)
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();
            for (int i = 0; i < iOTPLength; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }

        public string OTPMail(string Enquirer, string Subject1, string requestCode)
        {
            string msg = string.Empty;

            string ToMail, CCMail, FromMail, Subject;
            string Body = string.Empty;
            //Enquirer = dt.Rows[0]["Email"].ToString();
            CCMail = "";
            FromMail = "";

            Subject = "" + Subject1.ToUpper() + "MI-COIN";

            Body = "<!DOCTYPE html>"
                    + "<html>"
                    + "<head>"
                    + "    <title>" + Subject1.ToUpper() + "</title>"
                    + "</head>"
                    + "<body>"
                    + "    <div style='width:500px; margin:auto; background-color:#fff6;margin-top:50px;padding: 10px 10px 10px 10px;border-radius: 6px 7px;box-shadow: 0px 1px 11px 4px #2a220733;background:url('http://uat35.beechtreeitsolutions.com/Content/Front/images/logo-dark.png');background-position: 50% 50%;'>"
                    + "    <h2 style='text-align:center;width: 500px;'>Request Code for " + Subject1 + " MI-COIN</h2>"
                    + "    <div style='width:500px;'>"
                    + "        <h4 style='margin-left: 26px;text-align:center;'>Hi, Sir / Madam</h4>"
                    + "        <hr>"
                    + "        <div style='width:500px;'>"
                    + "            <br>"
                    + "            <p style='text-align:center;'>Your Login Request Code is : <strong>" + requestCode + "</strong></p>"
                    + "            <br>"
                    + "        </div>"
                    + "        <div style='text-align:center;'>"
                    + "        <img src='" + WebConfigurationManager.AppSettings["domainName"] + "/Content/userDesign/images/logo.png' style='height:65px;width:239px;'>"
                    + "        </div>"
                    + "        <p style='text-align:center;'>Thank You..<p>"
                    + "    </div>"
                    + "    </div>"
                    + "</body>"
                    + "</html>";


            string Status = SendMail.mailMain(Enquirer, Body, Subject);
            msg = Status;
            return msg;
        }


        public string EmailWith(string Enquirer, string Subject1, string btcAmount, string Amount,string type,string trno)
        {
            string msg = string.Empty;

            string ToMail, CCMail, FromMail, Subject;
            string Body = string.Empty;
            //Enquirer = dt.Rows[0]["Email"].ToString();
            CCMail = "";
            FromMail = "";

            Subject = "" + Subject1.ToUpper() + "TradexGuru";

            Body = "<!DOCTYPE html>"
                    + "<html>"
                    + "<head>"
                    + "    <title>" + Subject1.ToUpper() + "</title>"
                    + "</head>"
                    + "<body>"
                    + "    <div style='width:500px; margin:auto; background-color:#fff6;margin-top:50px;padding: 10px 10px 10px 10px;border-radius: 6px 7px;box-shadow: 0px 1px 11px 4px #2a220733;background:url('http://uat35.beechtreeitsolutions.com/Content/Front/images/logo-dark.png');background-position: 50% 50%;'>"
                    + "    <h2 style='text-align:center;width: 500px;'>Withdrwal Request TradexGuru</h2>"
                    + "    <div style='width:500px;'>"
                    + "        <h4 style='margin-left: 26px;text-align:center;'>Hi,"+ userInfo.username +"</h4>"
                    + "        <hr>"
                    + "        <div style='width:500px;'>"
                    + "            <br>"
                    + "            <p style='text-align:center;'>Your have Successfully Withdrwal : <strong> $ " + Amount + "</strong></p>"
                    + "            <p style='text-align:center;'>and "+ type + " : "+ btcAmount + "</p>"
                    + "            <p style='text-align:center;'>and your Transaction No. is : CTS" + trno + "</p>"
                    + "            <br>"
                    + "        </div>"
                    //+ "        <div style='text-align:center;'>"
                    //+ "        <img src='" + WebConfigurationManager.AppSettings["domainName"] + "/Content/userDesign/images/logo.png' style='height:65px;width:239px;'>"
                    //+ "        </div>"
                    + "        <p style='text-align:center;'>Thank You..<p>"
                    + "    </div>"
                    + "    </div>"
                    + "</body>"
                    + "</html>";


            string Status = SendMail.mailMain(Enquirer, Body, Subject);
            msg = Status;
            return msg;
        }

        public string EmailFUnd(string Enquirer, string Subject1, string username, string Amount)
        {
            string msg = string.Empty;

            string ToMail, CCMail, FromMail, Subject;
            string Body = string.Empty;
            //Enquirer = dt.Rows[0]["Email"].ToString();
            CCMail = "";
            FromMail = "";

            Subject = " " + Subject1.ToUpper() + "TradexGuru";

            Body = "<!DOCTYPE html>"
                    + "<html>"
                    + "<head>"
                    + "    <title>" + Subject1.ToUpper() + "</title>"
                    + "</head>"
                    + "<body>"
                    + "    <div style='width:500px; margin:auto; background-color:#fff6;margin-top:50px;padding: 10px 10px 10px 10px;border-radius: 6px 7px;box-shadow: 0px 1px 11px 4px #2a220733;background:url('http://uat35.beechtreeitsolutions.com/Content/Front/images/logo-dark.png');background-position: 50% 50%;'>"
                    + "    <h2 style='text-align:center;width: 500px;'>Fund Transfer TradexGuru</h2>"
                    + "    <div style='width:500px;'>"
                    + "        <h4 style='margin-left: 26px;text-align:center;'>Hi," + userInfo.memb_name + "</h4>"
                    + "        <hr>"
                    + "        <div style='width:500px;'>"
                    + "            <br>"
                    + "            <p style='text-align:center;'>Your have Successfully Transfer Fund $"+ Amount + " to <strong> " + username + "</strong></p>"
                    + "            <br>"
                    + "        </div>"
                    //+ "        <div style='text-align:center;'>"
                    //+ "        <img src='" + WebConfigurationManager.AppSettings["domainName"] + "/Content/userDesign/images/logo.png' style='height:65px;width:239px;'>"
                    //+ "        </div>"
                    + "        <p style='text-align:center;'>Thank You..<p>"
                    + "    </div>"
                    + "    </div>"
                    + "</body>"
                    + "</html>";


            string Status = SendMail.mailMain(Enquirer, Body, Subject);
            msg = Status;
            return msg;
        }

        public string EmailTopup(string Enquirer, string Subject1, string username, string Amount)
        {
            string msg = string.Empty;

            string ToMail, CCMail, FromMail, Subject;
            string Body = string.Empty;
            //Enquirer = dt.Rows[0]["Email"].ToString();
            CCMail = "";
            FromMail = "";

            Subject = "" + Subject1.ToUpper() + "TradexGuru";

            Body = "<!DOCTYPE html>"
                    + "<html>"
                    + "<head>"
                    + "    <title>" + Subject1.ToUpper() + "</title>"
                    + "</head>"
                    + "<body>"
                    + "    <div style='width:500px; margin:auto; background-color:#fff6;margin-top:50px;padding: 10px 10px 10px 10px;border-radius: 6px 7px;box-shadow: 0px 1px 11px 4px #2a220733;background:url('http://uat35.beechtreeitsolutions.com/Content/Front/images/logo-dark.png');background-position: 50% 50%;'>"
                    + "    <h2 style='text-align:center;width: 500px;'>Fund Transfer TradexGuru</h2>"
                    + "    <div style='width:500px;'>"
                    + "        <h4 style='margin-left: 26px;text-align:center;'>Hi," + userInfo.username + "</h4>"
                    + "        <hr>"
                    + "        <div style='width:500px;'>"
                    + "            <br>"
                    + "            <p style='text-align:center;'>Your have Successfully Top Up $" + Amount + " to <strong> " + username + "</strong></p>"
                    + "            <br>"
                    + "        </div>"
                    //+ "        <div style='text-align:center;'>"
                    //+ "        <img src='" + WebConfigurationManager.AppSettings["domainName"] + "/Content/userDesign/images/logo.png' style='height:65px;width:239px;'>"
                    //+ "        </div>"
                    + "        <p style='text-align:center;'>Thank You..<p>"
                    + "    </div>"
                    + "    </div>"
                    + "</body>"
                    + "</html>";


            string Status = SendMail.mailMain(Enquirer, Body, Subject);
            msg = Status;
            return msg;
        }

        public string EmailUpdate(string Enquirer, string Subject1)
        {
            string msg = string.Empty;

            string ToMail, CCMail, FromMail, Subject;
            string Body = string.Empty;
            //Enquirer = dt.Rows[0]["Email"].ToString();
            CCMail = "";
            FromMail = "";

            Subject = "" + Subject1.ToUpper() + "TradexGuru";

            Body = "<!DOCTYPE html>"
                    + "<html>"
                    + "<head>"
                    + "    <title>" + Subject1.ToUpper() + "</title>"
                    + "</head>"
                    + "<body>"
                    + "    <div style='width:500px; margin:auto; background-color:#fff6;margin-top:50px;padding: 10px 10px 10px 10px;border-radius: 6px 7px;box-shadow: 0px 1px 11px 4px #2a220733;background:url('http://uat35.beechtreeitsolutions.com/Content/Front/images/logo-dark.png');background-position: 50% 50%;'>"
                    + "    <h2 style='text-align:center;width: 500px;'>Fund Transfer TradexGuru</h2>"
                    + "    <div style='width:500px;'>"
                    + "        <h4 style='margin-left: 26px;text-align:center;'>Hi," + userInfo.username + "</h4>"
                    + "        <hr>"
                    + "        <div style='width:500px;'>"
                    + "            <br>"
                    + "            <p style='text-align:center;'>Your have Successfully Update Profile</p>"
                    + "            <br>"
                    + "        </div>"
                    //+ "        <div style='text-align:center;'>"
                    //+ "        <img src='" + WebConfigurationManager.AppSettings["domainName"] + "/Content/userDesign/images/logo.png' style='height:65px;width:239px;'>"
                    //+ "        </div>"
                    + "        <p style='text-align:center;'>Thank You..<p>"
                    + "    </div>"
                    + "    </div>"
                    + "</body>"
                    + "</html>";


            string Status = SendMail.mailMain(Enquirer, Body, Subject);
            msg = Status;
            return msg;
        }

        public string EmailPChange(string Enquirer, string Subject1,string password)
        {
            string msg = string.Empty;

            string ToMail, CCMail, FromMail, Subject;
            string Body = string.Empty;
            //Enquirer = dt.Rows[0]["Email"].ToString();
            CCMail = "";
            FromMail = "";

            Subject = "" + Subject1.ToUpper() + "TradexGuru";

            Body = "<!DOCTYPE html>"
                    + "<html>"
                    + "<head>"
                    + "    <title>" + Subject1.ToUpper() + "</title>"
                    + "</head>"
                    + "<body>"
                    + "    <div style='width:500px; margin:auto; background-color:#fff6;margin-top:50px;padding: 10px 10px 10px 10px;border-radius: 6px 7px;box-shadow: 0px 1px 11px 4px #2a220733;background:url('http://uat35.beechtreeitsolutions.com/Content/Front/images/logo.png');background-position: 50% 50%;'>"
                    + "    <h2 style='text-align:center;width: 500px;'>Fund Transfer TradexGuru</h2>"
                    + "    <div style='width:500px;'>"
                    + "        <h4 style='margin-left: 26px;text-align:center;'>Hi," + userInfo.username + "</h4>"
                    + "        <hr>"
                    + "        <div style='width:500px;'>"
                    + "            <br>"
                    + "            <p style='text-align:center;'>Your have Successfully Change PAssword. Your New Password :"+ password + "</p>"
                    + "            <br>"
                    + "        </div>"
                    //+ "        <div style='text-align:center;'>"
                    //+ "        <img src='" + WebConfigurationManager.AppSettings["domainName"] + "/Content/userDesign/images/logo.png' style='height:65px;width:239px;'>"
                    //+ "        </div>"
                    + "        <p style='text-align:center;'>Thank You..<p>"
                    + "    </div>"
                    + "    </div>"
                    + "</body>"
                    + "</html>";


            string Status = SendMail.mailMain(Enquirer, Body, Subject);
            msg = Status;
            return msg;
        }

        public string EmailInv(string Enquirer, string Subject1, string amount,string btcamount,string address,string type)
        {
            string msg = string.Empty;

            string ToMail, CCMail, FromMail, Subject;
            string Body = string.Empty;
            //Enquirer = dt.Rows[0]["Email"].ToString();
            CCMail = "";
            FromMail = "";

            Subject = "" + Subject1.ToUpper() + "TradexGuru";

            Body = "<!DOCTYPE html>"
                    + "<html>"
                    + "<head>"
                    + "    <title>" + Subject1.ToUpper() + "</title>"
                    + "</head>"
                    + "<body>"
                    + "    <div style='width:500px; margin:auto; background-color:#fff6;margin-top:50px;padding: 10px 10px 10px 10px;border-radius: 6px 7px;box-shadow: 0px 1px 11px 4px #2a220733;background:url('http://uat35.beechtreeitsolutions.com/Content/Front/images/logo-dark.png');background-position: 50% 50%;'>"
                    + "    <h2 style='text-align:center;width: 500px;'>Purchase Package TradexGuru</h2>"
                    + "    <div style='width:500px;'>"
                    + "        <h4 style='margin-left: 26px;text-align:center;'>Hi," + userInfo.username + "</h4>"
                    + "        <hr>"
                    + "        <div style='width:500px;'>"
                    + "            <br>"
                    + "            <p style='text-align:center;'>Your have Successfully Purchase Package $ "+ amount + " </p>"
                    + "            <p style='text-align:center;'>Please Pay "+ type + "" + btcamount + " to this Address : "+ address + " </p>"
                    + "            <br>"
                    + "        </div>"
                    //+ "        <div style='text-align:center;'>"
                    //+ "        <img src='" + WebConfigurationManager.AppSettings["domainName"] + "/Content/userDesign/images/logo.png' style='height:65px;width:239px;'>"
                    //+ "        </div>"
                    + "        <p style='text-align:center;'>Thank You..<p>"
                    + "    </div>"
                    + "    </div>"
                    + "</body>"
                    + "</html>";


            string Status = SendMail.mailMain(Enquirer, Body, Subject);
            msg = Status;
            return msg;
        }


        public string SystemMail(string Enquirer, string Subject1, string Body1)
        {
            string msg = string.Empty;

            string ToMail, CCMail, FromMail, Subject;
            string Body = string.Empty;
            //Enquirer = dt.Rows[0]["Email"].ToString();
            CCMail = "";
            FromMail = "";

            Subject = Subject1.ToUpper();

            Body = "<!DOCTYPE html>"
                    + "<html>"
                    + "<head>"
                    + "    <title>" + Subject1.ToUpper() + "</title>"
                    + "</head>"
                    + "<body>"
                    + "    <div style='width:500px; margin:auto; background-color:#fff6;margin-top:50px;padding: 10px 10px 10px 10px;border-radius: 6px 7px;box-shadow: 0px 1px 11px 4px #2a220733;background:url('http://uat35.beechtreeitsolutions.com/Content/Front/images/logo-dark.png');background-position: 50% 50%;'>"
                    + "    <h2 style='text-align:center;width: 500px;'>" + Subject1 + "</h2>"
                    + "    <div style='width:500px;'>"
                    + "        <h4 style='margin-left: 26px;text-align:center;'>Hi, Sir / Madam</h4>"
                    + "        <hr>"
                    + "        <div style='width:500px;'>"
                    + "            <br>"
                    + "           " + Body1 + ""
                    + "            <br>"
                    + "        </div>"
                    + "        <div style='text-align:center;'>"
                    + "        <img src='" + WebConfigurationManager.AppSettings["domainName"] + "/Content/mainDesign/images/logo-top.png' style='height:65px;width:239px;'>"
                    + "        </div>"
                    + "        <p style='text-align:center;'>Thank You..<p>"
                    + "    </div>"
                    + "    </div>"
                    + "</body>"
                    + "</html>";


            string Status = SendMail.mailMain(Enquirer, Body, Subject);
            msg = Status;
            return msg;
        }
    }
}