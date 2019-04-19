using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oc.Carbon.WebServices.Models
{
    public class InviteRequestInfo
    {
        public int ParentId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}