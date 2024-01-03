using Newtonsoft.Json;
using UniqueTrade_App.CommanFunction;
using UniqueTrade_App.DBEntity;
using UniqueTrade_App.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using UniqueTrade_App.CommonFunction;
using System.Collections;
using System.Linq.Expressions;
 namespace UniqueTrade_App.Controllers

{
    [HandleError]
    public class MemberPanelController : BasePage
    {
        DataTable dtResult = new DataTable();
        //public int TransApproval()
        //{
        //    try
        //    {
        //        DataTable dt = UserManager.User_Report(userInfo.memb_code, "PENDINGDEPOSITE");
        //        for (int j = 0; j < dt.Rows.Count; j++)
        //        {
        //            if (!string.IsNullOrEmpty(dt.Rows[j]["btsAmount"].ToString()) && !string.IsNullOrEmpty(dt.Rows[j]["address"].ToString()))
        //            {
        //                decimal amount = Convert.ToDecimal(dt.Rows[j]["btsAmount"].ToString());
        //                string commit_no = dt.Rows[j]["commit_no"].ToString();
        //                string type = dt.Rows[j]["BTC_Type"].ToString();

        //                amount = amount * 1000000;

        //                string Address = dt.Rows[j]["address"].ToString();
        //                if (type != "WALLET")
        //                {
        //                    HttpClient client = new HttpClient();
        //                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                    HttpResponseMessage response = client.GetAsync("https://api.blockcypher.com/v1/ltc/main/addrs/" + Address).Result;
        //                    if (type == "BTC")
        //                    {
        //                        response = client.GetAsync("https://blockchain.info/rawaddr/" + Address).Result;
        //                    }

        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        string JSON = response.Content.ReadAsStringAsync().Result;
        //                        var obj = JsonConvert.DeserializeObject<BTCModel>(JSON);
        //                        BTCModel objtran = new BTCModel();
        //                        objtran.address = obj.address;
        //                        objtran.final_balance = obj.final_balance;
        //                        objtran.total_received = obj.total_received;

        //                        if (type == "ETH")
        //                        {
        //                            objtran.address = "0x" + obj.address;
        //                            objtran.total_received = obj.total_received / 10000000000;
        //                        }

        //                        if (Address == objtran.address && amount <= objtran.total_received)
        //                        {
        //                            int result = UserManager.ConfirmPayment("CONFIRMPAYMENT", userInfo.memb_code, commit_no);

        //                            userInfo.authrised = "G";
        //                            userInfo.M_Status = "Y";
        //                        }
        //                    }
        //                }
        //            }
        //        }


        //        DataTable dte = UserManager.User_Report(userInfo.memb_code, "PENDINGFUND");
        //        for (int i = 0; i < dte.Rows.Count; i++)
        //        {
        //            if (!string.IsNullOrEmpty(dte.Rows[i]["BTC_AMT"].ToString()) && !string.IsNullOrEmpty(dte.Rows[i]["BTC_AC"].ToString()))
        //            {
        //                decimal amount = Convert.ToDecimal(dte.Rows[i]["BTC_AMT"].ToString());
        //                string commit_no = dte.Rows[i]["SRNO"].ToString();
        //                string type = dte.Rows[i]["BTC_TYP"].ToString();

        //                amount = amount * 1000000;

        //                string Address = dte.Rows[i]["BTC_AC"].ToString();
        //                if (type != "WALLET")
        //                {
        //                    HttpClient client = new HttpClient();
        //                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                    HttpResponseMessage response = client.GetAsync("https://api.blockcypher.com/v1/ltc/main/addrs/" + Address).Result;
        //                    if (type == "BTC")
        //                    {
        //                        // response = client.GetAsync("https://blockchain.info/rawaddr/" + Address).Result;
        //                        response = client.GetAsync("https://api.blockcypher.com/v1/btc/main/addrs/" + Address + "/balance").Result;
        //                    }

        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        string JSON = response.Content.ReadAsStringAsync().Result;
        //                        var obj = JsonConvert.DeserializeObject<BTCModel>(JSON);
        //                        BTCModel objtran = new BTCModel();
        //                        objtran.address = obj.address;
        //                        objtran.final_balance = obj.final_balance;
        //                        objtran.total_received = obj.total_received;

        //                        if (type == "ETH")
        //                        {
        //                            objtran.address = "0x" + obj.address;
        //                            objtran.total_received = obj.total_received / 10000000000;
        //                        }

        //                        if (Address == objtran.address && amount <= objtran.total_received)
        //                        {
        //                            int result = UserManager.ConfirmPayment("CONFIRMFUND", userInfo.memb_code, commit_no);

        //                            userInfo.authrised = "G";
        //                            userInfo.M_Status = "Y";
        //                        }
        //                    }
        //                }
        //            }
        //        }



        //        return 1;




        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}

        public int TransApproval_coinpayment()
        {
            try
            {
                DataTable dte = UserManager.User_Report(userInfo.memb_code, "PENDINGFUND");
                for (int i = 0; i < dte.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dte.Rows[i]["dr"].ToString()) && !string.IsNullOrEmpty(dte.Rows[i]["BTC_AC"].ToString()))
                    {
                        double amount = Convert.ToDouble(dte.Rows[i]["dr"].ToString());
                        string commit_no = dte.Rows[i]["SRNO"].ToString();
                        string transaction_no = dte.Rows[i]["txn_id"].ToString();
                        string type = dte.Rows[i]["BTC_TYP"].ToString();

                        //amount = amount * 1000000;

                        string Address = dte.Rows[i]["BTC_AC"].ToString();
                        if (type != "WALLET")
                        {
                            BTCAddressModel btcaddr = new BTCAddressModel();
                            //btcaddr = GetBTCAddress(trans.BTC_Type);
                            btcaddr = Gettransaction_confirm(transaction_no);
                            if (string.Equals(btcaddr.result, "ok"))
                            {
                                //string JSON = null;
                                //var obj = JsonConvert.DeserializeObject<BTCModel>(JSON);
                                BTCModel objtran = new BTCModel();
                                objtran.address = btcaddr.payment_address;
                                objtran.final_balance = Convert.ToDouble(btcaddr.amountf);
                                objtran.total_received = Convert.ToDouble(btcaddr.receivedf);

                                //if (type == "ETH")
                                //{
                                //    objtran.address = "0x" + obj.address;
                                //    objtran.total_received = obj.total_received / 10000000000;
                                //}

                                if (Address == objtran.address && amount <= objtran.total_received && btcaddr.status_text == "Complete")
                                {
                                    int result = UserManager.ConfirmPayment("CONFIRMFUND", userInfo.memb_code, commit_no);

                                    //userInfo.authrised = "G";
                                    //userInfo.M_Status = "Y";
                                }
                                //else if (btcaddr.time_created <= btcaddr.time_expires)
                                //{
                                //    int result = UserManager.ConfirmPayment("REJECTFUND", userInfo.memb_code, commit_no);
                                //}

                            }
                        }
                    }
                }



                return 1;




            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public ActionResult Index()
        {
            //TransApproval();
            TransApproval_coinpayment();
            DashboardModel dash = new DashboardModel();
            List<NewsModel> dNewsList = new List<NewsModel>();
            try
            {
                //TransApproval();
                DataTable dtTotal = UserManager.GetDASHBOARDData("Dashboard_Total", userInfo.memb_code);
                if (dtTotal.Rows.Count > 0)
                {
                    dash.ac_status = dtTotal.Rows[0]["ac_status"].ToString();
                    dash.Total_ROI = dtTotal.Rows[0]["Total_ROI"].ToString();
                    dash.Total_BUSINESS = dtTotal.Rows[0]["Total_BUSINESS"].ToString();
                    dash.Total_Direct_Income = dtTotal.Rows[0]["Total_Direct_Income"].ToString();
                    dash.Total_Reward = dtTotal.Rows[0]["Total_Reward"].ToString();
                    dash.Total_Binary_Income = dtTotal.Rows[0]["Total_Binary_Income"].ToString();
                    dash.Total_Speed_Bonus = dtTotal.Rows[0]["Total_Speed_Bonus"].ToString();
                    dash.Total_Booster_Club = dtTotal.Rows[0]["Total_Booster_Club"].ToString();
                    dash.Total_Team = dtTotal.Rows[0]["Total_Team"].ToString();
                    dash.activation_date = dtTotal.Rows[0]["activation_date"].ToString();
                    dash.Total_Earning = dtTotal.Rows[0]["Total_Earning"].ToString();
                    //dash.Total_Direct_Income = dtTotal.Rows[0]["Total_Direct_Income"].ToString();
                    dash.Total_PoolDirect = dtTotal.Rows[0]["Total_PoolDirect"].ToString();
                    dash.Total_PoolLevel = dtTotal.Rows[0]["Total_PoolLevel"].ToString();
                    dash.Total_Pool = dtTotal.Rows[0]["Total_Pool"].ToString();
                    dash.Total_MonthlyIncentive = dtTotal.Rows[0]["Total_MonthlyIncentive"].ToString();
                    //dash.ac_status_Monthly = dtTotal.Rows[0]["ac_status_Monthly"].ToString();
                    //dash.ac_status_Royalty_bonus = dtTotal.Rows[0]["ac_status_Royalty_bonus"].ToString();
                    dash.credit_date = dtTotal.Rows[0]["credit_date"].ToString();
                    //dash.Total_casino_token = dtTotal.Rows[0]["Total_casino_token"].ToString();


                    dash.Total_Withdraw = dtTotal.Rows[0]["Total_Withdraw"].ToString();
                    dash.Total_Investment = dtTotal.Rows[0]["Total_Investment"].ToString();
                    dash.Pending_Withdraw = dtTotal.Rows[0]["Pending_Withdraw"].ToString();
                    dash.Total_Direct_Referral = dtTotal.Rows[0]["Total_Direct_Referral"].ToString();
                    dash.Daily_Profil = dtTotal.Rows[0]["Daily_Profil"].ToString();

                    dash.Left_Team = dtTotal.Rows[0]["Left_Team"].ToString();
                    dash.Right_Team = dtTotal.Rows[0]["Right_Team"].ToString();
                    dash.Direct_team = dtTotal.Rows[0]["Direct_team"].ToString();
                    dash.Left_Team_Business = dtTotal.Rows[0]["Left_Team_Business"].ToString();
                    dash.Right_Team_Business = dtTotal.Rows[0]["Right_Team_Business"].ToString();
                    dash.POPUPIMAGE = dtTotal.Rows[0]["POPUPIMAGE"].ToString();
                    dash.Pmode = dtTotal.Rows[0]["Pmode"].ToString();
                    dash.Last_Package = dtTotal.Rows[0]["Last_Package"].ToString();
                    dash.TopUpWallet = dtTotal.Rows[0]["TopUpWallet"].ToString();
                    dash.Total_Balance = dtTotal.Rows[0]["Total_Balance"].ToString();
                    dash.Growth_Wallet = dtTotal.Rows[0]["Growth_Wallet"].ToString();
                    dash.Matching_Wallet = dtTotal.Rows[0]["Matching_Wallet"].ToString();
                    dash.Referral_Wallet = dtTotal.Rows[0]["Referral_Wallet"].ToString();
                 //   dash.Total_Reward = dtTotal.Rows[0]["Total_Rewards"].ToString();
                    dash.userrank = dtTotal.Rows[0]["USERRANK"].ToString();
                    dash.Fundwallet = dtTotal.Rows[0]["Fundwallet"].ToString();
                    dash.Total_Principle = dtTotal.Rows[0]["Total_Principle"].ToString();
                    dash.Total_level = dtTotal.Rows[0]["Total_level"].ToString();
                    dash.Total_level_ROI = dtTotal.Rows[0]["Total_level_ROI"].ToString();
                    //dash.Total_royalty = dtTotal.Rows[0]["Total_royalty"].ToString();
                    dash.Total_booster = dtTotal.Rows[0]["Total_booster"].ToString();
                    //dash.Total_Franchiseincome = dtTotal.Rows[0]["Total_Franchise"].ToString();
                    dash.Total_Team_Business = dtTotal.Rows[0]["Total_Team_Business"].ToString();
                    

                    
                }

                DataTable dt = UserManager.User_Report("0", "LATESTNEWS");
                if (dt.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dt);
                    dNewsList = JsonConvert.DeserializeObject<List<NewsModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            dash.NewsList = dNewsList;
            return View(dash);
        }

        [HttpPost]
        public ActionResult Index(DashboardModel trans)
        {
            try
            {
                if (string.IsNullOrEmpty(trans.BTC_Type))
                {
                    TempData["ActivationAlert"] = "Please select currency type.";
                    return RedirectToAction("Index");
                }

                trans.USD_Amount = "10";
                BTCAddressModel btcaddr = new BTCAddressModel();
                btcaddr = GetBTCAddress(trans.BTC_Type);
                if (string.Equals(btcaddr.result, "ok"))
                {
                    string btcAddress = btcaddr.BTCAddress;

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("https://min-api.cryptocompare.com/data/price?fsym=USD&tsyms=" + trans.BTC_Type).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string JSON = response.Content.ReadAsStringAsync().Result;
                        var obj = JsonConvert.DeserializeObject<AllCOINModel>(JSON);

                        string amtcoin = "0";
                        if (!string.IsNullOrEmpty(obj.BTC))
                            amtcoin = obj.BTC;
                        if (!string.IsNullOrEmpty(obj.ETH))
                            amtcoin = obj.ETH;
                        if (!string.IsNullOrEmpty(obj.LTC))
                            amtcoin = obj.LTC;

                        trans.BTC_Amount = (Convert.ToDecimal(trans.USD_Amount) * Convert.ToDecimal(amtcoin)).ToString();
                    }
                    int result = UserManager.UserDeposite("0", userInfo.memb_code, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, btcAddress, string.IsNullOrEmpty(trans.BTC_Amount) ? "0" : trans.BTC_Amount, trans.BTC_Type, null);
                    if (result > 0)
                    {
                        TempData["ActivationAlert"] = "1";
                        TempData["BTCTYPE"] = trans.BTC_Type;
                        TempData["BTCAddress"] = btcAddress;
                        TempData["BTCAmount"] = trans.BTC_Amount;
                    }
                    else
                        TempData["ActivationAlert"] = "Activation amount submitted failed.";
                }
                else
                {
                    TempData["ActivationAlert"] = "Error in generate " + trans.BTC_Type + " address.";
                }
            }
            catch (Exception ex)
            {
                TempData["ActivationAlert"] = "Activation amount submitted failed.";
            }
            return RedirectToAction("Index");
        }

        public JsonResult getNowTimer()
        {
            string nowtime = DateTime.Now.AddHours(7).ToString("MMM dd, yyyy hh:mm:ss");
            return Json(nowtime, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getLastTimer()
        {
            string lastTime = DateTime.Now.AddHours(7).ToString("MMM dd, yyyy hh:mm:ss");
            DataTable dt = UserManager.GetRateByDate(userInfo.memb_code);
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["ldate"].ToString()))
                    lastTime = Convert.ToDateTime(dt.Rows[0]["ldate"].ToString()).ToString("MMM dd, yyyy hh:mm:ss");
            }
            return Json(lastTime, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTimer()
        {
            var currentdate = System.DateTime.Now.ToString();
            var FromDate = string.Empty;
            var ToDate = string.Empty;
            var result = new { FromDate, ToDate };

            DataTable dt = UserManager.GetRateByDate(userInfo.memb_code);
            if (dt.Rows.Count > 0)
            {
                FromDate = Convert.ToString(dt.Rows[0]["cdate"]);
                ToDate = Convert.ToString(dt.Rows[0]["ldate"]);
                result = new { FromDate, ToDate };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult boostercountdown()
        {
            string booster = "0";
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "BOOSTERCOUNTDOWN");
                if (dt.Rows.Count > 0)
                {
                    booster = dt.Rows[0]["Boostercountdown"].ToString();
                }
            }
            catch (Exception exc)
            {

            }
            return Json(booster, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MyProfile()
        {
            return View();
        }


        public static List<string> CountryList()
        {
            List<string> Culturelist = new List<string>();
            CultureInfo[] getCultureinfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo item in getCultureinfo)
            {
                RegionInfo region = new RegionInfo(item.LCID);
                if (!Culturelist.Contains(region.EnglishName))
                {
                    Culturelist.Add(region.EnglishName);
                }
            }
            Culturelist.Sort();
            return Culturelist;
        }

        public void getCountries()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                foreach (var item in CountryList())
                {
                    items.Add(new SelectListItem
                    {
                        Text = item,
                        Value = item
                    });
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.AllCountries = items;
        }

        public static List<string> CountryCodeList()
        {
            List<string> Culturelist = new List<string>();
            string codes = "+93,+355,+213,+376,+244,+1-268,+54,+374,+297,+1-684,+61,+43,+994,+1-242,+880,+1-246,+257,+32,+229,+1-441,+975,+387,+501,+375,+591,+267,+55,+973,+673,+359,+226,236,+855,+1,+1-345,+242,+235,+56,+86,Cote d'Ivoire,+237,+243,+682,+57,+269,+238,+506,+385,+53,+357,+420,+45,+253,+1 767,+1 809,+593,+20,+291,+503,+34,+372,+251,+679,+358,+33,+691,+241,+220,+44,+245,+995,+240,+49,+233,+30,+1 473,+502,+224,+1 671,+592,+509,+852,+504,+36,+62,+91,+98,+353,+964,+354,+972,+00 1,+39,+1 284,+1 876,+962,+81,+7 6,+254,+996,+686,+82,+383,+966,+965,+856,+371,+218,+231,+1 758,+266,+961,+423,+370,+352,+261,+212,+60,+265,+373,+960,+52,+976,+692,+389,+223,+356,+382,+377,+258,+230,+222,+95,+264,+505,+31,+977,+234,+227,+47,+674,+64,+968,+92,+507,+595,+51,+63,+970,+680,+675,+48,+351,+850,+1 787,+974,+40,+27,+7,+250,+685,+221,+248,+65,+1 869,+232,+386,+378,+677,+252,+381,+94,+211,+239,+249,+41,+597,+421,+46,+268,+963,+255,+676,+66,+992,+993,+670,+228,+886,+1 868,+216,+90,+688,+971,+256,+380,+598,+1,+998,+678,+58,+84,+1 784,+967,+260,+255 24,+263";
            string[] codelist = codes.Split(',');
            for (int i = 0; i < codelist.Length; i++)
            {
                if (!Culturelist.Contains(codelist[i]))
                {
                    Culturelist.Add(codelist[i]);
                }
            }
            Culturelist.Sort();
            return Culturelist;
        }

        public void getCountryCodes()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                foreach (var item in CountryCodeList())
                {
                    items.Add(new SelectListItem
                    {
                        Text = item,
                        Value = item
                    });
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.AllCountryCode = items;
        }

        public ActionResult UpdateProfile()
        {
            getCountries();
            getCountryCodes();
            EditProfileModel user = new EditProfileModel();
            try
            {
                user.Memb_Name = userInfo.memb_name;
                user.M_COUNTRY = userInfo.M_COUNTRY;
                user.Mobile_No = userInfo.Mobile_No;
                user.dbo = userInfo.dbo1;
                user.Gender = userInfo.Gender.Trim();
                user.EMail = userInfo.EmailID;
                //user.EmailID = userInfo.EmailID;
                user.MembName_L = userInfo.MembName_L;
                user.City = userInfo.City;

                if (!string.IsNullOrEmpty(userInfo.Address1))
                    user.Address1 = userInfo.Address1.Replace("<br/>", "\r\n");

                if (!string.IsNullOrEmpty(userInfo.Address2))
                    user.Address2 = userInfo.Address2.Replace("<br/>", "\r\n");

                user.btc_ac = userInfo.btc_ac;
                user.eth_ac = userInfo.eth_ac;
                user.ltc_ac = userInfo.ltc_ac;

                user.ac_name = userInfo.ac_name;
                user.ac_no = userInfo.ac_no;
                user.bk_branch = userInfo.bk_branch;
                user.bk_name = userInfo.bk_name;
                user.bk_ifsc = userInfo.bk_ifsc;
                user.ac_type = userInfo.ac_type;
                user.aadhar_card = userInfo.aadhar_card;
                user.pan_card = userInfo.pan_card;
                user.PhonePay = userInfo.Phone_Pay;
                user.googlepay = userInfo.Google_Pay;
                user.bhimno = userInfo.BHIM_ID;
                user.debit_card_no = userInfo.debit_card_no;
                user.Paytmno = userInfo.Paytm_no;
                user.CountryCode = userInfo.Countrycode;
            }
            catch
            {

            }
            //return RedirectToAction("Index");
            return View(user);
        }

        [HttpPost]
        public ActionResult UpdateProfile(EditProfileModel user)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    TempData["EditProfileAlert"] = "Please enter valid data.";
                //    return RedirectToAction("editProfile", "Dashboard");
                //}

                //if ((userFile != null) && (userFile.ContentLength > 0))
                //{
                //    string sextension = Path.GetExtension(userFile.FileName);
                //    string wdate = DateTime.Now.ToString("yyyymmddhhMMss");
                //    string fileAttn = "USER" + wdate + sextension;
                //    string path = Server.MapPath("~/Content/userAttachment/" + fileAttn);
                //    Stream strm = userFile.InputStream;
                //    var targetFile = path;
                //    GenerateThumbnails(0.5, strm, targetFile);
                //    user.MembName_L = fileAttn;
                //}

                //if (!string.IsNullOrEmpty(Session["OTP_PROFILE"] as string))
                //{
                //    string otp = Crypto.Decrypt(Session["OTP_PROFILE"].ToString(), System.Text.Encoding.Unicode);
                //    if (otp.Equals(user.Request_Code))
                //    {

                //    }
                //    else
                //    {
                //        TempData["EditProfileAlert"] = "Profile OTP is Incorrect..!";
                //        return RedirectToAction("UpdateProfile", "MemberPanel");
                //    }
                //}
                //else
                //{
                //    TempData["EditProfileAlert"] = "Profile OTP is invalid";
                //    return RedirectToAction("UpdateProfile", "MemberPanel");
                //}



                if (!string.IsNullOrEmpty(user.Address1))
                {
                    user.Address1 = user.Address1.Trim().Replace("\r\n", "<br/>");
                }
                if (!string.IsNullOrEmpty(user.Address2))
                {
                    user.Address2 = user.Address2.Trim().Replace("\r\n", "<br/>");
                }


                //if (!string.IsNullOrEmpty(user.dbo))
                //{
                //    string[] dateb = user.dbo.Split('/');
                //    user.dbo = dateb[1] + "-" + dateb[0] + "-" + dateb[2];
                //}

                //if (string.IsNullOrEmpty(user.btc_ac))
                //    user.btc_ac = userInfo.btc_ac;

                //if (string.IsNullOrEmpty(user.eth_ac))
                //    user.eth_ac = userInfo.eth_ac;

                //if (string.IsNullOrEmpty(user.ltc_ac))
                //    user.ltc_ac = userInfo.ltc_ac;


                DataTable dtUser = UserManager.USER_DETAILS("EDITPROFILE", userInfo.memb_code, user.Memb_Name
                    , user.MembName_L, user.Gender, user.dbo, user.Mobile_No, user.EmailID, user.Address1
                    , user.Address2, user.M_COUNTRY, user.City, userInfo.username, userInfo.mpwd, userInfo.RV_Code
                    , null, null, user.btc_ac, user.eth_ac, user.ltc_ac);
                if (dtUser.Rows.Count > 0)
                {
                    if (string.Equals(dtUser.Rows[0]["SP_STATUS"].ToString(), "SUCCESS"))
                    {
                        TempData["EditProfileAlert"] = "PROFILE UPDATED SUCCESSFULLY.";

                        userInfo.memb_name = dtUser.Rows[0]["memb_name"].ToString();
                        userInfo.M_COUNTRY = dtUser.Rows[0]["M_COUNTRY"].ToString();
                        userInfo.Countrycode = dtUser.Rows[0]["MembName_M"].ToString();
                        userInfo.Mobile_No = dtUser.Rows[0]["Mobile_No"].ToString();
                        userInfo.dbo = dtUser.Rows[0]["dboS"].ToString();
                        userInfo.dbo1 = dtUser.Rows[0]["dboE"].ToString();
                        userInfo.Gender = dtUser.Rows[0]["Gender"].ToString();
                        userInfo.EmailID = dtUser.Rows[0]["EMail"].ToString();

                        userInfo.MembName_L = dtUser.Rows[0]["MembName_L"].ToString();
                        userInfo.Address1 = dtUser.Rows[0]["Address1"].ToString();
                        userInfo.Address2 = dtUser.Rows[0]["Address2"].ToString();
                        userInfo.City = dtUser.Rows[0]["City"].ToString();

                        userInfo.btc_ac = dtUser.Rows[0]["btc_ac"].ToString();
                        userInfo.eth_ac = dtUser.Rows[0]["eth_ac"].ToString();
                        userInfo.ltc_ac = dtUser.Rows[0]["ltc_ac"].ToString();
                        //userInfo.aadhar_card = dtUser.Rows[0]["aadhar_card"].ToString();
                        //userInfo.pan_card = dtUser.Rows[0]["pan_card"].ToString();
                        

                        string trno = "0";
                        string sub = "Update Profile";
                        //string result1 = EmailUpdate(userInfo.EmailID, sub);
                    }
                    else
                        TempData["EditProfileAlert"] = "Profile Update Failed.";
                }
                else
                {
                    TempData["EditProfileAlert"] = "Profile Update Failed.";
                }

                //if (string.IsNullOrEmpty(user.ac_name))
                //    user.ac_name = userInfo.ac_name.Trim();

                //if (string.IsNullOrEmpty(user.ac_no))
                //    user.ac_no = userInfo.ac_no.Trim();

                //if (string.IsNullOrEmpty(user.bk_branch))
                //    user.bk_branch = userInfo.bk_branch.Trim();

                //if (string.IsNullOrEmpty(user.bk_name))
                //    user.bk_name = userInfo.bk_name.Trim();

                //if (string.IsNullOrEmpty(user.bk_ifsc))
                //    user.bk_ifsc = userInfo.bk_ifsc.Trim();

                //if (string.IsNullOrEmpty(user.ac_type))
                //    user.ac_type = userInfo.ac_type.Trim();

                //DataTable dtUserAcc = UserManager.ADD_ACCOUNT_DETAILS(userInfo.memb_code, user.ac_name.Trim(), user.ac_no.Trim(), user.bk_name.Trim(),
                //    user.bk_branch.Trim(), user.bk_ifsc.Trim(), null, null, user.ac_type.Trim(), user.PhonePay, user.googlepay, user.bhimno, user.Paytmno, user.pan_card);
                //if (dtUserAcc.Rows.Count > 0)
                //{
                //    if (string.Equals(dtUserAcc.Rows[0]["SP_STATUS"].ToString(), "SUCCESS"))
                //    {
                //        TempData["AddAccountAlert"] = "ACCOUNT DETAILS SUBMITTED SUCCESSFULLY.";
                //        userInfo.ac_name = dtUserAcc.Rows[0]["ac_name"].ToString();
                //        userInfo.ac_no = dtUserAcc.Rows[0]["ac_no"].ToString();
                //        userInfo.bk_name = dtUserAcc.Rows[0]["bk_name"].ToString();
                //        userInfo.bk_branch = dtUserAcc.Rows[0]["bk_branch"].ToString();
                //        userInfo.bk_ifsc = dtUserAcc.Rows[0]["bk_ifsc"].ToString();
                //        userInfo.debit_card_no = dtUserAcc.Rows[0]["debit_card_no"].ToString();
                //        //  userInfo.transit_no = dtUserAcc.Rows[0]["transit_no"].ToString();
                //        userInfo.ac_type = dtUserAcc.Rows[0]["ac_type"].ToString();
                //        userInfo.Phone_Pay = dtUserAcc.Rows[0]["Phone_Pay"].ToString();
                //        userInfo.Google_Pay = dtUserAcc.Rows[0]["Google_Pay"].ToString();
                //        userInfo.BHIM_ID = dtUserAcc.Rows[0]["BHIM_ID"].ToString();
                //        userInfo.Paytm_no = dtUserAcc.Rows[0]["Paytm_no"].ToString();
                //        //userInfo.aadhar_card = dtUserAcc.Rows[0]["aadhar_card"].ToString();
                //        userInfo.pan_card = dtUserAcc.Rows[0]["pan_card"].ToString();
                //    }
                //}
            }
            catch (Exception ex)
            {
                TempData["EditProfileAlert"] = "Error in Edit Profile.";
            }
            // return View();
            return RedirectToAction("UpdateProfile", "MemberPanel");
        }
        //public ActionResult UpdateProfile(EditProfileModel user, HttpPostedFileBase userFile, HttpPostedFileBase debit_card_no)
        //{
        //    try
        //    {
        //        //if (!ModelState.IsValid)
        //        //{
        //        //    TempData["EditProfileAlert"] = "Please enter valid data.";
        //        //    return RedirectToAction("editProfile", "Dashboard");
        //        //}

        //        if ((userFile != null) && (userFile.ContentLength > 0))
        //        {
        //            string sextension = Path.GetExtension(userFile.FileName);
        //            string wdate = DateTime.Now.ToString("yyyymmddhhMMss");
        //            string fileAttn = "USER" + wdate + sextension;
        //            string path = Server.MapPath("~/Content/userAttachment/" + fileAttn);
        //            Stream strm = userFile.InputStream;
        //            var targetFile = path;
        //            GenerateThumbnails(0.5, strm, targetFile);
        //            user.MembName_L = fileAttn;
        //        }


        //        if (string.IsNullOrEmpty(user.Address1))
        //        {

        //            TempData["EditProfileAlert"] = " Update USDT Address !!!";

        //            return RedirectToAction("UpdateProfile", "MemberPanel");
        //        }


        //        if (string.IsNullOrEmpty(user.Address2))
        //        {

        //            TempData["EditProfileAlert"] = " Update BUSD Address !!!";

        //            return RedirectToAction("UpdateProfile", "MemberPanel");
        //        }



        //        if (!string.IsNullOrEmpty(user.Address1))
        //        {
        //            if (!user.Address1.StartsWith("T"))
        //            {
        //                TempData["EditProfileAlert"] = " Enter Only USDT Address !!!";

        //                return RedirectToAction("UpdateProfile", "MemberPanel");
        //            }

        //        }

        //        if (!string.IsNullOrEmpty(user.Address2))
        //        {
        //            if (!user.Address2.StartsWith("0x"))
        //            {
        //                TempData["EditProfileAlert"] = "Enter Only BUSD Address !!!";

        //                return RedirectToAction("UpdateProfile", "MemberPanel");
        //            }

        //        }




        //        if (!string.IsNullOrEmpty(user.dbo))
        //        {
        //            string[] dateb = user.dbo.Split('/');
        //            user.dbo = dateb[1] + "-" + dateb[0] + "-" + dateb[2];
        //        }

        //        if (string.IsNullOrEmpty(user.btc_ac))
        //            user.btc_ac = userInfo.btc_ac;

        //        if (string.IsNullOrEmpty(user.eth_ac))
        //            user.eth_ac = userInfo.eth_ac;

        //        if (string.IsNullOrEmpty(user.ltc_ac))
        //            user.ltc_ac = userInfo.ltc_ac;

        //        if (!string.IsNullOrEmpty(user.inpType) && user.inpType == "profile")
        //        {

        //            DataTable dtUser = UserManager.USER_DETAILS("EDITPROFILE", userInfo.memb_code, user.Memb_Name.Trim()
        //            , user.MembName_L, user.Gender, user.dbo, user.Mobile_No, userInfo.EmailID, user.Address1
        //            , user.Address2, user.M_COUNTRY, user.City, userInfo.username, userInfo.mpwd, userInfo.RV_Code
        //            , null, null, user.btc_ac, user.eth_ac, user.ltc_ac, user.CountryCode);
        //            if (dtUser.Rows.Count > 0)
        //            {
        //                if (string.Equals(dtUser.Rows[0]["SP_STATUS"].ToString(), "SUCCESS"))
        //                {
        //                    TempData["EditProfileAlert"] = "PROFILE UPDATED SUCCESSFULLY.";

        //                    userInfo.memb_name = dtUser.Rows[0]["memb_name"].ToString();
        //                    userInfo.M_COUNTRY = dtUser.Rows[0]["M_COUNTRY"].ToString();
        //                    userInfo.Countrycode = dtUser.Rows[0]["MembName_M"].ToString();
        //                    userInfo.Mobile_No = dtUser.Rows[0]["Mobile_No"].ToString();
        //                    userInfo.dbo = dtUser.Rows[0]["dboS"].ToString();
        //                    userInfo.dbo1 = dtUser.Rows[0]["dboE"].ToString();
        //                    userInfo.Gender = dtUser.Rows[0]["Gender"].ToString();
        //                    userInfo.EmailID = dtUser.Rows[0]["EMail"].ToString();

        //                    userInfo.MembName_L = dtUser.Rows[0]["MembName_L"].ToString();
        //                    userInfo.Address1 = dtUser.Rows[0]["Address1"].ToString();
        //                    userInfo.Address2 = dtUser.Rows[0]["Address2"].ToString();
        //                    userInfo.City = dtUser.Rows[0]["City"].ToString();

        //                    userInfo.btc_ac = dtUser.Rows[0]["btc_ac"].ToString();
        //                    userInfo.eth_ac = dtUser.Rows[0]["eth_ac"].ToString();
        //                    userInfo.ltc_ac = dtUser.Rows[0]["ltc_ac"].ToString();
        //                    //userInfo.aadhar_card = dtUser.Rows[0]["aadhar_card"].ToString();
        //                    //userInfo.pan_card = dtUser.Rows[0]["pan_card"].ToString();  
        //                    string trno = "0";
        //                    string sub = "Update Profile";
        //                    string result1 = EmailUpdate(userInfo.EmailID, sub);
        //                }
        //                else
        //                    TempData["EditProfileAlert"] = "Profile Update Failed.";
        //            }
        //            else
        //            {
        //                TempData["EditProfileAlert"] = "Profile Update Failed.";
        //            }


        //        }


        //        if (string.IsNullOrEmpty(user.ac_name))
        //            user.ac_name = userInfo.ac_name.Trim();

        //        if (string.IsNullOrEmpty(user.ac_no))
        //            user.ac_no = userInfo.ac_no.Trim();

        //        if (string.IsNullOrEmpty(user.bk_branch))
        //            user.bk_branch = userInfo.bk_branch.Trim();

        //        if (string.IsNullOrEmpty(user.bk_name))
        //            user.bk_name = userInfo.bk_name.Trim();

        //        if (string.IsNullOrEmpty(user.bk_ifsc))
        //            user.bk_ifsc = userInfo.bk_ifsc.Trim();

        //        if (string.IsNullOrEmpty(user.ac_type))
        //            user.ac_type = userInfo.ac_type.Trim();
        //        if (!string.IsNullOrEmpty(user.inpType) && user.inpType == "bank")
        //        {
        //            DataTable dtUserAcc = UserManager.ADD_ACCOUNT_DETAILS(userInfo.memb_code, user.ac_name.Trim(), user.ac_no.Trim(), user.bk_name.Trim(),
        //            user.bk_branch.Trim(), user.bk_ifsc.Trim(), null, null, user.ac_type.Trim(), user.PhonePay, user.googlepay, user.bhimno, user.Paytmno);
        //            if (dtUserAcc.Rows.Count > 0)
        //            {


        //                if (string.Equals(dtUserAcc.Rows[0]["SP_STATUS"].ToString(), "SUCCESS"))
        //                {
        //                    //TempData["AddAccountAlert"] = "ACCOUNT DETAILS SUBMITTED SUCCESSFULLY.";
        //                    userInfo.ac_name = dtUserAcc.Rows[0]["ac_name"].ToString();
        //                    userInfo.ac_no = dtUserAcc.Rows[0]["ac_no"].ToString();
        //                    userInfo.bk_name = dtUserAcc.Rows[0]["bk_name"].ToString();
        //                    userInfo.bk_branch = dtUserAcc.Rows[0]["bk_branch"].ToString();
        //                    userInfo.bk_ifsc = dtUserAcc.Rows[0]["bk_ifsc"].ToString();
        //                    userInfo.debit_card_no = dtUserAcc.Rows[0]["debit_card_no"].ToString();
        //                    //  userInfo.transit_no = dtUserAcc.Rows[0]["transit_no"].ToString();
        //                    userInfo.ac_type = dtUserAcc.Rows[0]["ac_type"].ToString();
        //                    userInfo.Phone_Pay = dtUserAcc.Rows[0]["Phone_Pay"].ToString();
        //                    userInfo.Google_Pay = dtUserAcc.Rows[0]["Google_Pay"].ToString();
        //                    userInfo.BHIM_ID = dtUserAcc.Rows[0]["BHIM_ID"].ToString();
        //                    userInfo.Paytm_no = dtUserAcc.Rows[0]["Paytm_no"].ToString();
        //                    //userInfo.aadhar_card = dtUserAcc.Rows[0]["aadhar_card"].ToString();
        //                    //userInfo.pan_card = dtUserAcc.Rows[0]["pan_no"].ToString();  

        //                    TempData["EditBankDetailsAlert"] = "Bank Details Updated Successfully.";

        //                }
        //                else if (string.Equals(dtUserAcc.Rows[0]["SP_STATUS"].ToString(), "EXISTS"))
        //                {
        //                    ViewBag.status = "EXISTS";

        //                    TempData["EditBankDetailsAlert"] = "Bank Details Can be Only Updated Once. Kindly Contact Admin ....";
        //                }
        //                else
        //                {
        //                    TempData["EditBankDetailsAlert"] = "Bank Details Update Failed.";
        //                }

        //            }
        //            else
        //            { TempData["EditBankDetailsAlert"] = "Bank Details Update Failed."; }



        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        if (!string.IsNullOrEmpty(user.inpType) && user.inpType == "profile")
        //        {

        //            TempData["EditProfileAlert"] = "Error in Edit Profile.";
        //        }
        //        else
        //        {
        //            TempData["EditBankDetailsAlert"] = "Bank Details Update Failed.";
        //        }
        //    }
        //    // return View();
        //    return RedirectToAction("UpdateProfile", "MemberPanel");
        //}
        //public ActionResult UpdateProfile(EditProfileModel user, HttpPostedFileBase userFile, HttpPostedFileBase debit_card_no)
        //{
        //    try
        //    {
        //        //if (!ModelState.IsValid)
        //        //{
        //        //    TempData["EditProfileAlert"] = "Please enter valid data.";
        //        //    return RedirectToAction("editProfile", "Dashboard");
        //        //}

        //        if ((userFile != null) && (userFile.ContentLength > 0))
        //        {
        //            string sextension = Path.GetExtension(userFile.FileName);
        //            string wdate = DateTime.Now.ToString("yyyymmddhhMMss");
        //            string fileAttn = "USER" + wdate + sextension;
        //            string path = Server.MapPath("~/Content/userAttachment/" + fileAttn);
        //            Stream strm = userFile.InputStream;
        //            var targetFile = path;
        //            GenerateThumbnails(0.5, strm, targetFile);
        //            user.MembName_L = fileAttn;
        //        }


        //        if (string.IsNullOrEmpty(user.Address1))
        //        {

        //            TempData["EditProfileAlert"] = " Update USDT Address !!!";

        //            return RedirectToAction("UpdateProfile", "MemberPanel");
        //        }


        //        if (string.IsNullOrEmpty(user.Address2))
        //        {

        //            TempData["EditProfileAlert"] = " Update BUSD Address !!!";

        //            return RedirectToAction("UpdateProfile", "MemberPanel");
        //        }



        //        if (!string.IsNullOrEmpty(user.Address1))
        //        {
        //            if (!user.Address1.StartsWith("T"))
        //            {
        //                TempData["EditProfileAlert"] = " Enter Only USDT Address !!!";

        //                return RedirectToAction("UpdateProfile", "MemberPanel");
        //            }

        //        }

        //        if (!string.IsNullOrEmpty(user.Address2))
        //        {
        //            if (!user.Address2.StartsWith("0x"))
        //            {
        //                TempData["EditProfileAlert"] = "Enter Only BUSD Address !!!";

        //                return RedirectToAction("UpdateProfile", "MemberPanel");
        //            }

        //        }




        //        if (!string.IsNullOrEmpty(user.dbo))
        //        {
        //            string[] dateb = user.dbo.Split('/');
        //            user.dbo = dateb[1] + "-" + dateb[0] + "-" + dateb[2];
        //        }

        //        if (string.IsNullOrEmpty(user.btc_ac))
        //            user.btc_ac = userInfo.btc_ac;

        //        if (string.IsNullOrEmpty(user.eth_ac))
        //            user.eth_ac = userInfo.eth_ac;

        //        if (string.IsNullOrEmpty(user.ltc_ac))
        //            user.ltc_ac = userInfo.ltc_ac;

        //        if (!string.IsNullOrEmpty(user.inpType ) && user.inpType == "profile")
        //        { 

        //            DataTable dtUser = UserManager.USER_DETAILS("EDITPROFILE", userInfo.memb_code, user.Memb_Name.Trim()
        //            , user.MembName_L, user.Gender, user.dbo, user.Mobile_No, userInfo.EmailID, user.Address1
        //            , user.Address2, user.M_COUNTRY, user.City, userInfo.username, userInfo.mpwd, userInfo.RV_Code
        //            , null, null, user.btc_ac, user.eth_ac, user.ltc_ac, user.CountryCode);
        //        if (dtUser.Rows.Count > 0)
        //        {
        //            if (string.Equals(dtUser.Rows[0]["SP_STATUS"].ToString(), "SUCCESS"))
        //            {
        //                TempData["EditProfileAlert"] = "PROFILE UPDATED SUCCESSFULLY.";

        //                userInfo.memb_name = dtUser.Rows[0]["memb_name"].ToString();
        //                userInfo.M_COUNTRY = dtUser.Rows[0]["M_COUNTRY"].ToString();
        //                userInfo.Countrycode = dtUser.Rows[0]["MembName_M"].ToString();
        //                userInfo.Mobile_No = dtUser.Rows[0]["Mobile_No"].ToString();
        //                userInfo.dbo = dtUser.Rows[0]["dboS"].ToString();
        //                userInfo.dbo1 = dtUser.Rows[0]["dboE"].ToString();
        //                userInfo.Gender = dtUser.Rows[0]["Gender"].ToString();
        //                userInfo.EmailID = dtUser.Rows[0]["EMail"].ToString();

        //                userInfo.MembName_L = dtUser.Rows[0]["MembName_L"].ToString();
        //                userInfo.Address1 = dtUser.Rows[0]["Address1"].ToString();
        //                userInfo.Address2 = dtUser.Rows[0]["Address2"].ToString();
        //                    userInfo.City = dtUser.Rows[0]["City"].ToString();

        //                userInfo.btc_ac = dtUser.Rows[0]["btc_ac"].ToString();
        //                userInfo.eth_ac = dtUser.Rows[0]["eth_ac"].ToString();
        //                userInfo.ltc_ac = dtUser.Rows[0]["ltc_ac"].ToString();
        //                //userInfo.aadhar_card = dtUser.Rows[0]["aadhar_card"].ToString();
        //                //userInfo.pan_card = dtUser.Rows[0]["pan_card"].ToString();  
        //                string trno = "0";
        //                string sub = "Update Profile";
        //                string result1 = EmailUpdate(userInfo.EmailID, sub);
        //            }
        //            else
        //                TempData["EditProfileAlert"] = "Profile Update Failed.";
        //        }
        //        else
        //        {
        //            TempData["EditProfileAlert"] = "Profile Update Failed.";
        //        }


        //        }


        //        if (string.IsNullOrEmpty(user.ac_name))
        //            user.ac_name = userInfo.ac_name.Trim();

        //        if (string.IsNullOrEmpty(user.ac_no))
        //            user.ac_no = userInfo.ac_no.Trim();

        //        if (string.IsNullOrEmpty(user.bk_branch))
        //            user.bk_branch = userInfo.bk_branch.Trim();

        //        if (string.IsNullOrEmpty(user.bk_name))
        //            user.bk_name = userInfo.bk_name.Trim();

        //        if (string.IsNullOrEmpty(user.bk_ifsc))
        //            user.bk_ifsc = userInfo.bk_ifsc.Trim();

        //        if (string.IsNullOrEmpty(user.ac_type))
        //            user.ac_type = userInfo.ac_type.Trim();
        //        if (!string.IsNullOrEmpty(user.inpType) && user.inpType == "bank")
        //        {
        //            DataTable dtUserAcc = UserManager.ADD_ACCOUNT_DETAILS(userInfo.memb_code, user.ac_name.Trim(), user.ac_no.Trim(), user.bk_name.Trim(),
        //            user.bk_branch.Trim(), user.bk_ifsc.Trim(), null, null, user.ac_type.Trim(), user.PhonePay, user.googlepay, user.bhimno, user.Paytmno);
        //            if (dtUserAcc.Rows.Count > 0)
        //            {


        //                if (string.Equals(dtUserAcc.Rows[0]["SP_STATUS"].ToString(), "SUCCESS"))
        //                {
        //                    //TempData["AddAccountAlert"] = "ACCOUNT DETAILS SUBMITTED SUCCESSFULLY.";
        //                    userInfo.ac_name = dtUserAcc.Rows[0]["ac_name"].ToString();
        //                    userInfo.ac_no = dtUserAcc.Rows[0]["ac_no"].ToString();
        //                    userInfo.bk_name = dtUserAcc.Rows[0]["bk_name"].ToString();
        //                    userInfo.bk_branch = dtUserAcc.Rows[0]["bk_branch"].ToString();
        //                    userInfo.bk_ifsc = dtUserAcc.Rows[0]["bk_ifsc"].ToString();
        //                    userInfo.debit_card_no = dtUserAcc.Rows[0]["debit_card_no"].ToString();
        //                    //  userInfo.transit_no = dtUserAcc.Rows[0]["transit_no"].ToString();
        //                    userInfo.ac_type = dtUserAcc.Rows[0]["ac_type"].ToString();
        //                    userInfo.Phone_Pay = dtUserAcc.Rows[0]["Phone_Pay"].ToString();
        //                    userInfo.Google_Pay = dtUserAcc.Rows[0]["Google_Pay"].ToString();
        //                    userInfo.BHIM_ID = dtUserAcc.Rows[0]["BHIM_ID"].ToString();
        //                    userInfo.Paytm_no = dtUserAcc.Rows[0]["Paytm_no"].ToString();
        //                    //userInfo.aadhar_card = dtUserAcc.Rows[0]["aadhar_card"].ToString();
        //                    //userInfo.pan_card = dtUserAcc.Rows[0]["pan_no"].ToString();  

        //                    TempData["EditBankDetailsAlert"] = "Bank Details Updated Successfully.";

        //                }
        //                else if (string.Equals(dtUserAcc.Rows[0]["SP_STATUS"].ToString(), "EXISTS"))
        //                {
        //                    ViewBag.status = "EXISTS";

        //                    TempData["EditBankDetailsAlert"] = "Bank Details Can be Only Updated Once. Kindly Contact Admin ....";
        //                }
        //                else
        //                {
        //                    TempData["EditBankDetailsAlert"] = "Bank Details Update Failed.";
        //                }

        //            }
        //            else
        //            {  TempData["EditBankDetailsAlert"] = "Bank Details Update Failed."; }



        //       }


        //    }
        //    catch (Exception ex)
        //    {
        //        if (!string.IsNullOrEmpty(user.inpType) && user.inpType == "profile")
        //        {

        //            TempData["EditProfileAlert"] = "Error in Edit Profile.";
        //        }
        //        else { 
        //            TempData["EditBankDetailsAlert"] = "Bank Details Update Failed.";
        //        }
        //    }
        //    // return View();
        //    return RedirectToAction("UpdateProfile", "MemberPanel");
        //}

        private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
        {
            try
            {
                using (var image = System.Drawing.Image.FromStream(sourcePath))
                {
                    var newWidth = (int)(image.Width * scaleFactor);
                    var newHeight = (int)(image.Height * scaleFactor);
                    var thumbnailImg = new Bitmap(newWidth, newHeight);
                    var thumbGraph = Graphics.FromImage(thumbnailImg);
                    thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                    thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                    thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                    thumbGraph.DrawImage(image, imageRectangle);
                    thumbnailImg.Save(targetPath, image.RawFormat);
                }
            }
            catch
            {

            }
        }

        public ActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordChange(ChangePasswordModel user)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    TempData["ChangePassAlert"] = "Please enter valid data.";
                //    return RedirectToAction("PasswordChange", "MemberPanel");
                //}

                if (!string.Equals(user.NewPass.Trim(), user.ConfirmPass.Trim()))
                {
                    TempData["ChangePassAlert"] = "New Password and Confirm Password Not Match";
                    return RedirectToAction("PasswordChange", "MemberPanel");
                }

                if (string.IsNullOrEmpty(user.NewPass.Trim()))
                {
                    TempData["ChangePassAlert"] = "Please Enter Password.";
                    return RedirectToAction("PasswordChange", "MemberPanel");
                }
                if (user.NewPass.Any(Char.IsWhiteSpace))
                {
                    TempData["ChangePassAlert"] = "Spaces not allow in Password.";
                    return RedirectToAction("PasswordChange", "MemberPanel");
                }

                if (string.Equals(user.OldPass, userInfo.mpwd))
                {
                    DataTable dtUser = UserManager.Change_Password(userInfo.memb_code, user.NewPass.Trim());
                    if (dtUser.Rows.Count > 0)
                    {
                        if (string.Equals(dtUser.Rows[0]["SP_STATUS"].ToString(), "SUCCESS"))
                        {
                            TempData["ChangePassAlert"] = "PASSWORD CHANGE SUCCESSFULLY.";
                            userInfo.mpwd = dtUser.Rows[0]["mpwd"].ToString();
                            string sub = "Password Change";
                            string result1 = EmailPChange(userInfo.EmailID, sub, userInfo.mpwd);
                        }
                        else
                            TempData["ChangePassAlert"] = "Password Change Failed.";
                    }
                    else
                    {
                        TempData["ChangePassAlert"] = "Password Change Failed.";
                    }
                }
                else
                {
                    TempData["ChangePassAlert"] = "Old Password Not Match";
                }
            }
            catch
            {
                TempData["ChangePassAlert"] = "Error in Change Password.";
            }
            return RedirectToAction("PasswordChange", "MemberPanel");
        }

        public ActionResult create_txn_password()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create_txn_password(ChangeTransactionPassModel user)
        {

            try
            {
                if (string.IsNullOrEmpty(user.OldTranPass.Trim()))
                {
                    TempData["ChangePassAlert"] = "Please Enter Password.";
                    return RedirectToAction("create_txn_password", "MemberPanel");
                }
                if (user.NewTranPass.Any(Char.IsWhiteSpace))
                {
                    TempData["ChangePassAlert"] = "Spaces not allow in Password.";
                    return RedirectToAction("create_txn_password", "MemberPanel");
                }

                if (string.Equals(user.OldTranPass, userInfo.RV_Code))
                {
                    int result = UserManager.ChangeTXPaasword(userInfo.memb_code, user.NewTranPass);
                    if (result > 0)
                    {
                        userInfo.RV_Code = user.NewTranPass;
                        TempData["ChangePassAlert"] = "Password Changed Successfull.";
                    }
                    else
                        TempData["ChangePassAlert"] = "Password Changed Failed.";
                }
                else
                {
                    TempData["ChangePassAlert"] = "Old Password Not Match";
                }
            }
            catch
            {
                TempData["ChangePassAlert"] = "Error in Change Password.";
            }
            return RedirectToAction("create_txn_password", "MemberPanel");
        }
        //------------------ user team ----------------//
        public ActionResult TeamDirectReport()
        {
            List<UserLevelModel> dList = new List<UserLevelModel>();
            try
            {
                DataTable dt = UserManager.User_Report("DIRECTTEAM",userInfo.memb_code );
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<UserLevelModel>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                // Response.Write(ex.ToString());
            }
            return View(dList.ToList());
        }

        public ActionResult TeamDownlineReport()
        {
            List<UserLevelModel> dList = new List<UserLevelModel>();
            try
            {
                DataTable dt = UserManager.User_Report( "TOTALDOWNLINE",userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<UserLevelModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult TeamDownlineLeft()
        {
            List<UserLevelModel> dList = new List<UserLevelModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "TOTALDOWNLINELEFT");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<UserLevelModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult TeamDownlineRight()
        {
            List<UserLevelModel> dList = new List<UserLevelModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "TOTALDOWNLINERIGHT");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<UserLevelModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult TeamTree()
        {
            return View();
        }

        public ActionResult userDetails(string membno)
        {
            int id = Convert.ToInt32(membno);
            UserDetailsModel user = new UserDetailsModel();
            DataTable dt = UserManager.User_Report(id, "USERDETAILS");
            if (dt.Rows.Count > 0)
            {
                string JSONString = JsonConvert.SerializeObject(dt);
                var obj = JsonConvert.DeserializeObject<List<UserDetailsModel>>(JSONString);

                user = obj.First();
            }
            return PartialView("userDetails", user);
        }

        public ActionResult TreePartial(string id)
        {
            int id1 = Convert.ToInt32(id);
            List<BinaryModel> dList = new List<BinaryModel>();
            BinaryModel binary = new BinaryModel();
            try
            {
                DataTable dt = UserManager.BINARY_TREEVIEW(id1);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<BinaryModel>>(jsonString);

                    binary = dList.First();
                }
            }
            catch
            {

            }
            return PartialView("TreePartial", binary);
        }

        public ActionResult signUP(string refer, string pl)
        {
            try
            {
                refer = refer.Replace("R", "");
                refer = refer.Replace("L", "");
                string referral = Crypto.Encrypt(refer, System.Text.Encoding.Unicode);
                return RedirectToAction("signup", "Home", new { REGUSER = referral, Post = pl });
            }
            catch
            {
                return RedirectToAction("signup", "Home");
            }
        }


        //------------------ user investment ----------------//
        public ActionResult Invest()
        {
            TransactionModel trans = new TransactionModel();

            return View(trans);
        }

        [HttpPost]
        public ActionResult Invest(TransactionModel trans, HttpPostedFileBase attachment)
        {
            try
            {
                if (string.IsNullOrEmpty(trans.USD_Amount))
                {
                    TempData["InvestmentAlert"] = "Please Select Package.";
                    return RedirectToAction("Invest");
                }

                // trans.BTC_Type = "INVESTEMENT";
                if (string.IsNullOrEmpty(trans.BTC_Type))
                {
                    TempData["InvestmentAlert"] = "Please select Purchase by.";
                    return RedirectToAction("Invest");
                }

                int depositeStatus = 0;
                string allPackages = "5000,10000,25000,50000,100000,200000";
                string[] pkg = allPackages.Split(',');
                //int depositeStatus = 0;
                for (int i = 0; i < pkg.Length; i++)
                {
                    if (string.Equals(trans.USD_Amount, pkg[i]))
                    {
                        depositeStatus = 1;
                        break;
                    }
                }
                if (depositeStatus == 1)
                {
                    if (attachment == null || attachment.ContentLength == 0)
                    {
                        TempData["InvestmentAlert"] = "Please upload attachment.";
                        return RedirectToAction("Invest");
                    }

                    string attachments = null;
                    if ((attachment != null) && (attachment.ContentLength > 0))
                    {
                        string sextension = Path.GetExtension(attachment.FileName);
                        if (sextension.ToLower() == ".png" || sextension.ToLower() == ".jpg" || sextension.ToLower() == ".jpeg")
                        {
                            string wdate = DateTime.Now.ToString("yyyymmddhhMMss");
                            string fileAttn = "USER" + wdate + sextension;
                            string path = Server.MapPath("~/Content/invetsmentAttch/" + fileAttn);
                            Stream strm = attachment.InputStream;
                            var targetFile = path;
                            GenerateThumbnails(0.5, strm, targetFile);
                            attachments = fileAttn;
                        }
                    }

                    if (string.IsNullOrEmpty(attachments))
                    {
                        TempData["InvestmentAlert"] = "Please select .png , .jpg or .jpeg file only.";
                        return RedirectToAction("Invest");
                    }

                    int result = UserManager.UserDeposite("0", userInfo.memb_code, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, attachments, null, trans.BTC_Type, null);
                    if (result > 0)
                    {
                        TempData["InvestmentAlert"] = "1";
                    }
                    else
                    {
                        TempData["InvestmentAlert"] = "Purchase Package failed.";
                    }
                }
                else
                {
                    TempData["InvestmentAlert"] = "Please Select Valid Package ";
                }
            }
            catch (Exception ex)
            {
                TempData["InvestmentAlert"] = "Purchase Package failed.";
            }
            return RedirectToAction("Invest");
        }

        public ActionResult InvestReport()
        {
            List<DepositeModel> dList = new List<DepositeModel>();
            try
            {
                DataTable dtTrans = UserManager.DepositeHistory(userInfo.memb_code);
                if (dtTrans.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    dList = JsonConvert.DeserializeObject<List<DepositeModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }


        //------------------ topup user ----------------//
        public ActionResult TopUp()
        {
            TransactionModel trans = new TransactionModel();
            //assigned to the variable trans
            try
            {
                trans.UserName = userInfo.username;
                DataTable dt = UserManager.User_Report("FUNDWALLET",userInfo.memb_code);
                //DataTable by calling the User_Report method of the UserManager
                if (dt.Rows.Count > 0)  //Checks if there is at least one row in the DataTable dt. 
                {

                    trans.Fund_Wallet = "$ " + dt.Rows[0]["Fund_Wallet"].ToString();
                    //prefixed with "$ ".

                    trans.Last_Package = dt.Rows[0]["Last_Package"].ToString();
                }
                else
                {
                    trans.Main_Wallet = "0";
                    trans.Fund_Wallet = "0";
                    //Main_Wallet and Fund_Wallet properties of the trans object to "0".
                }

                DataTable dtP = UserManager.User_Report( "PRINCIPLEWALLETBALANCE",userInfo.memb_code);
                //Retrieves another DataTable for principle wallet balance.
                if (dtP.Rows.Count > 0)
                {

                    trans.Principle_BalanceAmount = dtP.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.Principle_BalanceAmount = "0";
                }

                DataTable dtR = UserManager.User_Report( "ROIWALLETBALANCE",userInfo.memb_code);
                if (dtR.Rows.Count > 0)
                {

                    trans.ROI_Balance = dtR.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.ROI_Balance = "0";
                }
            }
            catch (Exception ex)
            {
                trans.ROI_Balance = "0";
                trans.Fund_Wallet = "0";
                trans.Principle_BalanceAmount = "0";
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult TopUp(TransactionModel trans)
        {
            try
            {
               // trans.UserName = "";
                if (string.IsNullOrEmpty(trans.UserName.Trim()))
                {
                    TempData["TopUpAlert"] = "Please enter User id.";
                    return RedirectToAction("TopUp");
                }

                if (string.IsNullOrEmpty(trans.USD_Amount.Trim()))
                {
                    TempData["TopUpAlert"] = "Enter Proper Package Amount.";
                    return RedirectToAction("TopUp");
                }
                //If the condition is true , it sets an alert message in TempData indicating that the USD amount should not be negative. 
                //Then, it redirects the user to the "TopUp" action 
                if (Convert.ToDecimal(trans.USD_Amount.Trim()) < 0)
                {
                    TempData["TopUpAlert"] = "USD Amount Should Not Be Negative.";
                    return RedirectToAction("TopUp");
                }

                //if (string.IsNullOrEmpty(trans.Password))
                //{
                //    TempData["TopUpAlert"] = "Please enter Password.";
                //    return RedirectToAction("TopUp");
                //}
                //string PASS = "";
                //if (string.IsNullOrEmpty(trans.BTC_Type))
                //{
                //    TempData["TopUpAlert"] = "First update your btc details.";
                //    return RedirectToAction("TopUp");
                //}


                //DataTable dtU = UserManager.CheckUsername(trans.UserName);
                //if (dtU.Rows.Count > 0)
                //{
                //    //USDAmount = "100";
                //    string membCode = dtU.Rows[0]["memb_code"].ToString();
                //    DataTable checkauthrised = UserManager.User_Active(membCode, "ActiveUser");
                //    if (checkauthrised.Rows.Count > 0)
                //    {
                //        if (checkauthrised.Rows[0]["authrised"].ToString() == "G")
                //        {
                //            TempData["TopUpAlert"] = "your id is Already activate.";
                //            return RedirectToAction("TopUp");
                //        }
                //    }
                //}

                //DataTable CHECKAUTHRISED1 = UserManager.User_Report1(userInfo.memb_code, "CHECKDEPOSITAMOUNT1", trans.USD_Amount);
                //if (CHECKAUTHRISED1.Rows.Count > 0)
                //{
                    //if (CHECKAUTHRISED1.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
                    //{
                    //    TempData["TopUpAlert"] = "This Package is already Activate ... Only Successive Package can be Purchased !!!";
                    //    return RedirectToAction("Topup");
                    //}
                //}
                //DataTable CHECKAUTHRISED2 = UserManager.User_Report(userInfo.memb_code, "CHECKDEPOSITAMOUNT2");
                //if (CHECKAUTHRISED2.Rows.Count > 0)
                //{
                //    if (CHECKAUTHRISED2.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
                //    {
                //        TempData["TopUpAlert"] = "This Package is already Activate for this ID please enter other package";
                //        return RedirectToAction("Topup");
                //    }
                //}
                //DataTable CHECKAUTHRISE38 = UserManager.User_Report(userInfo.memb_code, "CHECKDEPOSITAMOUNT3");
                //if (CHECKAUTHRISE38.Rows.Count > 0)
                //{
                //    if (CHECKAUTHRISE38.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
                //    {
                //        TempData["TopUpAlert"] = "This Package is already Activate for this ID please enter other package";
                //        return RedirectToAction("Topup");
                //    }
                //}
                //DataTable CHECKAUTHRISED4 = UserManager.User_Report(userInfo.memb_code, "CHECKDEPOSITAMOUNT4");
                //if (CHECKAUTHRISED4.Rows.Count > 0)
                //{
                //    if (CHECKAUTHRISED4.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
                //    {
                //        TempData["TopUpAlert"] = "This Package is already Activate for this ID please enter other package";
                //        return RedirectToAction("Topup");
                //    }
                //}


                //DataTable dtPASS = UserManager.User_psReport(userInfo.memb_code, "CHECKPASS");
                //if (dtPASS.Rows.Count > 0)
                //{
                //    PASS = dtPASS.Rows[0]["RV_Code"].ToString();
                //}
                //DataTable topdt = UserManager.User_Active(userInfo.memb_code, "ActiveUser");
                //if (topdt.Rows.Count > 0)
                //{
                //    TempData["TopUpAlert"] = "User All Ready Active.";
                //}
                //else
                //{
                //if (userInfo.RV_Code == trans.Password)
                //{
                //string allPackages = "100,200,300,400,500,600,700,800,900,1000";




                //DataTable count = UserManager.USER_REPORT("count", userInfo.memb_code);

                //string count1 = count.Rows[0]["countt"].ToString();

                //if (count.Rows.Count > 0)
                //{
                //    if ((Convert.ToDecimal(count1) < 2) && (Convert.ToDecimal(trans.USD_Amount) == 1000))
                //    {
                //        TempData["TopUpAlert"] = "Please Complete Your 2 Direct.";
                //        return RedirectToAction("Topup");

                //    }
                //    else if ((Convert.ToDecimal(count1) < 3) && (Convert.ToDecimal(trans.USD_Amount) == 500))
                //    {
                //        TempData["TopUpAlert"] = "Please Complete Your 3 Direct.";
                //        return RedirectToAction("Topup");
                //    }
                //else if ((Convert.ToDecimal(count1) == 3) || (Convert.ToDecimal(trans.USD_Amount) == 500))
                //{
                //    TempData["TopUpAlert"] = "Member ID is already activate please enter another id";
                //    return RedirectToAction("Topup");
                //}

                //if (((Convert.ToDecimal(count1) == 0) || (Convert.ToDecimal(count1) == 1)) && (Convert.ToDecimal(trans.USD_Amount) == 50))
                //{
                //    TempData["TopUpAlert"] = "Already activate this Pool. Please select next pool amount.";
                //    return RedirectToAction("TopUp");
                //}
                //if (count.Rows.Count > 0)
                //{
                //    //if ((Convert.ToDecimal(count1) == 1) && (Convert.ToDecimal(trans.USD_Amount) == 50))
                //    //{
                //    //    //TempData["TopUpAlert"] = "Already activate this Pool. Please select next pool amount.";
                //    //    //return RedirectToAction("TopUp");
                //    //}
                //    if ((Convert.ToDecimal(count1) <= 2) && (Convert.ToDecimal(trans.USD_Amount) == 1000))
                //    {
                //        TempData["TopUpAlert"] = "Please Complete Your 2 Direct.";
                //        return RedirectToAction("Topup");

                //    }
                //    else if ((Convert.ToDecimal(count1) <= 3) && (Convert.ToDecimal(trans.USD_Amount) == 500))
                //    {
                //        TempData["TopUpAlert"] = "Please Complete Your 3 Direct.";
                //        return RedirectToAction("Topup");
                //    }


                //-------------------------------------------------------------------------------------

                int depositeStatus = 0;
                //Initializes an integer variable depositeStatus with a value of 0.
                string allPackages = "25,50,100,200,500,1000,5000";
                //Creates a string named allPackages containing a comma-separated list of deposit amounts.
                string[] pkg = allPackages.Split(',');
                //Split method is a string manipulation function that is used to divide a string into an array of substrings based on a specified delimiter
                //Splits the allPackages string into an array named pkg based on commas.

                //value in the pkg array using a for loop. If trans.USD_Amount matches any value in the array, sets depositeStatus to 10 and breaks out of the loop.
                for (int i = 0; i < pkg.Length; i++)
                {
                    if (string.Equals(trans.USD_Amount, pkg[i]))
                    {
                        depositeStatus = 10;
                        break;
                    }

                    //else
                    //{
                    //    TempData["TopUpAlert"] = "Invalid Top up !!!";
                    //    return RedirectToAction("TopUp");

                    //}
                }

                trans.BTC_Type = "FUNDWALLET";
                //Sets the value of trans.BTC_Type to "FUNDWALLET".
                //trans.USD_Amount = "250000";

                depositeStatus = 10;
                //sets depositeStatus to 10

                //Enters this block if depositeStatus is equal to 10
                if (depositeStatus == 10)
                {
                    //if ((Convert.ToDecimal(trans.USD_Amount) == 100) || (Convert.ToDecimal(trans.USD_Amount) == 6000) || (Convert.ToDecimal(trans.USD_Amount) == 600))
                    //if ((Convert.ToDecimal(trans.USD_Amount) >= 10) && (Convert.ToDecimal(trans.USD_Amount) % 1 == 0))
                    //Checks if the converted value of trans.USD_Amount is between $25 and $100,000.
                    // (such as integers, doubles, strings) to decimal
                    if ((Convert.ToDecimal(trans.USD_Amount = trans.USD_Amount) >= 25) && (Convert.ToDecimal(trans.USD_Amount) <= 100000))
                    {
                        DataTable dtUser = UserManager.CheckUsername(trans.UserName);
                        //CheckUsername method of the UserManager class, passing the trans.UserName as an argument.

                        if (dtUser.Rows.Count > 0)
                        //Checks if there is at least one row in the dtUser DataTable. If true, it means user details were found.
                        {
                            string membCode = dtUser.Rows[0]["memb_code"].ToString();

                            //This section checks the value of trans.BTC_Type and executes different blocks of code based on the type of wallet selected.
                            if (trans.BTC_Type == "PRINCIPLE")
                            {
                                DataTable dt = UserManager.User_Report(userInfo.memb_code, "PRINCIPLEWALLETBALANCE");
                                if (dt.Rows.Count > 0)
                                {
                                    string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

                                    if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(Total_Balance))
                                    {
                                        int result = UserManager.User_TopUp("ADDTOPUP", userInfo.memb_code, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                        if (result > 0)
                                        {
                                            TempData["TopUpAlert"] = "1";

                                            string trno = "0";
                                            string sub = "Top Up";
                                            string result1 = EmailFUnd(userInfo.EmailID, sub, trans.UserName, trans.USD_Amount);
                                        }
                                        else
                                        {
                                            TempData["TopUpAlert"] = "Activation failed.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["TopUpAlert"] = "You have insufficient balance in your main wallet.";
                                    }
                                }
                                else
                                {
                                    TempData["TopUpAlert"] = "0 balance in your main wallet to top up this user.";
                                }
                            }
                            else if (trans.BTC_Type == "ROI")
                            {
                                DataTable dt = UserManager.User_Report(userInfo.memb_code, "ROIWALLETBALANCE");
                                if (dt.Rows.Count > 0)
                                {
                                    string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

                                    if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(Total_Balance))
                                    {
                                        int result = UserManager.User_TopUp1("ADDTOPUP1", userInfo.memb_code, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                        if (result > 0)
                                        {
                                            TempData["TopUpAlert"] = "1";

                                            string trno = "0";
                                            string sub = "Top Up";
                                            //string result1 = EmailFUnd(userInfo.EmailID, sub, trans.UserName, trans.USD_Amount);
                                        }
                                        else
                                        {
                                            TempData["TopUpAlert"] = "Activation failed.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["TopUpAlert"] = "You have insufficient balance in your main wallet.";
                                    }
                                }
                                else
                                {
                                    TempData["TopUpAlert"] = "0 balance in your main wallet to top up this user.";
                                }
                            }

                            else if (trans.BTC_Type == "FUNDWALLET")
                            {
                                trans.BTC_Type = "FUNDWALLET";
                                trans.Plan_Type = "INVESTMENT";
                                DataTable dt = UserManager.User_Report( "FUNDWALLET",userInfo.memb_code);
                                //user information from the UserManager based on the provided trans.UserName.

                                //Checks if there is at least one row in the DataTable dtUser and retrieves the value of the "memb_code" column for the first row.
                                if (dt.Rows.Count > 0)
                                {
                                    string total_balance = dt.Rows[0]["fund_wallet"].ToString();

                                    if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(total_balance))
                                        //This line checks if the converted value of trans.USD_Amount is less than or equal to the converted value of total_balance .
                                        //This condition ensures that the user has enough balance in their fund wallet to proceed with the top-up operation.
                                        // CONDITION TRUE THEN ENTER IF 
                                    {
                                        int result = UserManager.User_TopUp("ADDTOPUP", membCode, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                        //If the condition is true, it calls the User_TopUp method from the UserManager class to perform a top-up operation. 
                                        // Various parameters are passed to the method, including the operation type (ADDTOPUP), user code (membCode), top-up amount (trans.USD_Amount), and other relevant information.
                                        if (result > 0)
                                        //Checks if the result of the top-up operation (result) is greater than 0
                                        {
                                            TempData["TopUpAlert"] = "1";
                                            // If true, it sets TempData["TopUpAlert"] to "1,"
                                        }
                                        else
                                        {
                                            TempData["TopUpAlert"] = "Activation failed.";
                                            //If the result is not greater than 0, it sets the alert to "Activation failed."
                                        }
                                    }
                                    else
                                    {
                                        TempData["TopUpAlert"] = "You have insufficient balance in your fund wallet.";
                                        //If the condition in the initial if statement is not met (insufficient balance), it sets TempData["TopUpAlert"] to "You have insufficient balance in your fund wallet."
                                    }
                                }
                                else
                                {
                                    TempData["TopUpAlert"] = "0 balance in your fund wallet to top up this user.";
                                }
                            }

                            else
                            {
                                TempData["TopUpAlert"] = "Please Select Wallet Type.";
                            }
                        }
                        else
                        {
                            TempData["TopUpAlert"] = "Please enter valid User id.";
                        }
                    }
                    else
                    {
                        TempData["TopUpAlert"] = "Please enter package minimum $10 and multiple $1.";
                    }
                }
                //}
                //else
                //{
                //    TempData["TopUpAlert"] = "Please Enter Valid Transaction Password.";
                //}
                //}


                //else
                //{
                //    TempData["TopUpAlert"] = "Please TopUp.";
                //}



            }
            catch (Exception ex)
            {
                TempData["TopUpAlert"] = "Activation failed.";
            }
            return RedirectToAction("TopUp");
        }

        [HttpGet]
        public ActionResult AutoPoolTopUp()
        {
            TransactionModel trans = new TransactionModel();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                if (dt.Rows.Count > 0)
                {

                    trans.Fund_Wallet = "$ " + dt.Rows[0]["Fund_Wallet"].ToString();
                }
                else
                {
                    trans.Pool_Wallet = "0";
                }
            }
            catch (Exception ex)
            {
                trans.Pool_Balance = "0";
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult AutoPoolTopUp(TransactionModel trans)
        {
            try
            {
                if (string.IsNullOrEmpty(trans.UserName.Trim()))
                {
                    TempData["TopUpAlert"] = "Please enter User id.";
                    return RedirectToAction("TopUp");
                }

                if (string.IsNullOrEmpty(trans.USD_Amount.Trim()))
                {
                    TempData["TopUpAlert"] = "Enter Proper Package Amount.";
                    return RedirectToAction("TopUp");
                }

                if (Convert.ToDecimal(trans.USD_Amount.Trim()) < 0)
                {
                    TempData["TopUpAlert"] = "USD Amount Should Not Be Less Than 0.";
                    return RedirectToAction("AutoPoolTopUp");
                }



                int depositeStatus = 0;
                string allPackages = "20";
                string[] pkg = allPackages.Split(',');
                for (int i = 0; i < pkg.Length; i++)
                {
                    if (string.Equals(trans.USD_Amount, pkg[i]))
                    {
                        depositeStatus = 1;
                        break;
                    }
                }

                trans.BTC_Type = "FUNDWALLET";
                //trans.Plan_Type = trans.pool;
                //trans.Plan_Type = "Pool";


                depositeStatus = 1;

                if (depositeStatus == 1)
                {
                    if ((Convert.ToDecimal(trans.USD_Amount) == 20))
                    {
                        DataTable dtUser = UserManager.CheckUsername(trans.UserName);
                        if (dtUser.Rows.Count > 0)
                        {
                            string membCode = dtUser.Rows[0]["memb_code"].ToString();
                            string autho = dtUser.Rows[0]["authrised"].ToString();
                            if (autho == "R")
                            {
                                TempData["TopUpAlert"] = "Please topUp first minimum 10$";
                                return RedirectToAction("AutoPoolTopUp");
                            }
                            //DataTable CHECK = UserManager.User_Report(membCode, "CHECK");
                            //if (CHECK.Rows.Count > 1)
                            //{
                            //    //if (CHECK.Rows[0]["authrised='G' "].ToString() == trans.authrised)
                            //    //{
                            //    TempData["TopUpAlert"] = "Please topUp first minimum 10$";
                            //    return RedirectToAction("Topup");
                            //    //}
                            //}

                            if (trans.BTC_Type == "FUNDWALLET")
                            {
                                trans.BTC_Type = "FUNDWALLET";
                                trans.Plan_Type = "POOL";
                                DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                                if (dt.Rows.Count > 0)
                                {
                                    string total_balance = dt.Rows[0]["fund_wallet"].ToString();

                                    if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(total_balance))
                                    {
                                        int result = UserManager.User_TopUp("ADDTOPUP", membCode, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                        if (result > 0)
                                        {
                                            TempData["TopUpAlert"] = "1";
                                        }
                                        else
                                        {
                                            TempData["TopUpAlert"] = "Activation failed.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["TopUpAlert"] = "You have insufficient balance in your fund wallet.";
                                    }
                                }
                                else
                                {
                                    TempData["TopUpAlert"] = "0 balance in your fund wallet to top up this user.";
                                }
                            }

                            else
                            {
                                TempData["TopUpAlert"] = "Please Select Wallet Type.";
                            }
                        }
                        //    else
                        //    {
                        //        TempData["TopUpAlert"] = "Please enter valid User id.";
                        //    }
                        //}
                        else
                        {
                            TempData["TopUpAlert"] = "Please Select Valid Package.";
                        }
                        //}
                        //else
                        //{
                        //    TempData["TopUpAlert"] = "Please Enter Valid Transaction Password.";
                        //}


                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["TopUpAlert"] = "Activation failed.";
            }
            return RedirectToAction("AutoPoolTopUp");
        }

        public JsonResult getUserDetails(string email)
        {
            SponsorDetailsModel user = new SponsorDetailsModel();
            try
            {
                DataTable dt = UserManager.CheckUsername(email);
                if (dt.Rows.Count > 0)
                {
                    //DataTable dtD = UserManager.GetDownlineUser(userInfo.memb_code, dt.Rows[0]["memb_code"].ToString());
                    //if (dtD.Rows.Count > 0)
                    //{
                    user.Memb_Name = dt.Rows[0]["Memb_Name"].ToString();
                    //}
                    //else
                    //{
                    //    user.Memb_Name = "This user id not in your downline.";
                    //}
                }
                else
                {
                    user.Memb_Name = "User id not valid.";
                }
            }
            catch
            {
                user.Memb_Name = "User id not valid.";
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TopUpReport()
        {
            List<DepositeModel> dList = new List<DepositeModel>();
            try
            {
                // Call the UserManager method to retrieve top-up history for the current user
                DataTable dtTrans = UserManager.User_TopUp_History("TOPUPHISTORY", userInfo.memb_code);

                // Check if there are any rows in the DataTable
                if (dtTrans.Rows.Count > 0)
                {
                    // Serialize the DataTable to JSON string
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    // Deserialize the JSON string into a list of DepositeModel objects
                    dList = JsonConvert.DeserializeObject<List<DepositeModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }

        public ActionResult BoosterTopUp()
        {
            TransactionModel trans = new TransactionModel();
            try
            {
                trans.UserName = userInfo.username;

                DataTable dt = UserManager.User_Report("FUNDWALLET", userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {

                    trans.Fund_Wallet = "$ " + dt.Rows[0]["Fund_Wallet"].ToString();

                    trans.Last_Package = dt.Rows[0]["Last_Package"].ToString();
                }
                else
                {
                    trans.Main_Wallet = "0";
                    trans.Fund_Wallet = "0";
                }

                DataTable dtP = UserManager.User_Report("PRINCIPLEWALLETBALANCE", userInfo.memb_code);
                if (dtP.Rows.Count > 0)
                {

                    trans.Principle_BalanceAmount = dtP.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.Principle_BalanceAmount = "0";
                }
                DataTable dtR = UserManager.User_Report("ROIWALLETBALANCE", userInfo.memb_code);
                if (dtR.Rows.Count > 0)
                {

                    trans.ROI_Balance = dtR.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.ROI_Balance = "0";
                }
            }
            catch (Exception ex)
            {
                trans.ROI_Balance = "0";
                trans.Fund_Wallet = "0";
                trans.Principle_BalanceAmount = "0";
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult BoosterTopUp(TransactionModel trans)
        {
            try
            {
                //if (string.IsNullOrEmpty(trans.UserName.Trim()))
                //{
                //    TempData["MakeActivationAlert"] = "Please enter User id.";
                //    return RedirectToAction("MakeActivation");
                //}

                if (string.IsNullOrEmpty(trans.USD_Amount.Trim()))
                {
                    TempData["TopupAlert"] = "Please Select Package.";
                    return RedirectToAction("BoosterTopUp");
                }


                //if (string.IsNullOrEmpty(trans.Password))
                //{
                //    TempData["MakeActivationAlert"] = "Please enter Password.";
                //    return RedirectToAction("MakeActivation");
                //}

                string PASS = "";

                string amount = trans.USD_Amount;

                DataTable dtU = UserManager.CheckUsername(userInfo.username);
                string membCode = dtU.Rows[0]["memb_code"].ToString();

                //if (dtU.Rows.Count > 0)
                //    {
                //    //USDAmount = "100";
                //    string membCode = dtU.Rows[0]["memb_code"].ToString();
                //    DataTable checkauthrised = UserManager.User_Active(membCode, "ActiveUser");
                //    if (checkauthrised.Rows.Count > 0)
                //    {
                //        if (checkauthrised.Rows[0]["authrised"].ToString() == "G" && trans.Amount == "40")
                //        {
                //            TempData["MakeActivationAlert"] = "your id is Already activate First Pool.";
                //            return RedirectToAction("TopUp");
                //        }
                //    }
                //}

                //DataTable dtPASS = UserManager.User_psReport(userInfo.memb_code, "CHECKPASS");
                //if (dtPASS.Rows.Count > 0)
                //{
                //   string PASS = dtPASS.Rows[0]["pin_no"].ToString();
                //}
                //DataTable topdt = UserManager.User_Active(userInfo.memb_code, "ActiveUser");
                //if (topdt.Rows.Count > 0)
                //{
                //    TempData["MakeActivationAlert"] = "User All Ready Active.";
                //}
                //else
                //{

                //DataTable CONDITION = UserManager.User_Report(membCode, "MAXAMOUNT");

                //string amt = CONDITION.Rows[0]["maxcount"].ToString();

                //if (amt == amount)
                //{
                //    TempData["MakeActivationAlert"] = "You are already top up for this package amuont!!";
                //}

                //if (CONDITION.Rows.Count > 0)
                //{
                //    if (amt == "0" && amount != "40")
                //    {
                //        TempData["MakeActivationAlert"] = "Please TopUp 40";
                //        return RedirectToAction("BoosterTopUp");
                //    }
                //    else if (amt == "40" && amount != "80")
                //    {
                //        TempData["MakeActivationAlert"] = "Please TopUp 80";
                //        return RedirectToAction("BoosterTopUp");
                //    }
                //    else if (amt == "80" && amount != "160")
                //    {
                //        //TempData["TopUpAlert"] = "Member ID is already activate please enter another id";
                //        TempData["MakeActivationAlert"] = "Please TopUp 160";
                //        return RedirectToAction("BoosterTopUp");
                //    }
                //    else if (amt == "160" && amount != "320")
                //    {
                //        TempData["MakeActivationAlert"] = "Please TopUp 320";
                //        return RedirectToAction("BoosterTopUp");
                //    }
                //    else if (amt == "320" && amount != "640")
                //    {
                //        TempData["MakeActivationAlert"] = "Please TopUp 640";
                //        return RedirectToAction("BoosterTopUp");
                //    }
                //    else if (amt == "640" && amount != "1280")
                //    {
                //        TempData["MakeActivationAlert"] = "Please TopUp $1280";
                //        return RedirectToAction("BoosterTopUp");
                //    }
                //    else if (amt == "1280" && amount != "2560")
                //    {
                //        TempData["MakeActivationAlert"] = "Please TopUp 2560";
                //        return RedirectToAction("BoosterTopUp");
                //    }
                //    else if (amt == "2560" && amount != "5120")
                //    {
                //        TempData["MakeActivationAlert"] = "Please TopUp 5120";
                //        return RedirectToAction("MakeActivation");
                //    }
                //    else if (amt == "5120" && amount != "10240")
                //    {
                //        TempData["MakeActivationAlert"] = "Please TopUp 10240";
                //        return RedirectToAction("BoosterTopUp");
                //    }
                //    else if (amt == "10240" || amount == "10240")
                //    {
                //        TempData["MakeActivationAlert"] = "Member ID is already activate please enter another id";
                //        return RedirectToAction("BoosterTopUp");
                //    }
                //}


                //if (userInfo.pin_no == trans.pin_no)
                //{
                string allPackages = "10,50,100,200,500";
                string[] pkg = allPackages.Split(',');
                int depositeStatus = 0;
                for (int i = 0; i < pkg.Length; i++)
                {
                    if (string.Equals(trans.USD_Amount, pkg[i]))
                    {
                        depositeStatus = 1;
                        break;
                    }
                }

                trans.BTC_Type = "AUTOPOOL1";
                if (depositeStatus == 1)
                {



                    //if ((Convert.ToDecimal(trans.USD_Amount) >= 2500) && (Convert.ToDecimal(trans.USD_Amount) % 2500 == 0))
                    //{
                    DataTable dtUser = UserManager.CheckUsername(userInfo.username);
                    if (dtUser.Rows.Count > 0)
                    {
                        //string membCode = dtUser.Rows[0]["memb_code"].ToString();

                        //DataTable dtc = UserManager.User_Report("CONDITION", membCode, trans.USD_Amount);
                        //if (dtc.Rows.Count > 0)
                        //{
                        //    TempData["MakeActivationAlert"] = "You are already top up for this package amount!!";
                        //    return RedirectToAction("MakeActivation");
                        //}


                        if (trans.BTC_Type == "PRINCIPLE")
                        {
                            DataTable dt = UserManager.User_Report("PRINCIPLEWALLETBALANCE",userInfo.memb_code);
                            if (dt.Rows.Count > 0)
                            {
                                string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

                                if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(Total_Balance))
                                {
                                    //int result = UserManager.User_TopUp("ADDTOPUP", membCode, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                    int result = UserManager.User_TopUp("ADDTOPUP1", membCode, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                    if (result > 0)
                                    {
                                        TempData["TopUpAlert"] = "1";

                                        string trno = "0";
                                        string sub = "Top Up";
                                        string result1 = EmailFUnd(userInfo.EmailID, sub, trans.UserName, trans.USD_Amount);
                                    }
                                    else
                                    {
                                        TempData["TopUpAlert"] = "Activation failed.";
                                    }
                                }
                                else
                                {
                                    TempData["TopUpAlert"] = "You have insufficient balance in your main wallet.";
                                }
                            }
                            else
                            {
                                TempData["TopUpAlert"] = "0 balance in your main wallet to top up this user.";
                            }
                        }
                        else if (trans.BTC_Type == "ROI")
                        {
                            DataTable dt = UserManager.User_Report( "ROIWALLETBALANCE",userInfo.memb_code);
                            if (dt.Rows.Count > 0)
                            {
                                string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

                                if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(Total_Balance))
                                {
                                    int result = UserManager.User_TopUp("ADDTOPUP", membCode, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                    if (result > 0)
                                    {
                                        TempData["TopUpAlert"] = "1";

                                        string trno = "0";
                                        string sub = "Top Up";
                                        string result1 = EmailFUnd(userInfo.EmailID, sub, trans.UserName, trans.USD_Amount);
                                    }
                                    else
                                    {
                                        TempData["TopUpAlert"] = "Activation failed.";
                                    }
                                }
                                else
                                {
                                    TempData["TopUpAlert"] = "You have insufficient balance in your main wallet.";
                                }
                            }
                            else
                            {
                                TempData["TopUpAlert"] = "0 balance in your main wallet to top up this user.";
                            }
                        }
                        else if (trans.BTC_Type == "AUTOPOOL1")
                        {
                            trans.BTC_Type = "AUTOPOOL1";
                            DataTable dt = UserManager.User_Report( "FUNDWALLET",userInfo.memb_code);
                            if (dt.Rows.Count > 0)
                            {
                                string BalanceAmount = dt.Rows[0]["Fund_Wallet"].ToString();

                                if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(BalanceAmount))
                                {
                                    int result = UserManager.User_TopUp("ADDTOPUP1", membCode, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, "AUTOPOOL1", userInfo.memb_code);
                                    if (result > 0)
                                    {
                                        TempData["TopUpAlert"] = "1";
                                    }
                                    else
                                        TempData["TopUpAlert"] = "Activation failed.";
                                }
                                else
                                {
                                    TempData["TopUpAlert"] = "You have insufficient balance in your fund wallet.";
                                }
                            }
                            else
                            {
                                TempData["TopUpAlert"] = "0 balance in your fund wallet to top up this user.";
                            }

                        }

                        else
                        {
                            TempData["TopUpAlert"] = "Please Select Wallet Type.";
                        }
                    }
                    //    else
                    //    {
                    //        TempData["MakeActivationAlert"] = "Please enter valid User id.";
                    //    }
                    //}
                    else
                    {
                        TempData["TopUpAlert"] = "Please Select Valid Package.";
                    }
                }
                //}
                //else
                //{
                //    TempData["MakeActivationAlert"] = "Please Enter Valid Transaction Password.";
                //}

            }

            catch (Exception ex)
            {
                TempData["TopUpAlert"] = "Activation failed.";
            }
            return RedirectToAction("BoosterTopUp");
            //return RedirectToAction("PopUp1");
        }
        //        public ActionResult BoosterTopUp(TransactionModel trans)
        //        {
        //            try
        //            {
        //                trans.UserName = "";
        //                if (string.IsNullOrEmpty(trans.UserName.Trim()))
        //                {
        //                    TempData["TopUpAlert"] = "Please enter User id.";
        //                    return RedirectToAction("TopUp");
        //                }

        //                if (string.IsNullOrEmpty(trans.USD_Amount.Trim()))
        //                {
        //                    TempData["TopUpAlert"] = "Enter Proper Package Amount.";
        //                    return RedirectToAction("TopUp");
        //                }

        //                if (Convert.ToDecimal(trans.USD_Amount.Trim()) < 0)
        //                {
        //                    TempData["TopUpAlert"] = "USD Amount Should Not Be Negative.";
        //                    return RedirectToAction("TopUp");
        //                }

        //                if (string.IsNullOrEmpty(trans.Password))
        //                {
        //                    TempData["TopUpAlert"] = "Please enter Password.";
        //                    return RedirectToAction("TopUp");
        //                }
        //                string PASS = "";
        //                if (string.IsNullOrEmpty(trans.BTC_Type))
        //                {
        //                    TempData["TopUpAlert"] = "First update your btc details.";
        //                    return RedirectToAction("TopUp");
        //                }


        //                DataTable dtU = UserManager.CheckUsername(trans.UserName);
        //                if (dtU.Rows.Count > 0)
        //                {
        //                    //USDAmount = "100";
        //                    string membCode = dtU.Rows[0]["memb_code"].ToString();
        //                    DataTable checkauthrised = UserManager.User_Active(membCode, "ActiveUser");
        //                    if (checkauthrised.Rows.Count > 0)
        //                    {
        //                        if (checkauthrised.Rows[0]["authrised"].ToString() == "G")
        //                        {
        //                            TempData["TopUpAlert"] = "your id is Already activate.";
        //                            return RedirectToAction("TopUp");
        //                        }
        //                    }
        //                }

        //                DataTable CHECKAUTHRISED1 = UserManager.User_Report1(userInfo.memb_code, "CHECKDEPOSITAMOUNT1", trans.USD_Amount);
        //                if (CHECKAUTHRISED1.Rows.Count > 0)
        //                {
        //                    if (CHECKAUTHRISED1.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
        //                    {
        //                        TempData["TopUpAlert"] = "This Package is already Activate ... Only Successive Package can be Purchased !!!";
        //                        return RedirectToAction("Topup");
        //                    }
        //                }


        //                int depositeStatus = 0;
        //                string allPackages = "10,50,100,200,500";
        //                string[] pkg = allPackages.Split(',');
        //                for (int i = 0; i < pkg.Length; i++)
        //                {
        //                    if (string.Equals(trans.USD_Amount, pkg[i]))
        //                    {
        //                        depositeStatus = 10;
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        TempData["TopUpAlert"] = "Invalid Top up !!!";
        //                        return RedirectToAction("TopUp");

        //                    }
        //                }

        //                trans.BTC_Type = "AUTOPOOL1";
        //                trans.Plan_Type = "BOOSTER";
        //                trans.USD_Amount = "250000";

        //                depositeStatus = 10;

        //                if (depositeStatus == 10)
        //                {
        //                    if ((Convert.ToDecimal(trans.USD_Amount) == 10) || (Convert.ToDecimal(trans.USD_Amount) == 600) || (Convert.ToDecimal(trans.USD_Amount) == 600))
        //                        if ((Convert.ToDecimal(trans.USD_Amount = trans.USD_Amount) >= 10) && (Convert.ToDecimal(trans.USD_Amount) <= 100000))

        //                        {
        //                            DataTable dtUser = UserManager.CheckUsername(trans.UserName);
        //                            if (dtUser.Rows.Count > 0)
        //                            {
        //                                string membCode = dtUser.Rows[0]["memb_code"].ToString();

        //                                if (trans.BTC_Type == "AUTOPOOL1")
        //                                {
        //                                    DataTable dt = UserManager.User_Report("FUNDWALLET", userInfo.memb_code);
        //                                    if (dt.Rows.Count > 0)
        //                                    {
        //                                        string BalanceAmount = dt.Rows[0]["Fund_Wallet"].ToString();

        //                                        if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(BalanceAmount))
        //                                        {
        //                                            int result = UserManager.User_TopUp("ADDTOPUP1", membCode, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
        //                                            int result = UserManager.User_TopUp1("ADDTOPUP", userInfo.memb_code, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
        //                                            int result = UserManager.User_TopUp("ADDTOPUP", membCode, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, "AUTOPOOL1", userInfo.memb_code);
        //                                            if (result > 0)
        //                                            {
        //                                                TempData["TopUpAlert"] = "1";

        //                                                string trno = "0";
        //                                                string sub = "Top Up";
        //                                                string result1 = EmailFUnd(userInfo.EmailID, sub, trans.UserName, trans.USD_Amount);
        //                                            }
        //                                            else
        //                                            {
        //                                                TempData["TopUpAlert"] = "Activation failed.";
        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            TempData["TopUpAlert"] = "You have insufficient balance in your fund wallet.";
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        TempData["TopUpAlert"] = "0 balance in your main wallet to top up this user.";
        //                                    }
        //                                }
        //                                else if (trans.BTC_Type == "ROI")
        //                                {
        //                                    DataTable dt = UserManager.User_Report("ROIWALLETBALANCE", userInfo.memb_code);
        //                                    if (dt.Rows.Count > 0)
        //                                    {
        //                                        string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

        //                                        if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(Total_Balance))
        //                                        {
        //                                            int result = UserManager.User_TopUp("ADDTOPUP", userInfo.memb_code, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
        //                                            if (result > 0)
        //                                            {
        //                                                TempData["TopUpAlert"] = "1";

        //                                                string trno = "0";
        //                                                string sub = "Top Up";
        //                                                string result1 = EmailFUnd(userInfo.EmailID, sub, trans.UserName, trans.USD_Amount);
        //                                            }
        //                                            else
        //                                            {
        //                                                TempData["TopUpAlert"] = "Activation failed.";
        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            TempData["TopUpAlert"] = "You have insufficient balance in your fund wallet.";
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        TempData["TopUpAlert"] = "0 balance in your main wallet to top up this user.";
        //                                    }
        //                                }
        //                                else if (trans.BTC_Type == "FUNDWALLET")
        //                                {
        //                                    trans.BTC_Type = "FUNDWALLET";
        //                                    trans.Plan_Type = "INVESTMENT";
        //                                    DataTable dt = UserManager.User_Report("FUNDWALLET", userInfo.memb_code);
        //                                    if (dt.Rows.Count > 0)
        //                                    {
        //                                        string total_balance = dt.Rows[0]["fund_wallet"].ToString();

        //                                        if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(total_balance))
        //                                        {
        //                                            int result = UserManager.User_TopUp("ADDTOPUP", membCode, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
        //                                            if (result > 0)
        //                                            {
        //                                                TempData["TopUpAlert"] = "1";
        //                                            }
        //                                            else
        //                                            {
        //                                                TempData["TopUpAlert"] = "Activation failed.";
        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            TempData["TopUpAlert"] = "You have insufficient balance in your fund wallet.";
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        TempData["TopUpAlert"] = "0 balance in your fund wallet to top up this user.";
        //                                    }
        //                                }

        //                                else
        //                                {
        //                                    TempData["TopUpAlert"] = "Please Select Wallet Type.";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                TempData["TopUpAlert"] = "Please enter valid User id.";
        //                            }
        //                        }
        //                        else
        //                        {
        //                            TempData["TopUpAlert"] = "Please enter package minimum $100 and multiple $5000.";
        //                        }
        //                }
        //            }
        //                    else
        //            {
        //                TempData["TopUpAlert"] = "Please Enter Valid Transaction Password.";
        //            }
        //        }


        //                    else
        //                    {
        //                        TempData["TopUpAlert"] = "Please TopUp.";
        //                    }



        //}
        //            catch (Exception ex)
        //{
        //    TempData["TopUpAlert"] = "Activation failed.";
        //}
        //return RedirectToAction("BoosterTopUp");
        //        }



        public ActionResult BoosterTopUpReport()
        {
            List<DepositeModel> dList = new List<DepositeModel>();
            try
            {
                DataTable dtTrans = UserManager.User_TopUp_History("BOOSTERTOPUPHISTORY", userInfo.memb_code);
                if (dtTrans.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    dList = JsonConvert.DeserializeObject<List<DepositeModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }
        public ActionResult TopUpReport_partial()
        {
            List<DepositeModel> dList = new List<DepositeModel>();
            try
            {
                DataTable dtTrans = UserManager.User_TopUp_History("TOPUPHISTORY", userInfo.memb_code);
                if (dtTrans.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    dList = JsonConvert.DeserializeObject<List<DepositeModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }

        public ActionResult AutoPoolTopUpReport()
        {
            List<DepositeModel> dList = new List<DepositeModel>();
            try
            {
                DataTable dtTrans = UserManager.User_TopUp_History("TOPUPHISTORYPOOL", userInfo.memb_code);
                if (dtTrans.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    dList = JsonConvert.DeserializeObject<List<DepositeModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }
        [HttpGet]
        public ActionResult MainToTopUpWallet()
        {
            TransactionModel trans = new TransactionModel();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "WORKINGWALLETBALANCE1");
                if (dt.Rows.Count > 0)
                {

                    trans.Main_Wallet = dt.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.Main_Wallet = "0";
                }
                //try
                //{
                //    DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                //    if (dt.Rows.Count > 0)
                //    {

                //        trans.Fund_Wallet = "$ " + dt.Rows[0]["Fund_Wallet"].ToString();
                //    }
                //    else
                //    {
                //        trans.Main_Wallet = "0";
                //        trans.Fund_Wallet = "0";
                //    }

                DataTable dtP = UserManager.User_Report(userInfo.memb_code, "PRINCIPLEWALLETBALANCE");
                if (dtP.Rows.Count > 0)
                {

                    trans.Principle_BalanceAmount = dtP.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.Principle_BalanceAmount = "0";
                }
                DataTable dtR = UserManager.User_Report(userInfo.memb_code, "ROIWALLETBALANCE");
                if (dtR.Rows.Count > 0)
                {

                    trans.ROI_Balance = dtR.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.ROI_Balance = "0";
                }
            }
            catch (Exception ex)
            {
                trans.ROI_Balance = "0";
                trans.Fund_Wallet = "0";
                trans.Principle_BalanceAmount = "0";
            }
            return View(trans);

        }
        [HttpPost]
        public ActionResult MainToTopUpWallet(TransactionModel trans)
        {

            try
            {
                if (string.IsNullOrEmpty(trans.UserName.Trim()))
                {
                    TempData["TopUpAlert"] = "Please enter User id.";
                    return RedirectToAction("MainToTopUpWallet");
                }

                //if (string.IsNullOrEmpty(trans.USD_Amount.Trim()))
                //{
                //    TempData["TopUpAlert"] = "Enter Proper Package Amount.";
                //    return RedirectToAction("MainToTopUpWallet");
                //}

                if (Convert.ToDecimal(trans.USD_Amount.Trim()) < 0)
                {
                    TempData["TopUpAlert"] = "USD Amount Should Not Be Negative.";
                    return RedirectToAction("MainToTopUpWallet");
                }

                trans.Main_Wallet = "WORKING";
                if (string.IsNullOrEmpty(trans.Main_Wallet))
                {
                    TempData["WithdrawlAlert"] = "Please Select Wallet Type.";
                    return RedirectToAction("MainToTopUpWallet");
                }
                //if (string.IsNullOrEmpty(trans.Password))
                //{
                //    TempData["TopUpAlert"] = "Please enter Password.";
                //    return RedirectToAction("TopUp");
                //}
                //string PASS = "";
                //if (string.IsNullOrEmpty(trans.BTC_Type))
                //{
                //    TempData["TopUpAlert"] = "First update your btc details.";
                //    return RedirectToAction("TopUp");
                //}


                //DataTable dtU = UserManager.CheckUsername(trans.UserName);
                //if (dtU.Rows.Count > 0)
                //{
                //    //USDAmount = "100";
                //    string membCode = dtU.Rows[0]["memb_code"].ToString();
                //    DataTable checkauthrised = UserManager.User_Active(membCode, "ActiveUser");
                //    if (checkauthrised.Rows.Count > 0)
                //    {
                //        if (checkauthrised.Rows[0]["authrised"].ToString() == "G")
                //        {
                //            TempData["TopUpAlert"] = "your id is Already activate.";
                //            return RedirectToAction("TopUp");
                //        }
                //    }
                //}

                //DataTable CHECKAUTHRISED1 = UserManager.User_Report1(userInfo.memb_code, "CHECKDEPOSITAMOUNT1", trans.USD_Amount);
                //if (CHECKAUTHRISED1.Rows.Count > 0)
                //{
                //    if (CHECKAUTHRISED1.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
                //    {
                //        TempData["TopUpAlert"] = "This Package is already Activate for this ID please enter other package";
                //        return RedirectToAction("Topup");
                //    }
                //}
                //DataTable CHECKAUTHRISED2 = UserManager.User_Report(userInfo.memb_code, "CHECKDEPOSITAMOUNT2");
                //if (CHECKAUTHRISED2.Rows.Count > 0)
                //{
                //    if (CHECKAUTHRISED2.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
                //    {
                //        TempData["TopUpAlert"] = "This Package is already Activate for this ID please enter other package";
                //        return RedirectToAction("Topup");
                //    }
                //}
                //DataTable CHECKAUTHRISE38 = UserManager.User_Report(userInfo.memb_code, "CHECKDEPOSITAMOUNT3");
                //if (CHECKAUTHRISE38.Rows.Count > 0)
                //{
                //    if (CHECKAUTHRISE38.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
                //    {
                //        TempData["TopUpAlert"] = "This Package is already Activate for this ID please enter other package";
                //        return RedirectToAction("Topup");
                //    }
                //}
                //DataTable CHECKAUTHRISED4 = UserManager.User_Report(userInfo.memb_code, "CHECKDEPOSITAMOUNT4");
                //if (CHECKAUTHRISED4.Rows.Count > 0)
                //{
                //    if (CHECKAUTHRISED4.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
                //    {
                //        TempData["TopUpAlert"] = "This Package is already Activate for this ID please enter other package";
                //        return RedirectToAction("Topup");
                //    }
                //}


                //DataTable dtPASS = UserManager.User_psReport(userInfo.memb_code, "CHECKPASS");
                //if (dtPASS.Rows.Count > 0)
                //{
                //    PASS = dtPASS.Rows[0]["RV_Code"].ToString();
                //}
                //DataTable topdt = UserManager.User_Active(userInfo.memb_code, "ActiveUser");
                //if (topdt.Rows.Count > 0)
                //{
                //    TempData["TopUpAlert"] = "User All Ready Active.";
                //}
                //else
                //{
                //if (userInfo.RV_Code==trans.Password )
                //{
                //string allPackages = "100,200,300,400,500,600,700,800,900,1000";




                int depositeStatus = 0;
                //string allPackages = "20";
                //string[] pkg = allPackages.Split(',');
                //for (int i = 0; i < pkg.Length; i++)
                //{
                //    if (string.Equals(trans.USD_Amount, pkg[i]))
                //    {
                //        depositeStatus = 10;
                //        break;
                //    }
                //}

                trans.BTC_Type = "WALLET";
                //trans.USD_Amount = "250000";

                depositeStatus = 10;

                if (depositeStatus == 10)
                {
                    if ((Convert.ToDecimal(trans.USD_Amount) >= 10) && (Convert.ToDecimal(trans.USD_Amount) % 1 == 0))
                    {
                        DataTable dtUser = UserManager.CheckUsername(trans.UserName);
                        if (dtUser.Rows.Count > 0)
                        {
                            string membCode = dtUser.Rows[0]["memb_code"].ToString();

                            if (trans.BTC_Type == "PRINCIPLE")
                            {
                                DataTable dt = UserManager.User_Report(userInfo.memb_code, "PRINCIPLEWALLETBALANCE");
                                if (dt.Rows.Count > 0)
                                {
                                    string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

                                    if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(Total_Balance))
                                    {
                                        int result = UserManager.User_TopUp("ADDTOPUP", userInfo.memb_code, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                        if (result > 0)
                                        {
                                            TempData["TopUpAlert"] = "1";

                                            string trno = "0";
                                            string sub = "Top Up";
                                            string result1 = EmailFUnd(userInfo.EmailID, sub, trans.UserName, trans.USD_Amount);
                                        }
                                        else
                                        {
                                            TempData["TopUpAlert"] = "Activation failed.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["TopUpAlert"] = "You have insufficient balance in your main wallet.";
                                    }
                                }
                                else
                                {
                                    TempData["TopUpAlert"] = "0 balance in your main wallet to top up this user.";
                                }
                            }
                            else if (trans.BTC_Type == "ROI")
                            {
                                DataTable dt = UserManager.User_Report(userInfo.memb_code, "ROIWALLETBALANCE");
                                if (dt.Rows.Count > 0)
                                {
                                    string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

                                    if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(Total_Balance))
                                    {
                                        int result = UserManager.User_TopUp("ADDTOPUP", userInfo.memb_code, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                        if (result > 0)
                                        {
                                            TempData["TopUpAlert"] = "1";

                                            string trno = "0";
                                            string sub = "Top Up";
                                            string result1 = EmailFUnd(userInfo.EmailID, sub, trans.UserName, trans.USD_Amount);
                                        }
                                        else
                                        {
                                            TempData["TopUpAlert"] = "Activation failed.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["TopUpAlert"] = "You have insufficient balance in your main wallet.";
                                    }
                                }
                                else
                                {
                                    TempData["TopUpAlert"] = "0 balance in your main wallet to top up this user.";
                                }
                            }
                            else if (trans.BTC_Type == "WALLET")
                            {
                                trans.BTC_Type = "WALLET";
                                trans.Plan_Type = "INVESTMENT";
                                DataTable dt = UserManager.User_Report(userInfo.memb_code, "WORKINGWALLETBALANCE1");
                                if (dt.Rows.Count > 0)
                                {
                                    string total_balance = dt.Rows[0]["Total_Balance"].ToString();

                                    if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(total_balance))
                                    {
                                        int result = UserManager.User_TopUp("ADDTOPUP1", membCode, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                        if (result > 0)
                                        {
                                            TempData["TopUpAlert"] = "1";
                                        }
                                        else
                                        {
                                            TempData["TopUpAlert"] = "Activation failed.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["TopUpAlert"] = "You have insufficient balance in your fund wallet.";
                                    }
                                }
                                else
                                {
                                    TempData["TopUpAlert"] = "0 balance in your fund wallet to top up this user.";
                                }
                            }

                            else
                            {
                                TempData["TopUpAlert"] = "Please Select Wallet Type.";
                            }
                        }
                        else
                        {
                            TempData["TopUpAlert"] = "Please enter valid User id.";
                        }
                    }
                    else
                    {
                        TempData["TopUpAlert"] = "Please enter package minimum $1";
                    }
                    //}
                    //else
                    //{
                    //    TempData["TopUpAlert"] = "Please Enter Valid Transaction Password.";
                    //}


                    //}
                }
            }
            catch (Exception ex)
            {
                TempData["TopUpAlert"] = "Activation failed.";
            }
            return RedirectToAction("MainToTopUpWallet");
        }

        public ActionResult MainToTopUpWalleReport()
        {
            List<DepositeModel> dList = new List<DepositeModel>();
            try
            {
                DataTable dtTrans = UserManager.User_TopUp_History("MainToTopUpWalleReport", userInfo.memb_code);
                if (dtTrans.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    dList = JsonConvert.DeserializeObject<List<DepositeModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }

        //------------------- fund transfer detail-------------//

        public ActionResult FundSend()
        {
            FundTransModel trans = new FundTransModel();
            try
            {

                DataTable dt = UserManager.User_Report( "FUNDWALLET",userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {
                    trans.Main_Wallet = dt.Rows[0]["Main_Wallet"].ToString();
                    trans.Fund_Wallet = "$ " + dt.Rows[0]["Fund_Wallet"].ToString();
                }
                else
                {
                    trans.Main_Wallet = "0";
                    trans.Fund_Wallet = "0";
                }

                DataTable dtP = UserManager.User_Report("PRINCIPLEWALLETBALANCE",userInfo.memb_code);
                if (dtP.Rows.Count > 0)
                {

                    trans.Principle_BalanceAmount = dtP.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.Principle_BalanceAmount = "0";
                }


                DataTable dtR = UserManager.User_Report( "ROIWALLETBALANCE",userInfo.memb_code);
                if (dtR.Rows.Count > 0)
                {

                    trans.ROI_Balance = dtR.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.ROI_Balance = "0";
                }
            }
            catch (Exception ex)
            {
                trans.Main_Wallet = "0";
                trans.Fund_Wallet = "0";
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult FundSend(FundTransModel fund)
        {
            try
            {
                if (string.IsNullOrEmpty(fund.UserName))
                {
                    TempData["fundTransferAlert"] = "Please enter User Id.";
                    return RedirectToAction("FundSend");
                }

                if (string.IsNullOrEmpty(fund.Amount))
                {
                    TempData["fundTransferAlert"] = "Please enter amount.";
                    return RedirectToAction("FundSend");
                }

                if (string.Equals(fund.UserName.Trim(), userInfo.username.Trim()))
                {
                    TempData["fundTransferAlert"] = "Sorry You can not transfer fund your User Id.";
                    return RedirectToAction("FundSend");
                }

                DataTable dtUser = UserManager.CheckUsername(fund.UserName.Trim());
                if (dtUser.Rows.Count > 0)
                {
                    if (Convert.ToDecimal(fund.Amount) >= 5 && Convert.ToDecimal(fund.Amount) % 5 == 0)
                    {
                        DataTable dt = UserManager.User_Report( "FUNDWALLET",userInfo.memb_code);
                        if (dt.Rows.Count > 0)
                        {
                            string MODE = "";
                            string Total_Balance = "0";
                            Total_Balance = dt.Rows[0]["Fund_Wallet"].ToString();
                            MODE = "Req_addFundUSER";
                            fund.Wallet_Type = "FUNDWALLET";
                            if (fund.Wallet_Type == "PRINCIPLE")
                            {
                                DataTable dtP = UserManager.User_Report( "PRINCIPLEWALLETBALANCE",userInfo.memb_code);
                                Total_Balance = dtP.Rows[0]["Total_Balance"].ToString();
                                MODE = "Req_addFundUSERPR";
                            }
                            else if (fund.Wallet_Type == "FUND")
                            {
                                Total_Balance = dt.Rows[0]["Fund_Wallet"].ToString();
                                MODE = "Req_addFundUSERF";
                            }
                            else if (fund.Wallet_Type == "ROI")
                            {
                                DataTable dtR = UserManager.User_Report( "ROIWALLETBALANCE",userInfo.memb_code);
                                Total_Balance = dtR.Rows[0]["Total_Balance"].ToString();
                                MODE = "Req_addFundUSERROI";
                            }

                            //string PaidAmount = (((Convert.ToDecimal(fund.Amount) * 10) / 100) + Convert.ToDecimal(fund.Amount)).ToString();

                            string PaidAmount = fund.Amount;

                            if (Convert.ToDecimal(PaidAmount) <= Convert.ToDecimal(Total_Balance))
                            {
                                string memb_code = dtUser.Rows[0]["memb_code"].ToString();
                                int result = UserManager.AddUserfund(memb_code, fund.Amount, userInfo.memb_code, MODE);
                                if (result > 0)
                                {
                                    TempData["fundTransferAlert"] = "1";

                                    string trno = "0";
                                    string sub = "Fund Transfer";
                                    // string result1 = EmailFUnd(userInfo.EmailID, sub, fund.UserName, fund.Amount);

                                    string messagecontent = "You have sucessfully transfer fund" + "\r\n" + "USER ID : " + fund.UserName + "\r\n" + "Amount : " + fund.Amount;

                                    // string smsStatus = SmsHelper.SendSMS(userInfo.Mobile_No.Trim(), messagecontent);

                                }
                                else
                                {
                                    TempData["fundTransferAlert"] = "Fund transfer Failed.";
                                }
                            }
                            else
                            {
                                TempData["fundTransferAlert"] = "You have insufficient balance amount to transfer.";
                            }
                        }
                        else
                        {
                            TempData["fundTransferAlert"] = "You have insufficient balance amount to transfer.";
                        }
                    }
                    else
                    {
                        TempData["fundTransferAlert"] = "Please Enter amount greater or equal to";
                    }
                }
                else
                {
                    TempData["fundTransferAlert"] = "Enter valid User Id.";
                }
            }
            catch (Exception ex)
            {
                TempData["fundTransferAlert"] = "Fund transfer failed.";
            }
            return RedirectToAction("FundSend");
        }


        public ActionResult WalletSend()
        {
            FundTransModel trans = new FundTransModel();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                if (dt.Rows.Count > 0)
                {
                    trans.Main_Wallet = dt.Rows[0]["Main_Wallet"].ToString();
                    trans.Fund_Wallet = dt.Rows[0]["Fund_Wallet"].ToString();
                }
                else
                {
                    trans.Main_Wallet = "0";
                    trans.Fund_Wallet = "0";
                }
            }
            catch (Exception ex)
            {
                trans.Main_Wallet = "0";
                trans.Fund_Wallet = "0";
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult WalletSend(FundTransModel fund)
        {
            try
            {
                if (string.IsNullOrEmpty(fund.UserName.Trim()))
                {
                    TempData["fundTransferAlert"] = "Please enter User Id.";
                    return RedirectToAction("FundSend");
                }

                if (string.IsNullOrEmpty(fund.Amount.Trim()))
                {
                    TempData["fundTransferAlert"] = "Please enter amount.";
                    return RedirectToAction("FundSend");
                }

                if (string.Equals(fund.UserName.Trim(), userInfo.username.Trim()))
                {
                    TempData["fundTransferAlert"] = "Sorry You can not transfer fund your User Id.";
                    return RedirectToAction("FundSend");
                }

                DataTable dtUser = UserManager.CheckUsername(fund.UserName.Trim());
                if (dtUser.Rows.Count > 0)
                {
                    if (Convert.ToDecimal(fund.Amount) >= 30 && Convert.ToDecimal(fund.Amount) % 30 == 0)
                    {
                        DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                        if (dt.Rows.Count > 0)
                        {
                            string MODE = "";
                            string Total_Balance = "0";
                            //Total_Balance = dt.Rows[0]["Fund_Wallet"].ToString();
                            //MODE = "Req_addFundUSER";
                            //fund.BTC_Type= "FUNDWALLET";
                            if (fund.BTC_Type == "WALLET")
                            {
                                Total_Balance = dt.Rows[0]["Main_Wallet"].ToString();
                                MODE = "Req_addFundUSER";
                            }
                            else if (fund.BTC_Type == "FUNDWALLET")
                            {
                                Total_Balance = dt.Rows[0]["Fund_Wallet"].ToString();
                                MODE = "Req_addFundUSERF";
                            }

                            //string PaidAmount = (((Convert.ToDecimal(fund.Amount) * 10) / 100) + Convert.ToDecimal(fund.Amount)).ToString();

                            string PaidAmount = fund.Amount;

                            if (Convert.ToDecimal(PaidAmount) <= Convert.ToDecimal(Total_Balance))
                            {
                                string memb_code = dtUser.Rows[0]["memb_code"].ToString();
                                int result = UserManager.AddUserfund(memb_code, fund.Amount, userInfo.memb_code, MODE);
                                if (result > 0)
                                {
                                    TempData["fundTransferAlert"] = "1";

                                    string trno = "0";
                                    string sub = "Fund Transfer";
                                    string result1 = EmailFUnd(userInfo.EmailID, sub, fund.UserName, fund.Amount);
                                }
                                else
                                {
                                    TempData["fundTransferAlert"] = "Fund transfer Failed.";
                                }
                            }
                            else
                            {
                                TempData["fundTransferAlert"] = "You have insufficient balance amount to transfer.";
                            }
                        }
                        else
                        {
                            TempData["fundTransferAlert"] = "You have insufficient balance amount to transfer.";
                        }
                    }
                    else
                    {
                        TempData["fundTransferAlert"] = " Amount must be equal to 10 TRX or greater than 10 TRX.";
                    }
                }
                else
                {
                    TempData["fundTransferAlert"] = "Enter valid User Id.";
                }
            }
            catch (Exception ex)
            {
                TempData["fundTransferAlert"] = "Fund transfer failed.";
            }
            return RedirectToAction("FundSend");
        }

        public ActionResult FundSendReport()
        {
            FundTransferModel fund = new FundTransferModel();
            List<WalletModel> trnsferdList = new List<WalletModel>();
            //List<WalletModel> recievedList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report( "FUNDTRANSFERHISTORY",userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    trnsferdList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }

                //DataTable dtR = UserManager.User_Report(userInfo.memb_code, "FUNDRECIEVEHISTORY");
                //if (dtR.Rows.Count > 0)
                //{
                //    string jsonString = JsonConvert.SerializeObject(dtR);
                //    recievedList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                //}
            }
            catch
            {

            }
            fund.TrnsferList = trnsferdList;
            //fund.ReceiveList = recievedList;
            return View(fund);
        }


        public ActionResult transfund()
        {
            FundTransModel trans = new FundTransModel();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "WITHDRAWALWALLET");
                if (dt.Rows.Count > 0)
                {
                    trans.BalanceAmount = dt.Rows[0]["Total_Balance"].ToString();
                    //trans.ROI_Balance = dt.Rows[0]["ROI_Balance"].ToString();
                    //trans.Direct_Balance = dt.Rows[0]["Direct_Balance"].ToString();
                    //trans.Binary_Balance = dt.Rows[0]["Binary_Balance"].ToString();
                }
                else
                {
                    trans.ROI_Balance = "0";
                    trans.BalanceAmount = "0";
                    trans.Direct_Balance = "0";
                    trans.Binary_Balance = "0";
                }
            }
            catch (Exception ex)
            {

            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult transfund(FundTransModel fund)
        {
            try
            {
                if (string.IsNullOrEmpty(fund.Amount.Trim()))
                {
                    TempData["transfund"] = "Please enter amount.";
                    return RedirectToAction("transfund");
                }

                //if (string.Equals(fund.UserName.Trim(), userInfo.username.Trim()))
                //{
                //    TempData["transfund"] = "Sorry You can not transfer fund your User Id.";
                //    return RedirectToAction("transfund");
                //}

                DataTable dtUser = UserManager.CheckUsername(userInfo.username.Trim());
                if (dtUser.Rows.Count > 0)
                {
                    if (Convert.ToDecimal(fund.Amount) >= 10 /*&& Convert.ToDecimal(fund.Amount) % 10 == 0*/)
                    {
                        DataTable dt = UserManager.User_Report(userInfo.memb_code, "WITHDRAWALWALLET");
                        if (dt.Rows.Count > 0)
                        {
                            string MODE = "";
                            string Total_Balance = "0";
                            Total_Balance = dt.Rows[0]["Total_Balance"].ToString();
                            MODE = "Req_addFundUSERF";

                            string PaidAmount = fund.Amount;

                            if (Convert.ToDecimal(PaidAmount) <= Convert.ToDecimal(Total_Balance))
                            {
                                string memb_code = userInfo.memb_code;
                                int result = UserManager.AddUserfund(memb_code, fund.Amount, userInfo.memb_code, MODE);
                                if (result > 0)
                                {
                                    TempData["transfund"] = "1";
                                    string trno = "0";
                                    string sub = "Fund Transfer";
                                  //  string result1 = EmailFUnd(userInfo.EmailID, sub, fund.UserName, fund.Amount);
                                }
                                else
                                {
                                    TempData["transfund"] = "Fund transfer Failed.";
                                }
                            }
                            else
                            {
                                TempData["transfund"] = "You have insufficient balance amount to transfer.";
                            }
                        }
                        else
                        {
                            TempData["transfund"] = "You have insufficient balance amount to transfer.";
                        }
                    }
                    //else
                    //{
                    //    TempData["transfund"] = " Amount must be equal to ₹​ 300 and Multiple of ₹​ 300.";
                    //}
                }
                else
                {
                    TempData["transfund"] = "Enter valid User Id.";
                }
            }
            catch (Exception ex)
            {
                TempData["transfund"] = "Fund transfer failed.";
            }
            return RedirectToAction("transfund");
        }


        public ActionResult transfundreport()
        {
            List<FundTransferModel> dList = new List<FundTransferModel>();
            try
            {
                DataTable dtTrans = UserManager.User_Report(userInfo.memb_code, "FUNDRECIEVEHISTORYyy");
                if (dtTrans.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    dList = JsonConvert.DeserializeObject<List<FundTransferModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }
        //controller action
        public ActionResult fundRequest_Metamask()
        {
            FundRequest trans = new FundRequest();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                if (dt.Rows.Count > 0)
                {
                    trans.Fund_Wallet = dt.Rows[0]["Fund_Wallet"].ToString();
                }
                else
                {
                    trans.Main_Wallet = "0";
                    trans.Fund_Wallet = "0";
                }
            }
            catch (Exception ex)
            {
                trans.Main_Wallet = "0";
                trans.Fund_Wallet = "0";
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult fundRequest_Metamask(FundRequest trans, string account, string txthash, string amount)
        {
            try
            {
                trans.btcamount = amount;
                trans.BTC_Type = "BTC";
                if (string.IsNullOrEmpty(amount))
                {
                    TempData["fundRequest"] = "Please Enter Amount.";
                    return RedirectToAction("fundRequest_Metamask");
                }

                if (Convert.ToDouble(amount) < 0)
                {
                    TempData["fundRequest"] = "Please Enter Amount Proper Amount.";
                    return RedirectToAction("fundRequest_Metamask");
                }
                //if (string.IsNullOrEmpty(trans.BTC_Type))
                //{
                //    TempData["InvestmentAlert"] = "Please select Purchase by.";
                //    return RedirectToAction("Invest");
                //}

                //if (Convert.ToInt32(trans.donation_cycle) <= 0)
                //{
                //    TempData["InvestmentAlert"] = "Donation Cycle Should be Greater than or Equal to 1.";
                //    return RedirectToAction("Invest");
                //}

                //string allPackages = "30,100,500,1000";
                //string[] pkg = allPackages.Split(',');
                //int depositeStatus = 0;
                //for (int i = 0; i < pkg.Length; i++)
                //{
                //    if (string.Equals(trans.amount, pkg[i]))
                //    {
                //        depositeStatus = 1;
                //        break;
                //    }
                //}

                //if (trans.USD_Amount == "100000")
                //{
                //    trans.donation_cycle = "20";
                //}
                int depositeStatus = 1;
                if (depositeStatus == 1)
                {
                    //BTCAddressModel btcaddr = new BTCAddressModel();
                    //btcaddr = GetBTCAddress(trans.BTC_Type);
                    //if (string.Equals(btcaddr.result, "ok"))
                    //{
                    //    string btcAddress = btcaddr.BTCAddress;
                    //    //trans.BTC_Amount = "0";
                    //    HttpClient client = new HttpClient();
                    //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //    HttpResponseMessage response = client.GetAsync("https://min-api.cryptocompare.com/data/price?fsym=USD&tsyms=" + trans.BTC_Type).Result;
                    //    if (response.IsSuccessStatusCode)
                    //    {
                    //        string JSON = response.Content.ReadAsStringAsync().Result;
                    //        var obj = JsonConvert.DeserializeObject<AllCOINModel>(JSON);

                    //        string amtcoin = "0";
                    //        if (!string.IsNullOrEmpty(obj.BTC))
                    //            amtcoin = obj.BTC;
                    //        if (!string.IsNullOrEmpty(obj.ETH))
                    //            amtcoin = obj.ETH;
                    //        if (!string.IsNullOrEmpty(obj.LTC))
                    //            amtcoin = obj.LTC;
                    //        decimal amtcoin1 = decimal.Parse(amtcoin, NumberStyles.Float);
                    //        trans.btcamount = ((Convert.ToDecimal(trans.btcamount)) * amtcoin1).ToString();
                    //        //tokenamt = Convert.ToDecimal(trans.USD_Amount) / 10;
                    //    }
                    //DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                    //if (dt.Rows.Count > 0)
                    //{
                    //    string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

                    //if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Total_Balance))
                    //{
                    //DataTable dtUser = UserManager.CheckUsername(trans.USERNAME);
                    //if (dtUser.Rows.Count > 0)
                    //{
                    //    string membCode = dtUser.Rows[0]["memb_code"].ToString();
                    //if (Convert.ToDecimal(trans.USD_Amount) >= 2500 && Convert.ToDecimal(trans.USD_Amount) % 2500 == 0)
                    //{
                    int result = UserManager.AddUserfundRequestinmetamask("Req_addFundUSERF_API", userInfo.memb_code, amount, trans.btcamount, trans.BTC_Type, account, txthash);

                    if (result > 0)
                    {
                        TempData["fundRequest"] = "1";
                        TempData["BTCTYPE"] = trans.BTC_Type;
                        TempData["BTCAddress"] = account;
                        TempData["BTCAmount"] = trans.btcamount;
                    }
                    else
                    {
                        TempData["InvestmentAlert"] = "Purchase Package failed.";
                    }

                    //}
                    //else
                    //{
                    //    TempData["InvestmentAlert"] = "You have insufficient balance in your fund wallet.";
                    //}
                    //}
                    //else
                    //{
                    //    TempData["InvestmentAlert"] = "0 balance in your fund wallet to top up this user.";
                    //}
                    //}
                    //else
                    //{
                    //    TempData["fundRequest"] = "Error in generate bitcoin address.";
                    //}
                }
                else
                {
                    TempData["fundRequest"] = "Please Select Valid Package ";
                }
            }
            catch (Exception ex)
            {
                TempData["fundRequest"] = "Purchase Package failed.";
            }
            //return RedirectToAction("fundRequest_Metamask");
            return Json(trans, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Secure_Fund_Wallet()
        {

            try
            {

            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult Secure_Fund_Wallet(FundRequest trans)
        {
            return View();
        }

        public ActionResult Secure_Fund_WalletReport()
        {
            return View();
        }

        public ActionResult Fund_Wallet()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Fund_Wallet(FundRequest trans)
        {
            return View();
        }

        public ActionResult Fund_Wallet_History()
        {
            return View();
        }
        //------------------ user withdrawal ----------------//
        public ActionResult Withdraw()
        {
            WithdrawModel trans = new WithdrawModel();
            try
            {
                DataTable dt = UserManager.User_Report("WORKINGWALLETBALANCE", userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {

                    trans.Working_BalanceAmount = dt.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.Working_BalanceAmount = "0";
                }

                DataTable dtR = UserManager.User_Report( "ROIWALLETBALANCE",userInfo.memb_code);
                if (dtR.Rows.Count > 0)
                {

                    trans.ROI_Balance = dtR.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.ROI_Balance = "0";
                }

                DataTable dtB = UserManager.User_Report( "REWARDWALLETBALANCE",userInfo.memb_code);
                if (dtB.Rows.Count > 0)
                {

                    trans.reward_BalanceAmount = dtB.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.BalanceAmount = "0";
                }
            }

            catch (Exception ex)
            {
                trans.ROI_Balance = "0";
                trans.Direct_Balance = "0";
                trans.Binary_Balance = "0";
            }
            return View(trans);
        }
        [HttpPost]
        public ActionResult Withdraw(WithdrawModel trans)
        {
            try
            {

                if (string.IsNullOrEmpty(trans.Amount) || Convert.ToDecimal(trans.Amount) < 0)
                {
                    TempData["WithdrawlAlert"] = "Please enter withdrawal amount.";
                    return RedirectToAction("Withdraw");
                }

                DataTable dtchl = UserManager.User_Report( "GETLASTWITHDR",userInfo.memb_code);
                if (dtchl.Rows.Count > 0)
                {
                    TempData["WithdrawlAlert"] = "Your Previsous Withdrwal already Pending.";
                    return RedirectToAction("Withdraw");
                }


                string amount = trans.Amount;

                //DataTable dtUserr = UserManager.CheckUsername(trans.USERNAME);
                //string membCode = dtUserr.Rows[0]["memb_code"].ToString();



                //DataTable CONDITION = UserManager.User_Report( "Maxwithamount",userInfo.memb_code);

                //string amt = CONDITION.Rows[0]["maxcount"].ToString();

                //if (CONDITION.Rows.Count > 0)
                //{
                //    if ((Convert.ToInt64(amt)) != 2000)
                //    {
                //        TempData["WithdrawlAlert"] = "First Buy DIAMOND LEADER TOKEN!!";
                //    }



                    //if (!string.IsNullOrEmpty(Session["OTP_WITHDRAWAL"] as string))
                    //{
                    //    string otp = Crypto.Decrypt(Session["OTP_WITHDRAWAL"].ToString(), System.Text.Encoding.Unicode);
                    //    if (otp.Equals(trans.Request_Code))
                    //    {

                    //    }
                    //    else
                    //    {
                    //        TempData["WithdrawlAlert"] = "Withdrawal OTP is Incorrect..!";
                    //        return RedirectToAction("Withdraw", "MemberPanel");
                    //    }
                    //}
                    //else
                    //{
                    //    TempData["WithdrawlAlert"] = "Withdrawal OTP is invalid";
                    //    return RedirectToAction("Withdraw", "MemberPanel");
                    //}

                    string addrress = "";
                    string PASS = "";



                    //if (string.IsNullOrEmpty(userInfo.Address1))
                    //{
                    //    TempData["WithdrawlAlert"] = "First update your BEP(20) BUSD Wallate address in your profile then withdraw.";
                    //    return RedirectToAction("Withdraw");
                    //}
                    //addrress = userInfo.Address1;



                    //DataTable dtDe = UserManager.User_Report(userInfo.memb_code, "GETMATRIXDIRECT");
                    //if (dtDe.Rows.Count > 0)
                    //{
                    //    if (int.Parse(dtDe.Rows[0]["directcount"].ToString()) < 4)
                    //    {
                    //        TempData["WithdrawlAlert"] = "Your 4 direct is not completed";
                    //        return RedirectToAction("Withdraw");
                    //    }
                    //}

                    //DataTable dt = UserManager.User_Report(userInfo.memb_code, "TOTALTOPUP");
                    //if (dt.Rows.Count > 0)
                    //{
                    //    string DepositAmount = dt.Rows[0]["AMOUNT"].ToString();
                    //    if (Convert.ToDecimal(DepositAmount) > 0)
                    //    {
                    //        DataTable dt2 = UserManager.User_Report(userInfo.memb_code, "TOTALWITH");
                    //        if (dt2.Rows.Count > 0)
                    //        {
                    //            string Amountwith = dt2.Rows[0]["AMOUNT"].ToString();

                    //            string amountwith1 = (Convert.ToDecimal(trans.Amount) + Convert.ToDecimal(Amountwith)).ToString();
                    //            if ((Convert.ToDecimal(DepositAmount) * 8) < Convert.ToDecimal(amountwith1))
                    //            {
                    //                string withamt = ((Convert.ToDecimal(DepositAmount) * 8) - Convert.ToDecimal(Amountwith)).ToString();
                    //                TempData["WithdrawlAlert"] = "You cannot Wallet Transfer 8X above of deposit, so please re-topup  then you can withdraw transfer other wise you will withdraw only BUSD " + withamt;
                    //                return RedirectToAction("Withdraw");
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        TempData["WithdrawlAlert"] = "Upgrad your id 40$ then you can withdraw.";
                    //        return RedirectToAction("Withdraw");
                    //    }

                    //}
                    //else
                    //{
                    //    TempData["WithdrawlAlert"] = "First Topup your id 40$ than you can wallet transfer.";
                    //    return RedirectToAction("Withdraw");
                    //}



                    string mode = "";
                    string Amount = "0";
                    string btcAmount = "0";

                    //if (trans.Wallet_Type == "WORKING")
                    //{
                    //if (userInfo.pin_no == trans.pin_no)
                    //{
                    DataTable dtw = UserManager.User_Report("WORKINGWALLETBALANCE",userInfo.memb_code);
                    if (dtw.Rows.Count > 0)
                    {
                        Amount = dtw.Rows[0]["Total_Balance"].ToString();


                        mode = "WIDRAWLWORKING";

                        if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Amount))
                        {

                            if (Convert.ToDecimal(trans.Amount) >= 1 && Convert.ToDecimal(trans.Amount) % 1 == 0)
                            {

                                int result = UserManager.UserWithdrawals(mode, "0", "0", userInfo.memb_code, null, null, trans.Amount
                                , null, null, null, null, null, null, null, null, null, addrress, null, trans.Amount, null, trans.Withdrawal_In, trans.remark);
                                if (result > 0)
                                {
                                    TempData["WithdrawlAlert"] = "1";
                                }
                                else
                                {
                                    TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
                                }
                                //}
                            }
                            else
                            {
                                TempData["WithdrawlAlert"] = "Please enter amount minimum  $ 1. "; //and multiple of BUSD 5. ";
                            }
                        }
                        else
                        {
                            TempData["WithdrawlAlert"] = "Your balance is less than your withdrawal amount.";
                        }
                    }
                    //}
                    //else
                    //{
                    //    TempData["WithdrawlAlert"] = "Please Enter Valid Transaction Password.";
                    //}
                //}
            }
            catch (Exception ex)
            {
                TempData["WithdrawlAlert"] = "Withdrawal amount failed..." + ex;
            }
            return RedirectToAction("Withdraw");
        }
        //public ActionResult Withdraw(WithdrawModel trans)
        //{
        //    try
        //    {


        //        if (string.IsNullOrEmpty(trans.Amount))
        //        {
        //            TempData["WithdrawlAlert"] = "Please enter withdrawal amount.";
        //            return RedirectToAction("Withdraw");
        //        }

        //        DataTable dtchl = UserManager.User_Report(userInfo.memb_code, "GETLASTWITHDR");
        //        if (dtchl.Rows.Count > 0)
        //        {
        //            TempData["WithdrawlAlert"] = "Your Previsous Withdrwal already Pending.";
        //            return RedirectToAction("Withdraw");
        //        }

        //        trans.Wallet_Type = "WORKING";
        //        if (string.IsNullOrEmpty(trans.Wallet_Type))
        //        {
        //            TempData["WithdrawlAlert"] = "Please Select Wallet Type !!!";
        //            return RedirectToAction("Withdraw");
        //        }

        //        if (string.IsNullOrEmpty(trans.Withdrawal_In) || (trans.Withdrawal_In.Equals("USDT.TRC20") && trans.Withdrawal_In.Equals("BUSD.BEP20")))
        //        {
        //            TempData["WithdrawlAlert"] = "Please Select Currency Type !!!";
        //            return RedirectToAction("Withdraw");
        //        }

        //        if (trans.Withdrawal_In.Equals("USDT.TRC20"))
        //        {
        //            if (string.IsNullOrEmpty(userInfo.Address1))
        //            {
        //                TempData["WithdrawlAlert"] = "Please Update USDT Wallet Address in profile section !!!";
        //                return RedirectToAction("Withdraw");
        //            }

        //        }
        //        else if(trans.Withdrawal_In.Equals("BUSD.BEP20")){
        //            if (string.IsNullOrEmpty(userInfo.Address2))
        //            {
        //                TempData["WithdrawlAlert"] = "Please Update BUSD Wallet Address in profile section !!!";
        //                return RedirectToAction("Withdraw");
        //            }


        //        }



        //        string addrress = "";
        //        string PASS = "";


        //        //if (string.IsNullOrEmpty(userInfo.ac_name) || string.IsNullOrEmpty(userInfo.ac_no) || string.IsNullOrEmpty(userInfo.bk_name)
        //        //     || string.IsNullOrEmpty(userInfo.bk_branch) || string.IsNullOrEmpty(userInfo.bk_ifsc))
        //        //{
        //        //    TempData["WithdrawlAlert"] = "First update your TRX address in your profile then withdrawal.";
        //        //    return RedirectToAction("Withdraw");
        //        //}
        //        long amount = Convert.ToInt64(trans.Amount);


        //        if (amount < 0)
        //        {
        //            TempData["WithdrawlAlert"] = "Amount Can not be Negative !!!";
        //            return RedirectToAction("Withdraw");
        //        }


        //        addrress = userInfo.btc_ac;
        //        //}

        //        string mode = "";
        //        string Amount = "0";
        //        string btcAmount = "0";

        //        if (trans.Wallet_Type == "WORKING")
        //        {
        //            DataTable dt = UserManager.User_Report(userInfo.memb_code, "WORKINGWALLETBALANCE");
        //            if (dt.Rows.Count > 0)
        //            {

        //                Amount = dt.Rows[0]["Total_Balance"].ToString();
        //                mode = "WIDRAWLWORKING";


        //                if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Amount))
        //                {

        //                    if (Convert.ToDecimal(trans.Amount) >= 10 /*&& Convert.ToDecimal(trans.Amount) % 10 == 0*/)
        //                    {
        //                        int result = UserManager.UserWithdrawals(mode, "0", "0", userInfo.memb_code, null, null, trans.Amount
        //                        , null, null, null, null, null, null, null, null, null, addrress, null, trans.Amount, null, trans.Withdrawal_In, trans.remark);
        //                        if (result > 0)
        //                        {
        //                            TempData["WithdrawlAlert"] = "1";
        //                            string trno = "0";
        //                            DataTable dtsrno = UserManager.User_Report(userInfo.memb_code, "GETLASTWITHSRNO");
        //                            if (dtsrno.Rows.Count > 0)
        //                            {
        //                                trno = dtsrno.Rows[0]["srno"].ToString();
        //                            }
        //                            string sub = "Witrhdrwal Request";
        //                            //  string result1 = EmailWith(userInfo.EmailID, sub, btcAmount, trans.Amount, trans.Withdrawal_In, trno);
        //                        }
        //                        else
        //                        {
        //                            TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        TempData["WithdrawlAlert"] = "Please enter amount minimum 10$ & Above ";
        //                    }
        //                }
        //                else
        //                {
        //                    TempData["WithdrawlAlert"] = "Your balance is less than your withdrawal amount.";
        //                }

        //            }
        //        }
        //        else if (trans.Wallet_Type == "ROI")
        //        {
        //            string CurrentDay = DateTime.Now.Day.ToString();
        //            //DataTable dtD = UserManager.User_Report("0", "CURRENTDAY");
        //            //if (dtD.Rows.Count > 0)
        //            //{
        //            //    if (dtD.Rows[0]["CurrentDay"].ToString().ToLower() != "Saturday")
        //            //    {
        //            //        TempData["WithdrawlAlert"] = "Withdrawal only on saturday";
        //            //        return RedirectToAction("Withdraw");
        //            //    }
        //            //}

        //            DataTable dt = UserManager.User_Report(userInfo.memb_code, "ROIWALLETBALANCE");
        //            if (dt.Rows.Count > 0)
        //            {

        //                Amount = dt.Rows[0]["Total_Balance"].ToString();
        //                mode = "widrawlROI";


        //                if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Amount))
        //                {

        //                    //if (Convert.ToDecimal(trans.Amount) >= 5 && Convert.ToDecimal(trans.Amount) % 1 == 0)
        //                    //{
        //                    int result = UserManager.UserWithdrawals(mode, "0", "0", userInfo.memb_code, null, null, trans.Amount
        //                    , null, null, null, null, null, null, null, null, null, addrress, null, trans.Amount, null, trans.Withdrawal_In, trans.remark);
        //                    if (result > 0)
        //                    {
        //                        TempData["WithdrawlAlert"] = "1";
        //                        string trno = "0";
        //                        DataTable dtsrno = UserManager.User_Report(userInfo.memb_code, "GETLASTWITHSRNO");
        //                        if (dtsrno.Rows.Count > 0)
        //                        {
        //                            trno = dtsrno.Rows[0]["srno"].ToString();
        //                        }
        //                        string sub = "Witrhdrwal Request";
        //                        //string result1 = EmailWith(userInfo.EmailID, sub, btcAmount, trans.Amount, trans.Withdrawal_In, trno);
        //                    }
        //                    else
        //                    {
        //                        TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
        //                    }
        //                    //}
        //                    //else
        //                    //{
        //                    //    TempData["WithdrawlAlert"] = "Please enter amount minimum 5 TRX ";
        //                    //}
        //                }
        //                else
        //                {
        //                    TempData["WithdrawlAlert"] = "Your balance is less than your withdrawal amount.";
        //                }

        //            }
        //            else
        //            {
        //                TempData["WithdrawlAlert"] = "You have 0 balance to withdrawal.";
        //            }
        //        }
        //        //if (userInfo.RV_Code == trans.Password)
        //        //{
        //        //    if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Amount))
        //        //    {
        //        //        trans.Withdrawal_In = "USD";
        //        //        if (Convert.ToDecimal(trans.Amount) >= 5 && Convert.ToDecimal(trans.Amount) % 5 == 0)
        //        //        {
        //        //            int result = UserManager.UserWithdrawals(mode, "0", "0", userInfo.memb_code, null, null, trans.Amount
        //        //                , null, null, null, null, null, null, null, null, null, addrress, null, trans.Amount, null, trans.Withdrawal_In, trans.remark);
        //        //            if (result > 0)
        //        //            {

        //        //                TempData["WithdrawlAlert"] = "1";
        //        //                string trno = "0";
        //        //                DataTable dtsrno = UserManager.User_Report(userInfo.memb_code, "GETLASTWITHSRNO");
        //        //                if (dtsrno.Rows.Count > 0)
        //        //                {
        //        //                    trno = dtsrno.Rows[0]["srno"].ToString();
        //        //                }
        //        //                string sub = "Witrhdrwal Request";
        //        //                string result1 = EmailWith(userInfo.EmailID, sub, btcAmount, trans.Amount, trans.Withdrawal_In, trno);

        //        //                string messagecontent = "Witrhdrwal Request  " + "\r\n" + "Amount : " + trans.Amount;

        //        //                //   string smsStatus = SmsHelper.SendSMS(userInfo.Mobile_No.Trim(), messagecontent);

        //        //            }
        //        //            else
        //        //            {
        //        //                TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
        //        //            }
        //        //        }
        //        //        else
        //        //        {
        //        //            TempData["WithdrawlAlert"] = "Please enter amount minimum TRX5 ";
        //        //        }
        //        //    }
        //        //    else
        //        //    {
        //        //        TempData["WithdrawlAlert"] = "Your balance is less than your withdrawal amount.";
        //        //    }
        //        //}
        //        //else
        //        //    {
        //        //        TempData["WithdrawlAlert"] = "Enter Correct Password.";
        //        //        //TempData["WithdrawlAlert"] = "You have 0 balance to withdrawal.";
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    TempData["WithdrawlAlert"] = "You have 0 balance to withdrawal.";
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
        //    }
        //    return RedirectToAction("Withdraw");
        //}


        public ActionResult WalletTransfer()
        {
            WithdrawModel trans = new WithdrawModel();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "WORKINGWALLETBALANCE");
                if (dt.Rows.Count > 0)
                {

                    trans.Working_BalanceAmount = dt.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.BalanceAmount = "0";
                }
                DataTable dtP = UserManager.User_Report(userInfo.memb_code, "PRINCIPLEWALLETBALANCE");
                if (dtP.Rows.Count > 0)
                {

                    trans.Principle_BalanceAmount = dtP.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.Principle_BalanceAmount = "0";
                }
                DataTable dtR = UserManager.User_Report(userInfo.memb_code, "ROIWALLETBALANCE");
                if (dtR.Rows.Count > 0)
                {

                    trans.ROI_Balance = dtR.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.ROI_Balance = "0";
                }
                DataTable dtB = UserManager.User_Report(userInfo.memb_code, "REWARDWALLETBALANCE");
                if (dtB.Rows.Count > 0)
                {

                    trans.reward_BalanceAmount = dtB.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.BalanceAmount = "0";
                }
            }

            catch (Exception ex)
            {
                trans.ROI_Balance = "0";
                trans.Direct_Balance = "0";
                trans.Binary_Balance = "0";
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult WalletTransfer(WithdrawModel trans)
        {
            try
            {


                if (string.IsNullOrEmpty(trans.Amount))
                {
                    TempData["WithdrawlAlert"] = "Please enter withdrawal amount.";
                    return RedirectToAction("WalletTransfer");
                }

                //if (string.IsNullOrEmpty(trans.USERNAME))
                //{
                //    TempData["WithdrawlAlert"] = "Please enter USERID.";
                //    return RedirectToAction("WalletTransfer");
                //}

                if (string.IsNullOrEmpty(trans.Wallet_Type))
                {
                    TempData["WithdrawlAlert"] = "Please Select Wallet Type.";
                    return RedirectToAction("WalletTransfer");
                }

                string addrress = "";
                string PASS = "";

                string mode = "";
                string Amount = "0";
                string btcAmount = "0";


                if (trans.Wallet_Type == "WORKING")
                {

                    DataTable dt = UserManager.User_Report(userInfo.memb_code, "WORKINGWALLETBALANCE");
                    if (dt.Rows.Count > 0)
                    {

                        Amount = dt.Rows[0]["Total_Balance"].ToString();
                        mode = "Req_addWalletUSERWORKING";


                        if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Amount))
                        {

                            if (Convert.ToDecimal(trans.Amount) >= 1 && Convert.ToDecimal(trans.Amount) % 1 == 0)
                            {
                                int result = UserManager.AddUserfund(userInfo.memb_code, trans.Amount, userInfo.memb_code, mode);
                                if (result > 0)
                                {
                                    TempData["WithdrawlAlert"] = "1";
                                    string trno = "0";
                                    DataTable dtsrno = UserManager.User_Report(userInfo.memb_code, "GETLASTWITHSRNO");
                                    if (dtsrno.Rows.Count > 0)
                                    {
                                        trno = dtsrno.Rows[0]["srno"].ToString();
                                    }
                                    string sub = "Witrhdrwal Request";
                                    //  string result1 = EmailWith(userInfo.EmailID, sub, btcAmount, trans.Amount, trans.Withdrawal_In, trno);
                                }
                                else
                                {
                                    TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
                                }
                            }
                            else
                            {
                                TempData["WithdrawlAlert"] = "Please enter amount minimum 10 TRX ";
                            }
                        }
                        else
                        {
                            TempData["WithdrawlAlert"] = "Your balance is less than your withdrawal amount.";
                        }

                    }
                }
                //else
                //{
                //    TempData["WithdrawlAlert"] = "You have 0 balance to withdrawal.";
                //}


                else if (trans.Wallet_Type == "ROI")
                {
                    //string CurrentDay = DateTime.Now.Day.ToString();
                    //DataTable dtD = UserManager.User_Report("0", "CURRENTDAY");
                    //if (dtD.Rows.Count > 0)
                    //{
                    //    if (dtD.Rows[0]["CurrentDay"].ToString().ToLower() != "Saturday")
                    //    {
                    //        TempData["WithdrawlAlert"] = "Withdrawal only on saturday";
                    //        return RedirectToAction("Withdraw");
                    //    }
                    //}

                    DataTable dt = UserManager.User_Report(userInfo.memb_code, "ROIWALLETBALANCE");
                    if (dt.Rows.Count > 0)
                    {

                        Amount = dt.Rows[0]["Total_Balance"].ToString();
                        mode = "Req_addWalletUSERROI";


                        if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Amount))
                        {

                            if (Convert.ToDecimal(trans.Amount) >= 5 && Convert.ToDecimal(trans.Amount) % 1 == 0)
                            {
                                int result = UserManager.AddUserfund(userInfo.memb_code, trans.Amount, userInfo.memb_code, mode);
                                if (result > 0)
                                {
                                    TempData["WithdrawlAlert"] = "1";
                                    string trno = "0";
                                    DataTable dtsrno = UserManager.User_Report(userInfo.memb_code, "GETLASTWITHSRNO");
                                    if (dtsrno.Rows.Count > 0)
                                    {
                                        trno = dtsrno.Rows[0]["srno"].ToString();
                                    }
                                    string sub = "Witrhdrwal Request";
                                    string result1 = EmailWith(userInfo.EmailID, sub, btcAmount, trans.Amount, trans.Withdrawal_In, trno);
                                }
                                else
                                {
                                    TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
                                }
                            }
                            else
                            {
                                TempData["WithdrawlAlert"] = "Please enter amount minimum 10 TRX ";
                            }
                        }
                        else
                        {
                            TempData["WithdrawlAlert"] = "Your balance is less than your withdrawal amount.";
                        }

                    }
                    else
                    {
                        TempData["WithdrawlAlert"] = "You have 0 balance to withdrawal.";
                    }
                }
                //if (userInfo.RV_Code == trans.Password)
                //{
                //    if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Amount))
                //    {
                //        trans.Withdrawal_In = "USD";
                //        if (Convert.ToDecimal(trans.Amount) >= 5 && Convert.ToDecimal(trans.Amount) % 5 == 0)
                //        {
                //            int result = UserManager.UserWithdrawals(mode, "0", "0", userInfo.memb_code, null, null, trans.Amount
                //                , null, null, null, null, null, null, null, null, null, addrress, null, trans.Amount, null, trans.Withdrawal_In, trans.remark);
                //            if (result > 0)
                //            {

                //                TempData["WithdrawlAlert"] = "1";
                //                string trno = "0";
                //                DataTable dtsrno = UserManager.User_Report(userInfo.memb_code, "GETLASTWITHSRNO");
                //                if (dtsrno.Rows.Count > 0)
                //                {
                //                    trno = dtsrno.Rows[0]["srno"].ToString();
                //                }
                //                string sub = "Witrhdrwal Request";
                //                string result1 = EmailWith(userInfo.EmailID, sub, btcAmount, trans.Amount, trans.Withdrawal_In, trno);

                //                string messagecontent = "Witrhdrwal Request  " + "\r\n" + "Amount : " + trans.Amount;

                //                //   string smsStatus = SmsHelper.SendSMS(userInfo.Mobile_No.Trim(), messagecontent);

                //            }
                //            else
                //            {
                //                TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
                //            }
                //        }
                //        else
                //        {
                //            TempData["WithdrawlAlert"] = "Please enter amount minimum TRX5 ";
                //        }
                //    }
                //    else
                //    {
                //        TempData["WithdrawlAlert"] = "Your balance is less than your withdrawal amount.";
                //    }
                //}
                //else
                //    {
                //        TempData["WithdrawlAlert"] = "Enter Correct Password.";
                //        //TempData["WithdrawlAlert"] = "You have 0 balance to withdrawal.";
                //    }
                //}
                //else
                //{
                //    TempData["WithdrawlAlert"] = "You have 0 balance to withdrawal.";
                //}

            }
            catch (Exception ex)
            {
                TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
            }
            return RedirectToAction("WalletTransfer");
        }

        public ActionResult WalletTransferReport()
        {
            List<WithdrawalModel> dList = new List<WithdrawalModel>();
            try
            {
                DataTable dtTrans = UserManager.User_Report(userInfo.memb_code, "WITHDRAWHISTORY1");
                if (dtTrans.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    dList = JsonConvert.DeserializeObject<List<WithdrawalModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }


        public ActionResult Workingwithdrawl()
        {

            loadMobileNo();
            WithdrawModel trans = new WithdrawModel();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "WORKINGWALLETBALANCE");
                if (dt.Rows.Count > 0)
                {

                    trans.Working_BalanceAmount = dt.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.BalanceAmount = "0";
                }

                DataTable dtR = UserManager.User_Report(userInfo.memb_code, "ROIWALLETBALANCE");
                if (dtR.Rows.Count > 0)
                {

                    trans.ROI_Balance = dtR.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.ROI_Balance = "0";
                }

                DataTable dtB = UserManager.User_Report(userInfo.memb_code, "REWARDWALLETBALANCE");
                if (dtB.Rows.Count > 0)
                {

                    trans.reward_BalanceAmount = dtB.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.BalanceAmount = "0";
                }
            }
            catch (Exception ex)
            {
                trans.BalanceAmount = "0";
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult Workingwithdrawl(WithdrawModel trans)
        {
            try
            {
                //string CurrentDay = DateTime.Now.Day.ToString();
                //DataTable dtD = UserManager.User_Report("0", "CURRENTDAY");
                //if (dtD.Rows.Count > 0)
                //{
                //    if (dtD.Rows[0]["CurrentDay"].ToString().ToLower() == "saturday" || dtD.Rows[0]["CurrentDay"].ToString().ToLower() == "sunday")
                //    {
                //        TempData["WithdrawlAlert"] = "Withdrawal only on Monday to Friday";
                //        return RedirectToAction("Workingwithdrawl");
                //    }
                //}

                if (string.IsNullOrEmpty(trans.request_mobile_no))
                {
                    TempData["WithdrawlAlert"] = "Please select mobile Number.";
                    return RedirectToAction("Workingwithdrawl");
                }

                if (string.IsNullOrEmpty(trans.Amount))
                {
                    TempData["WithdrawlAlert"] = "Please enter withdrawal amount.";
                    return RedirectToAction("Workingwithdrawl");
                }

                DataTable dtchl = UserManager.User_Report(userInfo.memb_code, "GETLASTWITHDR");
                if (dtchl.Rows.Count > 0)
                {
                    TempData["WithdrawlAlert"] = "Your Previsous Withdrwal already Pending.";
                    return RedirectToAction("Workingwithdrawl");
                }

                if (string.IsNullOrEmpty(trans.Wallet_Type))
                {
                    TempData["WithdrawlAlert"] = "Please Select Wallet Type.";
                    return RedirectToAction("Workingwithdrawl");
                }

                string addrress = "";
                string PASS = "";
                if (string.IsNullOrEmpty(userInfo.ac_name) || string.IsNullOrEmpty(userInfo.ac_no) || string.IsNullOrEmpty(userInfo.bk_name) || string.IsNullOrEmpty(userInfo.bk_branch)
                    || string.IsNullOrEmpty(userInfo.bk_ifsc))
                {
                    TempData["WithdrawlAlert"] = "First update your account details in your profile then withdrawal.";
                    return RedirectToAction("Workingwithdrawl");
                }

                addrress = userInfo.btc_ac;

                string mode = "";
                string Amount = "0";
                string btcAmount = "0";

                DataTable dtBankl = UserManager.User_Report_Mobile_Srno(userInfo.memb_code, "GETBANKDETAILSBYSRNOMOBILE", trans.request_mobile_no, trans.bank_id);
                if (dtBankl.Rows.Count == 0)
                {
                    TempData["InvestmentAlert"] = "Please Select Valid Mobile No and Bank Account.";
                    return RedirectToAction("Workingwithdrawl");
                }

                string ac_type = dtBankl.Rows[0]["ac_type"].ToString();
                string ac_no = dtBankl.Rows[0]["ac_no"].ToString();
                string bk_ifsc = dtBankl.Rows[0]["bk_ifsc"].ToString();
                string ac_name = dtBankl.Rows[0]["ac_name"].ToString();
                string beneficiaryid = dtBankl.Rows[0]["beneficiaryid"].ToString();
                string beneficiaryid_corporate = dtBankl.Rows[0]["beneficiaryid_corporate"].ToString();


                string transaction_no = "";
                string BANKREFNO = "";
                string todaywith = "0";

                trans.Withdrawal_In = "BANK";
                string api_type = "DOOPME";
                string reqt_order_no = userInfo.memb_code + DateTime.Now.ToString("yyyymmddhhMMss") + trans.Amount;
                string AMOUNT = trans.Amount.ToString();

                if (trans.Wallet_Type == "WORKING")
                {

                    DataTable dt = UserManager.User_Report(userInfo.memb_code, "WORKINGWALLETBALANCE");
                    if (dt.Rows.Count > 0)
                    {

                        Amount = dt.Rows[0]["Total_Balance"].ToString();
                        mode = "WIDRAWLWORKING";


                        if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Amount))
                        {

                            if (Convert.ToDecimal(trans.Amount) >= 10 && Convert.ToDecimal(trans.Amount) % 10 == 0)
                            {
                                DataTable results = UserManager.UserssWithdrawalsIMPS("0", userInfo.memb_code, AMOUNT, "IMPS", trans.Withdrawal_In, null, ac_name, ac_no, null, null, null, bk_ifsc, null, mode, null, trans.bank_id, trans.request_mobile_no, reqt_order_no, api_type, null, null);
                                if (results.Rows.Count > 0)
                                {

                                    TempData["WithdrawlAlert"] = "1";
                                    string trno = "0";
                                    DataTable dtsrno = UserManager.User_Report(userInfo.memb_code, "GETLASTWITHSRNO");
                                    if (dtsrno.Rows.Count > 0)
                                    {
                                        trno = dtsrno.Rows[0]["srno"].ToString();
                                    }
                                    string sub = "Witrhdrwal Request";
                                    //  string result1 = EmailWith(userInfo.EmailID, sub, btcAmount, trans.Amount, trans.Withdrawal_In, trno);
                                }
                                else
                                {
                                    TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
                                }
                            }
                            else
                            {
                                TempData["WithdrawlAlert"] = "Please enter amount minimum 10 Rs. ";
                            }
                        }
                        else
                        {
                            TempData["WithdrawlAlert"] = "Your balance is less than your withdrawal amount.";
                        }

                    }
                    else
                    {
                        TempData["WithdrawlAlert"] = "You have 0 balance to withdrawal.";
                    }
                }
                else if (trans.Wallet_Type == "ROI")
                {
                    string CurrentDay = DateTime.Now.Day.ToString();
                    //DataTable dtD = UserManager.User_Report("0", "CURRENTDAY");
                    //if (dtD.Rows.Count > 0)
                    //{
                    //    if (dtD.Rows[0]["CurrentDay"].ToString().ToLower() != "saturday")
                    //    {
                    //        TempData["WithdrawlAlert"] = "Withdrawal only on saturday";
                    //        return RedirectToAction("Workingwithdrawl");
                    //    }
                    //}
                    DataTable dt = UserManager.User_Report(userInfo.memb_code, "ROIWALLETBALANCE");
                    if (dt.Rows.Count > 0)
                    {

                        Amount = dt.Rows[0]["Total_Balance"].ToString();
                        mode = "WIDRAWLROI";


                        if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Amount))
                        {

                            if (Convert.ToDecimal(trans.Amount) >= 10 && Convert.ToDecimal(trans.Amount) % 10 == 0)
                            {
                                DataTable results = UserManager.UserssWithdrawalsIMPS("0", userInfo.memb_code, AMOUNT, "IMPS", trans.Withdrawal_In, null, ac_name, ac_no, null, null, null, bk_ifsc, null, mode, null, trans.bank_id, trans.request_mobile_no, reqt_order_no, api_type, null, null);
                                if (results.Rows.Count > 0)
                                {

                                    TempData["WithdrawlAlert"] = "1";
                                    string trno = "0";
                                    DataTable dtsrno = UserManager.User_Report(userInfo.memb_code, "GETLASTWITHSRNO");
                                    if (dtsrno.Rows.Count > 0)
                                    {
                                        trno = dtsrno.Rows[0]["srno"].ToString();
                                    }
                                    string sub = "Witrhdrwal Request";
                                    string result1 = EmailWith(userInfo.EmailID, sub, btcAmount, trans.Amount, trans.Withdrawal_In, trno);
                                }
                                else
                                {
                                    TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
                                }
                            }
                            else
                            {
                                TempData["WithdrawlAlert"] = "Please enter amount minimum 10 Rs. ";
                            }
                        }
                        else
                        {
                            TempData["WithdrawlAlert"] = "Your balance is less than your withdrawal amount.";
                        }

                    }
                    else
                    {
                        TempData["WithdrawlAlert"] = "You have 0 balance to withdrawal.";
                    }
                }
                else if (trans.Wallet_Type == "REWARD")
                {

                    DataTable dt = UserManager.User_Report(userInfo.memb_code, "REWARDWALLETBALANCE");
                    if (dt.Rows.Count > 0)
                    {

                        Amount = dt.Rows[0]["Total_Balance"].ToString();
                        mode = "WIDRAWLREWARD";


                        if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Amount))
                        {

                            if (Convert.ToDecimal(trans.Amount) >= 10 && Convert.ToDecimal(trans.Amount) % 10 == 0)
                            {
                                DataTable results = UserManager.UserssWithdrawalsIMPS("0", userInfo.memb_code, AMOUNT, "IMPS", trans.Withdrawal_In, null, ac_name, ac_no, null, null, null, bk_ifsc, null, mode, null, trans.bank_id, trans.request_mobile_no, reqt_order_no, api_type, null, null);
                                if (results.Rows.Count > 0)
                                {

                                    TempData["WithdrawlAlert"] = "1";
                                    string trno = "0";
                                    DataTable dtsrno = UserManager.User_Report(userInfo.memb_code, "GETLASTWITHSRNO");
                                    if (dtsrno.Rows.Count > 0)
                                    {
                                        trno = dtsrno.Rows[0]["srno"].ToString();
                                    }
                                    string sub = "Witrhdrwal Request";
                                    string result1 = EmailWith(userInfo.EmailID, sub, btcAmount, trans.Amount, trans.Withdrawal_In, trno);
                                }
                                else
                                {
                                    TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
                                }
                            }
                            else
                            {
                                TempData["WithdrawlAlert"] = "Please enter amount minimum 10 Rs. ";
                            }
                        }
                        else
                        {
                            TempData["WithdrawlAlert"] = "Your balance is less than your withdrawal amount.";
                        }

                    }
                    else
                    {
                        TempData["WithdrawlAlert"] = "You have 0 balance to withdrawal.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["WithdrawlAlert"] = "Withdrawal amount failed.";
            }
            return RedirectToAction("Workingwithdrawl");
        }

        public ActionResult WithdrawReport()
        {
            List<WithdrawalModel> dList = new List<WithdrawalModel>();
            try
            {
                DataTable dtTrans = UserManager.User_Report( "WITHDRAWHISTORY",userInfo.memb_code);
                if (dtTrans.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    dList = JsonConvert.DeserializeObject<List<WithdrawalModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }

        public ActionResult WorkingwithdrawlReport()
        {
            List<WithdrawalModel> dList = new List<WithdrawalModel>();
            try
            {
                DataTable dtTrans = UserManager.User_Report(userInfo.memb_code, "WORKINGWITHDRAWHISTORY");
                if (dtTrans.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    dList = JsonConvert.DeserializeObject<List<WithdrawalModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }



        //------------------ all reports ----------------//

        public ActionResult IncomeGrowth_partial()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "ROI");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult IncomeGrowth()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "ROI");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        

        public ActionResult IncomePoolLevel()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "LEVELPOOLINCOME");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult IncomePoolDirect()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "DIRECTPOOLINCOME");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult IncomeMonthIncentive()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "MONTHINCENTIVE");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult StarBonus()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "STARBONUS");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult RoyaltyBonus()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "ROYALTYBONUS");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult MentorPoolBonus()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "MENTORPOOLBONUS");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult LegendBonus()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "LEGENDBONUS");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult IncomeDirect()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.USER_REPORT("DIRECT", userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }
        public ActionResult IncomeLEVEL()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.USER_REPORT("LEVELINCOME", userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }
        public ActionResult IncomeBooster()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.USER_REPORT("BOOSTER", userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }
        public ActionResult IncomeROI()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.USER_REPORT("ROIINCOME", userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult IncomeLevelRoi()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.USER_REPORT("LEVELINCOMEROI", userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }
        public ActionResult IncomeReward()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report( "REWARDS",userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }
        //public ActionResult IncomeBOOSTER()
        //{
        //    List<WalletModel> dList = new List<WalletModel>();
        //    try
        //    {
        //        DataTable dt = UserManager.User_Report("BOOSTER", userInfo.memb_code);
        //        if (dt.Rows.Count > 0)
        //        {
        //            string jsonString = JsonConvert.SerializeObject(dt);
        //            dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //    return View(dList.ToList());
        //}

        public ActionResult IncomeBinary()
        {
            List<BinaryReportModel> dList = new List<BinaryReportModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "PAIREDBONUS");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<BinaryReportModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult IncomeTransaction()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "ALLTRANSACTION");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult royalty_income()
        {
            List<ReportModel> lstDirectIncome = new List<ReportModel>();
            try
            {
                DataTable dtResult = UserManager.User_Report(userInfo.memb_code, "ROYALTYINCOME");
                if (dtResult.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtResult);
                    lstDirectIncome = JsonConvert.DeserializeObject<List<ReportModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {
                TempData["Error : "] = ex.Message;
            }
            return View(lstDirectIncome.ToList());
        }

        //public ActionResult booster_income()
        //{
        //    List<ReportModel> lstDirectIncome = new List<ReportModel>();
        //    try
        //    {
        //        DataTable dtResult = UserManager.User_Report(userInfo.memb_code, "BOOSTERINCOME");
        //        if (dtResult.Rows.Count > 0)
        //        {
        //            string JSONString = JsonConvert.SerializeObject(dtResult);
        //            lstDirectIncome = JsonConvert.DeserializeObject<List<ReportModel>>(JSONString);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error : "] = ex.Message;
        //    }
        //    return View(lstDirectIncome.ToList());
        //}

        public ActionResult Globalpoolstatus()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "Poolstatus");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult Globalpoolincome()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "Globalpoolincome");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());

        }

        public ActionResult DailyProfit()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "ROI");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult MonthlyIncentive()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "MonthlyIncentive");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult Team_pool2()
        {
            List<UserLevelModel> dList = new List<UserLevelModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "MATRIXTEAML2");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<UserLevelModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }
        public ActionResult Team_pool3()
        {
            List<UserLevelModel> dList = new List<UserLevelModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "MATRIXTEAML2");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<UserLevelModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult Team_pool4()
        {
            List<UserLevelModel> dList = new List<UserLevelModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "MATRIXTEAML2");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<UserLevelModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult Team_pool5()
        {
            List<UserLevelModel> dList = new List<UserLevelModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "MATRIXTEAML2");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<UserLevelModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }
        //------------------ support ticket ----------------//
        public ActionResult SentMessage()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SentMessage(SupportModel support)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["TicketAlert"] = "Please enter valid data.";
                    return RedirectToAction("SentMessage", "Home");
                }

                string subject = support.sub;
                string message = support.msg;

                if (string.IsNullOrEmpty(subject.Trim()))
                {
                    TempData["TicketAlert"] = "Please enter subject.";
                    return RedirectToAction("SentMessage");
                }

                if (string.IsNullOrEmpty(message.Trim()))
                {
                    TempData["TicketAlert"] = "Please enter message.";
                    return RedirectToAction("SentMessage");
                }

                message = message.Replace("\r\n", "<br/>");
                int result = SupportTicketManager.AddUpdate_Ticket("COMPOSE", "1", userInfo.memb_code, message.Trim(), subject.Trim());
                if (result > 0)
                {
                    TempData["TicketAlert"] = "Support ticket created successfully.";
                }
                else
                    TempData["TicketAlert"] = "Support ticket created failed.";
            }
            catch (Exception ex)
            {
                TempData["TicketAlert"] = "Support ticket created failed.";
            }
            return RedirectToAction("SentMessage");
        }

        public ActionResult IncommingMessage()
        {
            List<SupportTicketModel> dList = new List<SupportTicketModel>();
            try
            {
                //if (string.Equals(userInfo.authrised, "R"))
                //{
                //    return RedirectToAction("Index", "Dashboard");
                //}
                DataTable dtTrans = SupportTicketManager.GetTicket("INBOXUSER", userInfo.memb_code);
                if (dtTrans.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    dList = JsonConvert.DeserializeObject<List<SupportTicketModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }

        public ActionResult OutgoingMessage()
        {
            List<SupportTicketModel> dList = new List<SupportTicketModel>();
            try
            {
                //if (string.Equals(userInfo.authrised, "R"))
                //{
                //    return RedirectToAction("Index", "Dashboard");
                //}
                DataTable dtTrans = SupportTicketManager.GetTicket("OUTBOXUSER", userInfo.memb_code);
                if (dtTrans.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    dList = JsonConvert.DeserializeObject<List<SupportTicketModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }

        public JsonResult getUSERMemb(string id)
        {
            UserModel user = new UserModel();
            try
            {
                DataTable dt = UserManager.CheckUsername(id);
                if (dt.Rows.Count > 0)
                {
                    user.memb_code = dt.Rows[0]["memb_code"].ToString();
                }
            }
            catch
            {

            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getFirstUser(string sp_user)
        {
            UserModel user = new UserModel();
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(sp_user))
                dt = UserManager.GetFirstUser();
            else
                //dt = UserManager.GetUserDetailsByEmail(sp_user);
                dt = UserManager.GetUserDetailsByUsername(sp_user);
            if (dt.Rows.Count > 0)
            {
                user.sp_user = dt.Rows[0]["username"].ToString();
                user.Memb_Name = dt.Rows[0]["Memb_Name"].ToString();
                //user.Memb_Name = dt.Rows[0]["EMAIL"].ToString();
            }
            else
            {
                user.sp_user = "";
                user.Memb_Name = "";
            }

            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getRequestCode(string codeType)
        {
            string returnMSG = string.Empty;
            string requestCode = string.Empty;
            try
            {
                requestCode = GenerateRandomOTP(6);
                string email = userInfo.EmailID;

                string sub = string.Empty;
                if (codeType == "W")
                {
                    sub = "Withdrawal";
                    Session["WithdrawlRequestCode"] = requestCode;

                    string messagecontent = "Your Request Code for Withdrawal is " + requestCode;
                    //if (!string.IsNullOrEmpty(userInfo.Mobile_No))
                    //{
                    //    string smsStatus = SmsHelper.SendSMS(userInfo.Mobile_No.Trim(), messagecontent);
                    //}
                }
                else if (codeType == "PU")
                {
                    sub = "Profile Update";
                    Session["ProfileRequestCode"] = requestCode;
                }
                else if (codeType == "CP")
                {
                    sub = "Change Password";
                    Session["CPasswordRequestCode"] = requestCode;
                }
                else if (codeType == "TP")
                {
                    sub = "Top Up";
                    Session["TopUpRequestCode"] = requestCode;

                    //string messagecontent = "Your Request Code for Topup is " + requestCode;
                    //if (!string.IsNullOrEmpty(userInfo.Mobile_No))
                    //{
                    //    string smsStatus = SmsHelper.SendSMS(userInfo.Mobile_No.Trim(), messagecontent);
                    //}
                }
                else if (codeType == "FT")
                {
                    sub = "Fund Transfer";
                    Session["FundTransferRequestCode"] = requestCode;
                }

                string result = OTPMail(email, sub, requestCode);
                returnMSG = "1";
            }
            catch (Exception ex)
            {
                returnMSG = "Request code sending failed.";
            }
            return Json(returnMSG, JsonRequestBehavior.AllowGet);
        }

        public ActionResult plan()
        {
            return View();
        }



        public ActionResult Coinpayment_transaction()
        {
            FundRequest trans = new FundRequest();
            try
            {
                DataTable dt = UserManager.User_Report( "FUNDWALLET",userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {
                    trans.Fund_Wallet = dt.Rows[0]["Fund_Wallet"].ToString();
                }
                else
                {
                    trans.Main_Wallet = "0";
                    trans.Fund_Wallet = "0";
                }
            }
            catch (Exception ex)
            {
                trans.Main_Wallet = "0";
                trans.Fund_Wallet = "0";
            }
            return View(trans);
        }

        [HttpPost]

        public ActionResult Coinpayment_transaction(FundRequest trans)
        {
            try
            {

                trans.btcamount = trans.amount;
                // trans.BTC_Type = "USDC.TRC20";
                if (string.IsNullOrEmpty(trans.amount))
                {
                    TempData["fundRequest"] = "Please Enter Amount.";
                    return RedirectToAction("Coinpayment_transaction");
                }
                //if (string.IsNullOrEmpty(trans.USERNAME))
                //{
                //    TempData["fundRequest"] = "Please Enter Username.";
                //    return RedirectToAction("Coinpayment_transaction");
                //}

                if (string.IsNullOrEmpty(trans.BTC_Type))
                {
                    TempData["InvestmentAlert"] = "Please select Coin";
                    return RedirectToAction("Coinpayment_transaction");
                }
                //if (trans.USD_Amount == "100000")
                //{
                //    trans.donation_cycle = "20";
                //}
                int depositeStatus = 1;
                if (depositeStatus == 1)
                {
                    BTCAddressModel btcaddr = new BTCAddressModel();
                    //btcaddr = GetBTCAddress(trans.BTC_Type);
                    //btcaddr = GetCreateTransaction(trans.BTC_Type, trans.btcamount);
                    //if (string.Equals(btcaddr.result, "ok"))
                    //{
                    if (string.Equals("ok", "ok"))
                    {
                        string btcAddress = btcaddr.BTCAddress;
                        // trans.BTC_Amount = "0";
                        //HttpClient client = new HttpClient();
                        //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //HttpResponseMessage response = client.GetAsync("https://min-api.cryptocompare.com/data/price?fsym=USD&tsyms=" + trans.BTC_Type).Result;
                        //if (response.IsSuccessStatusCode)
                        //{
                        //    string JSON = response.Content.ReadAsStringAsync().Result;
                        //    var obj = JsonConvert.DeserializeObject<AllCOINModel>(JSON);

                        //    string amtcoin = "0";
                        //    if (!string.IsNullOrEmpty(obj.BTC))
                        //        amtcoin = obj.BTC;
                        //    if (!string.IsNullOrEmpty(obj.ETH))
                        //        amtcoin = obj.ETH;
                        //    if (!string.IsNullOrEmpty(obj.LTC))
                        //        amtcoin = obj.LTC;
                        //    decimal amtcoin1 = decimal.Parse(amtcoin, NumberStyles.Float);
                        //    trans.btcamount = ((Convert.ToDecimal(trans.btcamount)) * amtcoin1).ToString();
                        //    //tokenamt = Convert.ToDecimal(trans.USD_Amount) / 10;
                        //}
                        //DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                        //if (dt.Rows.Count > 0)
                        //{
                        //    string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

                        //if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Total_Balance))
                        //{
                        //DataTable dtUser = UserManager.CheckUsername(trans.USERNAME);
                        //if (dtUser.Rows.Count > 0)
                        //{
                        //    string membCode = dtUser.Rows[0]["memb_code"].ToString();

                        if (Convert.ToDecimal(trans.USD_Amount) >= 50 && Convert.ToDecimal(trans.USD_Amount) % 50 == 0)
                        {
                            int result = UserManager.AddUserfundRequestinbtc("REQUESTFUNDINCOINPAYMENT", userInfo.memb_code, trans.amount, /*btcaddr.txn_id,*/trans.txtHash, btcaddr.amount, trans.BTC_Type,
                            btcaddr.BTCAddress, btcaddr.confirms_needed, btcaddr.timeout, btcaddr.checkout_url, btcaddr.status_url, btcaddr.qrcode_url);

                            if (result > 0)
                            {
                                TempData["fundRequest"] = "1";
                                TempData["BTCTYPE"] = trans.BTC_Type;
                                TempData["BTCAddress"] = btcAddress;
                                TempData["BTCAmount"] = trans.btcamount;
                                TempData["TRXamount"] = btcaddr.amount;
                            }
                            else
                            {
                                TempData["InvestmentAlert"] = "Purchase Package failed.";
                            }

                            //}
                            //else
                            //{
                            //    TempData["InvestmentAlert"] = "You have insufficient balance in your fund wallet.";
                            //}
                            //}
                            //else
                            //{
                            //    TempData["InvestmentAlert"] = "0 balance in your fund wallet to top up this user.";
                            //}
                        }
                        else
                        {
                            TempData["fundRequest"] = "Please Select Valid Package ";

                        }

                    }
                    else
                    {
                        TempData["fundRequest"] = "Error in generate address.";
                    }
                }
                else
                {
                    TempData["fundRequest"] = "Please Select Valid Package ";
                }
            }
            catch (Exception ex)
            {
                TempData["fundRequest"] = "Purchase Package failed.";
            }
            return RedirectToAction("Coinpayment_transaction");
        }

        public ActionResult fundRequest_BTC()
        {
            FundRequest trans = new FundRequest();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                if (dt.Rows.Count > 0)
                {
                    trans.Fund_Wallet = dt.Rows[0]["Fund_Wallet"].ToString();
                }
                else
                {
                    trans.Main_Wallet = "0";
                    trans.Fund_Wallet = "0";
                }
            }
            catch (Exception ex)
            {
                trans.Main_Wallet = "0";
                trans.Fund_Wallet = "0";
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult fundRequest_BTC(FundRequest trans)
        {
            try
            {
                trans.btcamount = trans.amount;
                trans.BTC_Type = "USD";
                if (string.IsNullOrEmpty(trans.amount))
                {
                    TempData["fundRequest"] = "Please Enter Amount.";
                    return RedirectToAction("fundRequest_BTC");
                }

                //if (string.IsNullOrEmpty(trans.BTC_Type))
                //{
                //    TempData["InvestmentAlert"] = "Please select Purchase by.";
                //    return RedirectToAction("Invest");
                //}

                //if (Convert.ToInt32(trans.donation_cycle) <= 0)
                //{
                //    TempData["InvestmentAlert"] = "Donation Cycle Should be Greater than or Equal to 1.";
                //    return RedirectToAction("Invest");
                //}

                //string allPackages = "30,100,500,1000";
                //string[] pkg = allPackages.Split(',');
                //int depositeStatus = 0;
                //for (int i = 0; i < pkg.Length; i++)
                //{
                //    if (string.Equals(trans.amount, pkg[i]))
                //    {
                //        depositeStatus = 1;
                //        break;
                //    }
                //}

                //if (trans.USD_Amount == "100000")
                //{
                //    trans.donation_cycle = "20";
                //}
                int depositeStatus = 1;
                if (depositeStatus == 1)
                {
                    BTCAddressModel btcaddr = new BTCAddressModel();
                    btcaddr = GetCreateTransaction(trans.BTC_Type, trans.btcamount);
                    //btcaddr = GetCoinpaymentAddress(trans.BTC_Type);
                    if (string.Equals(btcaddr.result, "ok"))
                    {
                        string btcAddress = btcaddr.BTCAddress;
                        // trans.BTC_Amount = "0";
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage response = client.GetAsync("https://min-api.cryptocompare.com/data/price?fsym=USD&tsyms=" + trans.BTC_Type).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string JSON = response.Content.ReadAsStringAsync().Result;
                            var obj = JsonConvert.DeserializeObject<AllCOINModel>(JSON);

                            string amtcoin = "0";
                            if (!string.IsNullOrEmpty(obj.BTC))
                                amtcoin = obj.BTC;
                            if (!string.IsNullOrEmpty(obj.ETH))
                                amtcoin = obj.ETH;
                            if (!string.IsNullOrEmpty(obj.LTC))
                                amtcoin = obj.LTC;
                            decimal amtcoin1 = decimal.Parse(amtcoin, NumberStyles.Float);
                            trans.btcamount = ((Convert.ToDecimal(trans.btcamount)) * amtcoin1).ToString();
                            //tokenamt = Convert.ToDecimal(trans.USD_Amount) / 10;
                        }
                        //DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                        //if (dt.Rows.Count > 0)
                        //{
                        //    string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

                        //if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Total_Balance))
                        //{
                        //DataTable dtUser = UserManager.CheckUsername(trans.USERNAME);
                        //if (dtUser.Rows.Count > 0)
                        //{
                        //    string membCode = dtUser.Rows[0]["memb_code"].ToString();
                        //if (Convert.ToDecimal(trans.USD_Amount) >= 2500 && Convert.ToDecimal(trans.USD_Amount) % 2500 == 0)
                        //{
                        int result = UserManager.AddUserfundRequestinbtc(userInfo.memb_code, trans.amount, null, "REQUESTFUNDINBTC", null, trans.btcamount, "BTC", null, null, null, null, null/*, null, null*/);

                        if (result > 0)
                        {
                            TempData["fundRequest"] = "1";
                            TempData["BTCTYPE"] = trans.BTC_Type;
                            TempData["BTCAddress"] = btcAddress;
                            TempData["BTCAmount"] = trans.btcamount;
                        }
                        else
                        {
                            TempData["InvestmentAlert"] = "Purchase Package failed.";
                        }

                        //}
                        //else
                        //{
                        //    TempData["InvestmentAlert"] = "You have insufficient balance in your fund wallet.";
                        //}
                        //}
                        //else
                        //{
                        //    TempData["InvestmentAlert"] = "0 balance in your fund wallet to top up this user.";
                        //}
                    }
                    else
                    {
                        TempData["fundRequest"] = "Error in generate trx address.";
                    }
                }
                else
                {
                    TempData["fundRequest"] = "Please Select Valid Package ";
                }
            }
            catch (Exception ex)
            {
                TempData["fundRequest"] = "Purchase Package failed.";
            }
            return RedirectToAction("fundRequest_BTC");
        }

        public ActionResult fundRequest_history()
        {
            List<FundRequest> trnsferdList = new List<FundRequest>();
            try
            {
                DataTable dt = UserManager.User_Report("FUNDREQUESTHISTORY",userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    trnsferdList = JsonConvert.DeserializeObject<List<FundRequest>>(jsonString);
                }
            }
            catch
            {

            }
            return View(trnsferdList.ToList());
        }


        //public ActionResult Coinpayment_transaction()
        //{
        //    FundRequest trans = new FundRequest();
        //    try
        //    {
        //        DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
        //        if (dt.Rows.Count > 0)
        //        {
        //            trans.Fund_Wallet = dt.Rows[0]["Fund_Wallet"].ToString();
        //        }
        //        else
        //        {
        //            trans.Main_Wallet = "0";
        //            trans.Fund_Wallet = "0";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Main_Wallet = "0";
        //        trans.Fund_Wallet = "0";
        //    }
        //    return View(trans);
        //}

        //[HttpPost]
        //public ActionResult Coinpayment_transaction(FundRequest trans)
        //{
        //    try
        //    {
        //        trans.btcamount = trans.amount;
        //        trans.BTC_Type = "USD";
        //        if (string.IsNullOrEmpty(trans.amount))
        //        {
        //            TempData["fundRequest"] = "Please Enter Amount.";
        //            return RedirectToAction("Coinpayment_transaction");
        //        }

        //        if (Convert.ToDecimal(trans.amount) == 20)
        //        {

        //        }
        //        else
        //        {
        //            TempData["fundRequest"] = "PLEASE ENTER AMOUNT OF MINIMUM 20 USD.";
        //            return RedirectToAction("Coinpayment_transaction");
        //        }

        //        //if (string.IsNullOrEmpty(trans.BTC_Type))
        //        //{
        //        //    TempData["InvestmentAlert"] = "Please select Purchase by.";
        //        //    return RedirectToAction("Invest");
        //        //}

        //        //if (Convert.ToInt32(trans.donation_cycle) <= 0)
        //        //{
        //        //    TempData["InvestmentAlert"] = "Donation Cycle Should be Greater than or Equal to 1.";
        //        //    return RedirectToAction("Invest");
        //        //}

        //        //string allPackages = "30,100,500,1000";
        //        //string[] pkg = allPackages.Split(',');
        //        //int depositeStatus = 0;
        //        //for (int i = 0; i < pkg.Length; i++)
        //        //{
        //        //    if (string.Equals(trans.amount, pkg[i]))
        //        //    {
        //        //        depositeStatus = 1;
        //        //        break;
        //        //    }
        //        //}

        //        //if (trans.USD_Amount == "100000")
        //        //{
        //        //    trans.donation_cycle = "20";
        //        //}
        //        int depositeStatus = 1;
        //        if (depositeStatus == 1)
        //        {
        //            BTCAddressModel btcaddr = new BTCAddressModel();
        //            //btcaddr = GetBTCAddress(trans.BTC_Type);
        //            btcaddr = GetCreateTransaction(trans.BTC_Type, trans.btcamount);
        //            if (string.Equals(btcaddr.result, "ok"))
        //            {
        //                string btcAddress = btcaddr.BTCAddress;
        //                // trans.BTC_Amount = "0";
        //                //HttpClient client = new HttpClient();
        //                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //                //HttpResponseMessage response = client.GetAsync("https://min-api.cryptocompare.com/data/price?fsym=USD&tsyms=" + trans.BTC_Type).Result;
        //                //if (response.IsSuccessStatusCode)
        //                //{
        //                //    string JSON = response.Content.ReadAsStringAsync().Result;
        //                //    var obj = JsonConvert.DeserializeObject<AllCOINModel>(JSON);

        //                //    string amtcoin = "0";
        //                //    if (!string.IsNullOrEmpty(obj.BTC))
        //                //        amtcoin = obj.BTC;
        //                //    if (!string.IsNullOrEmpty(obj.ETH))
        //                //        amtcoin = obj.ETH;
        //                //    if (!string.IsNullOrEmpty(obj.LTC))
        //                //        amtcoin = obj.LTC;
        //                //    decimal amtcoin1 = decimal.Parse(amtcoin, NumberStyles.Float);
        //                //    trans.btcamount = ((Convert.ToDecimal(trans.btcamount)) * amtcoin1).ToString();
        //                //    //tokenamt = Convert.ToDecimal(trans.USD_Amount) / 10;
        //                //}
        //                //DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
        //                //if (dt.Rows.Count > 0)
        //                //{
        //                //    string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

        //                //if (Convert.ToDecimal(trans.Amount) <= Convert.ToDecimal(Total_Balance))
        //                //{
        //                //DataTable dtUser = UserManager.CheckUsername(trans.USERNAME);
        //                //if (dtUser.Rows.Count > 0)
        //                //{
        //                //    string membCode = dtUser.Rows[0]["memb_code"].ToString();
        //                //if (Convert.ToDecimal(trans.USD_Amount) >= 2500 && Convert.ToDecimal(trans.USD_Amount) % 2500 == 0)
        //                //{
        //                int result = UserManager.AddUserfundRequestinbtc("REQUESTFUNDINCOINPAYMENT", userInfo.memb_code, trans.amount, btcaddr.txn_id, btcaddr.amount, trans.BTC_Type,
        //                    btcaddr.BTCAddress, btcaddr.confirms_needed, btcaddr.timeout, btcaddr.checkout_url, btcaddr.status_url, btcaddr.qrcode_url);

        //                if (result > 0)
        //                {
        //                    TempData["fundRequest"] = "1";
        //                    TempData["BTCTYPE"] = trans.BTC_Type;
        //                    TempData["BTCAddress"] = btcAddress;
        //                    TempData["BTCAmount"] = trans.btcamount;
        //                }
        //                else
        //                {
        //                    TempData["InvestmentAlert"] = "Purchase Package failed.";
        //                }

        //                //}
        //                //else
        //                //{
        //                //    TempData["InvestmentAlert"] = "You have insufficient balance in your fund wallet.";
        //                //}
        //                //}
        //                //else
        //                //{
        //                //    TempData["InvestmentAlert"] = "0 balance in your fund wallet to top up this user.";
        //                //}
        //            }
        //            else
        //            {
        //                TempData["fundRequest"] = "Error in generate USD address.";
        //            }
        //        }
        //        else
        //        {
        //            TempData["fundRequest"] = "Please Select Valid Package ";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["fundRequest"] = "Purchase Package failed.";
        //    }
        //    return RedirectToAction("Coinpayment_transaction");
        //}


        //public ActionResult FranchiseR()
        //{
        //    FundRequest trans = new FundRequest();
        //    try
        //    {
        //        DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
        //        if (dt.Rows.Count > 0)
        //        {
        //            trans.Fund_Wallet = dt.Rows[0]["Fund_Wallet"].ToString();
        //        }
        //        else
        //        {
        //            trans.Main_Wallet = "0";
        //            trans.Fund_Wallet = "0";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Main_Wallet = "0";
        //        trans.Fund_Wallet = "0";
        //    }
        //    return View(trans);
        //}

        //[HttpPost]
        //public ActionResult FranchiseR(FundRequest trans, HttpPostedFileBase Attachment, string gpay, string fname, string mnumber, string state, string dstrict, string btcac)
        //{
        //    try
        //    {
        //        trans.btcamount = trans.amount;
        //        trans.BTC_Type = "FRANCHISE";
        //        if (string.IsNullOrEmpty(trans.amount))
        //        {
        //            TempData["fundRequest"] = "Please Enter Amount.";
        //            return RedirectToAction("FranchiseR");
        //        }



        //        int depositeStatus = 1;
        //        if (depositeStatus == 1)
        //        {

        //            if (Attachment == null || Attachment.ContentLength == 0)
        //            {
        //                TempData["fundRequest"] = "Please upload attachment.";
        //                return RedirectToAction("FranchiseR");
        //            }
        //            string attachments = null;
        //            if ((Attachment != null) && (Attachment.ContentLength > 0))
        //            {
        //                string sextension = Path.GetExtension(Attachment.FileName);
        //                if (sextension.ToLower() == ".png" || sextension.ToLower() == ".jpg" || sextension.ToLower() == ".jpeg")
        //                {
        //                    string wdate = DateTime.Now.ToString("yyyymmddhhMMss");
        //                    string fileAttn = "USER" + wdate + sextension;
        //                    string path = Server.MapPath("~/Content/invetsmentAttch/" + fileAttn);
        //                    Stream strm = Attachment.InputStream;
        //                    var targetFile = path;
        //                    GenerateThumbnails(0.5, strm, targetFile);
        //                    attachments = fileAttn;
        //                }
        //            }

        //            if (string.IsNullOrEmpty(attachments))
        //            {
        //                TempData["fundRequest"] = "Please select .png , .jpg or .jpeg file only.";
        //                return RedirectToAction("FranchiseR");
        //            }

        //            int result = UserManager.AddUserfundRequestinbtc(userInfo.memb_code, trans.amount, attachments, "REQUESTFORFRANCHISE", null, trans.btcamount, "FRANCHISE", null, gpay, fname, mnumber, state, dstrict, btcac);

        //            if (result > 0)
        //            {
        //                TempData["fundRequest"] = "1";
        //                TempData["BTCTYPE"] = trans.BTC_Type;
        //                // TempData["BTCAddress"] = btcAddress;
        //                TempData["BTCAmount"] = trans.btcamount;
        //            }
        //            else
        //            {
        //                TempData["InvestmentAlert"] = "fund request failed.";
        //            }


        //            //}
        //            //else
        //            //{
        //            //    TempData["fundRequest"] = "Error in generate bitcoin address.";
        //            //}
        //        }
        //        else
        //        {
        //            TempData["fundRequest"] = "fund request failed ";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["fundRequest"] = "fund request failed,please try after some time.";
        //    }
        //    return RedirectToAction("FranchiseR");
        //}


        public ActionResult FranchiseReoprt()
        {
            List<FundRequest> trnsferdList = new List<FundRequest>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "FRANCHISEREQUESTHISTORY");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    trnsferdList = JsonConvert.DeserializeObject<List<FundRequest>>(jsonString);
                }
            }
            catch
            {

            }
            return View(trnsferdList.ToList());
        }


        public ActionResult Logout()
        {
            Common.CurrentUserInfo = null;
            Common.CookieUserID = null;
            Common.CookieUserType = null;

            return RedirectToAction("Index", "Home");
        }




        //------------------------------IMPS------------------------//

        public ActionResult addBankDetails(string bankno)
        {

            BankDetailsModel user = new BankDetailsModel();
            try
            {
                //GetBankList();
                loadBankName();

                if (!string.IsNullOrEmpty(bankno))
                {
                    DataTable dt = UserManager.User_Report_Srno(userInfo.memb_code, "GETBANKDETAILSBYSRO", bankno);
                    if (dt.Rows.Count > 0)
                    {
                        string jsonString = JsonConvert.SerializeObject(dt);
                        var dList = JsonConvert.DeserializeObject<List<BankDetailsModel>>(jsonString);

                        user = dList.First();
                    }
                }
                user.Bank_Address = "Delhi";
                user.Pin_Code = "";
            }
            catch (Exception ex)
            {

            }

            return View(user);

        }

        [HttpPost]
        public ActionResult addBankDetails(BankDetailsModel user)
        {

            try
            {



                if (!ModelState.IsValid)
                {
                    TempData["ProfileAlert"] = "Please Enter All Valid Data.";
                    return RedirectToAction("addBankDetails", "MemberPanel");
                }

                if (string.IsNullOrEmpty(user.bank_mobile_no.Trim()))
                {
                    TempData["ProfileAlert"] = "Please enter Registered Mobile No.";
                    return RedirectToAction("addBankDetails", "MemberPanel");
                }


                if (string.IsNullOrEmpty(user.Pin_Code.Trim()))
                {
                    TempData["ProfileAlert"] = "Please enter Pin_Code.";
                    return RedirectToAction("addBankDetails", "MemberPanel");
                }


                //if (string.IsNullOrEmpty(user.request_code))
                //{
                //    TempData["ProfileAlert"] = "Please enter OTP.";
                //    return RedirectToAction("addBankDetails", "Dashboard");
                //}

                //if (!string.Equals(Session["Bank"], user.request_code))
                //{
                //    TempData["ProfileAlert"] = "Please Enter Valid OTP Details.";
                //    return RedirectToAction("addBankDetails", "Dashboard");
                //}

                Session["ac_name"] = user.ac_name;
                Session["ac_no"] = user.ac_no;
                Session["ac_type"] = user.ac_type;
                Session["bk_name"] = user.bk_name;
                Session["bk_branch"] = user.bk_branch;
                Session["bk_ifsc"] = user.bk_ifsc;
                Session["bank_mobile_no"] = user.bank_mobile_no;
                Session["Pin_Code"] = user.Pin_Code;
                //     Session["Bank_Address"] = user.Bank_Address;
                //Session["city"] = user.city;
                //Session["state"] = user.state;
                //Session["country"] = user.country;
                int bankstatus = 1;
                GETEXISTINGCUSTOMER customer = DoopmeApiServices.GET_EXISTING_CUSTOMER(user.bank_mobile_no);
                if (customer.NTDRESP.STATUSCODE == "0")
                {
                    if (customer.NTDRESP.STATUSMSG == "Success")
                    {

                        Session["FNAME"] = customer.FNAME;
                        Session["LNAME"] = customer.LNAME;
                        Session["LIMIT"] = customer.LIMIT;
                        Session["USED"] = customer.USED;
                        Session["REMAIN"] = customer.REMAIN;

                        if (customer.NTDRESP.STATUS == "1")
                        {

                            if (customer.NTDRESP.STATUSDESC == "Active")
                            {
                                beneficiary_listRootObject beneficiary = DoopmeApiServices.getBeneficiaryList(user.bank_mobile_no);

                                if (beneficiary.NTDRESP.STATUSCODE == "0")
                                {
                                    if (beneficiary.NTDRESP.STATUSMSG == "Success")
                                    {
                                        if (beneficiary.NTDRESP.BENELIST != null)
                                        {
                                            List<BENELIST> beneficiarylistNormal = beneficiary.NTDRESP.BENELIST;
                                            if (beneficiarylistNormal != null)
                                            {
                                                foreach (var item in beneficiarylistNormal)
                                                {
                                                    if (item.ACCNO == user.ac_no && item.IFSC == user.bk_ifsc)
                                                    {
                                                        if (bankstatus == 1)
                                                        {
                                                            Session["beneficiaryid_corporate"] = item.BENEID;
                                                            Session["beneficiary_name"] = item.BENENAME;
                                                            Session["beneficiaryid_mobileno"] = item.MOBILENO;
                                                            Session["beneficiary_bankid"] = item.BANKID;
                                                            Session["beneficiary_bankname"] = item.BANKNAME;
                                                            Session["beneficiary_accno"] = item.ACCNO;
                                                            Session["beneficiary_ifsc"] = item.IFSC;
                                                            Session["beneficiary_verified"] = item.VERIFIED;
                                                            Session["beneficiary_IMPS_SCHEDULE"] = item.IMPS_SCHEDULE;
                                                            Session["beneficiary_status"] = item.STATUS;
                                                            Session["beneficiary_statusdesc"] = item.STATUSDESC;
                                                            Session["beneficiaryid"] = item.BENEID;
                                                            bankstatus = 0;
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                TempData["BankAlert"] = "Bank Details Added Failed.";
                                            }

                                            if (bankstatus == 1)
                                            {
                                                string customer_id = customer.CUSTID;

                                                DataTable dtBankAcc = UserManager.User_Report_Mobile_Acc_No(userInfo.memb_code, "GETBANKDETAILSBYSRNOMOBILEACC_NO", user.bank_mobile_no, user.ac_no);
                                                if (dtBankAcc.Rows.Count > 0)
                                                {
                                                    customer_id = dtBankAcc.Rows[0]["customer_id"].ToString();
                                                    Session["customer_id"] = customer_id;
                                                }

                                                DataTable dt = UserManager.User_Report_Account("GETBANKDETAILSLISTBANKIFSC", user.bk_ifsc);
                                                if (dt.Rows.Count > 0)
                                                {
                                                    user.bankid = dt.Rows[0]["BANKID"].ToString();
                                                    AddBeneficiary addBeneficiary = DoopmeApiServices.AddBenificiaryDetails(user.bank_mobile_no, user.bankid, user.ac_name, user.bank_mobile_no, user.ac_no, user.bk_ifsc);
                                                    if (addBeneficiary.NTDRESP.STATUSCODE == "0")
                                                    {
                                                        if (addBeneficiary.NTDRESP.STATUSMSG == "Beneficiary Added Successfully")
                                                        {
                                                            Session["beneficiaryid_corporate"] = addBeneficiary.NTDRESP.BENEID;
                                                            Session["beneficiaryid"] = addBeneficiary.NTDRESP.BENEID;

                                                            DataTable dtErrorBen = UserManager.Search_Registration("ADDBANKDTL", addBeneficiary.NTDRESP.BANKID + " " + addBeneficiary.NTDRESP.STATUSMSG, null);
                                                            bankstatus = 0;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        DataTable dtErrorSe = UserManager.Search_Registration("ADDBANKDTL", user.bank_mobile_no + " " + addBeneficiary.NTDRESP.STATUSMSG, null);
                                                        TempData["BankAlert"] = "Bank Details Added Failed.";
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            TempData["BankAlert"] = "Bank Details Added Failed.";
                                        }
                                        DataTable dtError = UserManager.Search_Registration("ADDBANKDTL", user.bank_mobile_no + " " + beneficiary.MESSAGE, null);
                                    }
                                    else
                                    {
                                        string customer_id = customer.CUSTID;

                                        DataTable dtBankAcc = UserManager.User_Report_Mobile_Acc_No(user.ac_no, "GETBANKDETAILSBYSRNOMOBILEACC_NO", user.bank_mobile_no, user.ac_no);
                                        if (dtBankAcc.Rows.Count > 0)
                                        {
                                            customer_id = dtBankAcc.Rows[0]["customer_id"].ToString();
                                            Session["customer_id"] = customer_id;
                                        }


                                        AddBeneficiary addBeneficiary = DoopmeApiServices.AddBenificiaryDetails(customer.mobile, customer_id, user.ac_name, user.bank_mobile_no, user.ac_no, user.bk_ifsc);
                                        if (addBeneficiary.NTDRESP.STATUSCODE == "0")
                                        {
                                            Session["beneficiaryid_corporate"] = addBeneficiary.NTDRESP.BENEID;
                                            Session["beneficiaryid"] = addBeneficiary.NTDRESP.BENEID;


                                            DataTable dtError = UserManager.Search_Registration("ADDBANKDTL", addBeneficiary.NTDRESP.BENEID + " " + addBeneficiary.NTDRESP.STATUSMSG, null);
                                            bankstatus = 0;
                                        }
                                        else
                                        {
                                            DataTable dtError = UserManager.Search_Registration("ADDBANKDTL", user.bank_mobile_no + " " + addBeneficiary.NTDRESP.STATUSMSG, null);
                                            TempData["BankAlert"] = "Bank Details Added Failed.";
                                        }
                                    }
                                }
                                else
                                {
                                    DataTable dt = UserManager.User_Report_Account("GETBANKDETAILSLISTBANKIFSC", user.bk_ifsc);
                                    if (dt.Rows.Count > 0)
                                    {
                                        user.bankid = dt.Rows[0]["BANKID"].ToString();

                                        string bankid = user.bankid;
                                        AddBeneficiary addcust = DoopmeApiServices.AddBenificiaryDetails(user.bank_mobile_no, bankid, user.ac_name, user.bank_mobile_no, user.ac_no, user.bk_ifsc);

                                        if (addcust.NTDRESP.STATUSCODE == "0")
                                        {
                                            Session["beneficiaryid_corporate"] = addcust.NTDRESP.BENEID;
                                            Session["beneficiaryid"] = addcust.NTDRESP.BENEID;

                                            DataTable dtError = UserManager.Search_Registration("ADDBANKDTL", user.bank_mobile_no + " " + addcust.NTDRESP.STATUSMSG + " msr_00013", null);
                                            TempData["BankAlert"] = "Bank Details Added Successfully.";
                                            bankstatus = 0;

                                        }
                                        else
                                        {

                                            if (addcust.NTDRESP.STATUSMSG == "Beneficiary account already registered")
                                            {
                                                ADDCUSTOMER_RESENDVERIFYOTP otp = DoopmeApiServices.ValidateResendCustomerOTP(user.bank_mobile_no);
                                                if (otp.NTDRESP.STATUSCODE == "0")
                                                {
                                                    DataTable dtError = UserManager.Search_Registration("ADDBANKDTL", user.bank_mobile_no + " " + otp.NTDRESP.STATUSMSG + " msr_00013", null);
                                                    TempData["BankAlert"] = "Bank Details Added Successfully.";
                                                    bankstatus = 0;

                                                }

                                            }

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (customer.NTDRESP.STATUSDESC == "OTP Verify Pending")
                            {
                                ADDCUSTOMER addcust = DoopmeApiServices.ADD_CUSTOMER(user.bank_mobile_no, user.ac_name, user.bk_name, user.Pin_Code, user.bk_branch, user.bk_branch, user.bk_branch, user.bk_branch);
                                if (addcust.NTDRESP.STATUSCODE == "0")
                                {
                                    DataTable dtError = UserManager.Search_Registration("ADDBANKDTL", user.bank_mobile_no + " " + addcust.NTDRESP.STATUSMSG + " msr_00013", null);
                                    TempData["BankAlert"] = "Bank Details Added Successfully.";
                                    bankstatus = 0;

                                }
                                else
                                {

                                    if (addcust.NTDRESP.STATUSMSG == "Customer already registered but OTP Verification Pending")
                                    {
                                        ADDCUSTOMER_RESENDVERIFYOTP otp = DoopmeApiServices.ValidateResendCustomerOTP(user.bank_mobile_no);
                                        if (otp.NTDRESP.STATUSCODE == "0")
                                        {
                                            DataTable dtError = UserManager.Search_Registration("ADDBANKDTL", user.bank_mobile_no + " " + otp.NTDRESP.STATUSMSG + " msr_00013", null);
                                            TempData["BankAlert"] = "Bank Details Added Successfully.";
                                            bankstatus = 0;

                                        }

                                    }

                                }
                            }
                            else
                            {

                                DataTable dtError = UserManager.Search_Registration("ADDBANKDTL", user.bank_mobile_no + " Mob Failure message" + customer.NTDRESP.STATUSMSG, null);
                                TempData["BankAlert"] = "Bank Details Added Failed.";
                            }
                        }
                    }
                }
                else
                {


                    ADDCUSTOMER addcust = DoopmeApiServices.ADD_CUSTOMER(user.bank_mobile_no, user.ac_name, user.bk_name, user.Pin_Code, user.bk_branch, user.bk_branch, user.bk_branch, user.bk_branch);


                    if (addcust.NTDRESP.STATUSCODE == "0")
                    {
                        DataTable dtError = UserManager.Search_Registration("ADDBANKDTL", user.bank_mobile_no + " " + addcust.NTDRESP.STATUSMSG, null);
                        TempData["BankAlert"] = "Bank Details Added Successfully.";
                        bankstatus = 0;

                    }
                    else
                    {
                        DataTable dtError = UserManager.Search_Registration("ADDBANKDTL", user.bank_mobile_no + " " + addcust.NTDRESP.STATUSMSG, null);
                        TempData["BankAlert"] = "Bank Details Added Failed.";
                    }

                }
                if (bankstatus == 0)
                {
                    user.beneficiaryid = Session["beneficiaryid"].ToString();
                    user.beneficiaryid_corporate = Session["beneficiaryid_corporate"].ToString();
                    user.agent_id = customer.CUSTID;
                    string MODE = "ADDACCOUNT";

                    DataTable dtUserAcc = UserManager.ADD_ACCOUNT_DETAILS(userInfo.memb_code, user.ac_name.Trim(), user.ac_no.Trim()
                        , user.bk_name.Trim(), user.bk_branch.Trim(), user.bk_ifsc.Trim().ToUpper(), null, null, user.ac_type.Trim()
                        , user.phonepay_no, user.gpay_no, null, user.paytm_no, null, null
                        , user.bank_mobile_no, user.beneficiaryid, user.beneficiaryid_corporate, user.agent_id
                        , user.Pin_Code, user.Bank_Address);
                    if (dtUserAcc.Rows.Count > 0)
                    {
                        TempData["BankAlert"] = dtUserAcc.Rows[0]["SP_STATUS"].ToString();
                    }
                    else
                    {
                        DataTable dtError = UserManager.Search_Registration("ADDBANKDTL", "Bank Details Added Failed.", null);
                        TempData["BankAlert"] = "Bank Details Added Failed.";
                    }
                    // TempData["BankAlert"] = "This Bank details already added.";
                }
            }
            catch (Exception ex)
            {
                TempData["BankAlert"] = ex.ToString() + "Bank Details Added Failed.";
                DataTable dtError = UserManager.Search_Registration("ADDBANKDTL", ex.ToString() + "Bank Details Added Failed.", null);
                //TempData["BankAlert"] = ex.ToString();
            }

            return RedirectToAction("addBankDetails", "MemberPanel");
        }




        public void loadBankName()
        {

            //List<SelectListItem> items = new List<SelectListItem>();
            //try
            //{
            //    DataTable dt = UserManager.User_Report("GETBANKDETAILSLISTBANKNAME");
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        items.Add(new SelectListItem
            //        {
            //            Text = dt.Rows[i]["BANKNAME"].ToString(),
            //            Value = dt.Rows[i]["BANKNAME"].ToString()
            //        });
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            //ViewBag.Allbkname = items;

        }

        public ActionResult GETIFSCBYNAME(string bk_name)
        {
            List<BankDetailsModel> dList = new List<BankDetailsModel>();
            try
            {
                DataTable dt = UserManager.User_Report_Account("GETIFSC_DETAILSBYNAME", bk_name);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<BankDetailsModel>>(jsonString);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(dList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult bank_detail_partial()
        {

            List<BankDetailsModel> dList = new List<BankDetailsModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "GETBANKDETAILSLIST");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<BankDetailsModel>>(jsonString);
                }
            }
            catch (Exception ex)
            {
            }
            return PartialView("bank_detail_partial", dList.ToList());

        }



        public ActionResult getBankAccByMobile(string mobile)
        {
            List<BankDetailsModel> dList = new List<BankDetailsModel>();
            try
            {
                DataTable dt = UserManager.User_Report_Mobile(userInfo.memb_code, "GETBANKDETAILSBYMOBILE", mobile);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<BankDetailsModel>>(jsonString);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(dList, JsonRequestBehavior.AllowGet);
        }


        public void loadMobileNo()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                DataTable dt = UserManager.USER_REPORT("GETBANKDETAILSLISTMOBILE", userInfo.memb_code);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    items.Add(new SelectListItem
                    {
                        Text = dt.Rows[i]["bank_mobile_no"].ToString(),
                        Value = dt.Rows[i]["bank_mobile_no"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.AllMobileNo = items;
        }




        //----------------------------- PIN Pages --------------------//

        #region PIN Details

        public ActionResult Pin_request()
        {
            PINDETAILSMODEL trans = new PINDETAILSMODEL();
            try
            {
                trans.amount = "300";
            }
            catch (Exception ex)
            {
                TempData["Error : "] = ex.Message;
                trans.amount = "600";
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult Pin_request(PINDETAILSMODEL fund, HttpPostedFileBase pin_slip)
        {
            string fileAttn1 = "";
            try
            {
                if ((pin_slip != null) && (pin_slip.ContentLength > 0))
                {
                    string sextension = Path.GetExtension(pin_slip.FileName);
                    if (sextension.ToLower() == ".png" || sextension.ToLower() == ".jpg" || sextension.ToLower() == ".jpeg")
                    {
                        string wdate = DateTime.Now.ToString("yyyymmddhhMMss");
                        fileAttn1 = "USER" + wdate + sextension;
                        string path = Server.MapPath("~/Content/PinSlip/" + fileAttn1);

                        Stream strm = pin_slip.InputStream;
                        var targetFile = path;
                        GenerateThumbnails(0.5, strm, targetFile);

                    }
                }
                // fund.amount = "400";
                if (string.IsNullOrEmpty(fund.amount))
                {
                    TempData["CreatePinAlert"] = "Please select amount.";
                    return RedirectToAction("Pin_request");
                }
                if (string.IsNullOrEmpty(fund.Quantiy))
                {
                    TempData["CreatePinAlert"] = "Please enter quantity.";
                    return RedirectToAction("Pin_request");
                }
                if (Convert.ToString(pin_slip) == "")
                {
                    TempData["CreatePinAlert"] = "Please Select Attachment.";
                    return RedirectToAction("Pin_request");
                }

                if (Convert.ToDecimal(fund.Quantiy) <= 0)
                {
                    TempData["CreatePinAlert"] = "Please enter quantity greater than 0.";
                    return RedirectToAction("Pin_request");
                }

                dtResult = UserManager.PinRequestdetails("ADDPINREQUEST", "0", userInfo.memb_code, null, null
                    , userInfo.memb_code, fund.amount, fund.Quantiy, fileAttn1);
                if (dtResult.Rows.Count > 0)
                {
                    TempData["CreatePinAlert"] = dtResult.Rows[0]["Success"].ToString();
                }
                else
                {
                    TempData["CreatePinAlert"] = "Pin Request Added failed.";
                }
            }
            catch (Exception ex)
            {
                TempData["CreatePinAlert"] = "Pin Request Added failed.Error:" + ex.Message;
            }
            return RedirectToAction("Pin_request");
        }

        public ActionResult Pin_request_history()
        {
            List<PINDETAILSMODEL> dList = new List<PINDETAILSMODEL>();
            try
            {
                dtResult = UserManager.Pindetails("MYREQUESTPINHISTORY", "0", userInfo.memb_code, null, null, userInfo.memb_code, null);
                if (dtResult.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dtResult);
                    dList = JsonConvert.DeserializeObject<List<PINDETAILSMODEL>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                TempData["Error : "] = ex.Message;
            }
            return View(dList);
        }

        public ActionResult Pin_history()
        {
            List<PINDETAILSMODEL> dList = new List<PINDETAILSMODEL>();
            try
            {
                dtResult = UserManager.Pindetails("MYPINHISTORY", "0", userInfo.memb_code, null, null, userInfo.memb_code, null);
                if (dtResult.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dtResult);
                    dList = JsonConvert.DeserializeObject<List<PINDETAILSMODEL>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                TempData["Error : "] = ex.Message;

            }
            return View(dList);
        }

        public ActionResult Pin_create()
        {
            PINDETAILSMODEL trans = new PINDETAILSMODEL();
            try
            {
                //  trans.amount = "300";
                dtResult = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                if (dtResult.Rows.Count > 0)
                {
                    trans.Main_Wallet = dtResult.Rows[0]["Fund_Wallet"].ToString();
                }
                else
                {
                    trans.Main_Wallet = "0";
                }
            }
            catch (Exception ex)
            {
                TempData["Error : "] = ex.Message;
                trans.Main_Wallet = "0";
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult Pin_create(PINDETAILSMODEL fund)
        {
            try
            {
                //fund.amount = "200";
                if (string.IsNullOrEmpty(fund.amount))
                {
                    TempData["CreatePinAlert"] = "Please select amount.";
                    return RedirectToAction("Pin_create");
                }

                if (string.IsNullOrEmpty(fund.Quantiy))
                {
                    TempData["CreatePinAlert"] = "Please enter quantity.";
                    return RedirectToAction("Pin_create");
                }

                if (Convert.ToDecimal(fund.Quantiy) <= 0)
                {
                    TempData["CreatePinAlert"] = "Please enter quantity greater than 0.";
                    return RedirectToAction("Pin_create");
                }

                string allPackages = "5000,10000,25000,50000,100000";
                string[] pkg = allPackages.Split(',');
                int depositeStatus = 0;
                for (int i = 0; i < pkg.Length; i++)
                {
                    if (string.Equals(fund.amount, pkg[i]))
                    {
                        depositeStatus = 1;
                        break;
                    }
                }
                if (depositeStatus == 1)
                {
                    dtResult = UserManager.USER_REPORT("FUNDWALLET", userInfo.memb_code);
                    if (dtResult.Rows.Count > 0)
                    {
                        string totalBalance = dtResult.Rows[0]["Fund_Wallet"].ToString();
                        decimal pinAmount = Convert.ToDecimal(fund.Quantiy) * Convert.ToDecimal(fund.amount);
                        if (pinAmount <= Convert.ToDecimal(totalBalance))
                        {
                            for (int i = 1; i <= Convert.ToDecimal(fund.Quantiy); i++)
                            {
                                string Pin = CreateRandomPin(15);
                                int sta = 0;
                                while (sta < 1)
                                {
                                    DataTable dtUser = UserManager.Pindetails("CHECKPINADD", "0", userInfo.memb_code, Pin, null, userInfo.memb_code, fund.amount);
                                    if (dtUser.Rows.Count == 0)
                                    {
                                        sta = 1;
                                    }
                                }
                                DataTable dtAdd = UserManager.Pindetails("ADDPIN", "0", userInfo.memb_code, Pin, null, userInfo.memb_code, fund.amount);
                            }
                            TempData["CreatePinAlert"] = "1";
                        }
                        else
                        {
                            TempData["CreatePinAlert"] = "You have insufficient balance to create pin.";
                        }
                    }
                    else
                    {
                        TempData["CreatePinAlert"] = "You have insufficient balance to create pin.";
                    }
                }
                else
                {
                    TempData["CreatePinAlert"] = "Please Enter Valid pin.";
                }
            }
            catch (Exception ex)
            {
                TempData["CreatePinAlert"] = "Pin Create failed.Error:" + ex.Message;
            }
            return RedirectToAction("Pin_create");
        }

        public ActionResult Pin_createHistory()
        {
            List<PINDETAILSMODEL> dList = new List<PINDETAILSMODEL>();
            try
            {
                dtResult = UserManager.Pindetails("CreatePinHistory", "0", userInfo.memb_code, null, null, userInfo.memb_code, null);
                if (dtResult.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dtResult);
                    dList = JsonConvert.DeserializeObject<List<PINDETAILSMODEL>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                TempData["Error : "] = ex.Message;
            }
            return View(dList);
        }

        public ActionResult Pin_transfer()
        {
            PINDETAILSMODEL trans = new PINDETAILSMODEL();
            try
            {

                dtResult = UserManager.Pindetails("GETAVAILABLEPIN", "0", userInfo.memb_code, null, null, null, null);
                if (dtResult.Rows.Count > 0)
                {
                    trans.Available_Pin = dtResult.Rows[0]["Available_Pin"].ToString();
                }
                else
                {
                    trans.Available_Pin = "0";
                }
            }
            catch (Exception ex)
            {
                trans.Available_Pin = "0";
                TempData["Error : "] = ex.Message;
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult Pin_transfer(PINDETAILSMODEL fund)
        {
            try
            {
                if (string.IsNullOrEmpty(fund.username))
                {
                    TempData["TransferPinAlert"] = "Please enter User ID.";
                    return RedirectToAction("Pin_transfer");
                }

                if (string.IsNullOrEmpty(fund.amount))
                {
                    TempData["TransferPinAlert"] = "Please select amount.";
                    return RedirectToAction("Pin_transfer");
                }

                if (string.IsNullOrEmpty(fund.Quantiy))
                {
                    TempData["TransferPinAlert"] = "Please enter Pin Quantity.";
                    return RedirectToAction("Pin_transfer");
                }

                if (Convert.ToInt32(fund.Quantiy) <= 0)
                {
                    TempData["TransferPinAlert"] = "Please enter Pin Quantity greater than 0.";
                    return RedirectToAction("Pin_transfer");
                }

                dtResult = UserManager.CheckUsername(fund.username.Trim());
                if (dtResult.Rows.Count > 0)
                {
                    string memb_code = dtResult.Rows[0]["memb_code"].ToString();
                    string Available_Pin = "0";
                    DataTable dtAvail = UserManager.Pindetails("GETAVAILABLEPIN", "0", userInfo.memb_code, null, null, null, fund.amount);
                    if (dtAvail.Rows.Count > 0)
                    {
                        Available_Pin = dtAvail.Rows[0]["Available_Pin"].ToString();
                    }

                    if (Convert.ToInt32(fund.Quantiy) <= Convert.ToInt32(Available_Pin))
                    {
                        DataTable dt = UserManager.Pindetails("MYPINHISTORYNOTUSED", "0", userInfo.memb_code, fund.Pin, null, userInfo.memb_code, fund.amount);
                        for (int i = 0; i < Convert.ToInt32(fund.Quantiy); i++)
                        {
                            string amount = dt.Rows[i]["amount"].ToString();
                            string pinid = dt.Rows[i]["PinID"].ToString();
                            string Pin = dt.Rows[i]["Pin"].ToString();

                            DataTable dtAdd = UserManager.Pindetails("TRANSFERPIN", pinid, memb_code, Pin, null, userInfo.memb_code, amount);
                            TempData["TransferPinAlert"] = "Pin Transfer Successfully.";
                        }

                        //DataTable dt = UserManager.Pindetails("CHECKPINTRANSFER", "0", userInfo.memb_code, fund.Pin, null, userInfo.memb_code, null);
                        //if (dt.Rows.Count > 0)
                        //{
                        //    if (dt.Rows[0]["tf_flag"].ToString() == "Y")
                        //    {
                        //        TempData["TransferPinAlert"] = "This Pin Already transfer.";
                        //    }
                        //    else if (dt.Rows[0]["u_flag"].ToString() == "Y")
                        //    {
                        //        TempData["TransferPinAlert"] = "This Pin Already Used.";
                        //    }
                        //    else
                        //    {
                        //        string amount = dt.Rows[0]["amount"].ToString();
                        //        string pinid = dt.Rows[0]["PinID"].ToString();

                        //        DataTable dtAdd = UserManager.Pindetails("TRANSFERPIN", pinid, memb_code, fund.Pin, null, userInfo.memb_code, amount);
                        //        TempData["TransferPinAlert"] = "Pin Transfer Successfully.";
                        //    }
                        //}
                        //else
                        //{
                        //    TempData["TransferPinAlert"] = "This Pin is invalid";
                        //}
                    }
                    else
                    {
                        TempData["TransferPinAlert"] = "You have only " + Available_Pin + " pin quantity to transfer.";
                    }
                }
                else
                {
                    TempData["TransferPinAlert"] = "User ID is invalid";
                }
            }
            catch (Exception ex)
            {
                TempData["TransferPinAlert"] = "Pin Transfer failed.Error:" + ex.Message;
            }
            return RedirectToAction("Pin_transfer");
        }

        public ActionResult Pin_TOPUP(string Pin)
        {
            return View();
        }

        [HttpPost]
        //public ActionResult Pin_TOPUP(PINDETAILSMODEL trans)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(trans.username.Trim()))
        //        {
        //            TempData["TopUpAlert"] = "Please enter User ID.";
        //            return RedirectToAction("Pin_TOPUP");
        //        }

        //        if (string.IsNullOrEmpty(userInfo.ac_name) || string.IsNullOrEmpty(userInfo.ac_no)
        //         || string.IsNullOrEmpty(userInfo.bk_name) || string.IsNullOrEmpty(userInfo.bk_branch)
        //         || string.IsNullOrEmpty(userInfo.bk_ifsc))
        //        {
        //            TempData["TopUpAlert"] = "Please first update your bank details.";
        //            return RedirectToAction("Pin_TOPUP");
        //        }

        //        if (string.IsNullOrEmpty(trans.Pin.Trim()))
        //        {
        //            TempData["TopUpAlert"] = "Please enter Pin No.";
        //            return RedirectToAction("Pin_TOPUP");
        //        }

        //        dtResult = UserManager.Pindetails("CHECKPINTRANSFER", "0", userInfo.memb_code, trans.Pin.Trim(), null, userInfo.memb_code, null);
        //        if (dtResult.Rows.Count > 0)
        //        {
        //            if (dtResult.Rows[0]["tf_flag"].ToString() == "Y")
        //            {
        //                TempData["TopUpAlert"] = "This Pin Already transfer.";
        //            }
        //            else if (dtResult.Rows[0]["u_flag"].ToString() == "Y")
        //            {
        //                TempData["TopUpAlert"] = "This Pin Already Used.";
        //            }
        //            else
        //            {
        //                string amount = dtResult.Rows[0]["amount"].ToString();
        //                string pinid = dtResult.Rows[0]["PinID"].ToString();

        //                DataTable dtUser = UserManager.CheckUsername(trans.username);
        //                if (dtUser.Rows.Count > 0)
        //                {
        //                    string membCode = dtUser.Rows[0]["memb_code"].ToString();
        //                    string s_mobileno = dtUser.Rows[0]["memb_code"].ToString();

        //                    if (!string.Equals(userInfo.memb_code, dtUser.Rows[0]["Spon_code"].ToString()))
        //                    {
        //                        TempData["TopUpAlert"] = "Pin Top up only Direct Downline Memebers";
        //                        return RedirectToAction("topuppin");
        //                    }


        //                    DataTable CHECKAUTHRISED = UserManager.USER_REPORT("CHECKAUTHRISED", membCode);
        //                    if (CHECKAUTHRISED.Rows.Count > 0)
        //                    {
        //                        if (CHECKAUTHRISED.Rows[0]["authrised"].ToString() == "G")
        //                        {
        //                            TempData["TopUpAlert"] = "Member ID is already activate please enter another id";
        //                            return RedirectToAction("Pin_TOPUP");
        //                        }
        //                    }

        //                    int result = UserManager.User_TopUp("ADDTOPUP", membCode, amount, null, "0", "PIN", amount, userInfo.memb_code, pinid);
        //                    if (result > 0)
        //                    {
        //                        TempData["TopUpAlert"] = "1";
        //                    }
        //                    else
        //                    {
        //                        TempData["TopUpAlert"] = "Topup failed.";
        //                    }
        //                }
        //                else
        //                {
        //                    TempData["TopUpAlert"] = "Please enter valid User id.";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            TempData["TopUpAlert"] = "This Pin is invalid";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["TopUpAlert"] = "Topup failed.Error:" + ex.Message;
        //    }
        //    return RedirectToAction("Pin_TOPUP");
        //}

        public ActionResult Pin_transferHistory()
        {
            PinTransferModel pin = new PinTransferModel();
            List<PINDETAILSMODEL> tdList = new List<PINDETAILSMODEL>();
            List<PINDETAILSMODEL> rdList = new List<PINDETAILSMODEL>();
            try
            {
                dtResult = UserManager.Pindetails("TransferPinHistory", "0", userInfo.memb_code, null, null, userInfo.memb_code, null);
                if (dtResult.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dtResult);
                    tdList = JsonConvert.DeserializeObject<List<PINDETAILSMODEL>>(jsonString);
                }

                dtResult = UserManager.Pindetails("ReceivePinHistory", "0", userInfo.memb_code, null, null, userInfo.memb_code, null);
                if (dtResult.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dtResult);
                    rdList = JsonConvert.DeserializeObject<List<PINDETAILSMODEL>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                TempData["Error : "] = ex.Message;
            }
            pin.trnsferHistory = tdList;
            pin.receiveHistory = rdList;
            return View(pin);
        }




        public ActionResult Unusedpin_transferHistory()
        {
            PinTransferModel pin = new PinTransferModel();
            List<PINDETAILSMODEL> tdList = new List<PINDETAILSMODEL>();

            try
            {
                dtResult = UserManager.Pindetails("NOTUSEDPINHISTORY", "0", userInfo.memb_code, null, null, userInfo.memb_code, null);
                if (dtResult.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dtResult);
                    tdList = JsonConvert.DeserializeObject<List<PINDETAILSMODEL>>(jsonString);
                }


            }
            catch (Exception ex)
            {
                TempData["Error : "] = ex.Message;
            }
            pin.trnsferHistory = tdList;

            return View(pin);
        }

        public ActionResult Usedpin_transferHistory()
        {
            PinTransferModel pin = new PinTransferModel();
            List<PINDETAILSMODEL> tdList = new List<PINDETAILSMODEL>();

            try
            {
                dtResult = UserManager.Pindetails("USEDPINHISTORY", "0", userInfo.memb_code, null, null, userInfo.memb_code, null);
                if (dtResult.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dtResult);
                    tdList = JsonConvert.DeserializeObject<List<PINDETAILSMODEL>>(jsonString);
                }


            }
            catch (Exception ex)
            {
                TempData["Error : "] = ex.Message;
            }
            pin.trnsferHistory = tdList;

            return View(pin);
        }




        public JsonResult checkPinDetails(string pin)
        {
            string msg = "";
            try
            {
                dtResult = UserManager.Pindetails("CHECKPINTRANSFER", "0", userInfo.memb_code, pin, null, userInfo.memb_code, null);
                if (dtResult.Rows.Count > 0)
                {
                    if (dtResult.Rows[0]["tf_flag"].ToString() == "Y")
                    {
                        msg = "This Pin Already transfer.";
                    }
                    else if (dtResult.Rows[0]["u_flag"].ToString() == "Y")
                    {
                        msg = "This Pin Already Used.";
                    }
                    else
                    {
                        msg = "";
                    }
                }
                else
                {
                    msg = "This Pin is invalid";
                }
            }
            catch
            {
                msg = "This Pin is invalid";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Getpincounts(string amount)
        {
            PINDETAILSMODEL trans = new PINDETAILSMODEL();
            try
            {

                dtResult = UserManager.Pindetails("GETAVAILABLEPIN", "0", userInfo.memb_code, null, null, null, amount);
                if (dtResult.Rows.Count > 0)
                {
                    trans.Available_Pin = dtResult.Rows[0]["Available_Pin"].ToString();
                }
                else
                {
                    trans.Available_Pin = "0";
                }
            }
            catch (Exception ex)
            {
                trans.Available_Pin = "0";
                TempData["Error : "] = ex.Message;
            }

            return Json(trans, JsonRequestBehavior.AllowGet);
        }


        #endregion


        //----------------------------tree----------------------------------------------//

        public ActionResult sponsor_tree_partial(string memb_code)
        {
            UserModel user = new UserModel();
            List<UserModel> userLIST = new List<UserModel>();
            try
            {
                if (string.IsNullOrEmpty(memb_code))
                {
                    memb_code = userInfo.memb_code;
                }
                DataTable dtR = UserManager.USER_REPORT("TREEVIEWDTL", memb_code);
                if (dtR.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtR);
                    userLIST = JsonConvert.DeserializeObject<List<UserModel>>(JSONString);
                }

            }
            catch (Exception ex)
            {

            }
            return PartialView("sponsor_tree_partial", userLIST.ToList());

        }

        public ActionResult sponsor_tree()
        {
            try
            {

                DataTable dtTotal = UserManager.GetDASHBOARDData("Dashboard_Total", userInfo.memb_code);
                if (dtTotal.Rows.Count > 0)
                {
                    ViewBag.total_direct = dtTotal.Rows[0]["Downline_Total_Team"].ToString();
                }
            }
            catch
            {

            }
            return View();
        }

        //-----------------------------button----------------------------------------//

        [HttpPost]
        public ActionResult Generate_ROI_HOURS()
        {
            List<UserLevelModel> dList = new List<UserLevelModel>();
            try
            {
                string amount = "50";

                DataTable dt = UserManager.Generate_ROI_HOURS(userInfo.memb_code.ToString(), amount);
                if (dt.Rows.Count > 0)
                {
                    TempData["ROIAlert"] = dt.Rows[0]["msg"].ToString();
                    return RedirectToAction("Index", "MemberPanel");
                }
            }
            catch (Exception ex)
            {
                // Response.Write(ex.ToString());
            }
            return RedirectToAction("Index", "MemberPanel");
        }

        public ActionResult Generate_ROI_HOURS1()
        {
            List<UserLevelModel> dList = new List<UserLevelModel>();
            try
            {
                string amount = "1000";

                DataTable dt = UserManager.Generate_ROI_HOURS1(userInfo.memb_code.ToString(), amount);
                if (dt.Rows.Count > 0)
                {
                    TempData["ROIAlert1"] = dt.Rows[0]["msg"].ToString();
                    return RedirectToAction("Index", "MemberPanel");
                }
            }
            catch (Exception ex)
            {
                // Response.Write(ex.ToString());
            }
            return RedirectToAction("Index", "MemberPanel");
        }

        public ActionResult Generate_ROI_HOURS2()
        {
            List<UserLevelModel> dList = new List<UserLevelModel>();
            try
            {
                string amount = "500";

                DataTable dt = UserManager.Generate_ROI_HOURS2(userInfo.memb_code.ToString(), amount);
                if (dt.Rows.Count > 0)
                {
                    TempData["ROIAlert2"] = dt.Rows[0]["msg"].ToString();
                    return RedirectToAction("Index", "MemberPanel");
                }
            }
            catch (Exception ex)
            {
                // Response.Write(ex.ToString());
            }
            return RedirectToAction("Index", "MemberPanel");
        }




        public ActionResult purchase_package()
        {
            TransactionModel trans = new TransactionModel();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                if (dt.Rows.Count > 0)
                {

                    trans.Fund_Wallet = "$ " + dt.Rows[0]["Fund_Wallet"].ToString();
                }
                else
                {
                    trans.Main_Wallet = "0";
                    trans.Fund_Wallet = "0";
                }

                DataTable dtP = UserManager.User_Report(userInfo.memb_code, "PRINCIPLEWALLETBALANCE");
                if (dtP.Rows.Count > 0)
                {

                    trans.Principle_BalanceAmount = dtP.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.Principle_BalanceAmount = "0";
                }
                DataTable dtR = UserManager.User_Report(userInfo.memb_code, "ROIWALLETBALANCE");
                if (dtR.Rows.Count > 0)
                {

                    trans.ROI_Balance = dtR.Rows[0]["Total_Balance"].ToString();
                }
                else
                {
                    trans.ROI_Balance = "0";
                }
            }
            catch (Exception ex)
            {
                trans.ROI_Balance = "0";
                trans.Fund_Wallet = "0";
                trans.Principle_BalanceAmount = "0";
            }
            return View(trans);
        }

        [HttpPost]
        public ActionResult purchase_package(TransactionModel trans)
        {
            try
            {
                if (string.IsNullOrEmpty(trans.UserName.Trim()))
                {
                    TempData["TopUpAlert"] = "Please enter User id.";
                    return RedirectToAction("TopUp");
                }

                if (string.IsNullOrEmpty(trans.USD_Amount.Trim()))
                {
                    TempData["TopUpAlert"] = "Enter Proper Package Amount.";
                    return RedirectToAction("TopUp");
                }

                if (Convert.ToDecimal(trans.USD_Amount.Trim()) < 0)
                {
                    TempData["TopUpAlert"] = "USD Amount Should Not Be Negative.";
                    return RedirectToAction("TopUp");
                }

                //if (string.IsNullOrEmpty(trans.Password))
                //{
                //    TempData["TopUpAlert"] = "Please enter Password.";
                //    return RedirectToAction("TopUp");
                //}
                //string PASS = "";
                //if (string.IsNullOrEmpty(trans.BTC_Type))
                //{
                //    TempData["TopUpAlert"] = "First update your btc details.";
                //    return RedirectToAction("TopUp");
                //}


                //DataTable dtU = UserManager.CheckUsername(trans.UserName);
                //if (dtU.Rows.Count > 0)
                //{
                //    //USDAmount = "100";
                //    string membCode = dtU.Rows[0]["memb_code"].ToString();
                //    DataTable checkauthrised = UserManager.User_Active(membCode, "ActiveUser");
                //    if (checkauthrised.Rows.Count > 0)
                //    {
                //        if (checkauthrised.Rows[0]["authrised"].ToString() == "G")
                //        {
                //            TempData["TopUpAlert"] = "your id is Already activate.";
                //            return RedirectToAction("TopUp");
                //        }
                //    }
                //}

                //DataTable CHECKAUTHRISED1 = UserManager.User_Report1(userInfo.memb_code, "CHECKDEPOSITAMOUNT1", trans.USD_Amount);
                //if (CHECKAUTHRISED1.Rows.Count > 0)
                //{
                //    if (CHECKAUTHRISED1.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
                //    {
                //        TempData["TopUpAlert"] = "This Package is already Activate for this ID please enter other package";
                //        return RedirectToAction("Topup");
                //    }
                //}
                //DataTable CHECKAUTHRISED2 = UserManager.User_Report(userInfo.memb_code, "CHECKDEPOSITAMOUNT2");
                //if (CHECKAUTHRISED2.Rows.Count > 0)
                //{
                //    if (CHECKAUTHRISED2.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
                //    {
                //        TempData["TopUpAlert"] = "This Package is already Activate for this ID please enter other package";
                //        return RedirectToAction("Topup");
                //    }
                //}
                //DataTable CHECKAUTHRISE38 = UserManager.User_Report(userInfo.memb_code, "CHECKDEPOSITAMOUNT3");
                //if (CHECKAUTHRISE38.Rows.Count > 0)
                //{
                //    if (CHECKAUTHRISE38.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
                //    {
                //        TempData["TopUpAlert"] = "This Package is already Activate for this ID please enter other package";
                //        return RedirectToAction("Topup");
                //    }
                //}
                //DataTable CHECKAUTHRISED4 = UserManager.User_Report(userInfo.memb_code, "CHECKDEPOSITAMOUNT4");
                //if (CHECKAUTHRISED4.Rows.Count > 0)
                //{
                //    if (CHECKAUTHRISED4.Rows[0]["AMOUNT"].ToString() == trans.USD_Amount)
                //    {
                //        TempData["TopUpAlert"] = "This Package is already Activate for this ID please enter other package";
                //        return RedirectToAction("Topup");
                //    }
                //}


                //DataTable dtPASS = UserManager.User_psReport(userInfo.memb_code, "CHECKPASS");
                //if (dtPASS.Rows.Count > 0)
                //{
                //    PASS = dtPASS.Rows[0]["RV_Code"].ToString();
                //}
                //DataTable topdt = UserManager.User_Active(userInfo.memb_code, "ActiveUser");
                //if (topdt.Rows.Count > 0)
                //{
                //    TempData["TopUpAlert"] = "User All Ready Active.";
                //}
                //else
                //{
                //if (userInfo.RV_Code==trans.Password )
                //{
                //string allPackages = "100,200,300,400,500,600,700,800,900,1000";




                DataTable count = UserManager.USER_REPORT("count", userInfo.memb_code);

                string count1 = count.Rows[0]["countt"].ToString();

                if (count.Rows.Count > 0)
                {
                    if ((Convert.ToDecimal(count1) < 2) && (Convert.ToDecimal(trans.USD_Amount) == 1000))
                    {
                        TempData["TopUpAlert"] = "Please Complete Your 2 Direct.";
                        return RedirectToAction("Topup");

                    }
                    else if ((Convert.ToDecimal(count1) < 3) && (Convert.ToDecimal(trans.USD_Amount) == 500))
                    {
                        TempData["TopUpAlert"] = "Please Complete Your 3 Direct.";
                        return RedirectToAction("Topup");
                    }
                    //else if ((Convert.ToDecimal(count1) == 3) || (Convert.ToDecimal(trans.USD_Amount) == 500))
                    //{
                    //    TempData["TopUpAlert"] = "Member ID is already activate please enter another id";
                    //    return RedirectToAction("Topup");
                    //}

                    //if (((Convert.ToDecimal(count1) == 0) || (Convert.ToDecimal(count1) == 1)) && (Convert.ToDecimal(trans.USD_Amount) == 50))
                    //{
                    //    TempData["TopUpAlert"] = "Already activate this Pool. Please select next pool amount.";
                    //    return RedirectToAction("TopUp");
                    //}
                    //if (count.Rows.Count > 0)
                    //{
                    //    //if ((Convert.ToDecimal(count1) == 1) && (Convert.ToDecimal(trans.USD_Amount) == 50))
                    //    //{
                    //    //    //TempData["TopUpAlert"] = "Already activate this Pool. Please select next pool amount.";
                    //    //    //return RedirectToAction("TopUp");
                    //    //}
                    //    if ((Convert.ToDecimal(count1) <= 2) && (Convert.ToDecimal(trans.USD_Amount) == 1000))
                    //    {
                    //        TempData["TopUpAlert"] = "Please Complete Your 2 Direct.";
                    //        return RedirectToAction("Topup");

                    //    }
                    //    else if ((Convert.ToDecimal(count1) <= 3) && (Convert.ToDecimal(trans.USD_Amount) == 500))
                    //    {
                    //        TempData["TopUpAlert"] = "Please Complete Your 3 Direct.";
                    //        return RedirectToAction("Topup");
                    //    }


                    int depositeStatus = 0;
                    string allPackages = "50,500,100";
                    string[] pkg = allPackages.Split(',');
                    //for (int i = 0; i < pkg.Length; i++)
                    //{
                    //    if (string.Equals(trans.USD_Amount, pkg[i]))
                    //    {
                    //        depositeStatus = 10;
                    //        break;
                    //    }
                    //}

                    trans.BTC_Type = "FUNDWALLET";
                    //trans.USD_Amount = "250000";

                    depositeStatus = 10;

                    if (depositeStatus == 10)
                    {
                        if ((Convert.ToDecimal(trans.USD_Amount) == 50) || (Convert.ToDecimal(trans.USD_Amount) == 500) || (Convert.ToDecimal(trans.USD_Amount) == 1000))
                        //if ((Convert.ToDecimal(trans.USD_Amount) >= 10) && (Convert.ToDecimal(trans.USD_Amount) % 1 == 0))
                        {
                            DataTable dtUser = UserManager.CheckUsername(trans.UserName);
                            if (dtUser.Rows.Count > 0)
                            {
                                string membCode = dtUser.Rows[0]["memb_code"].ToString();

                                if (trans.BTC_Type == "PRINCIPLE")
                                {
                                    DataTable dt = UserManager.User_Report(userInfo.memb_code, "PRINCIPLEWALLETBALANCE");
                                    if (dt.Rows.Count > 0)
                                    {
                                        string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

                                        if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(Total_Balance))
                                        {
                                            int result = UserManager.User_TopUp("ADDTOPUP", userInfo.memb_code, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                            if (result > 0)
                                            {
                                                TempData["TopUpAlert"] = "1";

                                                string trno = "0";
                                                string sub = "Top Up";
                                                string result1 = EmailFUnd(userInfo.EmailID, sub, trans.UserName, trans.USD_Amount);
                                            }
                                            else
                                            {
                                                TempData["TopUpAlert"] = "Activation failed.";
                                            }
                                        }
                                        else
                                        {
                                            TempData["TopUpAlert"] = "You have insufficient balance in your main wallet.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["TopUpAlert"] = "0 balance in your main wallet to top up this user.";
                                    }
                                }
                                else if (trans.BTC_Type == "ROI")
                                {
                                    DataTable dt = UserManager.User_Report(userInfo.memb_code, "ROIWALLETBALANCE");
                                    if (dt.Rows.Count > 0)
                                    {
                                        string Total_Balance = dt.Rows[0]["Total_Balance"].ToString();

                                        if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(Total_Balance))
                                        {
                                            int result = UserManager.User_TopUp("ADDTOPUP", userInfo.memb_code, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                            if (result > 0)
                                            {
                                                TempData["TopUpAlert"] = "1";

                                                string trno = "0";
                                                string sub = "Top Up";
                                                string result1 = EmailFUnd(userInfo.EmailID, sub, trans.UserName, trans.USD_Amount);
                                            }
                                            else
                                            {
                                                TempData["TopUpAlert"] = "Activation failed.";
                                            }
                                        }
                                        else
                                        {
                                            TempData["TopUpAlert"] = "You have insufficient balance in your main wallet.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["TopUpAlert"] = "0 balance in your main wallet to top up this user.";
                                    }
                                }
                                else if (trans.BTC_Type == "FUNDWALLET")
                                {
                                    trans.BTC_Type = "FUNDWALLET";
                                    trans.Plan_Type = "INVESTMENT";
                                    DataTable dt = UserManager.User_Report(userInfo.memb_code, "FUNDWALLET");
                                    if (dt.Rows.Count > 0)
                                    {
                                        string total_balance = dt.Rows[0]["fund_wallet"].ToString();

                                        if (Convert.ToDecimal(trans.USD_Amount) <= Convert.ToDecimal(total_balance))
                                        {
                                            int result = UserManager.User_TopUp("ADDTOPUP", membCode, string.IsNullOrEmpty(trans.USD_Amount) ? "0" : trans.USD_Amount, null, "0", trans.BTC_Type, trans.Plan_Type, userInfo.memb_code);
                                            if (result > 0)
                                            {
                                                TempData["TopUpAlert"] = "1";
                                            }
                                            else
                                            {
                                                TempData["TopUpAlert"] = "Activation failed.";
                                            }
                                        }
                                        else
                                        {
                                            TempData["TopUpAlert"] = "You have insufficient balance in your fund wallet.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["TopUpAlert"] = "0 balance in your fund wallet to top up this user.";
                                    }
                                }

                                else
                                {
                                    TempData["TopUpAlert"] = "Please Select Wallet Type.";
                                }
                            }
                            else
                            {
                                TempData["TopUpAlert"] = "Please enter valid User id.";
                            }
                        }
                        else
                        {
                            TempData["TopUpAlert"] = "Please enter package minimum $10 and multiple $1.";
                        }
                    }
                    //}
                    //else
                    //{
                    //    TempData["TopUpAlert"] = "Please Enter Valid Transaction Password.";
                    //}
                    //}
                }

                else
                {
                    TempData["TopUpAlert"] = "Please TopUp.";
                }



            }
            catch (Exception ex)
            {
                TempData["TopUpAlert"] = "Activation failed.";
            }
            return RedirectToAction("TopUp");
        }


        public ActionResult purchase_package_history()
        {
            List<DepositeModel> dList = new List<DepositeModel>();
            try
            {
                DataTable dtTrans = UserManager.User_TopUp_History("TOPUPHISTORY", userInfo.memb_code);
                if (dtTrans.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtTrans);
                    dList = JsonConvert.DeserializeObject<List<DepositeModel>>(JSONString);
                }
            }
            catch (Exception ex)
            {

            }
            return View(dList.ToList());
        }


        public ActionResult all_transaction()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.USER_REPORT("DIRECT", userInfo.memb_code);
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

   
    public ActionResult GetRentalIncome (string id)
        {
           if (!string.IsNullOrEmpty(id))
            {
                if (id.Equals("1"))
                {
                    ViewBag.plantype = "Basic";
                }
            else if (id.Equals("2"))
                {
                    ViewBag.plantype = "Business";
                }
                else if (id.Equals("3"))
                {
                    ViewBag.plantype = "Business Plus";
                }
            }
            else
            {
                ViewBag.plantype = "Invalid Plan";
            }

            return View();
        }

        public ActionResult Rental_Income()
        {
            List<WalletModel> dList = new List<WalletModel>();
            try
            {
                DataTable dt = UserManager.User_Report(userInfo.memb_code, "ROI");
                if (dt.Rows.Count > 0)
                {
                    string jsonString = JsonConvert.SerializeObject(dt);
                    dList = JsonConvert.DeserializeObject<List<WalletModel>>(jsonString);
                }
            }
            catch
            {

            }
            return View(dList.ToList());
        }

        public ActionResult GetRentalIncomeData(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                int package;
                GetRentalJsonData  rentData = new GetRentalJsonData();

                try
                {
                    if (id.Equals("Basic"))
                    {
                        package = 50;

                        DataTable dt = UserManager.Generate_Rental_Income("NextIncomeDate", userInfo.memb_code, package.ToString());
                        if (dt.Rows.Count > 0)
                        {

                            rentData.NEXTDATE = dt.Rows[0]["NEXTDATE"].ToString();
                            rentData.CURRENTWEEK = dt.Rows[0]["CURRENTWEEK"].ToString();
                            return Json(rentData, JsonRequestBehavior.AllowGet);


                        }
                    }
                    else if (id.Equals("Business"))
                    {
                        package = 300;
                        DataTable dt = UserManager.Generate_Rental_Income("NextIncomeDate", userInfo.memb_code, package.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            rentData.NEXTDATE = dt.Rows[0]["NEXTDATE"].ToString();
                            rentData.CURRENTWEEK = dt.Rows[0]["CURRENTWEEK"].ToString();
                            return Json(rentData, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else if (id.Equals("Business Plus"))
                    {
                        package = 600;
                        DataTable dt = UserManager.Generate_Rental_Income("NextIncomeDate", userInfo.memb_code, package.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            rentData.NEXTDATE = dt.Rows[0]["NEXTDATE"].ToString();
                            rentData.CURRENTWEEK = dt.Rows[0]["CURRENTWEEK"].ToString();
                            return Json(rentData, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("404", JsonRequestBehavior.AllowGet);
                    }
                } catch (Exception ex) {

                    return Json("404", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("404", JsonRequestBehavior.AllowGet);
            }

            return Json("404", JsonRequestBehavior.AllowGet);
        }


        public ActionResult SetRentalIncomeData(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                int package;
                SetRentalJsonData rentData = new SetRentalJsonData();

                try
                {
                    if (id.Equals("Basic"))
                    {
                        package = 50;

                        DataTable dt = UserManager.Generate_Rental_Income("RentalIncomeInsert", userInfo.memb_code, package.ToString());
                        if (dt.Rows.Count > 0)
                        {

                            rentData.STATUS = dt.Rows[0]["STATUS"].ToString();
                            rentData.AMOUNT = dt.Rows[0]["AMOUNT"].ToString();
                            rentData.WeekNo = dt.Rows[0]["WeekNo"].ToString();
                            return Json(rentData, JsonRequestBehavior.AllowGet);


                        }
                    }
                    else if (id.Equals("Business"))
                    {
                        package = 300;
                        DataTable dt = UserManager.Generate_Rental_Income("RentalIncomeInsert", userInfo.memb_code, package.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            rentData.STATUS = dt.Rows[0]["STATUS"].ToString();
                            rentData.AMOUNT = dt.Rows[0]["AMOUNT"].ToString();
                            rentData.WeekNo = dt.Rows[0]["WeekNo"].ToString();
                            return Json(rentData, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else if (id.Equals("Business Plus"))
                    {
                        package = 600;
                        DataTable dt = UserManager.Generate_Rental_Income("RentalIncomeInsert", userInfo.memb_code, package.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            rentData.STATUS = dt.Rows[0]["STATUS"].ToString();
                            rentData.AMOUNT = dt.Rows[0]["AMOUNT"].ToString();
                            rentData.WeekNo = dt.Rows[0]["WeekNo"].ToString();
                            return Json(rentData, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json("404", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {

                    return Json("404", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("404", JsonRequestBehavior.AllowGet);
            }

            return Json("404", JsonRequestBehavior.AllowGet);
        }
    }
}

        