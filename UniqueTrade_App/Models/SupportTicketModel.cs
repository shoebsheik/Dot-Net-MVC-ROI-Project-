using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniqueTrade_App.Models
{
    public class SupportTicketModel
    {
        public string Memb_Name { get; set; }
        public string username { get; set; }
        public string ttime { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }



        [Required(ErrorMessage = "Message is Required", AllowEmptyStrings = false)]
        public string msg { get; set; }


        public string status { get; set; }



        [Required(ErrorMessage = "Subject is Required", AllowEmptyStrings = false)]
        public string sub { get; set; }

    }

    public class SupportModel
    {
        [Required(ErrorMessage = "Message is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[a-z0-9A-Z\\s]+$", ErrorMessage = "Enter Valid Message")]
        public string msg { get; set; }

        [Required(ErrorMessage = "Subject is Required", AllowEmptyStrings = false)]
        [RegularExpression("^[a-z0-9A-Z\\s]+$", ErrorMessage = "Enter Valid Subject")]
        public string sub { get; set; }
    }
}