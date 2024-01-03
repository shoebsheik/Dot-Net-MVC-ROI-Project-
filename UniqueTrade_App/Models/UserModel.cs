using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniqueTrade_App.Models
{
    public class UserModel
    {
        public string request_code { get; set; }
        public string memb_code { get; set; }
        public string Spon_Code { get; set; }
        public string Plac_Code { get; set; }
        public string totmeb { get; set; }
        public string confirm_ph { get; set; }

        [Required(ErrorMessage = "Place is Required", AllowEmptyStrings = false)]
        public string Place { get; set; }
        public string Reg_Date { get; set; }
        public string Reg_Time { get; set; }
        public string MembName_F { get; set; }
        public string MembName_M { get; set; }
        public string MembName_L { get; set; }


        [Required(ErrorMessage = "Name is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Enter Valid Name")]
        public string Memb_Name { get; set; }

        [Required(ErrorMessage = "Gender is Required", AllowEmptyStrings = false)]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Mobile No is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Enter Valid Mobile No")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Enter Valid Mobile No")]
        public string Mobile_No { get; set; }



        public string Phone_No { get; set; }


        [EmailAddress(ErrorMessage = "Enter Valid Email Id")]
        [Required(ErrorMessage = "Email Id is Required", AllowEmptyStrings = false)]
        [RegularExpression("^([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z\\-])+\\.)+([a-zA-Z]{2,6})$", ErrorMessage = "Enter Valid Email Id")]
        public string EMail { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }


        [Required(ErrorMessage = "Country is Required", AllowEmptyStrings = false)]
        public string M_COUNTRY { get; set; }
        public string State { get; set; }
        public string District { get; set; }

        [Required(ErrorMessage = "City is Required")]
        public string City { get; set; }
        public string Pin_Code { get; set; }
        public string Reg_Amt { get; set; }

        [StringLength(30, ErrorMessage = "User Id length must be minimum 30 characters")]
        [Required(ErrorMessage = "User Id is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[a-z0-9A-Z]+$", ErrorMessage = "Enter Valid User Id")]
        public string username { get; set; }


        public string usernameEncrypt { get; set; }
        public string mpwd { get; set; }
        public string RV_Code { get; set; }
        public string PIN_ID { get; set; }
        public string pin_no { get; set; }
        public string authrised { get; set; }
        public string auth_date { get; set; }
        public string M_Status { get; set; }
        public string MPOSITION { get; set; }
        public string FLAG { get; set; }
        public string TEMPF { get; set; }
        // public string REMARK { get; set; }
        public string client_ip { get; set; }
        public string last_log_in { get; set; }
        public string dbo { get; set; }


        //public bool istrue { get { return true; } }
        //[Required(ErrorMessage = "Terms and Condition is Required")]
        //[Compare("istrue", ErrorMessage = "Terms and Condition is Required")]


        //[Range(typeof(bool), "true", "true", ErrorMessage = "Terms and Condition is Required")]
        public bool checkbox { get; set; }



        [Required(ErrorMessage = "Current Password is Required")]
        public string OldPass { get; set; }

        [Required(ErrorMessage = "New Password is Required")]
        [StringLength(30, MinimumLength = 7, ErrorMessage = "Password length must be minimum 7 characters")]
        [RegularExpression("[a-zA-Z0-9.^;<>?|=%*#$@!+&_]*$", ErrorMessage = "Invalid Password")]
        public string NewPass { get; set; }

        [Required(ErrorMessage = "Re-type New Password is Required")]
        [Compare("NewPass", ErrorMessage = "The password and re-type password not match.")]
        public string ConfirmPass { get; set; }



        //------------ Account ------------//
        //[Required(ErrorMessage = "Bitcoin Address is Required")]
        [RegularExpression("^[A-Z0-9a-z]+$", ErrorMessage = "Enter Valid Bitcoin Address")]
        [StringLength(40, MinimumLength = 30, ErrorMessage = "Enter Valid Bitcoin Address")]
        public string btc_ac { get; set; }

        //[Required(ErrorMessage = "Ethereum Address is Required")]
        [RegularExpression("^(0x)?[0-9a-fA-F]{40}$", ErrorMessage = "Enter Valid Ethereum Address")]
        public string eth_ac { get; set; }

        //[Required(ErrorMessage = "Litecoin Address is Required")]
        [RegularExpression("^[A-Z0-9a-z]+$", ErrorMessage = "Enter Valid Litecoin Address")]
        [StringLength(40, MinimumLength = 30, ErrorMessage = "Enter Valid Litecoin Address")]
        public string ltc_ac { get; set; }
        public string accStatus { get; set; }

        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Enter Valid Account Name")]
        public string ac_name { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Enter Valid Account No")]
        public string ac_no { get; set; }

        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Enter Valid Bank Name")]
        public string bk_name { get; set; }

        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Enter Valid Branch Name")]
        public string bk_branch { get; set; }

        [RegularExpression("^[a-z0-9A-Z]+$", ErrorMessage = "Enter Valid IFSC Code")]
        public string bk_ifsc { get; set; }
        public string debit_card_no { get; set; }
        public string ac_type { get; set; }



        public string level_no { get; set; }

        [Required(ErrorMessage = "Sponsor User Id is Required")]
        public string sp_user { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }

        public string remark { get; set; }
        public string tdate { get; set; }
        public string credit_date { get; set; }
        public string amount { get; set; }
        public string status { get; set; }

        public string hsrno { get; set; }
    }

    public class NewsLetter
    {
        // [EmailAddress(ErrorMessage = "Enter Valid Email Id")]
        [Required(ErrorMessage = "Email Id is Required", AllowEmptyStrings = false)]
        [RegularExpression("^([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z\\-])+\\.)+([a-zA-Z]{2,6})$", ErrorMessage = "Enter Valid Email Id")]
        public string email { get; set; }
    }

    public class ContactEnquiry
    {
        [Required(ErrorMessage = "Name is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Enter Valid Name")]
        public string username { get; set; }

        //[EmailAddress(ErrorMessage = "Enter Valid Email Id")]
        [Required(ErrorMessage = "Email Id is Required", AllowEmptyStrings = false)]
        [RegularExpression("^([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z\\-])+\\.)+([a-zA-Z]{2,6})$", ErrorMessage = "Enter Valid Email Id")]
        public string email { get; set; }

        [Required(ErrorMessage = "Subject is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[a-z0-9A-Z\\s]+$", ErrorMessage = "Enter Valid Subject")]
        public string subject { get; set; }

        [Required(ErrorMessage = "Message is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[a-z0-9A-Z\\s]+$", ErrorMessage = "Enter Valid Message")]
        public string message { get; set; }

        [Required(ErrorMessage = "Mobile No is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Enter Valid Mobile No")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Enter Valid Mobile No")]
        public string phone_no { get; set; }
        public string Popup { get; set; }
        public string Pmode { get; set; }




        public List<NewsModel> Newslist { get; set; }
    }

    public class SignInModel
    {
        [StringLength(30, ErrorMessage = "User Id length must be minimum 30 characters")]
        [Required(ErrorMessage = "User Id is Required", AllowEmptyStrings = false)]
        public string username { get; set; }


        //[StringLength(16, MinimumLength = 7, ErrorMessage = "Password length must be minimum 7 characters")]
        [Required(ErrorMessage = "Password is Required", AllowEmptyStrings = false)]
        public string mpwd { get; set; }


        //[Required(ErrorMessage = "Request Code is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Enter Valid Request Code")]
        public string Request_Code { get; set; }
    }

    public class ForgetPasswordModel
    {
        [StringLength(30, ErrorMessage = "User Id length must be minimum 30 characters")]
        [Required(ErrorMessage = "User Id is Required", AllowEmptyStrings = false)]
        public string username { get; set; }

        //[EmailAddress(ErrorMessage = "Enter Valid Email Id")]
        //[Required(ErrorMessage = "Email Id is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z\\-])+\\.)+([a-zA-Z]{2,6})$", ErrorMessage = "Enter Valid Email Id")]
        //public string Email { get; set; }

    }

    public class RegistrationModel
    {
        [Required(ErrorMessage = "Name is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Enter Valid Name")]
        public string Memb_Name { get; set; }


        [Required(ErrorMessage = "Mobile No is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Enter Valid Mobile No")]
        [StringLength(16, MinimumLength = 10, ErrorMessage = "Enter Valid Mobile No")]
        public string Mobile_No { get; set; }
        public string Phone_No { get; set; }
        //[EmailAddress(ErrorMessage = "Enter Valid Email Id")]
        [Required(ErrorMessage = "Email Id is Required", AllowEmptyStrings = false)]
        [RegularExpression("^([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z\\-])+\\.)+([a-zA-Z]{2,6})$", ErrorMessage = "Enter Valid Email Id")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Sponsor User Id is Required")]
        public string sp_user { get; set; }

        [Required(ErrorMessage = "Sponsor User Id is Required")]
        public string username { get; set; }

        [Required(ErrorMessage = "Country Code is Required")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Please Select Country")]
        public string Country { get; set; }

        public string Place { get; set; }
        public string MembName_L { get; set; }
        //[Required(ErrorMessage = "Password is required")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string mpwd { get; set; }


        //[Required(ErrorMessage = "Confirm Password is required")]
        //[Compare("mpwd", ErrorMessage = "The password and confirmation password do not match.")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("mpwd")]
        public string ConfirmPass { get; set; }
        public string Aadhar_No { get; set; }
        
    }



    public class SponsorDetailsModel
    {
        public string Memb_Name { get; set; }
    }

    public class EditProfileModel
    {
        [Required(ErrorMessage = "Name is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Enter Valid Name")]
        public string Memb_Name { get; set; }

        [Required(ErrorMessage = "Gender is Required", AllowEmptyStrings = false)]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Mobile No is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Enter Valid Mobile No")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Enter Valid Mobile No")]
        public string Mobile_No { get; set; }

        public string EMail { get; set; }
        

        public string EmailID { get; set; }

        public string MembName_L { get; set; }

        [Required(ErrorMessage = "USDT Address is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[A-Z0-9a-z]+$", ErrorMessage = "Enter Valid  Address")]
        [StringLength(50, MinimumLength = 50, ErrorMessage = "Enter Valid  Address")]
        public string Address1 { get; set; }
        public string dbo { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "Country is Required", AllowEmptyStrings = false)]
        public string M_COUNTRY { get; set; }

        [Required(ErrorMessage = "Country Code is Required", AllowEmptyStrings = false)]
        public string CountryCode { get; set; }


        //------------ Account ------------//
        //[Required(ErrorMessage = "Bitcoin Address is Required")]
        [RegularExpression("^[A-Z0-9a-z]+$", ErrorMessage = "Enter Valid Bitcoin Address")]
        [StringLength(50, MinimumLength = 32, ErrorMessage = "Enter Valid Bitcoin Address")]
        public string btc_ac { get; set; }

        //[Required(ErrorMessage = "Ethereum Address is Required")]
        //[RegularExpression("^(0x)?[0-9a-fA-F]{40}$", ErrorMessage = "Enter Valid Ethereum Address")]
        public string eth_ac { get; set; }

        //[Required(ErrorMessage = "Litecoin Address is Required")]
        //[RegularExpression("^[A-Z0-9a-z]+$", ErrorMessage = "Enter Valid Litecoin Address")]
        [StringLength(40, MinimumLength = 30, ErrorMessage = "Enter Valid Litecoin Address")]
        public string ltc_ac { get; set; }
    
           

       // [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Enter Valid Account Name")]
        public string ac_name { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Enter Valid Account No")]
        public string ac_no { get; set; }

      //  [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Enter Valid Bank Name")]
        public string bk_name { get; set; }

       // [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Enter Valid Branch Name")]
        public string bk_branch { get; set; }

        //[RegularExpression("^[a-z0-9A-Z]+$", ErrorMessage = "Enter Valid IFSC Code")]
        public string bk_ifsc { get; set; }

        //[Required(ErrorMessage = "Phone Pay No is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]+$", ErrorMessage = "Enter Valid Phone Pay No")]
        //[StringLength(16, MinimumLength = 8, ErrorMessage = "Enter Valid Phone Pay No")]
        public string PhonePay { get; set; }

        //[Required(ErrorMessage = "Google Pay No is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]+$", ErrorMessage = "Enter Valid Google Pay No")]
        //[StringLength(16, MinimumLength = 8, ErrorMessage = "Enter Valid Google Pay No")]
        public string googlepay { get; set; }

        //[Required(ErrorMessage = "BHIM UPI No is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]+$", ErrorMessage = "Enter Valid BHIM UPI No")]
        //[StringLength(16, MinimumLength = 8, ErrorMessage = "Enter Valid BHIM UPI No")]
        public string bhimno { get; set; }

        //[Required(ErrorMessage = "PAYTM No is Required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]+$", ErrorMessage = "Enter Valid PAYTM No")]
        //[StringLength(16, MinimumLength = 8, ErrorMessage = "Enter Valid PAYTM No")]
        public string Paytmno { get; set; }
        public string debit_card_no { get; set; }
        public string ac_type { get; set; }
        //Add shri
        public string aadhar_card { get; set; }
      
        public string pan_card { get; set; }

        public string inpType { get; set; }



        [Required(ErrorMessage = "BUSD Address is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[A-Z0-9a-z]+$", ErrorMessage = "Enter Valid  Address")]
        [StringLength(50, MinimumLength = 50, ErrorMessage = "Enter Valid  Address")]
        public string Address2 { get; set; }

    }

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Old Password is Required")]
        public string OldPass { get; set; }

        [Required(ErrorMessage = "New Password is Required")]
        [StringLength(30, MinimumLength = 7, ErrorMessage = "Password length must be minimum 7 characters")]
        [RegularExpression("[a-zA-Z0-9.^;<>?|=%*#$@!+&_]*$", ErrorMessage = "Invalid Password")]
        public string NewPass { get; set; }

        [Required(ErrorMessage = "Re-type New Password is Required")]
        [Compare("NewPass", ErrorMessage = "The password and re-type password not match.")]
        public string ConfirmPass { get; set; }
    }
    public class ChangeTransactionPassModel
    {
        [Required(ErrorMessage = "Old Password is Required")]
        public string OldTranPass { get; set; }

        [Required(ErrorMessage = "New Password is Required")]
        [StringLength(30, MinimumLength = 7, ErrorMessage = "Password length must be minimum 7 characters")]
        [RegularExpression("[a-zA-Z0-9.^;<>?|=%*#$@!+&_]*$", ErrorMessage = "Invalid Password")]
        public string NewTranPass { get; set; }

        [Required(ErrorMessage = "Re-type New Password is Required")]
        [Compare("NewTranPass", ErrorMessage = "The password and re-type password not match.")]
        public string ConfirmTranPass { get; set; }
    }
}