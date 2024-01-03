using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniqueTrade_App.Models
{

    //---------------- Add User -------------//

    #region CUSTOMER


    public class NTDRESP
    {
        public string STATUSCODE { get; set; }
        public string STATUSMSG { get; set; }
        public string OTPREQ { get; set; }
        public string OTP { get; set; }
        public string BENEID { get; set; }
        public string BENENAME { get; set; }
        public string MOBILENO { get; set; }
        public string BANKID { get; set; }
        public string BANKNAME { get; set; }
        public string ACCNO { get; set; }
        public string IFSC { get; set; }
        public string VERIFIED { get; set; }
        public string IMPS_SCHEDULE { get; set; }
        public string STATUS { get; set; }
        public string STATUSDESC { get; set; }
        public List<BENELIST> BENELIST { get; set; }
        public List<BANKLIST> BANKLIST { get; set; }
        public string VERIFYID { get; set; }
        public string REMAIN { get; set; }
        public string USED { get; set; }
        public string LIMIT { get; set; }
        public string REFNO { get; set; }
        public string TRNID { get; set; }
        public string BANKREFNO { get; set; }
        public string TOTALCHARGE { get; set; }
        public string CHARGE { get; set; }
        public string GST { get; set; }
        public string APBD { get; set; }
        public string ATDS { get; set; }
        public string TRNSTATUS { get; set; }
        public string TRNSTATUSDESC { get; set; }
        public string BAL { get; set; }
        public string CMPID { get; set; }

    }

    public class BENELIST
    {
        // public NTDRESP NTDRESP { get; set; }


        public string BENEID { get; set; }
        public string BENENAME { get; set; }
        public string MOBILENO { get; set; }
        public string BANKID { get; set; }
        public string BANKNAME { get; set; }
        public string ACCNO { get; set; }
        public string IFSC { get; set; }
        public string VERIFIED { get; set; }
        public string IMPS_SCHEDULE { get; set; }
        public string STATUS { get; set; }
        public string STATUSDESC { get; set; }
    }

    public class BANKLIST
    {
        // public NTDRESP NTDRESP { get; set; }

        //public List<BANKNAME_LIST> BANKNAME { get; set; }

        public string BANKID { get; set; }
        public string BANKNAME { get; set; }
        public string BANKCODE { get; set; }
        public string MIFSCCODE { get; set; }
        public string IMPS { get; set; }
        public string NEFT { get; set; }
        public string ACVERIFYAVAIL { get; set; }
        public string STATUS { get; set; }
    }

    public class BANKNAME_LIST
    {
        // public NTDRESP NTDRESP { get; set; }

        public string BANKNAME { get; set; }
       
    }




    public class ADDCUSTOMER
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string mobile { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string pincode { get; set; }
        //-----1. In case of Request Success-----//
        public string RESPONSESTATUS { get; set; }
        public string CUSTID { get; set; }
        public string MESSAGE { get; set; }
        public string OTPSENT { get; set; }
    }

    public class DoopmeAddUserModel
    {
        public NTDRESP NTDRESP { get; set; }

        public string ERROR { get; set; }
        public string STATUS { get; set; }
        public string ACCOUNTEXISTS { get; set; }
        public int REMID { get; set; }
        public string APISRC { get; set; }
        public string MOBILENO { get; set; }
        public object REQUESTNO { get; set; }
        public string MESSAGE { get; set; }
        public string ip { get; set; }
        public ADDCUSTOMER request { get; set; }
    }

    public class ADDCUSTOMER_VERIFYOTP
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string customer_id { get; set; }
        public string customer_mobile { get; set; }
        public string otp { get; set; }
        public string RESPONSESTATUS { get; set; }
        public string MESSAGE { get; set; }
    }

    public class ADDCUSTOMER_RESENDVERIFYOTP
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string customer_id { get; set; }
        public string customer_mobile { get; set; }
        public string otp { get; set; }
        public string RESPONSESTATUS { get; set; }
        public string MESSAGE { get; set; }
    }


    

    public class GETEXISTINGCUSTOMER
    {
        public NTDRESP NTDRESP { get; set; }


        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string mobile { get; set; }

        //--1. In case of Request Success//
        public string RESPONSESTATUS { get; set; }
        public string CUSTFOUND { get; set; }
        public string CUSTID { get; set; }
        public string NAME { get; set; }
        public string PINCODE { get; set; }
        public string AVAILLIMIT { get; set; }
        public string MESSAGE { get; set; }
        public string ISVERIFIED { get; set; }
        public string OTPSENT { get; set; }
        public string STATUSCODE { get; set; }
        public string STATUSMSG { get; set; }
        public string FNAME { get; set; }
        public string LNAME { get; set; }
        public string USED { get; set; }
        public string LIMIT { get; set; }
        public string REMAIN { get; set; }
        public string STATUS { get; set; }
        public string STATUSDESC { get; set; }
    }

    public class CheckUserRequest
    {
        public NTDRESP NTDRESP { get; set; }

        public string RESPONSESTATUS { get; set; }
        public string CUSTID { get; set; }
        public string MESSAGE { get; set; }
        public string OTPSENT { get; set; }
    }

    public class DoopmeCheckUserModel
    {
        public NTDRESP NTDRESP { get; set; }

        public string RESPONSESTATUS { get; set; }
        public string CUSTID { get; set; }
        public string OTPSENT { get; set; }
        public string ERROR { get; set; }
        public string STATUS { get; set; }
        public string ACCOUNTEXISTS { get; set; }
        public string APISRC { get; set; }
        public string MESSAGE { get; set; }
        public string ip { get; set; }
        public CheckUserRequest request { get; set; }
    }

    #endregion 



    //-------------- Get Benificiary Details ----------------//

    #region Benificiary 

    public class BenificiaryREMITTER
    {
        public NTDRESP NTDRESP { get; set; }

        public string id { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string pincode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string kycstatus { get; set; }
        public int consumedlimit { get; set; }
        public int remaininglimit { get; set; }
        public string kycdocs { get; set; }
        public int perm_txn_limit { get; set; }
    }

    public class BenificiaryDetailRequest
    {
        public NTDRESP NTDRESP { get; set; }

        public string member_id { get; set; }
        public string api_password { get; set; }
        public string api_pin { get; set; }
        public string mobile_no { get; set; }
    }

    public class BenificiaryDetailRequestRootObject
    {
        public NTDRESP NTDRESP { get; set; }

        public string ERROR { get; set; }
        public string STATUS { get; set; }
        public string MOBILENO { get; set; }
        public string ACCOUNTEXISTS { get; set; }
        public string NAME { get; set; }
        public string APISRC { get; set; }
        public int LIMIT { get; set; }
        public List<object> BENEFICIARYLIST { get; set; }
        public string MESSAGE { get; set; }
        public BenificiaryREMITTER REMITTER { get; set; }
        public string ip { get; set; }
        public BenificiaryDetailRequest request { get; set; }
    }


    //-------------------- Add Benificiary Details -------------//

    
    public class AddBeneficiary
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string customer_mobile { get; set; }
        public string customer_id { get; set; }
        public string ben_name { get; set; }
        public string ben_mob { get; set; }
        public string bank_acno { get; set; }
        public string ifsc { get; set; }


        //------------response
        public string RESPONSESTATUS { get; set; }
        public string BENID { get; set; }
        public string OTPSENT { get; set; }
        public string MESSAGE { get; set; }
    }

    public class AddBeneficiaryRootObject
    {
        public NTDRESP NTDRESP { get; set; }

        public string RESPONSESTATUS { get; set; }
        public string BENID { get; set; }
        public string OTPSENT { get; set; }
        public string MESSAGE { get; set; }
        public string ip { get; set; }
        public string REQUESTNO { get; set; }
        public AddBeneficiary request { get; set; }
    }





    public class ADDBENEFICIARY_VERIFYOTP
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string customer_id { get; set; }
        public string ben_id { get; set; }
        public string otp { get; set; }
        public string RESPONSESTATUS { get; set; }
        public string MESSAGE { get; set; }
    }

    public class VerifyOTPRequestRootObject
    {
        public NTDRESP NTDRESP { get; set; }

        public string RESPONSESTATUS { get; set; }
        public string STATUS { get; set; }
        public ADDBENEFICIARY_VERIFYOTP request { get; set; }
    }

    public class beneficiary_list
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string mobile { get; set; }
        public ADDBENEFICIARY_VERIFYOTP request { get; set; }
    }
    public class beneficiary_listRootObject
    {
        public NTDRESP NTDRESP { get; set; }

        public string RESPONSESTATUS { get; set; }
        public string BENEFICIARYCOUNT { get; set; }
        public string MESSAGE { get; set; }
        public List<BENEFICIARYDATA> BENEFICIARYDATA { get; set; } 
        public List<BANKLIST> BANKLIST { get; set; } 
        public string locked_amt { get; set; }
        public string charged_amt { get; set; }
        public string verification_status { get; set; }

        public string BENEID { get; set; }
        public string BENENAME { get; set; }
        public string MOBILENO { get; set; }
        public string BANKID { get; set; }
        public string BANKNAME { get; set; }
        public string ACCNO { get; set; }
        public string IFSC { get; set; }
        public string VERIFIED { get; set; }
        public string IMPS_SCHEDULE { get; set; }
        public string STATUS { get; set; }
        public string STATUSDESC { get; set; }
    }

    public class beneficiary_RootObject
    {
        public NTDRESP NTDRESP { get; set; }

        public string RESPONSESTATUS { get; set; }
        public string BENEFICIARYCOUNT { get; set; }
        public string MESSAGE { get; set; }
        public List<BENEFICIARYDATA> BENEFICIARYDATA { get; set; }
        public string locked_amt { get; set; }
        public string charged_amt { get; set; }
        public string verification_status { get; set; }
    }
    public class BENEFICIARYDATA
    {
        public NTDRESP NTDRESP { get; set; }

        public string BENID { get; set; }
        public string NAME { get; set; }
        public string MOBILE { get; set; }
        public string ACCOUNTNO { get; set; }
        public string BANKNAME { get; set; }
        public string IFSC { get; set; }
        public string ISOTPDELETE { get; set; }
    }




    public class VERIFYBeneficiaryDATA
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string customer_mob { get; set; }
        public string ben_id { get; set; }
        public string bank_acc_no { get; set; }
        public string ifsc { get; set; }
        public string order_id { get; set; }
        //--------response
        public string RESPONSESTATUS { get; set; }
        public string VERIFYSTATUS { get; set; }
        public string BENEFICIARYNAME { get; set; }
        public string BANKREFNO { get; set; }
        public string APITRANSNO { get; set; }
        public string MESSAGE { get; set; }
    }

    public class ValidateBeneficiaryRequest
    {
        public NTDRESP NTDRESP { get; set; }

        public string RESPONSESTATUS { get; set; }
        public string VERIFYSTATUS { get; set; }
        public string BENEFICIARYNAME { get; set; }
        public string BANKREFNO { get; set; }
        public string APITRANSNO { get; set; }
        public string MESSAGE { get; set; }
    }



    public class DeleteBeneficiaryOTP
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string customer_id { get; set; }
        public string ben_id { get; set; }
        public string otp { get; set; }
        public string RESPONSESTATUS { get; set; }
        public string MESSAGE { get; set; }
    }

    public class ResendDeleteBeneficiaryOTP
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string customer_id { get; set; }
        public string ben_id { get; set; }
        public string otp { get; set; }
        public string RESPONSESTATUS { get; set; }
        public string MESSAGE { get; set; }
    }

    public class DeleteBeneficiaryOTPRootObject
    {
        public NTDRESP NTDRESP { get; set; }

        public string RESPONSESTATUS { get; set; }
        public string MESSAGE { get; set; }
        public DeleteBeneficiaryOTP request { get; set; }
    }




    public class DeleteBeneficiaryRequest
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string customer_id { get; set; }
        public string ben_id { get; set; }
        public string RESPONSESTATUS { get; set; }
        public string MESSAGE { get; set; }
    }

    public class DeleteBeneficiaryRootObject
    {
        public NTDRESP NTDRESP { get; set; }

        public string RESPONSESTATUS { get; set; }
        public string MESSAGE { get; set; }
        public DeleteBeneficiaryRequest request { get; set; }
    }




    public class ResendAddBeneficiaryOTP
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string customer_id { get; set; }
        public string ben_id { get; set; }
        public string RESPONSESTATUS { get; set; }
        public string MESSAGE { get; set; }
    }

    public class ResendAddBeneficiaryOTPRootObject
    {
        public NTDRESP NTDRESP { get; set; }

        public string RESPONSESTATUS { get; set; }
        public string MESSAGE { get; set; }
        public ResendAddBeneficiaryOTP request { get; set; }
    }



    public class ValidateBeneficiaryModel
    {
        public NTDRESP NTDRESP { get; set; }

        public int ERROR { get; set; }
        public string MESSAGE { get; set; }
        public string STATUS { get; set; }
        public VERIFYBeneficiaryDATA DATA { get; set; }
        public int TRNXNO { get; set; }
        public string ip { get; set; }
        public ValidateBeneficiaryRequest request { get; set; }
    }

    #endregion


    #region Tranaction 
    public class GetTranactionStatusDATA
    {
        public NTDRESP NTDRESP { get; set; }

        public string ipayid { get; set; }
        public string agentid { get; set; }
        public string opr_id { get; set; }
        public string trans_amt { get; set; }
        public string charged_amt { get; set; }
        public string opening_bal { get; set; }
        public string req_dt { get; set; }
        public string locked_amt { get; set; }
        public string beneficiary_name { get; set; }
        public string refund_allowed { get; set; }
        public string bank_alias { get; set; }
    }

    public class GetTranactionStatusRequest
    {
        public NTDRESP NTDRESP { get; set; }

        public string member_id { get; set; }
        public string api_password { get; set; }
        public string api_pin { get; set; }
        public string ipay_id { get; set; }
    }

    public class GetTranactionStatusModel
    {
        public NTDRESP NTDRESP { get; set; }

        public string ERROR { get; set; }
        public string MESSAGE { get; set; }
        public GetTranactionStatusDATA DATA { get; set; }
        public string ip { get; set; }
        public GetTranactionStatusRequest request { get; set; }
    }
    #endregion

    #region TRANSFER 
    public class TRANSFER
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string customer_mob { get; set; }
        public string ben_id { get; set; }
        public string amt { get; set; }
        public string mode { get; set; }
        public string order_id { get; set; }


        public string RESPONSESTATUS { get; set; }
        public string TRANSACTIONSTATUS { get; set; }
        public string APITRANSNO { get; set; }
        public string ORDERID { get; set; }
        public string AMOUNT { get; set; }
        public string CUSTOMERMOB { get; set; }
        public string BENID { get; set; }
        public string BANKREFNO { get; set; }
        public string MESSAGE { get; set; }
    }

    public class TransferMoneryModel
    {
        public NTDRESP NTDRESP { get; set; }

        public string RESPONSESTATUS { get; set; }
        public string TRANSACTIONSTATUS { get; set; }
        public string APITRANSNO { get; set; }
        public string ORDERID { get; set; }
        public string AMOUNT { get; set; }
        public string CUSTOMERMOB { get; set; }
        public string BENID { get; set; }
        public string BANKREFNO { get; set; }
        public string MESSAGE { get; set; }
        public TRANSFER request { get; set; }
    }
    #endregion

    public class BALANCE_CHECK
    {
        public NTDRESP NTDRESP { get; set; }

        public string acc_no { get; set; }
        public string api_key { get; set; }
        public string Balance_Amount { get; set; }
    }

}