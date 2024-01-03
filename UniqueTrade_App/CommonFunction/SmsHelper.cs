using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace UniqueTrade_App.CommanFunction
{
    public class SmsHelper
    {
        public static string SendSMS(string Tos, string msg)
        {
            try
            {

                //  string strUrl = "https://www.logonutility.in/app/smsapi/index.php?key=25E2FEE06CE110&campaign=15810&routeid=20&type=text&contacts=" + Tos.Trim() + "&senderid=GTRADE&msg=" + msg + "";

                string strUrl = "http://nimbusit.co.in/api/swsendSingle.asp?username=t1ankitrastogi&password=955940&sender=GLOBAL&sendto=" + Tos.Trim() + "&message=" + msg + " ";
                // Create a request object  
                WebRequest request = HttpWebRequest.Create(strUrl);
                // Get the response back  
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();
                return "Message Send Successfully.";
            }
            catch (Exception ex)
            {
                return "Message Sending Failed";
            }
        }
    }
}