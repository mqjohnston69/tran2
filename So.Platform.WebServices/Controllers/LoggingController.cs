using Oc.Carbon.Common.Contracts;
using Oc.Carbon.DTO.SolutionDTO;
using Oc.Carbon.ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Oc.Carbon.WebServices.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoggingController : ApiController
    {
        ILogService _loggerService;
        ILogentryService _ILogentryService;

        public LoggingController(ILogService loggerService, ILogentryService logentryService)
        {
            this._loggerService = loggerService;
            this._ILogentryService = logentryService;
        }
   
        [HttpGet]
        public HttpResponseMessage Log(string message, string categoryCd, string application, string errorCd)
        {
            try
            {
                _ILogentryService.AddLogentry(
                    new DataAccess.LogEntry
                    {
                        CreateDate = DateTime.UtcNow.ToString(),
                        Application = application,
                        Message = message,
                        ErrorCode = errorCd,
                        LogCatId = int.Parse(categoryCd)
                    }
                    );

                return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
            }
            catch (Exception daExp)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, daExp.Message);
            }
        }

    }
}