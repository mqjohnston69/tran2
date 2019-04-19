using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oc.Carbon.WebServices.Models
{
    public class GenericSearchRequest
    {
        public int OrdId { get; set; }

        public string FilterText { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? StartDate { get; set; }

        public int? StatusId { get; set; }

    }
}