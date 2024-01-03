using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniqueTrade_App.Models
{
    public class AllCOINModel
    {
        public string BTC { get; set; }
        public string ETH { get; set; }
        public string LTC { get; set; }
    }

    public class BTCModel
    {
        public double BTC { get; set; }
        public string hash160 { get; set; }
        public string address { get; set; }
        public int n_tx { get; set; }
        public double total_received { get; set; }
        public long total_sent { get; set; }
        public double final_balance { get; set; }
        //public List<Tx> txs { get; set; }
    }

    public class DashboardModel
    {
        public string ac_status { get; set; }
        public string StarBonus { get; set; }

        public string RoyaltyBonus { get; set; }
        public string MentorPoolBonus { get; set; }

        public string LegendBonus { get; set; }



        public string Pmode { get; set; }
        public string Total_ROI { get; set; }
        public string Total_Direct_Income { get; set; }
        public string Total_Reward { get; set; }

        public string POPUPIMAGE { get; set; }
        public string Total_Binary_Income { get; set; }
        public string Total_Speed_Bonus { get; set; }
        public string Total_Booster_Club { get; set; }
        public string Total_Team { get; set; }
        public string Total_Earning { get; set; }
        public string Fundwallet { get; set; }
        public string Total_Withdraw { get; set; }
        public string Total_level { get; set; }
        public string Total_Franchiseincome { get; set; }
        public string Total_royalty { get; set; }
        public string Total_booster { get; set; }
        public string Total_Investment { get; set; }
        public string Pending_Withdraw { get; set; }
        public string Total_Direct_Referral { get; set; }
        public string Total_MonthlyIncentive { get; set; }
        public string Total_PoolDirect { get; set; }
        public string Total_PoolLevel { get; set; }
        public string Total_Pool { get; set; }

        public string Daily_Profil { get; set; }

        public string Left_Team { get; set; }
        public string Right_Team { get; set; }
        public string Left_Team_Business { get; set; }
        public string Right_Team_Business { get; set; }

        public string Last_Package { get; set; }

        public string Reg_CountDown { get; set; }
        public string Reg_CountStatus { get; set; }
        public string Speed_CountDown { get; set; }
        public string Speed_CountStatus { get; set; }

        public string Booster_Stastus { get; set; }

        public string Total_Balance { get; set; }
        public string TopUpWallet { get; set; }
        public string activation_date { get; set; }


        public string Growth_Wallet { get; set; }
        public string Matching_Wallet { get; set; }
        public string Referral_Wallet { get; set; }


        [Required(ErrorMessage = "Currency Type is Required", AllowEmptyStrings = false)]
        public string BTC_Type { get; set; }
        public string userrank { get; set; }

        public string USD_Amount { get; set; }
        public string BTC_Amount { get; set; }

        public string Direct_team { get; set; }
        public string Total_Principle { get; set; }
        public string Total_Team_Business { get; set; }
        public string Total_BUSINESS { get; set; }
        public string ac_status_Monthly { get; set; }
        public string ac_status_Royalty_bonus { get; set; }
        public string credit_date { get; set; }
        public string roigenratesecond { get; set; }
        public string roigenratesecond2 { get; set; }
        public string roigenratesecond3 { get; set; }
        public string pkg1 { get; set; }
        public string pkg2 { get; set; }
        public string pkg3 { get; set; }
        public string btncount50 { get; set; }
        public string btncount1000 { get; set; }
        public string btncount500 { get; set; }
        public string Total_casino_token { get; set; }
        public string Total_level_ROI { get; set; }
        public List<NewsModel> NewsList { get; set; }

    }

    public class NewsModel
    {
        public string srno { get; set; }
        public string sub { get; set; }
        public string heading { get; set; }
        public string msg { get; set; }
        public string ttime { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string news_image { get; set; }

        //-----------Franchise--------//
        public string Name { get; set; }
        public string Mobile_nu { get; set; }
        public string Phonepay { get; set; }
        public string State { get; set; }
    }

    public class DepositeModel
    {
        public string commit_no { get; set; }
        public string ttime { get; set; }
        public string amount { get; set; }
        public string Provide_Status { get; set; }
        public string minLimit { get; set; }
        public string maxLimit { get; set; }
        public decimal LastProvide { get; set; }
        public string address { get; set; }
        public string btsAmount { get; set; }
        public string provide_id { get; set; }
        public string provide_by { get; set; }
        public string BTC_Type { get; set; }
        public string plan_type { get; set; }

        public string charges { get; set; }
        public string deductable_amount { get; set; }

        public string EMail { get; set; }
        public string Memb_Name { get; set; }

        public string fromUser { get; set; }

        public string username { get; set; }

    }
    public class FundRequest
    {
        public string srno { get; set; }
        public string MEMB_CODE { get; set; }

        [Required(ErrorMessage = "Amount is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]\\d*(\\.\\d+)?TRX", ErrorMessage = "Enter Valid Amount")]
        public string amount { get; set; }
        public string flag { get; set; }
        public string UserName { get; set; }
        //public string Amount { get; set; }
        public string tdate { get; set; }
        public string ttime { get; set; }

        [Required(ErrorMessage = "Attachment is Required", AllowEmptyStrings = false)]
        public string Attachment { get; set; }

        //public string USERNAME { get; set; }
        public string MEMB_NAME { get; set; }
        public string EMAIL { get; set; }
        public string MOBILENO { get; set; }
        public string BalanceAmount { get; set; }
        public string growthwallet { get; set; }
        public string Fund_Wallet { get; set; }
        public string Main_Wallet { get; set; }
        public string workingwallet { get; set; }

        public string btcamount { get; set; }
        public string BTC_Type { get; set; }
        public string DR { get; set; }
        public string Provide_Status { get; set; }
        public string USD_Amount { get; set; } 
        public string txtHash { get; set; }

    }





    public class BankDetailsModel
    {
        public string request_code { get; set; }
        public string debit_card_no { get; set; }
        public string transit_no { get; set; }
        [Required(ErrorMessage = "Please Enter Account Number", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]+TRX", ErrorMessage = "Enter Valid Account No")]
        public string ac_no { get; set; }

        [Required(ErrorMessage = "Please Enter Account Holder Name", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z\\s]+TRX", ErrorMessage = "Enter Valid Account Name")]
        public string ac_name { get; set; }

        [Required(ErrorMessage = "Please Enter Bank Name", AllowEmptyStrings = false)]
        public string bk_name { get; set; }

        [Required(ErrorMessage = "Please Enter Branch Name", AllowEmptyStrings = false)]
        public string bk_branch { get; set; }

        [Required(ErrorMessage = "Please Enter IFSC Code", AllowEmptyStrings = false)]
        [RegularExpression("^[a-z0-9A-Z]+TRX", ErrorMessage = "Enter Valid IFSC Code")]
        public string bk_ifsc { get; set; }

        //  [Required(ErrorMessage = "Please Select Account Type", AllowEmptyStrings = false)]
        public string ac_type { get; set; }

        // [Required(ErrorMessage = "Please Enter Google pay no", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]+TRX", ErrorMessage = "Enter Valid Google pay")]
        public string gpay_no { get; set; }

        //[Required(ErrorMessage = "Please Enter Phone pay no", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]+TRX", ErrorMessage = "Enter Valid Phonepe No")]
        public string phonepay_no { get; set; }

        // [Required(ErrorMessage = "Please Enter Paytm no", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]+TRX", ErrorMessage = "Enter Valid Paytm No")]
        public string paytm_no { get; set; }

        //[Required(ErrorMessage = "Enter OTP Details", AllowEmptyStrings = false)]
        //public string request_code { get; set; }

        [Required(ErrorMessage = "Mobile No is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]+TRX", ErrorMessage = "Enter Valid Mobile No")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Enter Valid Mobile No")]
        public string bank_mobile_no { get; set; }


        [Required(ErrorMessage = "Pin Code is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]+TRX", ErrorMessage = "Enter Valid Pin Code")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Enter Valid Pin Code")]
        public string Pin_Code { get; set; }

        public string srno { get; set; }
        public string beneficiaryid { get; set; }
        public string beneficiaryid_corporate { get; set; }
        public string agent_id { get; set; }
        public string customer_id { get; set; }

        //    [Required(ErrorMessage = "Address is Required", AllowEmptyStrings = false)]
        public string Bank_Address { get; set; }

        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string OTP { get; set; }

        public string bankid { get; set; }
        public string BANKCODE { get; set; }
        public string IMPS { get; set; }
        public string NEFT { get; set; }
        public string ACVERIFYAVAIL { get; set; }
        public string STATUS { get; set; }

    }



    public class WithdrawalModel
    {
        public string tdate { get; set; }
        public string btcac { get; set; }
        public string amount { get; set; }
        public string charges { get; set; }
        public string Tds_charges { get; set; }
        public string paid_amount { get; set; }
        public string gst { get; set; }
        public string final_paid_amount { get; set; }
        public string withdrawal_in { get; set; }
        public string deductamt { get; set; }
        public string flag { get; set; }
        public string wstatus { get; set; }
        public string flag_date { get; set; }
        public string INR_Amt { get; set; }
        public string Tstatus { get; set; }
    }

    public class GetHelpModel
    {
        public string OrderID { get; set; }
        public string OrderNo { get; set; }
        public string tdate { get; set; }
        public string amount { get; set; }
        public string amountx { get; set; }
        public string Get_Status { get; set; }

        public string Total_Balance { get; set; }
        public string Total_Wallet_Balance { get; set; }
        public string matching_amt { get; set; }
    }

    public class UserLevelModel
    {
        public string reg_date { get; set; }
        public string auth_date { get; set; }
        public string memb_name { get; set; }
        public string username { get; set; }
        public string m_country { get; set; }
        public string email { get; set; }
        public string level_no { get; set; }
        public string mobile_number { get; set; }

        public string sp_user { get; set; }
        public string sp_name { get; set; }
        public string confirm_ph { get; set; }
        public string unconfirm_ph { get; set; }

        public string Place { get; set; }
        public string Total_Business { get; set; }
        public string ac_status { get; set; }
        public string activation_date { get; set; }

        public string LEVELMATRIX1 { get; set; }
        public string LEVELMATRIX2 { get; set; }
        public string LEVELMATRIX3 { get; set; }
        public string LEVELMATRIX4 { get; set; }
        public string LEVELMATRIX5 { get; set; }
        public string LEVELMATRIX6 { get; set; }
        public string LEVELMATRIX7 { get; set; }
        public string LEVELMATRIX8 { get; set; }
        public string LEVELMATRIX9 { get; set; }
        public string LEVELMATRIX10 { get; set; }
        public string LEVELMATRIX11 { get; set; }
        public string LEVELMATRIX12 { get; set; }
        public string LEVELMATRIX13 { get; set; }
        public string LEVELMATRIX14 { get; set; }
        public string LEVELMATRIX15 { get; set; }
        public string LEVELMATRIX16 { get; set; }
        public string LEVELMATRIX17 { get; set; }
        public string LEVELMATRIX18 { get; set; }
        public string LEVELMATRIX19 { get; set; }
        public string LEVELMATRIX20 { get; set; }
        public string credit_date { get; set; }
        public string Stage_entry { get; set; }

        //public string mobile_number { get; set; }
    }

    public class FundTransferModel
    {
        public string created_at { get; set; }
        public string provide_id { get; set; }
        public string provide_by { get; set; }
        public string amount { get; set; }
        public List<WalletModel> TrnsferList { get; set; }

        public List<WalletModel> ReceiveList { get; set; }
    }

    public class WalletModel
    {
        public string flag { get; set; }
        public string created_at { get; set; }
        public string provide_by { get; set; }
        public string provide_id { get; set; }
        public string amount { get; set; }
        public string transaction_by { get; set; }
        public string balance_amt { get; set; }
        public string level_no { get; set; }
        public string username { get; set; }
        public string clubtype { get; set; }
        public string Provide_Status { get; set; }


        public string amount_CR { get; set; }
        public string amount_DR { get; set; }
        public string Description { get; set; }
        public string Package { get; set; }

        public string requestamt { get; set; }
        public string paid { get; set; }
        public string charges { get; set; }
        public string mobile_number { get; set; }
        public string StarBonus { get; set; }
        public string RoyeltyBonus { get; set; }
        public string MemntorPoolBonus { get; set; }
        public string status { get; set; }
        public string emailid { get; set; }

        public string Week_count { get; set; }


    }

    public class ADSDetailsModel
    {
        public string ads_id { get; set; }
        public string ads_title { get; set; }
        public string ads_type { get; set; }
        public string ads_attahment { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        public string option_ans { get; set; }

        public string ads_date { get; set; }
        public string flag { get; set; }
        public string view_date { get; set; }

        public string View_Ads { get; set; }
        public string Pending_Ads { get; set; }
        public string Total_Ads { get; set; }
        public string Expired_Ads { get; set; }
        public string Total_Revenue { get; set; }
        public string Ads_Amount { get; set; }
        public string commit_no { get; set; }
    }

    public class BTCAddressModel
    {
        public string BTCAddress { get; set; }
        public string pubkey { get; set; }
        public string result { get; set; }
        public string Currency { get; set; }
        public string txn_id { get; set; }
        public string confirms_needed { get; set; }
        public string timeout { get; set; }
        public string checkout_url { get; set; }
        public string status_url { get; set; }
        public string qrcode_url { get; set; }
        public string amount { get; set; }
        public int time_created { get; set; }
        public int time_expires { get; set; }
        public string status { get; set; }
        public string status_text { get; set; }
        public string type { get; set; }
        public string coin { get; set; }
        public string amountf { get; set; }
        public string receivedf { get; set; }
        public string recv_confirms { get; set; }
        public string received { get; set; }
        public string payment_address { get; set; }


    }

    public class TransactionModel
    {
        [Required(ErrorMessage = "User Name is Required", AllowEmptyStrings = false)]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Purchase By is Required", AllowEmptyStrings = false)]
        public string BTC_Type { get; set; }


        //  [Required(ErrorMessage = "Topup By is Required", AllowEmptyStrings = false)]
        public string Topup_Type { get; set; }

        public string Password { get; set; }

        // [Required(ErrorMessage = "Withdrawal in is Required", AllowEmptyStrings = false)]
        public string Withdrawal_In { get; set; }


        public string BTC_Amount { get; set; }
        public string INR_Amount { get; set; }// Add Shri


        [Required(ErrorMessage = "Package is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]*TRX", ErrorMessage = "Enter Valid Amount")]
        [Range(5, 100000, ErrorMessage = "Enter Only Amount TRX 30 ")]
        public string USD_Amount { get; set; }

        public string coinAMT { get; set; }
        [Required(ErrorMessage = "Plan type is Required", AllowEmptyStrings = false)]
        public string Plan_Type { get; set; }

        //   [Required(ErrorMessage = "Attachment is Required", AllowEmptyStrings = false)]
        public string attachment { get; set; }


        [Required(ErrorMessage = "Amount is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]\\d*(\\.\\d+)?TRX", ErrorMessage = "Enter Valid Amount")]
        public string Amount { get; set; }

        public string Main_Wallet { get; set; }
        public string Fund_Wallet { get; set; }
        public string Pool_Wallet { get; set; }
        public string Pool_Balance { get; set; }
        public string ROI_Balance { get; set; }
        public string Principle_BalanceAmount { get; set; }
        public string Working_BalanceAmount { get; set; }
        public string reward_BalanceAmount { get; set; }
        public string BalanceAmount { get; set; }


        //[Required(ErrorMessage = "Request Code is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*TRX", ErrorMessage = "Enter Valid Request Code")]
        public string Request_Code { get; set; }
        public string address1 { get; set; }
        public string sp_user { get; set; }
        public string Memb_Name { get; set; }
        public string nodays { get; set; }
        public string pool { get; set; }
        public string PinID { get; set; }
        public string authrised { get; set; }

        public string Last_Package { get; set; }

    }

    public class FundTransModel
    {
        [Required(ErrorMessage = "Wallet is Required", AllowEmptyStrings = false)]
        public string Wallet_Type { get; set; }

        [Required(ErrorMessage = "User Name is Required", AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Transfer By is Required", AllowEmptyStrings = false)]
        public string BTC_Type { get; set; }

        [Required(ErrorMessage = "Amount is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]\\d*(\\.\\d+)?TRX", ErrorMessage = "Enter Valid Amount")]
        public string Amount { get; set; }




        public string Attachment { get; set; }



        public string BalanceAmount { get; set; }
        public string Main_Wallet { get; set; }
        public string Fund_Wallet { get; set; }

        public string ROI_Balance { get; set; }
        public string Principle_BalanceAmount { get; set; }
        public string Direct_Balance { get; set; }
        public string Binary_Balance { get; set; }


        //[Required(ErrorMessage = "Request Code is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]*TRX", ErrorMessage = "Enter Valid Request Code")]
        //public string Request_Code { get; set; }
    }

    public class BinaryModel
    {
        public string Left_Team { get; set; }
        public string Right_Team { get; set; }
        public string Left_Team_Business { get; set; }
        public string Right_Team_Business { get; set; }

        public string Left_Team_BusinessUSD { get; set; }
        public string Right_Team_BusinessUSD { get; set; }

        public string M1 { get; set; }
        public string M2 { get; set; }
        public string M3 { get; set; }
        public string M4 { get; set; }
        public string M5 { get; set; }
        public string M6 { get; set; }
        public string M7 { get; set; }
        public string M8 { get; set; }
        public string M9 { get; set; }
        public string M10 { get; set; }
        public string M11 { get; set; }
        public string M12 { get; set; }
        public string M13 { get; set; }
        public string M14 { get; set; }
        public string M15 { get; set; }
        public string U1 { get; set; }
        public string U2 { get; set; }
        public string U3 { get; set; }
        public string U4 { get; set; }
        public string U5 { get; set; }
        public string U6 { get; set; }
        public string U7 { get; set; }
        public string U8 { get; set; }
        public string U9 { get; set; }
        public string U10 { get; set; }
        public string U11 { get; set; }
        public string U12 { get; set; }
        public string U13 { get; set; }
        public string U14 { get; set; }
        public string U15 { get; set; }
    }

    public class BinaryReportModel
    {
        public string PAYOUT_NO { get; set; }
        public string CLOSING { get; set; }
        public string Payout_Date { get; set; }
        public string Memb_Code { get; set; }
        public string PLEFT { get; set; }
        public string PRIGHT { get; set; }
        public string PAIR { get; set; }
        public string PAID_PAIR { get; set; }
        public string CLEFT { get; set; }
        public string CRIGHT { get; set; }
        public string AMOUNT { get; set; }
        public string CAPPING { get; set; }
        public string PAID_AMT { get; set; }
        public string POWER_AMT { get; set; }
        public string LEFT_JOIN { get; set; }
        public string RIGHT_JOIN { get; set; }
        public string ldb_point { get; set; }
        public string ldb_point_value { get; set; }
        public string ldb_point_rate { get; set; }
        public string JCODE { get; set; }
        public string tds { get; set; }
        public string process { get; set; }

        public string MEMB_NAME { get; set; }
    }



    public class ReportModel
    {
        public string amount { get; set; }
        public string ph_amount { get; set; }
        public string ph_date { get; set; }
        public string tdate { get; set; }
        public string from_name { get; set; }
        public string from_userid { get; set; }
        public string order_no { get; set; }
        public string level_no { get; set; }
        public string levelPer { get; set; }

        public string created_at { get; set; }
        public string provide_by { get; set; }
        public string provide_id { get; set; }
        public string REMARK { get; set; }
        public string username { get; set; }
        public string MEMB_Name { get; set; }
        public string Mobile_no { get; set; }
        public string INR_AMT { get; set; }
    }

    public class UserDetailsModel
    {
        public string username { get; set; }
        public string Memb_Name { get; set; }
        public string EMail { get; set; }
        public string sp_username { get; set; }
        public string sp_Memb_Name { get; set; }
        public string sp_EMail { get; set; }
        public string Left_Team { get; set; }
        public string Right_Team { get; set; }
        public string Left_Team_Business { get; set; }
        public string Right_Team_Business { get; set; }

        public string Left_Team_BusinessUSD { get; set; }
        public string Right_Team_BusinessUSD { get; set; }

        public string Total_Business { get; set; }
        public string Total_BusinessUSD { get; set; }

        public string Joinind_Date { get; set; }
    }

    public class WithdrawModel
    {
        [Required(ErrorMessage = "Wallet is Required", AllowEmptyStrings = false)]
        public string Wallet_Type { get; set; }

        [Required(ErrorMessage = "Withdrawal in is Required", AllowEmptyStrings = false)]
        public string Withdrawal_In { get; set; }

        //[Required(ErrorMessage = "Amount is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]\\d*(\\.\\d+)?TRX", ErrorMessage = "Enter Valid Amount")]
        public string Amount { get; set; }

        public string remark { get; set; }
        public string bank_id { get; set; }
        public string request_mobile_no { get; set; }

        //[Required(ErrorMessage = "Request Code is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]*TRX", ErrorMessage = "Enter Valid Request Code")]
        public string Request_Code { get; set; }

        public string BalanceAmount { get; set; }
        public string Working_BalanceAmount { get; set; }
        public string Principle_BalanceAmount { get; set; }
        public string reward_BalanceAmount { get; set; }
        public string ROI_Balance { get; set; }
        public string Direct_Balance { get; set; }
        public string Binary_Balance { get; set; }
        public string PaidAmount { get; set; }
        public string Password { get; set; }
        public string USERNAME { get; set; }
        public string Memb_Name { get; set; }
    }


    public class PINDETAILSMODEL
    {
        public string PinID { get; set; }
        public string Memb_Code { get; set; }

        [Required(ErrorMessage = "Pin No is Required", AllowEmptyStrings = false)]
        public string Pin { get; set; }


        public string u_date { get; set; }

        public string Flag { get; set; }
        public string Commit_No { get; set; }
        public string TF_Date { get; set; }
        public string from_ID { get; set; }
        public string Tdate { get; set; }
        public string tf_flag { get; set; }

        [Required(ErrorMessage = "Amount is Required", AllowEmptyStrings = false)]
        public string amount { get; set; }


        public string u_flag { get; set; }

        [Required(ErrorMessage = "User ID is Required", AllowEmptyStrings = false)]
        public string username { get; set; }


        public string Memb_Name { get; set; }
        public string EMail { get; set; }
        public string Main_Wallet { get; set; }


        public string Available_Pin { get; set; }



        [Required(ErrorMessage = "Quantity is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*TRX", ErrorMessage = "Enter Valid Quantity")]
        public string Quantiy { get; set; }

    }

    public class PinTransferModel
    {
        public List<PINDETAILSMODEL> trnsferHistory { get; set; }
        public List<PINDETAILSMODEL> receiveHistory { get; set; }
    }


    public class GetRentalJsonData
    {
        public string NEXTDATE { get; set; }
        public string CURRENTWEEK { get; set; } = "0";

    }


    public class SetRentalJsonData
    {
        public string STATUS { get; set; }
        public string AMOUNT { get; set; }

        public string WeekNo { get; set; }

    }
}

//mlmdiary2005@gmail.com