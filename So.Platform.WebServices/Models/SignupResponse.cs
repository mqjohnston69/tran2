using Oc.Carbon.DTO.PlatformDTO;
using Oc.Carbon.DTO.SolutionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oc.Carbon.WebServices.Models
{
    public class SignupResponse
    {
        public bool successful { get; set; }

        public string Message { get; set; }

        public OrgDTO Org { get; set; }

        public CustomerDTO Customer { get; set; }

        public ResellerDTO Reseller { get; set; }

        public DTO.SolutionDTO.UserDTO User { get; set; }

        public int WkflowInstanceId { get; set; }
    }
}