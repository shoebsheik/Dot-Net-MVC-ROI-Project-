using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace UniqueTrade_App.CommanFunction
{
    public class Common
    {
        public static string CurrentPageName
        {
            get
            {
                string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                string sRet = oInfo.Name.ToLower();
                return sRet;
            }
        }

        public static string CurrentControllerName
        {
            get
            {
                string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;

                string[] pathsss = sPath.Split('/');

                //System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                string sRet = pathsss[1];
                return sRet;
            }
        }

        public static string CookieUserID
        {
            get
            {
                return System.Web.HttpContext.Current.Request.Cookies["UserId"] != null ?
                    System.Web.HttpContext.Current.Request.Cookies["UserId"].Value : "";
            }
            set
            {
                System.Web.HttpContext.Current.Response.Cookies["UserId"].Value = value;
            }
        }


        public static string CookieUserType
        {
            get
            {
                return System.Web.HttpContext.Current.Request.Cookies["UserType"] != null ?
                    System.Web.HttpContext.Current.Request.Cookies["UserType"].Value : "";
            }
            set
            {
                System.Web.HttpContext.Current.Response.Cookies["UserType"].Value = value;
            }
        }

        public static bool RememberMe
        {
            get
            {
                return System.Web.HttpContext.Current.Request.Cookies["RememberMe"] != null ?
                    Convert.ToBoolean(System.Web.HttpContext.Current.Request.Cookies["RememberMe"].Value) : false;

            }
            set
            {
                System.Web.HttpContext.Current.Response.Cookies["RememberMe"].Value = value.ToString();
            }
        }

        public static UserInfo CurrentUserInfo
        {
            get
            {
                UserInfo userInfo = System.Web.HttpContext.Current.Session["UserInfo"] as UserInfo;
                if (userInfo == null && !String.IsNullOrEmpty(CookieUserID))
                {
                    //userInfo = new UserInfo(CookieUserID, CookieUserType);
                    //CurrentUserInfo = userInfo;


                    System.Web.HttpContext.Current.Response.Cookies.Clear();
                    System.Web.HttpContext.Current.Request.Cookies.Clear();
                }
                return userInfo;
            }
            set
            {
                System.Web.HttpContext.Current.Session["UserInfo"] = value;
                System.Web.HttpContext.Current.Session.Timeout = 15;
            }
        }
    }
}