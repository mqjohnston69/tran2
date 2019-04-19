using Oc.Carbon.Common.Contracts;
using Oc.Carbon.DataAccess;
using Oc.Carbon.DataAccess.Contracts;
using Oc.Carbon.DTO.Mapping.Core;
using Oc.Carbon.DTO.PlatformDTO;
using Oc.Carbon.ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Oc.Carbon.WebServices.Controllers
{
    public class PlatformAdminController : ApiController
    {
        ILogService _LoggerService;
        IUserService _IUserService;
        IPortService _IPortalService;
        IOrgService _IOrgService;
        IOrgtypService _IOrgtypServicer;

        IPortuserService _IPortalUserService;
        IUnitOfWork _UnitOfWork;
        IAuthrolService _IAuthrolService;


        public PlatformAdminController(ILogService loggerService, IUserService userService, IPortService portalService, IPortuserService portalUserService,
            IUnitOfWork unitOfWork, IOrgService orgService, IOrgtypService orgtypServicer, IAuthrolService authrolService)
        {
            this._LoggerService = loggerService;
            this._IUserService = userService;
            this._IPortalService = portalService;
            this._IPortalUserService = portalUserService;
            this._UnitOfWork = unitOfWork;

            this._IOrgService = orgService;
            this._IOrgtypServicer = orgtypServicer;
            this._IAuthrolService = authrolService;
        }

        #region Users

        // Get All Users
        //[Authorize]
        public HttpResponseMessage GetUsers()
        {
            var users = _IUserService.GetUsers();

            var result = PlatformMappingHelper.Map<IList<User>, IList<UserDTO>>(users.ToList()).ToList();
            
            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<UserDTO>>(HttpStatusCode.OK, result);
        }
        #endregion

        #region Orgs
        public HttpResponseMessage GetOrgs()
        {
            var orgs = _IOrgService.GetOrgs();

            var result = PlatformMappingHelper.Map<IList<Org>, IList<OrgDTO>>(orgs.ToList()).ToList();

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<OrgDTO>>(HttpStatusCode.OK, result);
        }

        //public HttpResponseMessage GetOrgsByOrgTyp(string orgTypCd)
        //{
        //    var orgs = _IOrgService.QueryData().Where(p => p.OrgTyp.TypCd == orgTypCd).ToList();

        //    var result = PlatformMappingHelper.Map<IList<Org>, IList<OrgDTO>>(orgs.ToList()).ToList();

        //    if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
        //    return Request.CreateResponse<IEnumerable<OrgDTO>>(HttpStatusCode.OK, result);
        //}

        #endregion

        #region Authorization
        public HttpResponseMessage GetAuthRols()
        {
            var authRols = _IAuthrolService.GetAuthrols();

            var result = PlatformMappingHelper.Map<IList<AuthRol>, IList<AuthRolDTO>>(authRols.ToList()).ToList();

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<AuthRolDTO>>(HttpStatusCode.OK, result);
        }
        #endregion




    }
}