using RAMLSharp.Attributes;
using Oc.Carbon.Common.Contracts;
using Oc.Carbon.DataAccess;
using Oc.Carbon.DataAccess.Contracts;
using Oc.Carbon.DTO.Mapping.Core;
using Oc.Carbon.DTO.PlatformDTO;
using Oc.Carbon.DTO.Requests;
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
    public class DocumentsController : ApiController
    {
        ILogService _LoggerService;
        IUserService _IUserService;    
        IUnitOfWork _UnitOfWork;
        ISouploadService _IDocService;
        IWkflowdefService _IWkflowdefService;
        IOrgService _IOrgService;
        IWkflowinstanceService _IWkflowinstanceService;

        public DocumentsController(ILogService loggerService, IUserService userService, IUnitOfWork unitOfWork, ISouploadService docService, IWkflowdefService wkflowdefService, IOrgService orgService, IWkflowinstanceService wkflowinstanceService)
        {

            this._LoggerService = loggerService;
            this._IUserService = userService;
            this._IDocService = docService;           
          
            this._UnitOfWork = unitOfWork;

            this._IWkflowdefService = wkflowdefService;
            this._IOrgService = orgService;
            this._IWkflowinstanceService = wkflowinstanceService;

        }

        
        public HttpResponseMessage GetDocumentsByFilter(string orgIds, string docTypCd, int? elmId,string value)
        {
            // var docs = _IDocService.QueryData().Where(p => p.DocTyp.DocTypCd == docTypCd).ToList();

            var docs = _IDocService.GetSouploads().ToList().Where(p=>p.OrgId== Convert.ToInt32( orgIds));

            var result = PlatformMappingHelper.Map<IList<soUpload>, IList<DocDTO>>(docs.ToList()).ToList();

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Request.CreateResponse<IEnumerable<DocDTO>>(HttpStatusCode.OK, result);
        }

        public HttpResponseMessage GetDocument(int id)
        {
            var docs = _IDocService.GetSoupload(id);
            var result = PlatformMappingHelper.Map<soUpload, DocDTO>(docs);

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<DocDTO>(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Returns details
        /// </summary>
        /// <param name="id">id</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "detail"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage detail(int id)
        {

            var document = _IWkflowinstanceService.GetWkflowinstance(id);

            var result = PlatformMappingHelper.Map<WkflowInstance, DPWorkflowDTO>(document);

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<DPWorkflowDTO>(HttpStatusCode.OK, result);
        }

    }
}