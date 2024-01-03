using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace UniqueTrade_App.CommonFunction
{
    public class CoinPayments
    {
        private string s_privkey = null;
        private string s_pubkey = null;
        private static readonly Encoding encoding = Encoding.UTF8;

        public CoinPayments(string privkey, string pubkey)
        {
            s_privkey = privkey;
            s_pubkey = pubkey;
            if (s_privkey.Length == 0 || s_pubkey.Length == 0)
            {
                throw new ArgumentException("Private or Public Key is empty");
            }
        }

        //public Dictionary<string, dynamic> GetCallbackAddressBTC()
        //{
        //    var ret = new Dictionary<string, dynamic>();

        //    CoinPayments cp = new CoinPayments(WebConfigurationManager.AppSettings["Coinpayment_Private_Key"], WebConfigurationManager.AppSettings["Coinpayment_Public_Key"]);

        //    var parms = new SortedList<string, string>();
        //    //parms["version"] = "1";
        //    //parms["key"] = s_pubkey;
        //    parms["cmd"] = "get_callback_address";
        //    parms["currency"] = "BTC";
        //    // parms["version"] = "1";
        //    // parms["key"] = s_pubkey;

        //    var datacp = cp.CallAPI("get_callback_address", parms);

        //    return datacp;

        //}
        public Dictionary<string, dynamic> CallAPIFOR_CALLBACK_ADDRESS(string currency)
        {
            var parms = new SortedList<string, string>();
            if (parms == null)
            {
                parms = new SortedList<string, string>();
            }
            parms["version"] = "1";
            parms["key"] = s_pubkey;
            parms["format"] = "json";
            parms["cmd"] = "get_callback_address";
            // parms["eip55"] = btcamount;
            parms["currency"] = currency;
            // parms["address"] = address;
            parms["label"] = currency;
            parms["ipn_url"] = "https://localhost:44367/Home/Coinpaymentcallbackpage";

            string post_data = "";
            foreach (KeyValuePair<string, string> parm in parms)
            {
                if (post_data.Length > 0) { post_data += "&"; }
                post_data += parm.Key + "=" + Uri.EscapeDataString(parm.Value);
            }

            byte[] keyBytes = encoding.GetBytes(s_privkey);
            byte[] postBytes = encoding.GetBytes(post_data);
            var hmacsha512 = new HMACSHA512(keyBytes);
            string hmac = BitConverter.ToString(hmacsha512.ComputeHash(postBytes)).Replace("-", string.Empty);

            // do the post:
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.WebClient cl = new System.Net.WebClient();
            cl.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            cl.Headers.Add("HMAC", hmac);
            cl.Encoding = encoding;

            var ret = new Dictionary<string, dynamic>();
            try
            {
                string resp = cl.UploadString("https://www.coinpayments.net/api.php", post_data);
                var decoder = new System.Web.Script.Serialization.JavaScriptSerializer();
                ret = decoder.Deserialize<Dictionary<string, dynamic>>(resp);
            }
            catch (System.Net.WebException e)
            {
                ret["error"] = "Exception while contacting CoinPayments.net: " + e.Message;
            }
            catch (Exception e)
            {
                ret["error"] = "Unknown exception: " + e.Message;
            }

            return ret;
        }
        public Dictionary<string, dynamic> CallAPI(string currency)
        {
            var parms = new SortedList<string, string>();
            if (parms == null)
            {
                parms = new SortedList<string, string>();
            }
            parms["version"] = "1";
            parms["key"] = s_pubkey;
            parms["format"] = "json";
            parms["cmd"] = "get_callback_address";
            parms["currency"] = currency;

            string post_data = "";
            foreach (KeyValuePair<string, string> parm in parms)
            {
                if (post_data.Length > 0) { post_data += "&"; }
                post_data += parm.Key + "=" + Uri.EscapeDataString(parm.Value);
            }

            byte[] keyBytes = encoding.GetBytes(s_privkey);
            byte[] postBytes = encoding.GetBytes(post_data);
            var hmacsha512 = new HMACSHA512(keyBytes);
            string hmac = BitConverter.ToString(hmacsha512.ComputeHash(postBytes)).Replace("-", string.Empty);

            // do the post:
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.WebClient cl = new System.Net.WebClient();
            cl.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            cl.Headers.Add("HMAC", hmac);
            cl.Encoding = encoding;

            var ret = new Dictionary<string, dynamic>();
            try
            {
                string resp = cl.UploadString("https://www.coinpayments.net/api.php", post_data);
                var decoder = new System.Web.Script.Serialization.JavaScriptSerializer();
                ret = decoder.Deserialize<Dictionary<string, dynamic>>(resp);
            }
            catch (System.Net.WebException e)
            {
                ret["error"] = "Exception while contacting CoinPayments.net: " + e.Message;
            }
            catch (Exception e)
            {
                ret["error"] = "Unknown exception: " + e.Message;
            }

            return ret;
        }

        public Dictionary<string, dynamic> CallAPIFORWITH(string currency, string btcamount, string address)
        {
            var parms = new SortedList<string, string>();
            if (parms == null)
            {
                parms = new SortedList<string, string>();
            }
            parms["version"] = "1";
            parms["key"] = s_pubkey;
            parms["format"] = "json";
            parms["cmd"] = "create_withdrawal";
            parms["amount"] = btcamount;
            parms["currency"] = currency;
            parms["address"] = address;
            parms["auto_confirm"] = "1";
            parms["ipn_url"] = "https://bitaxen.com//ipn-handler-script";

            string post_data = "";
            foreach (KeyValuePair<string, string> parm in parms)
            {
                if (post_data.Length > 0) { post_data += "&"; }
                post_data += parm.Key + "=" + Uri.EscapeDataString(parm.Value);
            }

            byte[] keyBytes = encoding.GetBytes(s_privkey);
            byte[] postBytes = encoding.GetBytes(post_data);
            var hmacsha512 = new HMACSHA512(keyBytes);
            string hmac = BitConverter.ToString(hmacsha512.ComputeHash(postBytes)).Replace("-", string.Empty);

            // do the post:
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.WebClient cl = new System.Net.WebClient();
            cl.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            cl.Headers.Add("HMAC", hmac);
            cl.Encoding = encoding;

            var ret = new Dictionary<string, dynamic>();
            try
            {
                string resp = cl.UploadString("https://www.coinpayments.net/api.php", post_data);
                var decoder = new System.Web.Script.Serialization.JavaScriptSerializer();
                ret = decoder.Deserialize<Dictionary<string, dynamic>>(resp);
            }
            catch (System.Net.WebException e)
            {
                ret["error"] = "Exception while contacting CoinPayments.net: " + e.Message;
            }
            catch (Exception e)
            {
                ret["error"] = "Unknown exception: " + e.Message;
            }

            return ret;
        }
        public Dictionary<string, dynamic> create_transaction(string currency, string email, string name, string amount)
        {
            var parms = new SortedList<string, string>();
            if (parms == null)
            {
                parms = new SortedList<string, string>();
            }
            parms["version"] = "1";
            parms["key"] = s_pubkey;
            parms["format"] = "json";
            parms["cmd"] = "create_transaction";
            parms["amount"] = amount;
            parms["currency1"] = currency;
            parms["currency2"] = currency;
            parms["buyer_email"] = email;
            // parms["address"] = currency2;
            parms["buyer_name"] = name;
            parms["format"] = "json";
            parms["ipn_url"] = "https://localhost:44367/Home/Coinpayment_transactioncallbackpage";

            parms["success_url"] = "https://localhost:44367/Home/Coinpayment_transactioncallbackpage";

            parms["cancel_url"] = "https://localhost:44367/Home/Coinpayment_transactioncallbackpage";


            string post_data = "";
            foreach (KeyValuePair<string, string> parm in parms)
            {
                if (post_data.Length > 0) { post_data += "&"; }
                post_data += parm.Key + "=" + Uri.EscapeDataString(parm.Value);
            }

            byte[] keyBytes = encoding.GetBytes(s_privkey);
            byte[] postBytes = encoding.GetBytes(post_data);
            var hmacsha512 = new HMACSHA512(keyBytes);
            string hmac = BitConverter.ToString(hmacsha512.ComputeHash(postBytes)).Replace("-", string.Empty);

            // do the post:
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.WebClient cl = new System.Net.WebClient();
            cl.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            cl.Headers.Add("HMAC", hmac);
            cl.Encoding = encoding;

            var ret = new Dictionary<string, dynamic>();
            try
            {
                string resp = cl.UploadString("https://www.coinpayments.net/api.php", post_data);
                var decoder = new System.Web.Script.Serialization.JavaScriptSerializer();
                ret = decoder.Deserialize<Dictionary<string, dynamic>>(resp);
            }
            catch (System.Net.WebException e)
            {
                ret["error"] = "Exception while contacting CoinPayments.net: " + e.Message;
            }
            catch (Exception e)
            {
                ret["error"] = "Unknown exception: " + e.Message;
            }

            return ret;
        }

        public Dictionary<string, dynamic> transaction_confirm(string transaction_no)
        {
            var parms = new SortedList<string, string>();
            if (parms == null)
            {
                parms = new SortedList<string, string>();
            }
            parms["version"] = "1";
            parms["key"] = s_pubkey;
            parms["format"] = "json";
            parms["cmd"] = "get_tx_info_multi";
            parms["txid"] = transaction_no;


            string post_data = "";
            foreach (KeyValuePair<string, string> parm in parms)
            {
                if (post_data.Length > 0) { post_data += "&"; }
                post_data += parm.Key + "=" + Uri.EscapeDataString(parm.Value);
            }

            byte[] keyBytes = encoding.GetBytes(s_privkey);
            byte[] postBytes = encoding.GetBytes(post_data);
            var hmacsha512 = new HMACSHA512(keyBytes);
            string hmac = BitConverter.ToString(hmacsha512.ComputeHash(postBytes)).Replace("-", string.Empty);

            // do the post:
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.WebClient cl = new System.Net.WebClient();
            cl.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            cl.Headers.Add("HMAC", hmac);
            cl.Encoding = encoding;

            var ret = new Dictionary<string, dynamic>();
            try
            {
                string resp = cl.UploadString("https://www.coinpayments.net/api.php", post_data);
                var decoder = new System.Web.Script.Serialization.JavaScriptSerializer();
                ret = decoder.Deserialize<Dictionary<string, dynamic>>(resp);
            }
            catch (System.Net.WebException e)
            {
                ret["error"] = "Exception while contacting CoinPayments.net: " + e.Message;
            }
            catch (Exception e)
            {
                ret["error"] = "Unknown exception: " + e.Message;
            }

            return ret;
        }


    }
}