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
using RAMLSharp.Attributes;
using System.Threading.Tasks;
using System.Diagnostics;
using Oc.Carbon.WebServices.Common;
using Oc.Carbon.ServiceLayer.Implementations;

namespace Oc.Carbon.WebServices.Controllers
{
    public class UploadActivityController : ApiController
    {

        ILogService _LoggerService;
        IUserService _IUserService;
        IUnitOfWork _UnitOfWork;

        IWkflowdefService _IWkflowdefService;
        IWkflowinstanceService _IWkflowinstanceService;
        IDpworkflowService _IDpworkflowService;
        IOrgdoctypdataelmService _IOrgdoctypdataelmService;
        IOrgService _IOrgService;
        IOrgdoctypService _IOrgdoctypService;
        IWkflowdefstatreaService _IWkflowdefstatreaService;
        IPortsettingService _IPortsettingService;
        IOrgcustService _IOrgcustService;

        //static public string LocalRoot = @"C:\ScanOptics\LocalStorage\";
        //private const string JEFF_CONNECTION_STRING = @"DefaultEndpointsProtocol=https;AccountName=jlyons;AccountKey=immiQhR3OgjXZ0xrBl4Q9KcdM8ZZjIwR4n5oRDfnCwAl2quuUkZQNQJYP8XIby8pHWDGwOxqaA9N/r85AESrLg==";

        private const string METADATA_CAPTION_KEY = "Caption";
        private const string METADATA_DESCRIPTION_KEY = "Description";
        private const string METADATA_UPLOADKEY_KEY = "UploadKey";
        private const string METADATA_CUSTOMER_KEY = "CustomerID";



        public UploadActivityController(ILogService loggerService, IUserService userService, IUnitOfWork unitOfWork,
            IWkflowdefService wkflowdefService, IWkflowinstanceService wkflowinstanceService,
            IDpworkflowService dpworkflowService, IOrgdoctypdataelmService orgdoctypdataelmService, 
            IOrgService orgService, IOrgdoctypService orgdoctypService, IWkflowdefstatreaService wkflowdefstatreaService,
            IPortsettingService portsettingService, IOrgcustService orgcustService)
        {

            this._LoggerService = loggerService;
            this._IUserService = userService;
            this._IWkflowdefService = wkflowdefService;
            this._IWkflowinstanceService = wkflowinstanceService;
            this._IDpworkflowService = dpworkflowService;
            this._IOrgdoctypdataelmService = orgdoctypdataelmService;
            this._UnitOfWork = unitOfWork;
            this._IOrgService = orgService;
            this._IOrgdoctypService = orgdoctypService;
            this._IWkflowdefstatreaService = wkflowdefstatreaService;
            this._IPortsettingService = portsettingService;
            this._IOrgcustService = orgcustService;
        }

        /// <summary>
        /// Returns Upload Activity List based on Filters
        /// </summary>
        /// <param name="orgId">orgId</param>
        /// <param name="sourceFile">sourceFile</param>
        /// <param name="startDate">startDate</param>
        /// <param name="endDate">endDate</param>
        /// <param name="strFileName">strFileName</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "search"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage search(UploadActivitySearchRequest filter)
        {
            if (filter.OrgId == null)
            {
                filter.OrgId = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());
            }

            //filter.LastId = filter.LastId == null? int.MaxValue: filter.LastId;

            var uploadActivities = _IWkflowinstanceService.GetUploadActivityByFilter(filter);

            var result = PlatformMappingHelper.Map<IList<WkflowInstance>, IList<BatchProcessingWorkflowDTO>>(uploadActivities).ToList();

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<BatchProcessingWorkflowDTO>>(HttpStatusCode.OK, result);
        }



        /// <summary>
        /// Returns Upload file Type and Name
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

            var uploadactivity = _IWkflowinstanceService.GetWkflowinstance(id);

            var result = PlatformMappingHelper.Map<WkflowInstance, BatchProcessingWorkflowDTO>(uploadactivity);

            var storageAccess = _IPortsettingService.GetPortsettings().Where(p => p.PortId == 1 && p.Setting.Name == "StorageAccess").FirstOrDefault().PortSettingValues.FirstOrDefault().Value;
            var storageContainer = uploadactivity.WkflowInstanceDocs.FirstOrDefault().Doc.soStorageContainer;
            var versionData = new VersionData();
            versionData.container = storageContainer;
            versionData.accountKey = storageAccess;
            int imageType = 0;
            try
            {
                if (result.FileName.ToUpper().Contains(".PDF"))
                {
                    imageType = 1;
                    var pdfimage = BlobImage.GetImage(uploadactivity.WkflowInstanceDocs.FirstOrDefault().Doc.soStorageKey, versionData, imageType);
                    result.image = pdfimage[0];
                }
                else
                {
                    imageType = 2;
                    result.preview = BlobImage.GetImage(uploadactivity.WkflowInstanceDocs.FirstOrDefault().Doc.soStorageKey, versionData, imageType);
                }
            }
            catch(Exception e)
            {

            }


            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<BatchProcessingWorkflowDTO>(HttpStatusCode.OK, result);
        }



        /// <summary>
        /// Returns List of fileExceptions
        /// </summary>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "fileException"
        )]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage fileExceptionStatReasonList()
        {

            var wkflowdefreasons = _IWkflowdefstatreaService.GetWkflowdefstatreas().Where(p => p.WkflowDefWkflowStat.WkflowStat.Code == "EXCP" && p.WkflowDefWkflowStat.WkflowDef.Code.Contains("FPW")).ToList();

            List<dynamic> fileExceptionReasons = new List<dynamic>();

           foreach(var reason in wkflowdefreasons)
            {
                fileExceptionReasons.Add(new {id= reason.WkflowStatRea.Id, Code= reason.WkflowStatRea.Code, Name= reason.WkflowStatRea.Descript  });
            }

            if (fileExceptionReasons == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Request.CreateResponse<object>(HttpStatusCode.OK, fileExceptionReasons);
        }


        /// <summary>
        /// Returns List of docExceptions
        /// </summary>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "docExceptions"
        )]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage docExceptionStatReasonList()
        {

            var wkflowdefreasons = _IWkflowdefstatreaService.GetWkflowdefstatreas().Where(p => p.WkflowDefWkflowStat.WkflowStat.Code == "EXCP" && p.WkflowDefWkflowStat.WkflowDef.Code.Contains("DPW")).ToList();


            List<dynamic> fileExceptionReasons = new List<dynamic>();

            foreach (var reason in wkflowdefreasons)
            {
                fileExceptionReasons.Add(new { id = reason.WkflowStatRea.Id, Code = reason.WkflowStatRea.Code, Name = reason.WkflowStatRea.Descript });
            }

            if (fileExceptionReasons == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Request.CreateResponse<object>(HttpStatusCode.OK, fileExceptionReasons);
        }



        /// <summary>
        /// Set Exception DocType
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="reasonid">reasonid</param>
        /// <param name="reason">reason</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "Set to Exception"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpDelete]
        [Authorize]
        public HttpResponseMessage setFileException(int id, int reasonid,string reason)
        {

            try
            {
                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());


                var wkflow = _IWkflowinstanceService.GetWkflowinstance(id);

                var batchProcessingWorkflow =
                                _IWkflowdefService.GetWkflowdefs().Where(p => p.Code == "FPW").FirstOrDefault();

                var statusID = batchProcessingWorkflow.WkflowDefWkflowStats.Where(p => p.WkflowStat.Code == "EXCP").FirstOrDefault().WkflowStatId;


                wkflow.CurrWkflowStatId = statusID;

               var ccare = wkflow.Org.OrgUsers.Where(p => p.Type == "CustomerCare");

                if (ccare.Count() > 0)
                {
                    wkflow.CCUserId = ccare.FirstOrDefault().UserId;
                }

                var wkflowHist = new WkflowStepHist
                {
                    CreateDate = DateTime.UtcNow,
                    DateLastMaint = DateTime.UtcNow,
                    WkflowStatId = statusID,
                    WkflowStatReasId = reasonid,
                    CreatedUserId = userID
                };

                wkflowHist.WkflowStepNotes.Add(new WkflowStepNote() { WkflowStepHist = wkflowHist, CreatedDate = DateTime.UtcNow, NoteText = reason, Order = 1 });
                wkflow.WkflowStepHists.Add(wkflowHist);

                _IWkflowinstanceService.UpdateWkflowinstance(wkflow);


            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }


        /// <summary>
        /// Set Exception DocType
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="reasonid">reasonid</param>
        /// <param name="reason">reason</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "Set to Exception"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpDelete]
        [Authorize]
        public HttpResponseMessage setDocException(int id, int reasonid, string reason)
        {

            try
            {
                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                var wkflow = _IWkflowinstanceService.GetWkflowinstance(id);

                var batchProcessingWorkflow =
                                _IWkflowdefService.GetWkflowdefs().Where(p => p.Code == "DPW").FirstOrDefault();

                var statusID = batchProcessingWorkflow.WkflowDefWkflowStats.Where(p => p.WkflowStat.Code == "EXCP").FirstOrDefault().WkflowStatId;

                wkflow.CurrWkflowStatId = statusID;
                var ccare = wkflow.Org.OrgUsers.Where(p => p.Type == "CustomerCare");

                if (ccare.Count() > 0)
                {
                    wkflow.CCUserId = ccare.FirstOrDefault().UserId;
                }

                var wkflowHist = new WkflowStepHist
                {
                    CreateDate = DateTime.UtcNow,
                    DateLastMaint = DateTime.UtcNow,
                    WkflowStatId = statusID,
                    WkflowStatReasId = reasonid,
                    CreatedUserId = userID
                };

                wkflowHist.WkflowStepNotes.Add(new WkflowStepNote() { WkflowStepHist = wkflowHist, CreatedDate = DateTime.UtcNow, NoteText = reason, Order = 1 });
                wkflow.WkflowStepHists.Add(wkflowHist);

                _IWkflowinstanceService.UpdateWkflowinstance(wkflow);


            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }


        [HttpPost]
        [Authorize]

        public HttpResponseMessage FileUpload(int docTypeId, bool test)
        {

            try
            {
                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());                
                var org = _IUserService.GetUser(userID).OrgUsers.Where(p => p.Type == null || p.Type.Contains("Primary")).FirstOrDefault().Org;
                var docType = org.OrgDocTyps.Where(p => p.Id == docTypeId).FirstOrDefault();
                var path = "";
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                int iUploadedCnt = 0;
                string fileName = "";
                var guid = org.soKey;
                Guid parentGuid = Guid.Empty;

                if (org.Id != 1)
                    parentGuid = org.OrgOrgs1.FirstOrDefault().Org.soKey;

                #region Upload to Blob

                var status = "";
                var storageKey = Guid.NewGuid();
                var storageAccess = _IPortsettingService.GetPortsettings().Where(p => p.PortId == 1 && p.Setting.Name == "StorageAccess").FirstOrDefault().PortSettingValues.FirstOrDefault().Value;
                //var storageContainer = _IPortsettingService.GetPortsettings().Where(p => p.PortId == 1 && p.Setting.Name == "StorageContainer").FirstOrDefault().PortSettingValues.FirstOrDefault().Value;
                var versionData = new VersionData();
                versionData.container = guid.ToString();
                versionData.accountKey = storageAccess;
                var success = false;
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        BinaryReader b = new BinaryReader(hpf.InputStream);
                        byte[] binData = b.ReadBytes(hpf.ContentLength);

                        success = BlobImage.UploadImage(hpf.FileName, binData, storageKey.ToString() + ".UPL", versionData, org.soKey.ToString(), hpf.FileName);
                        fileName = hpf.FileName;
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
                #endregion


                if (iUploadedCnt > 0 && success)
                {
                    status = iUploadedCnt + " Files Uploaded Successfully";

                    var batchProcessingWorkflow =
                        _IWkflowdefService.GetWkflowdefs().Where(p => p.Code == "FPW").FirstOrDefault();

                    var UploadingstatusID = batchProcessingWorkflow.WkflowDefWkflowStats.Where(p => p.WkflowStat.Code == "UPLOADED").FirstOrDefault().WkflowStatId;

                    // Create a new batch processing workflow 
                    var batchWkflowInstance = new WkflowInstance
                    {
                        CreateDate = DateTime.UtcNow,
                        DateLastMaint = DateTime.UtcNow,
                        WkflowDefId = batchProcessingWorkflow.Id,
                        OrgId = org.Id,
                        UserId = userID,
                        CurrWkflowStatId = UploadingstatusID
                    };

                    // Set INIT state for new workflow instance.
                    batchWkflowInstance.WkflowStepHists.Add(new WkflowStepHist
                    {
                        CreateDate = DateTime.UtcNow,
                        DateLastMaint = DateTime.UtcNow,
                        WkflowStatId = UploadingstatusID,
                        CreatedUserId = userID
                    });


                    var wkflowInstanceDoc = new WkflowInstanceDoc
                    {
                        WkflowInstanceId = batchWkflowInstance.Id,
                        Doc = new soUpload
                        {
                            Name = fileName,
                            Descript = fileName,
                            soFileName = fileName,
                            OrgDocTypId = docTypeId,
                            soStorageLocation = path,
                            soStorageKey = storageKey.ToString() + ".UPL",
                            soStorageContainer = guid.ToString(),
                            soUploadApp = "WebPortal",
                            soUploadAppVersion = "1.0.0.0",
                            soUploadDurationMS = 10,
                            soStorageType = "Local",
                            soFormType = docType.Descript,
                            soFormTypesKey = docType.soKey,
                            soUserData = "",
                            soUploadTime = DateTime.UtcNow,
                            soKey = storageKey,
                            soMethod = "Web",
                            soWorkstation = "Portal",
                            soOrganizationsKey = guid,
                            soParentOrganizationsKey = parentGuid,
                            soUserID = userID.ToString(),
                            OrgId = org.Id,
                            FileTypeId = 2,
                            FileExt = Path.GetExtension(fileName),
                            soTest = test
                        }
                    };

                    batchWkflowInstance.WkflowInstanceDocs.Add(wkflowInstanceDoc);
                    _IWkflowinstanceService.AddWkflowinstance(batchWkflowInstance);


                }
                else
                {
                    var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Upload Failed");
                    throw new HttpResponseException(createresponse);
                }
                return Request.CreateResponse<string>(HttpStatusCode.OK, status);
            }
            catch
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Upload Failed");
                throw new HttpResponseException(createresponse);
            }

        }


        [HttpPost]
        [Authorize]
        public HttpResponseMessage LogoUpload(int custId)
        {
               
       
            var customer = _IOrgcustService.GetOrgcusts().Where(p => p.Org.Id == custId).FirstOrDefault();

            try
            {

                foreach (string key in HttpContext.Current.Request.Form.AllKeys)
                {
                    string value = HttpContext.Current.Request.Form[key];
                }
                
                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string fileName = "";
                byte[] binData = null;



                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        BinaryReader b = new BinaryReader(hpf.InputStream);
                        binData = b.ReadBytes(hpf.ContentLength);

                        fileName = hpf.FileName;
                
                    }
                }

                customer.Org.Logo = binData;

                _IOrgcustService.UpdateOrgcust(customer);

                return Request.CreateResponse<string>(HttpStatusCode.OK, "Logo uploaded successfully");
            }
            catch
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Logo Upload Failed");
                throw new HttpResponseException(createresponse);
            }

        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage SowUpload(int custId)
        {


            var customer = _IOrgcustService.GetOrgcusts().Where(p => p.Org.Id == custId).FirstOrDefault();

            try
            {

                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                string fileName = "";
                byte[] binData = null;



                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        BinaryReader b = new BinaryReader(hpf.InputStream);
                        binData = b.ReadBytes(hpf.ContentLength);

                        fileName = hpf.FileName;

                    }
                }

                customer.Org.Agreement = binData;

                _IOrgcustService.UpdateOrgcust(customer);

                return Request.CreateResponse<string>(HttpStatusCode.OK, "SOW uploaded successfully");
            }
            catch
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "SOW Upload Failed");
                throw new HttpResponseException(createresponse);
            }

        }
    }
}