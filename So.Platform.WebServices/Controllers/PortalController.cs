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
    public class PortalController : ApiController
    {
        ILogService _loggerService;
        IPortService _PortService;

        public PortalController(ILogService loggerService, IPortService portService)
        {
            this._loggerService = loggerService;
            this._PortService = portService;
        }

        public PortalDTO GetPortal(int portalId)
        {
            var port = _PortService.GetPort(portalId);

            var portalDTO = PlatformMappingHelper.Map<Port, PortalDTO>(port);

            return portalDTO;

        }
    }
}