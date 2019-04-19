using Oc.Carbon.Common.Contracts;
using Oc.Carbon.DataAccess;
using Oc.Carbon.DTO.Mapping.Core;
using Oc.Carbon.DTO.PlatformDTO;
using Oc.Carbon.DTO.SolutionDTO;
using Oc.Carbon.ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Oc.Carbon.WebServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrgController : ApiController
    {
        /// <summary>
        /// Add the Following....
        ///     Create Customer Sign Up
        ///     Create Reseller Sign Up
        ///     Update Customer
        ///     Update Reseller
        ///     Hold Customer
        ///     Hold Reseller
        /// 
        /// </summary>

        ILogService _loggerService;
        IOrgService _OrgService;
        IOrgcustService _IOrgcustService;
        IOrgresellerService _IOrgresellerService;
        IOrgorgService _IOrgorgService;
        public OrgController(ILogService loggerService, IOrgService orgService, IOrgcustService orgCustService,
            IOrgresellerService orgResellerService, IOrgorgService orgorgService)
        {
            this._loggerService = loggerService;
            this._OrgService = orgService;

            _IOrgcustService = orgCustService;
            _IOrgresellerService = orgResellerService;
            _IOrgorgService = orgorgService;
        }

        
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetOrgs()
        {
            var orgs = _OrgService.GetOrgs().Where(p=> p.InactiveDate == null);

            var result = PlatformMappingHelper.Map<IList<Org>, IList<OrgDTO>>(orgs.ToList()).ToList();

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<OrgDTO>>(HttpStatusCode.OK, result);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetOrgStructure(int? parentId)
        {
            int orgid=0;
            if (parentId == null)
            {
                orgid = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());
            }
            else
            {
                orgid = int.Parse(parentId.ToString());
            }
            var orgTree = _OrgService.GetOrgStructure(orgid);
            if (orgTree == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<OrgTreeDTO>(HttpStatusCode.OK, orgTree);
        }


        public List<int> GetOrgChildrenString(int? parentId)
        {
            int orgid = 0;
            if (parentId == null)
            {
                orgid = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());
            }
            else
            {
                orgid = int.Parse(parentId.ToString());
            }

            return _OrgService.GetOrgChildrenString(orgid);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetParentOrgs()
        {
            var parentorgs = _OrgService.GetOrgs().Where(p => p.OrgTyp.TypCd != "CUST").Select(q => new { id = q.Id, name = q.Name });
            if (parentorgs == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, parentorgs);
        }


    }
}