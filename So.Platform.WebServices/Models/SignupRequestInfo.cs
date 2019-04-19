using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oc.Carbon.WebServices.Models
{

    public class SignupRequestInfo
    {
        public string id { get; set; }
        public string userType { get; set; }
        public bool agreeUla { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string companyName { get; set; }
        public string addressStreet1 { get; set; }
        public string addressStreet2 { get; set; }
        public string addressCity { get; set; }
        public string addressState { get; set; }
        public string addressZip { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }

        public string contactName { get; set; }
        public string contactTitle { get; set; }
        public string phone { get; set; }
        public Int16 docSubmissionType { get; set; }
        public bool removeBlankImages { get; set; }
        public bool imageCleanup { get; set; }
        public Int16 docSla { get; set; }
        public string captchaResponse { get; set; }
        public Byte[] uploadLogo { get; set; }
        public Byte[] agreement { get; set; }

    }
    public class ResetPWDRequestInfo
    {
        public string userName { get; set; }
    }

}