using RAMLSharp.Attributes;
using Oc.Carbon.Common.Contracts;
using Oc.Carbon.DataAccess;
using Oc.Carbon.DataAccess.Contracts;
using Oc.Carbon.DTO.Mapping.Core;
using Oc.Carbon.DTO.PlatformDTO;
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
    public class DocTypeController : ApiController
    {
        ILogService _LoggerService;
        IUserService _IUserService;
        IUnitOfWork _UnitOfWork;
        IOrgdoctypService _IOrgdoctypService;
        IDataelmtypService _IDataelmtypService;
        IDoctypstatusService _IDoctypstatusService;
        IWkflowdefService _IWkflowdefService;
        IWkflowinstanceService _IWkflowinstanceService;
        IMessagetemplateService _IMessagetemplateService;
        IMessageuserService _IMessageuserService;


        public DocTypeController(ILogService loggerService, IUserService userService, IUnitOfWork unitOfWork,
            IOrgdoctypService orgdoctypService, IDataelmtypService dataelmtypService, IDoctypstatusService doctypstatusService,
            IWkflowdefService wkflowdefService, IWkflowinstanceService wkflowinstanceService, IMessagetemplateService messagetemplateService,
            IMessageuserService messageuserService)
        {

            this._LoggerService = loggerService;
            this._IUserService = userService;
            this._IOrgdoctypService = orgdoctypService;
            this._UnitOfWork = unitOfWork;
            this._IDoctypstatusService = doctypstatusService;
            this._IWkflowdefService = wkflowdefService;
            this._IWkflowinstanceService = wkflowinstanceService;
            this._IDataelmtypService = dataelmtypService;
            this._IMessagetemplateService = messagetemplateService;
            this._IMessageuserService = messageuserService;
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage Ping()
        {
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Pong");
        }

        /// <summary>
        /// Returns List of DataTypes
        /// </summary>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "GetDataTypes"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage DataTypes()
        {

            var dataElmTyps = _IDataelmtypService.GetDataelmtyps();

            var datatypes = PlatformMappingHelper.Map<IList<DataElmTyp>, IList<DataElmTypDTO>>(dataElmTyps.ToList()).ToList();

            if (datatypes == null) throw new HttpResponseException(HttpStatusCode.NotFound);


            return Request.CreateResponse<List<DataElmTypDTO>>(HttpStatusCode.OK, datatypes);
        }


        /// <summary>
        /// Returns List of Created users of Doc Types in a Organization
        /// </summary>
        /// <param name="orgIds">OrgID</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "creators"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage creators(int org_id)
        {

            if (org_id == null)
            {
                org_id = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());
            }

            var createdusers = _IOrgdoctypService.GetOrgdoctyps().Where(p => org_id==p.OrgId).Select(k=> k.User).Distinct().Select(u => new { Id= u.Id, Name = u.Per.FirstName + ", " + u.Per.LastName });

            if (createdusers == null) throw new HttpResponseException(HttpStatusCode.NotFound);


            return Request.CreateResponse<object>(HttpStatusCode.OK, createdusers);
        }

        /// <summary>
        /// Returns List of Doctyp Statuses
        /// </summary>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "GetDocTypeStatuses"
        )]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage DocTypeStatus()
        {

            var docTypestatus = _IDoctypstatusService.GetDoctypstatus();

            var doctypestatus = PlatformMappingHelper.Map<IList<DocTypStatus>, IList<DocTypStatusDTO>>(docTypestatus.ToList()).ToList();

            if (doctypestatus == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Request.CreateResponse<IList<DocTypStatusDTO>>(HttpStatusCode.OK, doctypestatus);
        }

        /// <summary>
        /// Returns DocTyp List based on Filters
        /// </summary>
        /// <param name="orgIds">OrgID</param>
        /// <param name="docName">docName</param>
        /// <param name="startDate">startDate</param>
        /// <param name="endDate">endDate</param>
        /// <param name="createUserID">createUserID</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "GetDocTypesByFilter"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage search(int? OrgId, string docName,DateTime? startDate, DateTime? endDate, int? createUserID)
        {
            if(OrgId == null)
            {
                OrgId = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());
            }


            var docTypes = _IOrgdoctypService.GetDoctypsByFilter(OrgId, docName, startDate, endDate, createUserID);

            var search = PlatformMappingHelper.Map<IList<OrgDocTyp>, IList<DocTypDTO>>(docTypes.ToList()).ToList();

            if (search == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Request.CreateResponse<IList<DocTypDTO>>(HttpStatusCode.OK, search);
        }


        /// <summary>
        /// Returns Default DocTyp List
        /// </summary>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "GetDefaultDoctyps"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage DefaultDoctyps()
        {

            var defaultdocTypes = _IOrgdoctypService.GetDefaultDoctyps();

            var defaults = PlatformMappingHelper.Map<IList<OrgDocTyp>, IList<DocTypDTO>>(defaultdocTypes.ToList()).ToList();

            if (defaults == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Request.CreateResponse<IList<DocTypDTO>>(HttpStatusCode.OK, defaults);
        }

        /// <summary>
        /// Returns Default DocTyp List
        /// </summary>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "GetDefaultDoctyps"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage get(int id)
        {

            var docType = _IOrgdoctypService.GetOrgdoctyp(id);

            var doctypedto = PlatformMappingHelper.Map<OrgDocTyp, DocTypDTO>(docType);

            if (doctypedto == null) throw new HttpResponseException(HttpStatusCode.NotFound);


            return Request.CreateResponse<DocTypDTO>(HttpStatusCode.OK, doctypedto);
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage PendingApprovals()
        {

            var docTypes = _IOrgdoctypService.GetOrgdoctyps().Where(p => p.WkflowInstance!=null).Where(q=> q.WkflowInstance.WkflowDefId == 3 && (q.WkflowInstance.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Code == "CANC" || q.WkflowInstance.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Code == "Closed")).Select(s=> new {CustomerId=s.Org.Id, CustomerName=s.Org.Name, Name= s.Descript, CreateDate = s.WkflowInstance.CreateDate});

            return Request.CreateResponse<object>(HttpStatusCode.OK, docTypes);
        }


        /// <summary>
        /// Create DocTyp
        /// </summary>
        /// <param name="docTypeDTO">DocTypDTO</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "CreateNewDocType"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage Create(DocTypDTO docTypeDTO)
        {

            try
            {
                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());
                int statusID = _IDoctypstatusService.GetDoctypstatus().Where(p => p.StatusCd == "INREVIEW").FirstOrDefault().Id;

                var sameNames = _IOrgdoctypService.GetOrgdoctyps().Where(p => p.OrgId == docTypeDTO.OrgId && p.Descript == docTypeDTO.Descript);

                if (sameNames.Count()<=0)
                {
                    var DocTypWorkflowdef =
                            _IWkflowdefService.GetWkflowdefs().Where(p => p.Code == "DTSW").FirstOrDefault();

                    var wkstatusID = DocTypWorkflowdef.WkflowDefWkflowStats.Where(p => p.WkflowStat.Code == "INREVIEW").FirstOrDefault().WkflowStatId;

                    var messagetemp = _IMessagetemplateService.GetMessagetemplates().Where(p => p.Name == "NewDocTypeSetup").FirstOrDefault();

                    var user = _IUserService.GetUser(userID);

                    // Create a new Document Type Setup workflow 
                    var batchWkflowInstance = new WkflowInstance
                    {
                        CreateDate = DateTime.UtcNow,
                        WkflowDefId = DocTypWorkflowdef.Id,
                        OrgId = docTypeDTO.OrgId,
                        User = user,
                        CurrWkflowStatId = wkstatusID
                    };

                    // Set INIT state for new workflow instance.
                    batchWkflowInstance.WkflowStepHists.Add(new WkflowStepHist
                    {
                        CreateDate = DateTime.UtcNow,
                        WkflowStatId = wkstatusID,
                        CreatedUserId = userID
                    });

                    if (messagetemp != null)
                    {
                        _IMessageuserService.AddMessageuser(new MessageUser() { User = user,  DeliveryMethodId = 1, Message = new Message() { CreateDate=DateTime.UtcNow, WkflowInstance= batchWkflowInstance, HeaderText = messagetemp.HeaderText, MessageBody = string.Format(messagetemp.TemplateText, docTypeDTO.Name) } });
                    }

                    _IWkflowinstanceService.AddWkflowinstance(batchWkflowInstance);
                    _IOrgdoctypService.CreateNewDocType(docTypeDTO, userID, statusID);
                    _UnitOfWork.Commit();
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "DocType Name Should be Unique");
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }

        /// <summary>
        /// Update DocType
        /// </summary>
        /// <param name="docTypeDTO">DocTypDTO</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "UpdateDocType"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpPut]
        [Authorize]
        public HttpResponseMessage Update(DocTypDTO docTypeDTO)
        {

            try
            {
                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());
                var doctyp = _IOrgdoctypService.GetOrgdoctyp(docTypeDTO.Id);
                MessageTemplate messagetemp=null;

                if (doctyp.Approved != docTypeDTO.Approved)
                {
                    if (docTypeDTO.Approved)
                    {
                        messagetemp = _IMessagetemplateService.GetMessagetemplates().Where(p => p.Name == "DocTypeAccepted").FirstOrDefault();
                    }
                    else
                    {
                        messagetemp = _IMessagetemplateService.GetMessagetemplates().Where(p => p.Name == "DocTypeNeedInfo").FirstOrDefault();
                    }
                }

                var user = _IUserService.GetUser(userID);

                if (messagetemp != null)
                {
                    _IMessageuserService.AddMessageuser(new MessageUser() { User = user, DeliveryMethodId = 1, Message = new Message() { CreateDate=DateTime.UtcNow, HeaderText = messagetemp.HeaderText, MessageBody = string.Format(messagetemp.TemplateText, docTypeDTO.Name) } });
                }

                _IOrgdoctypService.UpdateDocType(docTypeDTO, userID);
               _UnitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }

        /// <summary>
        /// InActivate DocType
        /// </summary>
        /// <param name="docTypeID">docTypeID</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "InActivateDocType"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpDelete]
        [Authorize]
        public HttpResponseMessage inActivate(int id, string reason)
        {

            try
            {
                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());
                int statusID = _IDoctypstatusService.GetDoctypstatus().Where(p => p.StatusCd == "INACTIVE").FirstOrDefault().Id;

                _IOrgdoctypService.InActivateDocType(id, userID, statusID, reason);
                _UnitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }

    }
}