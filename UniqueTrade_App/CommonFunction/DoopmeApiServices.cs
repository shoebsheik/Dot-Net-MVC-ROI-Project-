using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using UniqueTrade_App.Models;
using System.Xml;
using System.Xml.Serialization;

namespace UniqueTrade_App.CommonFunction
{
    public class DoopmeApiServices
    {
        static string sURL = "https://www.doopme.com/RechargeAPI/DoopMePBPDMRService.asmx";
        static string sMobileNo = WebConfigurationManager.AppSettings["sMobileNo"].ToString();
        static string sAPIKey = WebConfigurationManager.AppSettings["sAPIKey"].ToString();
        static string sRespType = "JSON";
        static string sAPIVersion = "1.0";
        static string sAgentCode = "RT001";

        public static string GetDoopMeDMRAPIBal()
        {
            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sMobileNo = string.Empty;
            string sAPIKey = string.Empty;
            string sRespType = string.Empty;
            string sChecksum = string.Empty;
            string sAPIVersion = string.Empty;
            string sAgentCode = string.Empty;
            string sURL = string.Empty;
            try
            {
                //sURL = "https://www.doopme.com/RechargeAPI/DoopMePBPDMRService.asmx";
                //sMobileNo = "9999999999"; //Please use your registered mobile no
                //sAPIKey = "0Cr6CaLJD0YADyj8HXz9D2AMk8gPTAWwLb5"; //Please use your API Key
                //sRespType = "XML";
                //sAPIVersion = "1.0";
                //sAgentCode = "RT001"; //Please use your Retailer's Unique ID/Code

                sRequest = "<NTDREQ><REQTYPE>GPB</REQTYPE></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><GetPartnerBalance xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></GetPartnerBalance></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                using (System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream()))
                {
                    sWSResponse = responseReader.ReadToEnd();
                }
                //Handle response process here
            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return sWSResponse;
        }

        public static GETEXISTINGCUSTOMER GET_EXISTING_CUSTOMER(string mobile)
        {
            GETEXISTINGCUSTOMER data = new GETEXISTINGCUSTOMER();
            
            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                //sURL = "https://www.doopme.com/RechargeAPI/DoopMePBPDMRService.asmx";
                //sMobileNo = "8888972383"; //Please use your registered mobile no
                //sAPIKey = "0Cr6CaLJD0YADyj8HXz9D2AMk8gPTAWwLb5"; //Please use your API Key
                //sRespType = "JSON";
                //sAPIVersion = "1.0";
                //sAgentCode = "RT001"; //Please use your Retailer's Unique ID/Code

                sRequest = "<NTDREQ><REQTYPE>GC</REQTYPE><CUSTOMERNO>"+ mobile + "</CUSTOMERNO></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><GetCustomer xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></GetCustomer></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("GetCustomerResult")[0];
                string innerObject = soapBody.InnerXml;

               var  data1 =JsonConvert.DeserializeObject<GETEXISTINGCUSTOMER>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static ADDCUSTOMER ADD_CUSTOMER(string mobile, string name, string surname, string pincode, string address, string city, string state, string country)
        {
            ADDCUSTOMER data = new ADDCUSTOMER();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>AC</REQTYPE><CUSTOMERNO>"+ mobile +"</CUSTOMERNO><FNAME>"+ name + "</FNAME><LNAME>"+ surname + "</LNAME><ANAME>"+ address +"</ANAME><ADD1>"+ address +"</ADD1><ADD2>"+ address +"</ADD2><CITY>"+ city +"</CITY><STATE>"+ state +"</STATE><COUNTRY>"+ country +"</COUNTRY><PCODE>"+ pincode +"</PCODE></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><AddCustomer xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></AddCustomer></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("AddCustomerResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<ADDCUSTOMER>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static ADDCUSTOMER_VERIFYOTP ADD_CUSTOMER_VERIFY_OTP(string mobile, string OTP)
        {
            ADDCUSTOMER_VERIFYOTP data = new ADDCUSTOMER_VERIFYOTP();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>VCO</REQTYPE><CUSTOMERNO>"+ mobile +"</CUSTOMERNO><OTP>"+OTP+"</OTP></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><ValidateCustomerOTP xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></ValidateCustomerOTP></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("ValidateCustomerOTPResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<ADDCUSTOMER_VERIFYOTP>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static ADDCUSTOMER_RESENDVERIFYOTP ValidateResendCustomerOTP(string mobile)
        {
            ADDCUSTOMER_RESENDVERIFYOTP data = new ADDCUSTOMER_RESENDVERIFYOTP();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>RACO</REQTYPE><CUSTOMERNO>"+ mobile +"</CUSTOMERNO></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><ResendAddCustomerOTP xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></ResendAddCustomerOTP></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("ResendAddCustomerOTPResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<ADDCUSTOMER_RESENDVERIFYOTP>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }
        
        public static beneficiary_RootObject getBeneficiary(string mobile, string OTP ,string ben_id)
        {
            beneficiary_RootObject data = new beneficiary_RootObject();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>GB</REQTYPE><CUSTOMERNO>"+ mobile +"</CUSTOMERNO><BENEID>"+ ben_id +"</BENEID></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><GetBeneficiary xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></GetBeneficiary></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("GetBeneficiaryResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<beneficiary_RootObject>(innerObject);

                data = data1;

            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static beneficiary_listRootObject getBeneficiaryList(string mobile)
        {
            beneficiary_listRootObject data = new beneficiary_listRootObject();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>GAB</REQTYPE><CUSTOMERNO>"+ mobile +"</CUSTOMERNO></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><GetAllBeneficiary xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></GetAllBeneficiary></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("GetAllBeneficiaryResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<beneficiary_listRootObject>(innerObject);

                data = data1;

            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static AddBeneficiary AddBenificiaryDetails(string customer_mobile, string bank_id, string ben_name, string ben_mob, string bank_acno, string ifsc)
        {
            AddBeneficiary data = new AddBeneficiary();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>AB</REQTYPE><CUSTOMERNO>"+ customer_mobile +"</CUSTOMERNO><NAME>"+ ben_name +"</NAME><MOBILENO>"+ ben_mob +"</MOBILENO><BANKID>"+ bank_id + "</BANKID><ACCNO>"+ bank_acno +"</ACCNO><IFSC>"+ ifsc +"</IFSC></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><AddBeneficiary xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></AddBeneficiary></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("AddBeneficiaryResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<AddBeneficiary>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static ADDBENEFICIARY_VERIFYOTP AddBenificiary_verifyOtpDetails(string customer_mobile, string ben_id, string otp)
        {
            ADDBENEFICIARY_VERIFYOTP data = new ADDBENEFICIARY_VERIFYOTP();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>VBO</REQTYPE><CUSTOMERNO>"+ customer_mobile + "</CUSTOMERNO><BENEID>"+ ben_id +"</BENEID><OTP>"+ otp +"</OTP></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><ValidateBeneficiaryOTP xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></ValidateBeneficiaryOTP></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("ValidateBeneficiaryOTPResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<ADDBENEFICIARY_VERIFYOTP>(innerObject);

                data = data1;

            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static ResendAddBeneficiaryOTP AddBenificiary_ResendOtpDetails(string customer_mobile, string ben_id)
        {
            ResendAddBeneficiaryOTP data = new ResendAddBeneficiaryOTP();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>RABO</REQTYPE><CUSTOMERNO>"+ customer_mobile +"</CUSTOMERNO><BENEID>"+ ben_id +"</BENEID></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><ResendAddBeneficiaryOTP xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></ResendAddBeneficiaryOTP></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("ResendAddBeneficiaryOTPResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<ResendAddBeneficiaryOTP>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static DeleteBeneficiaryRequest DeleteBeneficiaryRequest(string customer_mobile, string ben_id)
        {
            DeleteBeneficiaryRequest data = new DeleteBeneficiaryRequest();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>DB</REQTYPE><CUSTOMERNO>"+ customer_mobile +"</CUSTOMERNO><BENEID>"+ ben_id +"</BENEID></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><DeleteBeneficiary xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></DeleteBeneficiary></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("DeleteBeneficiaryResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<DeleteBeneficiaryRequest>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static DeleteBeneficiaryOTP DeleteBeneficiaryOTP(string customer_mobile, string ben_id, string otp)
        {
            DeleteBeneficiaryOTP data = new DeleteBeneficiaryOTP();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>VDBO</REQTYPE><CUSTOMERNO>"+ customer_mobile +"</CUSTOMERNO><BENEID>"+ ben_id +"</BENEID><OTP>"+ otp +"</OTP></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><ValidateDeleteBeneficiaryOTP xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></ValidateDeleteBeneficiaryOTP></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("ValidateDeleteBeneficiaryOTPResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<DeleteBeneficiaryOTP>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static ResendDeleteBeneficiaryOTP ResendDeleteBeneficiaryOTP(string customer_mobile, string ben_id)
        {
            ResendDeleteBeneficiaryOTP data = new ResendDeleteBeneficiaryOTP();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>RDBO</REQTYPE><CUSTOMERNO>"+ customer_mobile +"</CUSTOMERNO><BENEID>"+ ben_id +"</BENEID></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><ResendDelBeneficiaryOTP xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></ResendDelBeneficiaryOTP></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("ResendDelBeneficiaryOTPResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<ResendDeleteBeneficiaryOTP>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static VERIFYBeneficiaryDATA VerifyBenificiaryBankAc(string customer_mob, string ben_id, string bank_acc_no, string ifsc, string order_id)
        {
            VERIFYBeneficiaryDATA data = new VERIFYBeneficiaryDATA();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>BAV</REQTYPE><CUSTOMERNO>"+ customer_mob +"</CUSTOMERNO><BANKID>"+ ben_id +"</BANKID><ACCNO>"+ bank_acc_no +"</ACCNO><IFSC>"+ ifsc +"</IFSC><REFNO>"+ order_id +"</REFNO></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><BeneficiaryAccountVerify xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></BeneficiaryAccountVerify></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("BeneficiaryAccountVerifyResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<VERIFYBeneficiaryDATA>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static TRANSFER FianlMoney_Transfer(string customer_mob, string ben_id, string amt, string mode, string order_id)
        {
            TRANSFER data = new TRANSFER();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>SM</REQTYPE><CUSTOMERNO>"+ customer_mob +"</CUSTOMERNO><BENEID>" + ben_id + "</BENEID><AMT>"+ amt +"</AMT><TRNTYPE>"+ mode +"</TRNTYPE><IMPS_SCHEDULE>0</IMPS_SCHEDULE><REFNO>"+ order_id +"</REFNO><CHN>WEB</CHN><UR>INR</CUR><AG_LAT></AG_LAT><AG_LONG></AG_LONG></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><SendMoney xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></SendMoney></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("SendMoneyResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<TRANSFER>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static BALANCE_CHECK GETTRANSACTIONSTATUSBALANCE_CHECK(string order_id)
        {
            BALANCE_CHECK data = new BALANCE_CHECK();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>GTS</REQTYPE><REFNO>"+ order_id + "</REFNO></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><GetTransactionStatus xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></GetTransactionStatus></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("GetTransactionStatusResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<BALANCE_CHECK>(innerObject);

                data = data1;

            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static BALANCE_CHECK GETTRANSACTIONREFUNDBALANCE_CHECK(string order_id, string otp)
        {
            BALANCE_CHECK data = new BALANCE_CHECK();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>GTR</REQTYPE><REFNO>"+ order_id +"</REFNO><OTP>"+ otp +"</OTP></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><GetTransactionRefund xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></GetTransactionRefund></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("GetTransactionRefundResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<BALANCE_CHECK>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static BALANCE_CHECK RESENDTRANSACTIONREFUNDOTPBALANCE_CHECK(string customer_mob, string order_id)
        {
            BALANCE_CHECK data = new BALANCE_CHECK();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>RDBO</REQTYPE><CUSTOMERNO>"+ customer_mob +"</CUSTOMERNO><REFNO>"+ order_id +"</REFNO></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><ResendTransactionRefundOTP xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></ResendTransactionRefundOTP></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("ResendTransactionRefundOTPResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<BALANCE_CHECK>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static BALANCE_CHECK BALANCE_CHECK()
        {
            BALANCE_CHECK data = new BALANCE_CHECK();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>GPB</REQTYPE></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><GetPartnerBalance xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></GetPartnerBalance></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("GetPartnerBalanceResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<BALANCE_CHECK>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static beneficiary_listRootObject GETBANKLIST()
        {
            beneficiary_listRootObject data = new beneficiary_listRootObject();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>GBL</REQTYPE></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><GetBankList xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></GetBankList></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("GetBankListResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<beneficiary_listRootObject>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }

        public static beneficiary_listRootObject GenerateTransactionComplaint(string order_id,string status)
        {
            beneficiary_listRootObject data = new beneficiary_listRootObject();

            byte[] buffer;
            System.Net.HttpWebRequest webRequest;
            string sWSRequest = string.Empty;
            string sWSResponse = string.Empty;
            string sRequest = string.Empty;
            string sChecksum = string.Empty;
            try
            {
                sRequest = "<NTDREQ><REQTYPE>GTC</REQTYPE><REFNO>"+ order_id +"</REFNO><CMTYPE>1</CMTYPE><CMDESC>"+ status + "</CMDESC></NTDREQ>";
                sChecksum = GenerateSHA512(sAPIKey, sRequest);

                sWSRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Header><DMRAuthHeader xmlns=\"http://tempuri.org/\"><MobileNo>" + sMobileNo + "</MobileNo><APIKey>" + sAPIKey + "</APIKey><ResponseType>" + sRespType + "</ResponseType><Checksum>" + sChecksum + "</Checksum><Version>" + sAPIVersion + "</Version><AgentCode>" + sAgentCode + "</AgentCode></DMRAuthHeader></soap:Header><soap:Body><GenerateTransactionComplaint xmlns=\"http://tempuri.org/\"><sRequest>" + EncodeForXml(sRequest) + "</sRequest></GenerateTransactionComplaint></soap:Body></soap:Envelope>";

                buffer = System.Text.Encoding.ASCII.GetBytes(sWSRequest);
                webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = buffer.Length;

                System.IO.Stream stream = webRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();

                System.IO.StreamReader responseReader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
                sWSResponse = responseReader.ReadToEnd();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sWSResponse);

                var soapBody = xmlDocument.GetElementsByTagName("GenerateTransactionComplaintResult")[0];
                string innerObject = soapBody.InnerXml;

                var data1 = JsonConvert.DeserializeObject<beneficiary_listRootObject>(innerObject);
                data = data1;


            }
            catch (Exception ex)
            {
                //Handle your exception here
            }
            return data;
        }
        
        public static string EncodeForXml(string data)
        {
            System.Text.RegularExpressions.Regex badAmpersand = new System.Text.RegularExpressions.Regex("&(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
            data = badAmpersand.Replace(data, "&amp;");

            return data.Replace("<", "&lt;").Replace("\"", "&quot;").Replace(">", "&gt;");
        }

        public static string GenerateSHA512(string keyString, string inputString)
        {
            System.Text.UTF8Encoding mEncoding = new System.Text.UTF8Encoding();
            string sResult = string.Empty;
            byte[] keyBytes;
            byte[] messageBytes;
            try
            {
                keyBytes = mEncoding.GetBytes(keyString);
                messageBytes = mEncoding.GetBytes(inputString);

                using (var hmacsha = new System.Security.Cryptography.HMACSHA512(keyBytes))
                {
                    hmacsha.ComputeHash(messageBytes);
                    sResult = GetStringFromHash(hmacsha.Hash);
                }

                return sResult;
            }
            catch (Exception ex)
            {
                // Handle your exception here
                return "";
            }
        }

        public static string GetStringFromHash(byte[] hash)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            for (int i = 0; i <= hash.Length - 1; i++)
                result.Append(hash[i].ToString("X2"));

            return result.ToString();
        }
    }
}