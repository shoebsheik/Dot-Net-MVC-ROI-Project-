using UniqueTrade_App.Models;
using UniqueTrade_App.CommanFunction;
using UniqueTrade_App.DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Drawing;
using System.IO;
using System.Globalization;

namespace UniqueTrade_App.Controllers
{
    [HandleError]
    public class HomeController : BasePage
    {
        DataTable dtResult = new DataTable();

        public ActionResult Index()
        {
            ContactEnquiry con = new ContactEnquiry();
            List<NewsModel> newList = new List<NewsModel>();
            try
            {

                dtResult = UserManager.GetLatestFranchisee();
                if (dtResult.Rows.Count > 0)
                {
                    string JSONString = JsonConvert.SerializeObject(dtResult);
                    newList = JsonConvert.DeserializeObject<List<NewsModel>>(JSONString);
                }
                con.Newslist = newList;

                DataTable dt = UserManager.User_Report("0", "GETFRONTPOPUP");
                if (dt.Rows.Count > 0)
                {
                    con.Popup = dt.Rows[0]["popup"].ToString();
                    con.Pmode = dt.Rows[0]["Pmode"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return View(con);
        }

        public ActionResult aboutus()
        {
            return View();
        }

        public ActionResult opportunity()
        {
            return View();
        }

        public ActionResult blog()
        {
            return View();
        }
        public ActionResult faq()
        {
            return View();
        }
        public ActionResult Plan()
        {
            return View();
        }
        public ActionResult tradinggg()
        {
            return View();
        }


        //----------------------- Sign Up --------------------//

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

        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();
        }

        public FileResult GetCaptchaImage(string id)
        {
            string text = GetRandomText();
            if (string.Equals(id, "R"))
            {
                Session["CAPTCHASignUp"] = text;
            }
            else if (string.Equals(id, "L"))
            {
                Session["CAPTCHASignIn"] = text;
            }
            else if (string.Equals(id, "S"))
            {
                Session["CAPTCHASubscribe"] = text;
            }
            else if (string.Equals(id, "C"))
            {
                Session["CAPTCHAContact"] = text;
            }

            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            Font font = new Font("Arial", 15);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
            drawing = Graphics.FromImage(img);

            Color backColor = Color.SeaShell;
            Color textColor = Color.Red;
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 20, 10);

            drawing.Save();

            font.Dispose();
            textBrush.Dispose();
            drawing.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }

        public JsonResult SponcerDetails(string email)
        {
            SponsorDetailsModel user = new SponsorDetailsModel();
            try
            {
                DataTable dt = UserManager.GetUserLogin(email);
                if (dt.Rows.Count > 0)
                {
                    user.Memb_Name = dt.Rows[0]["Memb_Name"].ToString();
                }
                else
                {
                    user.Memb_Name = "";
                }
            }
            catch
            {
                user.Memb_Name = "";
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        //public class CaptchaResponse
        //{
        //    [JsonProperty("success")]
        //    public string Success { get; set; }

        //    [JsonProperty("error-codes")]
        //    public List<string> ErrorCodes { get; set; }
        //}

        public ActionResult signup(string REGUSER, string Post)
        {
            // Initialize some data for dropdowns or other purposes
            getCountries();
            getCountryCodes();

            // Create a RegistrationModel object
            RegistrationModel user = new RegistrationModel();
            try
            {
                // Set a default value for the 'Place' property
                user.Place = "L";
                // Check if user information is available
                if (userInfo != null)
                {
                    user.sp_user = userInfo.username;
                    user.MembName_L = userInfo.memb_name;

                    // If 'Post' parameter is not empty, set 'Place' to its value
                    if (!string.IsNullOrEmpty(Post))
                    {
                        user.Place = Post;
                    }
                }
                else
                {
                    // If 'REGUSER' parameter is not empty
                    if (!string.IsNullOrEmpty(REGUSER))
                    {
                        //REGUSER = Crypto.Decrypt(REGUSER, System.Text.Encoding.Unicode);
                        // Retrieve user details from UserManager based on 'REGUSER'
                        DataTable dt = UserManager.GetUserDetailsByMemb_Code(REGUSER);
                        if (dt.Rows.Count > 0)
                        {
                            // Populate user properties from retrieved data
                            user.sp_user = dt.Rows[0]["username"].ToString();
                            user.MembName_L = dt.Rows[0]["Memb_Name"].ToString();

                            // If 'Post' parameter is not empty, set 'Place' to its value
                            if (!string.IsNullOrEmpty(Post))
                            {
                                user.Place = Post;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Set 'Place' to a default value if an exception occurs
                user.Place = "L";
            }
            // Return a view with the populated user object
            return View(user);
        }

        [HttpPost]
        public ActionResult signup(RegistrationModel user)
        {       //var response = Request["g-recaptcha-response"];
                ////const string secret = "6LdSmqwUAAAAADbPHlkFxk8ySECDX2mqTDej13TD";
                //const string secret = "6Lc8isoUAAAAAJr80U6-lue3DsPBxcwNCbsLstFR";

            ////const string secret = "6LdSmqwUAAAAADbPHlkFxk8ySECDX2mqTDej13TD"; 
            //var client = new WebClient();

            //var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            //var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //if (captchaResponse.Success == null)
            //{
            //    if (captchaResponse.ErrorCodes != null)
            //    {

            //        var error = captchaResponse.ErrorCodes[0].ToLower();
            //        switch (error)
            //        {
            //            case ("missing-input-secret"):
            //                TempData["SignUpAlert"] = "The secret parameter is missing.";
            //                break;
            //            case ("invalid-input-secret"):
            //                TempData["SignUpAlert"] = "The secret parameter is invalid or malformed.";
            //                break;
            //            case ("missing-input-response"):
            //                TempData["SignUpAlert"] = "The response parameter is missing.";
            //                break;
            //            case ("invalid-input-response"):
            //                TempData["SignUpAlert"] = "The response parameter is invalid or malformed.";
            //                break;
            //            default:
            //                TempData["SignUpAlert"] = "Error occured. Please try again";
            //                break;
            //        }
            //        return RedirectToAction("signup", "Home");
            //    }
            //}
            //else
            //{
            try
            {
                // Validate that password and confirm password match
                if (!string.Equals(user.mpwd, user.ConfirmPass))
                {
                    TempData["SignUpAlert"] = "Password And Confirm Password Must Be Same.";
                    return RedirectToAction("signup", "Home");
                }

                // Check if sponsor user ID is provided
                if (string.IsNullOrEmpty(user.sp_user.Trim()))
                {
                    TempData["SignUpAlert"] = "Please Enter Sponsor User Id.";
                    return RedirectToAction("signup", "Home");
                }

                //if (string.IsNullOrEmpty(user.Place))
                //{
                //    TempData["SignUpAlert"] = "Please Select Position.";
                //    return RedirectToAction("signup", "Home");
                //}

                // More validation checks for other form fields...
                
                if (string.IsNullOrEmpty(user.Memb_Name.Trim()))
                {
                    TempData["SignUpAlert"] = "Please Enter Name.";
                    return RedirectToAction("signup", "Home");
                }

                if (string.IsNullOrEmpty(user.EMail.Trim()))
                {
                    TempData["SignUpAlert"] = "Please Enter Email Id.";
                    return RedirectToAction("signup", "Home");
                }

                if (string.IsNullOrEmpty(user.Mobile_No.Trim()))
                {
                    TempData["SignUpAlert"] = "Please Enter Mobile No.";
                    return RedirectToAction("signup", "Home");
                }

                // Set default values for certain properties
                string client_ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (client_ip == "" || client_ip == null)
                {
                    client_ip = Request.ServerVariables["REMOTE_ADDR"];
                }
                string SPID = null;
                string PLACE = null;
                string PLAN_TYPE = null;

                SPID = user.sp_user;
                PLACE = "L";
                PLAN_TYPE = "BINARY";
                // Additional logic to set values for SPID, PLACE, PLAN_TYPE...

                // More logic to handle registration process using UserManager.REGISTRATION method

                // Check the result of the registration process
                if (string.IsNullOrEmpty(SPID))
                {
                    SPID = "TG000000";
                }


                //if(string.Equals(SPID, "CS000000"))
                //{
                //    SPID = "MULTITRADEDNDAD";
                //}


                if (!string.IsNullOrEmpty(user.Place))
                {
                    if (user.Place == "L" || user.Place == "R")
                        PLACE = user.Place;
                }

                string MODE = "REGISTRATION";

                string USERNAME = "";

                int sta = 0;
                while (sta < 1)
                {
                    USERNAME = "RB" + getRandomPassword();
                    DataTable dtUser = UserManager.CheckUsername(USERNAME);
                    if (dtUser.Rows.Count == 0)
                    {
                        sta = 1;
                    }
                }

                string MPWD = user.mpwd;
                string MembName_F = null;
                string MembName_M = user.CountryCode;
                string MembName_L = null;
                string Memb_Name = user.Memb_Name.Trim();
                string Gender = null;
                string Mobile_No = user.Mobile_No;
                string Phone_No = user.Aadhar_No;
                string EMail = user.EMail;
                string Address1 = null;
                string Address2 = null;
                string M_COUNTRY = user.Country;
                string State = null;
                string District = null;
                string City = null;
                string Pin_Code = null;
                string Reg_Amt = "10";
                string RV_Code = user.mpwd;
                string PIN_ID = null;
                string pin_no = null;
                string REMARK = null;
                string ac_name = null;
                string ac_no = null;
                string bk_name = null;
                string bk_branch = null;
                string bk_ifsc = null;
                string bk_card_no = "0";
                string btc_ac = null;
                ac_name = null;



                DataTable dtResult = UserManager.REGISTRATION(MODE, USERNAME, MPWD, SPID, PLACE, PLAN_TYPE
                        , MembName_F, MembName_M, MembName_L, Memb_Name, Gender, Mobile_No
                        , Phone_No, EMail, Address1, Address2, M_COUNTRY, State
                        , District, City, Pin_Code, Reg_Amt, RV_Code, PIN_ID
                        , pin_no, REMARK, client_ip, ac_name, ac_no, bk_name
                        , bk_branch, bk_ifsc, bk_card_no, btc_ac);

                // Check the result of the registration process
                if (dtResult.Rows.Count > 0)
                {
                    if (string.Equals(dtResult.Rows[0]["SP_STATUS"].ToString(), "SUCCESS"))
                    {
                        // Logic for sending a welcome email to the user

                        string encryptEmail = Crypto.Encrypt(USERNAME, System.Text.Encoding.Unicode);

                        string ToMail, CCMail, FromMail, Subject, Enquirer;
                        string Body = string.Empty;
                        Enquirer = EMail;
                        CCMail = "";
                        FromMail = "";
                        Subject = user.Memb_Name + " - WELCOME Royal Blocks";

                        string website = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);
                        string imgpath = website+ "/Content/front/images/logo.png";

                        string login = website + "/home/signin";

                        

                        Body = "<html>"
                             + "<head>"
                             + "<meta http - equiv = 'Content-Type' content = ''text/html; charset=UTF-8'>"
                             + "<meta name='viewport' content='width = device - width, initial - scale = 1.0'>"
                             + "<title>Royal Blocks : Email</title>"
                             + "</head>"
                             + "<body style='font-family: Arial, Helvetica, sans-serif; '>"
                             + "<table align='center' width='650' border='0' cellpadding='0' cellspacing='0'style='padding: 15px;background-color:rgb(240, 240, 240);'>"
                             + "<tbody>"
                             + "<tr>"
                             + "<td>"
                             + "<table align = 'center'>"
                             + "<tbody style='box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>"

                             + "<tr>"
                             + "<td style='text-align: center;'>"
                             + "<img src='" + imgpath + "' alt=' Royal Blocks' style='position: absolute;top:70px;width:160px;text-align: center;'>"
                             + "</td>"
                             + "</tr>"
                             + "<tr>"
                             + "<td>"
                             + "<table align = 'center'>"
                             + "<tbody>"
                             + "<td>"
                             + "<h2><strong>Congratulations</strong></h2>"
                             + "</td>"
                             + "</tr>"
                             + "<tr>"
                             + "<td style = 'padding:10px 0 20px 0;font-size: 13px;line-height: 18px;' >"
                             + "<p style='text-align:left'>  Dear <strong>" + user.Memb_Name + "</strong>, Congratulation You have Successfully registered With  Royal Blocks. Now You Can Login to Our website your login details are as follows: </p>"
                             + "</td>"
                             + "</tr>"
                             + "<tr>"
                             + "<td style = 'padding:5px 0;border-bottom:1px solid gray;' >"
                             + "</td>"
                             + "</tr>"
                             + "<tr>"
                             + "<td style = 'padding:20px 0 20px 0;font-size: 13px;line-height: 18px;' >"
                             + "<strong> Here's your login information:</strong>"
                             + "</td>"
                             + "</tr>"

                             + "<tr>"
                             + "<td>"
                             + "<table>"

                             + "<tr>"
                             + "<td style = 'font-size: 12px;line-height: 18px;color: grey;'><b> USERNAME :</b >"
                             + "</td>"
                             + "<td style = 'font-size: 12px;line-height: 18px;padding-left: 25px;'>" + USERNAME
                             + " </td>"
                             + "</tr>"
                             + "<tr>"
                             + "<td style = 'font-size: 12px;line-height: 18px;color: grey;'><b> PASSWORD :</b>"
                             + "</td>"
                             + "<td style = 'font-size: 12px;line-height: 18px;padding-left: 25px;'> " + MPWD
                             + "</td>"
                             + "</tr>"
                             + "</table>"
                             + "</td>"
                             + "</tr>"

                             + "<tr>"
                             + "<td><a href = '"+ login + "'/Home/UserLogin' targrt = '_blank'>"
                             + "<button style = 'background-color: rgb(16, 187, 16);color: white;border: 0px;padding: 10px;border-radius: 5px;margin-top: 25px;cursor: pointer;'><b> Log In Now"
                             + "</b></button>"
                             + "</a>"
                             + "</td>"
                             + "</tr>"
                             + "<tr>"
                             + "<td style = 'padding:5px 0;border-bottom:1px solid gray;'>"
                             + "</td>"
                             + "</tr>"
                             + "<tr>"
                             + "<td style = 'padding:20px 0 20px 0;font-size: 13px;line-height: 18px;'>"
                             + "Thanks for choosing  Royal Blocks.<br>"
                             + "-To visit our website"
                             + "<a href = '"+ website + "'> Royal Blocks </a>"
                             + "</td>"
                             + "</tr>"
                             + "</tbody>"
                             + "</table>"
                             + "</td>"
                             + "</tr>"
                             + "</tbody>"
                             + "</table>"

                             + "</tbody>"
                             + "</table>"
                             + "</body>"
                             + "<html>";


                        string Status = SendMail.mailMain(Enquirer, Body, Subject);

                        //   string messagecontent = "You have Successfully Registered with Royal Blocks " + "\r\n" + "USER ID : " + USERNAME + "\r\n" + "Password : " + MPWD + " for more details visit us on https://Royal Blocks.co/";

                        //  string smsStatus = SmsHelper.SendSMS(user.Mobile_No.Trim(), messagecontent);

                        // Logic for sending a welcome email to the user

                        TempData["SignUpEmailAlert"] = "Registration Successfull. Please Check Your Email.";

                        Session["VerifyEmail"] = "R";
                        Session["VerifyEMAILID"] = user.EMail;
                        Session["VerifySPID"] = USERNAME;

                        Session["Regmemb_name"] = Memb_Name;
                        Session["Regusername"] = USERNAME;
                        Session["Regmpwd"] = MPWD;
                        // Redirect to the "success" action in the "Home" controller
                        return RedirectToAction("success", "Home");
                    }
                    else
                    {
                        TempData["SignUpAlert"] = dtResult.Rows[0]["SP_STATUS"].ToString();
                    }
                }
                else
                {
                    TempData["SignUpAlert"] = "Registration Failed.";
                }

                // Set TempData values for use in the view
                TempData["SignUpEmail"] = user.EMail;
                TempData["SignUpSponcer"] = user.sp_user;
            }
            //}
            catch (Exception ex)
            {
                TempData["SignUpAlert"] = "Registration Failed.";
            }
            // Redirect to the "signup" action in the "Home" controller
            return RedirectToAction("signup", "Home");
        }

        public ActionResult success()
        {
            UserModel user = new UserModel();
            try
            {
                if (Session["VerifyEMAILID"] != null)
                    user.EMail = Session["VerifyEMAILID"].ToString();

                if (Session["VerifyEmail"] != null)
                    user.status = Session["VerifyEmail"].ToString();

                if (Session["VerifySPID"] != null)
                    user.sp_user = Session["VerifySPID"].ToString();

                if (Session["Regusername"] != null)
                    user.username = Session["Regusername"].ToString();

                if (Session["Regmemb_name"] != null)
                    user.Memb_Name = Session["Regmemb_name"].ToString();

                if (Session["Regmpwd"] != null)
                    user.mpwd = Session["Regmpwd"].ToString();
            }
            catch (Exception ex)
            {

            }
            return View(user);
        }


        //----------------------- Sign In --------------------//
        public ActionResult signin(string email, string password)
        {
            try
            {


                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    string Domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
                    TempData["LoginAlert"] = null;
                    password = Crypto.Decrypt(password, System.Text.Encoding.Unicode);
                    UserInfo userInfo = new UserInfo(email, password, "1", "U");  //CommonFunction => UserInfo.cs public class UserInfo
                    if (String.IsNullOrEmpty(userInfo.UserID))
                    {
                        TempData["LoginAlert"] = "Invalid UserId or Password";
                    }
                    else
                    {
                        if (!userInfo.IsAuthenticated)
                        {
                            TempData["LoginAlert"] = "Invalid Password"; 
                        }
                        else if (userInfo.IsAuthenticated)
                        {
                            Common.CurrentUserInfo = userInfo;  //Common.cs   public static UserInfo CurrentUserInfo  line no - 83
                            Common.CookieUserID = userInfo.EmailID;  //UserInfo.cs  public string EmailID = "";  line no - 20
                            Common.CookieUserType = userInfo.userType; //UserInfo.cs  public string userType = "";  line no - 21 

                            return RedirectToAction("Index", "MemberPanel");
                        }
                    }
                }

                if (Common.CurrentUserInfo == null)
                {
                    Common.CurrentUserInfo = null;
                    Common.CookieUserID = null;
                    Common.CookieUserType = null;
                }
                else
                {
                    UserInfo u = new UserInfo(Common.CookieUserID, Common.CookieUserType);
                    return RedirectToAction("Index", "MemberPanel");
                }
            }
            catch (Exception ex)
            {
                TempData["LoginAlert"] = "Login failed!";
            }
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult signin(SignInModel user, FormCollection form)
        {
            try
            {
                //if (string.IsNullOrEmpty(user.Request_Code))
                //{
                //    TempData["LoginAlert"] = "Please enter request code.";
                //    return RedirectToAction("signin");
                //}

                //if (Session["LoginRequestCode"] == null)
                //{
                //    TempData["LoginAlert"] = "Your request code is expired.";
                //    return RedirectToAction("signin");
                //}

                //if (!string.Equals(user.Request_Code, Session["LoginRequestCode"].ToString()))
                //{
                //    TempData["LoginAlert"] = "Your request code is invalid.";
                //    return RedirectToAction("signin");
                //}

                //string clientCaptcha = form["clientCaptcha"].ToUpper();
                //string serverCaptcha = Session["CAPTCHASignIn"].ToString().ToUpper();

                //if (!clientCaptcha.Equals(serverCaptcha))
                //{
                //    ViewBag.ShowCAPTCHA = serverCaptcha;
                //    TempData["LoginAlert"] = "Sorry, please write exact text as written above.";
                //    return RedirectToAction("signin");
                //}

                string Domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
                TempData["LoginAlert"] = null;

                UserInfo userInfo = new UserInfo(user.username, user.mpwd, "1", "U");
                if (String.IsNullOrEmpty(userInfo.UserID))
                {
                    TempData["LoginAlert"] = "Invalid UserId or Password";
                }
                else
                {
                    if (!userInfo.IsAuthenticated)
                    {
                        TempData["LoginAlert"] = "Invalid Password";
                    }
                    else if (userInfo.IsAuthenticated)
                    {
                        Common.CurrentUserInfo = userInfo;
                        Common.CookieUserID = userInfo.username;
                        Common.CookieUserType = userInfo.userType;

                        string client_ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (client_ip == "" || client_ip == null)
                        {
                            client_ip = Request.ServerVariables["REMOTE_ADDR"];
                        }
                        DataTable dtl = UserManager.User_LOGIN_History("LoginHis", userInfo.username, userInfo.memb_name, userInfo.memb_code, client_ip);

                        //  int result = UserManager.Generate_ROI(userInfo.memb_code);

                        Session["LoginRequestCode"] = null;

                        return RedirectToAction("Index", "MemberPanel");
                    }
                }
                return RedirectToAction("signin", "Home");
            }
            catch (Exception ex)
            {
                Common.CurrentUserInfo = null;
                Common.CookieUserID = null;
                Common.CookieUserType = null;
                TempData["LoginAlert"] = "Login failed!";
                return RedirectToAction("signin", "Home");
            }
        }


        public ActionResult Logout()
        {
            Common.CurrentUserInfo = null;
            Common.CookieUserID = null;
            Common.CookieUserType = null;

            return RedirectToAction("Index", "Home");
        }

        //----------------------- Forget Password --------------------//
        public ActionResult resetpassword()
        {
            try
            {
                if (Common.CurrentUserInfo == null)
                {
                    Common.CurrentUserInfo = null;
                    Common.CookieUserID = null;
                    Common.CookieUserType = null;
                }
                else
                {
                    UserInfo u = new UserInfo(Common.CookieUserID, Common.CookieUserType);
                    return RedirectToAction("Index", "MemberPanel");
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpPost]
        public ActionResult resetpassword(ForgetPasswordModel forget)
        {
            try
            {

                DataTable dt = UserManager.GetUserByEmail(forget.username);
                if (dt.Rows.Count > 0)
                {
                    string USERNAME = dt.Rows[0]["USERNAME"].ToString();
                    string MPWD = dt.Rows[0]["MPWD"].ToString();
                    string MobileNo = dt.Rows[0]["Mobile_No"].ToString();
                    string Memb_name = dt.Rows[0]["Memb_Name"].ToString();

                    string emailEncrypt = Crypto.Encrypt(dt.Rows[0]["USERNAME"].ToString(), System.Text.Encoding.Unicode);
                    string ToMail, CCMail, FromMail, Subject, Enquirer;
                    string Body = string.Empty;
                    Enquirer = dt.Rows[0]["EMail"].ToString();
                    CCMail = "";
                    FromMail = "";

                    Subject = "RESET PASSWORD - Royal Blocks";


                    Body = "<html>"
                            + "<head>"
                            + "<title></title>"
                            + "</head>"
                            + "<body>"
                            + "<header></header>"
                            + "<div style='width:50%;margin:0 auto;font-family:Arial'>"
                            + "<div style='height:auto;background:#F1F1F1;'>"
                            + "<div style='display:flex;padding:0px 30px'>"
                            + "<div style='width:80%'>"
                            + "<div style='width:200px;height:auto'>"
                            + "<img src='https://Royal Blocks.co/Content/Dash/assets/img/logo.png' alt='EzyBitz' style='width:100%'>"
                            + "</div>"
                            + "</div>"
                            + "<div style='width:30%'>"
                            + "<ul style='display:flex;list-style-type:none'></ul>"
                            + "</div>"
                            + "</div>"
                            + "</div>"
                            + "<div style='background:#000;padding:10px;margin:10px 0'>"
                            + "</div>"
                            + "<h2 style='text-align:center;font-weight:bold'>Welcome to Royal Blocks</h2>"
                            + "<div style='height:auto;background:#F1F1F1;font-weight:normal'>"
                            + "<div style='padding:30px'>"
                            + "<h2 style='text-align:center;font-weight:bold'>Welcome to Royal Blocks</h2>"
                            + "<p style='text-align:center'>  Dear " + Memb_name + ", Congratulation You have Successfully registered With Royal Blocks. Now You Can Login to Our website your login details are as follows: </p>"
                            + "<p style='padding:10px;color:#000;!important;font-weight:bold;text-align:center'>"
                            + "USERNAME:"
                            + "<span> " + USERNAME + " </span>"
                            + "</p>"
                            + "<p style='padding:10px;color:#000;!important;font-weight:bold;text-align:center'>"
                            + "PASSWORD:"
                            + "<span>" + MPWD + "</span>"
                            + "</p>"
                            + "<p style='color:#000!important;text-align:center'>"
                            + "For any queries, Please Contact Our Customer Support team at:"
                            + "</p>"
                            + "<p style='color:#000!important;text-align:center'>"
                            + "support@Royal Blocks.co"
                            + "</p>"
                            + "<p style='color:#000!important'>"
                            + "<strong>Regards,</strong>"
                            + "</p>"
                            + "<p style='color:#000!important'>"
                            + "<strong>Team DND Multitrade Ad</strong>"
                            + "</p>"
                            + "<div style='padding:2px;background:#f9b707'>"
                            + "</div>"
                            + "<h4 style='color:#000!important'>"
                            + "Please do not reply to this email. Please contact our member support on support@Royal Blocks.co for any assistance"
                            + "</h4>"
                            + "</div>"
                            + "</div>"
                            + "</div>"
                            + "</body>"
                            + "</html>";

                    //Body = "<html xmlns='http://www.w3.org/1999/xhtml'>"
                    //    + "<head>"
                    //    + "<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />"
                    //    + "<title>Reset Password</title>"
                    //    + "</head>"
                    //    + "<body topmargin='0' rightmargin='0' bottommargin='0' leftmargin='0' marginwidth='0' marginheight='0' width='100%' style='background-image:url(" + WebConfigurationManager.AppSettings["URL"] + "/Content/Front/image/bg3.jpg); background-repeat:no-repeat; background-size:cover;' >"
                    //    + "<div style='border-collapse: collapse; border-spacing: 0; margin: 0; padding: 0; width: 100%; height: 100%; -webkit-font-smoothing: antialiased; text-size-adjust: 100%; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; line-height: 100%;padding: 100px 0px;'>"
                    //    + "<div style='width:400px; margin: auto; border:1px solid #ccc; background-color:aliceblue; padding:0;'>"
                    //    + "<img src='" + WebConfigurationManager.AppSettings["URL"] + "/Content/Front/image/pic2.png'  width='50%' height='200px' style='top:0; margin:auto;display:block'>"
                    //    + "<center>"
                    //    + "<h2 style='color:#000;'>Email Conformation</h2>"
                    //    + "<p style='font-size:16px; text-align:center; padding:0 20px 0 20px; color:#000;'>Dear "+ Memb_name + " Congratulations  you have successfully registered with DND Multitrade Ad, Now you can Login to Our Website your Login details are as follows:</p>"
                    //    //+ "<input type='button' name='btn' value='Verify Email Address' style='padding:10px; background-color:#c64c27;'>"
                    //    + "<h2 style='font-size:20px'>USERNAME: " + USERNAME + "</h2>"
                    //    + "<h2 style='font-size:20px'>PASSWORD: " + MPWD + "</h2>"
                    //    + "</center>"
                    //    + "<p style='padding:0 20px; margin-top:50px; text-align:left; font-style:italic; color:#000;'>Note:<br>Using our , For any queries, Please Contact Our Customer Support Team ats part of our security policy.</p>"
                    //    + "</div>"
                    //    + "</body>"
                    //    + "</html>";

                    string Status = SendMail.mailMain(Enquirer, Body, Subject);

                    string messagecontent = "You have Successfully Registered with Royal Blocks" + "\r\n" + "USER ID : " + USERNAME + "\r\n" + "Password : " + MPWD + " for more details visit us on https://Royal Blocks.co/";

                    // string smsStatus = SmsHelper.SendSMS(MobileNo.Trim(), messagecontent);

                    TempData["ForgetPassAlert"] = "Password Reset Successfully. Please Check Your Email.";
                }
                else
                {
                    TempData["ForgetPassAlert"] = "Please Enter Valid User Id.";
                }
            }
            catch (Exception ex)
            {
                TempData["ForgetPassAlert"] = "Password Reset Failed.";
            }
            return RedirectToAction("resetpassword", "Home");
        }



        //--------------------- Contact Enquiry -----------//
        public ActionResult contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult contact(ContactEnquiry contact)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ContactAlert"] = "Please enter valid data.";
                    return RedirectToAction("contact", "Home");
                }

                if (!string.IsNullOrEmpty(contact.username.Trim()) && !string.IsNullOrEmpty(contact.email.Trim()) && !string.IsNullOrEmpty(contact.message.Trim()))
                {
                    contact.message = contact.message.Trim().Replace("\r\n", "<br/>");
                    int result = UserManager.SP_Contact_Outer("CONTACT", contact.username.Trim(), contact.email.Trim(), contact.phone_no, contact.subject, contact.message.Trim());
                    if (result > 0)
                    {
                        TempData["ContactAlert"] = "Enquiry Submitted Successfully.";
                    }
                    else
                    {
                        TempData["ContactAlert"] = "Enquiry Submitted Failed.";
                    }
                }
                else
                {
                    TempData["ContactAlert"] = "Enquiry Submitted Failed.";
                }
            }
            catch (Exception ex)
            {
                TempData["ContactAlert"] = "Enquiry Submitted Failed.";
            }
            return RedirectToAction("contact", "Home");
        }



        //--------------------- Newsletter -----------//
        public ActionResult newsLetter()
        {
            string actionName = string.Empty;
            try
            {
                actionName = Common.CurrentPageName;
            }
            catch
            {

            }
            ViewBag.PageNameNewsLetter = actionName;
            return PartialView("newsLetter");
        }

        [HttpPost]
        public ActionResult newsLetterPost(NewsLetter contact, string pageNewsLetter)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["NewsLetterAlert"] = "Please enter valid data.";
                    if (string.IsNullOrEmpty(pageNewsLetter))
                        return RedirectToAction("Index", "Home");
                    else
                        return RedirectToAction(pageNewsLetter, "Home");
                }

                if (!string.IsNullOrEmpty(contact.email.Trim()))
                {
                    int result = UserManager.SP_Contact_Outer("NEWSLETTER", null, contact.email.Trim(), null, null, null);
                    if (result > 0)
                    {
                        TempData["NewsLetterAlert"] = "News Letter Submitted Successfully.";
                    }
                    else
                    {
                        TempData["NewsLetterAlert"] = "News Letter Submitted Failed.";
                    }
                }
                else
                {
                    TempData["NewsLetterAlert"] = "News Letter Submitted Failed.";
                }
            }
            catch (Exception ex)
            {
                TempData["NewsLetterAlert"] = "News Letter Submitted Failed.";
            }

            if (string.IsNullOrEmpty(pageNewsLetter))
                return RedirectToAction("Index", "Home");
            else
                return RedirectToAction(pageNewsLetter, "Home");
        }


        public ActionResult binaryclosing(string secretkey)
        {
            string msg = "False";
            try
            {
                if (string.Equals(secretkey, "p1o2i3u4y5t6r7e8w9q0"))
                {
                    int result = UserManager.BINARY_CLOSING();
                    if (result > 0)
                    {
                        msg = "True";
                    }
                    else
                    {
                        msg = "False";
                    }
                }
                else
                {
                    msg = "False";
                }
            }
            catch
            {
                msg = "False";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BOOSTORclosing(string secretkey)
        {
            string msg = "False";
            try
            {
                if (string.Equals(secretkey, "p1o2i3u4y5t6r7e8w9q0"))
                {
                    int result = UserManager.BOOSTER_CLOSING();
                    if (result > 0)
                    {
                        msg = "True";
                    }
                    else
                    {
                        msg = "False";
                    }
                }
                else
                {
                    msg = "False";
                }
            }
            catch
            {
                msg = "False";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getRequestCode(string username)
        {
            string returnMSG = string.Empty;
            string requestCode = string.Empty;
            try
            {
                DataTable dt = UserManager.CheckUsername(username);
                if (dt.Rows.Count > 0)
                {
                    string Mobile_No = dt.Rows[0]["Mobile_No"].ToString();
                    string email = dt.Rows[0]["Email"].ToString();

                    requestCode = GenerateRandomOTP(6);

                    string sub = "Login";
                    Session["LoginRequestCode"] = requestCode;

                    string messagecontent = "Your Request Code for Login on OYO Busines Point is " + requestCode;

                    //if (!string.IsNullOrEmpty(Mobile_No))
                    //{
                    //    string smsStatus = SmsHelper.SendSMS(Mobile_No.Trim(), messagecontent);
                    //}
                    string result = OTPMail(email, sub, requestCode);
                    returnMSG = "1";
                }
                else
                {
                    returnMSG = "User id not valid.";
                }
            }
            catch (Exception ex)
            {
                returnMSG = "Request code sending failed.";
            }
            return Json(returnMSG, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BusinessPlan()
        {
            return View();
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
                user.sp_user = dt.Rows[0]["Memb_Name"].ToString();
                user.Memb_Name = dt.Rows[0]["username"].ToString();
                //user.Memb_Name = dt.Rows[0]["EMAIL"].ToString();
            }
            else
            {
                user.sp_user = "";
                user.Memb_Name = "";
            }

            return Json(user, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getCountryCode(string CountryName)
        {
            UserModel user = new UserModel();
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(CountryName))
                dt = UserManager.GetCountry();
            else
                dt = UserManager.GetUserCountry(CountryName);
            if (dt.Rows.Count > 0)
            {
                user.CountryName = dt.Rows[0]["Country"].ToString();
                user.CountryCode = dt.Rows[0]["code"].ToString();
            }
            else
            {
                user.CountryName = "";
                user.CountryCode = "";
            }

            return Json(user, JsonRequestBehavior.AllowGet);
        }



        //-----------------callback----------------//
        public JsonResult callbackpage(string ClientRefNo, string Status, string StatusMsg, string TrnID, string BankRefNo, string TTC, string TC, string TCG, string APBD, string ATDS)
        {

            dtResult = UserManager.Get_DataRequest("WITHDRALDETAILS", ClientRefNo);
            if (dtResult.Rows.Count > 0)
            {
                string commit = dtResult.Rows[0]["srno"].ToString();
                string content;
                content = "Status - " + Status + "/StatusMsg - " + StatusMsg + "/TrnID - " + TrnID + "/BankRefNo - " + BankRefNo;

                if (Status == "1" || Status == "0" || Status == "4")
                {

                    dtResult = UserManager.Update_request("UPDATESUCCESS", content, commit, ClientRefNo);
                }
                else if (Status != "1" || Status != "0" || Status != "4")
                {

                    dtResult = UserManager.Update_request("UPDATEFAIL", content, commit, ClientRefNo);


                }

            }
            //return View();

            return Json("true", JsonRequestBehavior.AllowGet);
        }


        public JsonResult Coinpaymentcallbackpage(string ClientRefNo, string Status, string StatusMsg, string TrnID, string BankRefNo, string TTC, string TC, string TCG, string APBD, string ATDS)
        {

            dtResult = UserManager.Get_DataRequest("WITHDRALDETAILS", ClientRefNo);
            if (dtResult.Rows.Count > 0)
            {
                string commit = dtResult.Rows[0]["srno"].ToString();
                string content;
                content = "Status - " + Status + "/StatusMsg - " + StatusMsg + "/TrnID - " + TrnID + "/BankRefNo - " + BankRefNo;

                if (Status == "1" || Status == "0" || Status == "4")
                {

                    dtResult = UserManager.Update_request("UPDATESUCCESS", content, commit, ClientRefNo);
                }
                else if (Status != "1" || Status != "0" || Status != "4")
                {

                    dtResult = UserManager.Update_request("UPDATEFAIL", content, commit, ClientRefNo);


                }

            }
            //return View();

            return Json("true", JsonRequestBehavior.AllowGet);
        }
        public JsonResult Coinpayment_transactioncallbackpage(string ClientRefNo, string Status, string StatusMsg, string TrnID, string BankRefNo, string TTC, string TC, string TCG, string APBD, string ATDS)
        {

            dtResult = UserManager.Get_DataRequest("WITHDRALDETAILS", ClientRefNo);
            if (dtResult.Rows.Count > 0)
            {
                string commit = dtResult.Rows[0]["srno"].ToString();
                string content;
                content = "Status - " + Status + "/StatusMsg - " + StatusMsg + "/TrnID - " + TrnID + "/BankRefNo - " + BankRefNo;

                if (Status == "1" || Status == "0" || Status == "4")
                {

                    dtResult = UserManager.Update_request("UPDATESUCCESS", content, commit, ClientRefNo);
                }
                else if (Status != "1" || Status != "0" || Status != "4")
                {

                    dtResult = UserManager.Update_request("UPDATEFAIL", content, commit, ClientRefNo);


                }

            }
            //return View();

            return Json("true", JsonRequestBehavior.AllowGet);
        }
    }
}