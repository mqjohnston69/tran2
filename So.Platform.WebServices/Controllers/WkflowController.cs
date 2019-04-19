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
using System.Configuration;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;

namespace Oc.Carbon.WebServices.Controllers
{
    public class WkflowController : ApiController
    {
        ILogService _LoggerService;
        IUserService _IUserService;
        IUnitOfWork _UnitOfWork;

        IWkflowdefService _IWkflowdefService;
        IWkflowinstanceService _IWkflowinstanceService;
        IDpworkflowService _IDpworkflowService;
        IOrgdoctypdataelmService _IOrgdoctypdataelmService;
        IOrgService _IOrgService;

        //static public string LocalRoot = @"C:\ScanOptics\LocalStorage\";
        //private const string JEFF_CONNECTION_STRING = @"DefaultEndpointsProtocol=https;AccountName=jlyons;AccountKey=immiQhR3OgjXZ0xrBl4Q9KcdM8ZZjIwR4n5oRDfnCwAl2quuUkZQNQJYP8XIby8pHWDGwOxqaA9N/r85AESrLg==";

        private const string METADATA_CAPTION_KEY = "Caption";
        private const string METADATA_DESCRIPTION_KEY = "Description";
        private const string METADATA_UPLOADKEY_KEY = "UploadKey";
        private const string METADATA_CUSTOMER_KEY = "CustomerID";


        public WkflowController(ILogService loggerService, IUserService userService, IUnitOfWork unitOfWork, 
            IWkflowdefService wkflowdefService, IWkflowinstanceService wkflowinstanceService, 
            IDpworkflowService dpworkflowService, IOrgdoctypdataelmService orgdoctypdataelmService,IOrgService orgService)
        {

            this._LoggerService = loggerService;
            this._IUserService = userService;
            this._IWkflowdefService = wkflowdefService;
            this._IWkflowinstanceService = wkflowinstanceService;
            this._IDpworkflowService = dpworkflowService;
            this._IOrgdoctypdataelmService = orgdoctypdataelmService;
            this._UnitOfWork = unitOfWork;
            this._IOrgService = orgService;

        }

        [Authorize]
        public HttpResponseMessage GetWkflowsByFilter(int? orgId, int? docTypeId)
        {
            var wkflowInstances = _IWkflowinstanceService.GetWkflowinstances().Where(p=> p.OrgId == orgId && p.WkflowInstanceDocs.FirstOrDefault().Doc.OrgDocTypId == docTypeId ).ToList();
            var result = PlatformMappingHelper.Map<IList<WkflowInstance>, IList<WkflowInstanceDTO>>(wkflowInstances.ToList()).ToList();

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<WkflowInstanceDTO>>(HttpStatusCode.OK, result);
        }

        public HttpResponseMessage GetWkflowInstance(int id)
        {
            var wkflowInstance = _IWkflowinstanceService.GetWkflowinstance(id);
            var result = PlatformMappingHelper.Map<WkflowInstance, WkflowInstanceDTO>(wkflowInstance);

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<WkflowInstanceDTO>(HttpStatusCode.OK, result);
        }


    }
}