using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class MailingViewModel
    {
        public int MailingID { get; set; }

        public DateTime UpdateDate { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsSent { get; set; }
    }
}