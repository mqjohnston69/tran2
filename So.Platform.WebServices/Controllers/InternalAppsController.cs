using Oc.Carbon.Common.Contracts;
using Oc.Carbon.DataAccess;
using Oc.Carbon.DataAccess.Contracts;
using Oc.Carbon.DTO.Mapping.Core;
using Oc.Carbon.DTO.PlatformDTO;
using Oc.Carbon.DTO.Requests;
using Oc.Carbon.DTO.SolutionDTO;
using Oc.Carbon.ServiceLayer.Contracts;
using Oc.Carbon.WebServices.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Oc.Carbon.WebServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InternalAppsController : ApiController
    {
        ILogService _LoggerService;
        IUserService _IUserService;
        IUnitOfWork _UnitOfWork;
        ISouploadService _IDocService;
        IWkflowdefService _IWkflowdefService;
        IOrgService _IOrgService;
        IWkflowinstanceService _IWkflowinstanceService;
        IPortService _IPortService;
        IPortsettingService _IPortsettingService;
        IOrgdoctypService _IOrgdoctypService;
        IMessageService _IMessageService;
        IMessageuserService _IMessageuserService;
        IOrgdoctypmonthService _IOrgdoctypmonthService;
        IMonthService _IMonthService;
        IOrgdoctypdailyuploadService _IOrgdoctypdailyuploadService;
        IOrgmonthcommitmentService _IOrgmonthcommitmentService;

        private static string fileExts = ".TIF,.TIFF,.GIF,.JPEG,.JPG,.PDF,.PNG";

        public InternalAppsController(ILogService loggerService, IUserService userService,
            IUnitOfWork unitOfWork, ISouploadService docService, IWkflowdefService wkflowdefService,
            IOrgService orgService, IWkflowinstanceService wkflowinstanceService,
            IPortService portService, IPortsettingService portsettingService,
            IOrgdoctypService orgdoctypService, IMessageService messageService,
            IMessageuserService messageuserService, IOrgdoctypmonthService orgdoctypmonthService,
            IMonthService monthService, IOrgdoctypdailyuploadService orgdoctypdailyuploadService,
            IOrgmonthcommitmentService orgmonthcommitmentService)
        {

            this._LoggerService = loggerService;
            this._IUserService = userService;
            this._IDocService = docService;

            this._UnitOfWork = unitOfWork;

            this._IWkflowdefService = wkflowdefService;
            this._IOrgService = orgService;
            this._IWkflowinstanceService = wkflowinstanceService;
            this._IPortService = portService;
            this._IPortsettingService = portsettingService;
            this._IOrgdoctypService = orgdoctypService;
            this._IMessageService = messageService;
            this._IMessageuserService = messageuserService;
            this._IOrgdoctypmonthService = orgdoctypmonthService;
            this._IMonthService = monthService;
            this._IOrgdoctypdailyuploadService = orgdoctypdailyuploadService;
            this._IOrgmonthcommitmentService = orgmonthcommitmentService;
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage VersionCheck(string appName, string version)
        {
            DateTime start = DateTime.Now;
            try
            {
                string portalid = Request.Headers.GetValues("portId").FirstOrDefault();

                var result = _IPortsettingService.VersionCheck(int.Parse(portalid), appName, version);
                ApiLog.Write("VersionCheck", start);
                return Request.CreateResponse<object>(HttpStatusCode.OK, result);

            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error Retriving Version Number/StorageAccessKey");
                throw new HttpResponseException(createresponse);
            }

        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetCustomers()
        {
            DateTime start = DateTime.Now;
            try
            {
                int? org_id = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());

                var org = _IOrgService.GetOrgs().Where(p => p.Id == org_id).FirstOrDefault();

                if (org != null)
                {
                    IList<Object> result = new List<object>();

                    if (org.OrgTyp.TypCd == "CUST")
                    {
                        result.Add(new { soKey = org.soKey, Name = org.Name });
                    }
                    else
                    {
                        foreach (OrgOrg custorg in org.OrgOrgs1)
                        {
                            result.Add(new { soKey = custorg.Org1.soKey, Name = custorg.Org1.Name });
                        }
                    }
                    ApiLog.Write("GetCustomers", start);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Org Key");
                    throw new HttpResponseException(createresponse);
                }

            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error Retriving Version Number/StorageAccessKey");
                throw new HttpResponseException(createresponse);
            }

        }



        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetDocTyps(string soOrgKey)
        {
            DateTime start = DateTime.Now;
            try
            {

                var org = _IOrgService.GetOrgs().Where(p => p.soKey == Guid.Parse(soOrgKey)).FirstOrDefault();

                if (org != null)
                {
                    //Go Up the chain and get call DocTypes
                    List<OrgDocTyp> docTypes = new List<OrgDocTyp>();
                    GetAllDocs(ref docTypes, org);
                    var result = PlatformMappingHelper.Map<IList<OrgDocTyp>, IList<DocTypDTO>>(docTypes.ToList()).ToList();
                    ApiLog.Write("GetDocTypes", start);
                    return Request.CreateResponse<IList<DocTypDTO>>(HttpStatusCode.OK, result);
                }
                else
                {
                    var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Org Key");
                    throw new HttpResponseException(createresponse);
                }

            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error Retriving Version Number/StorageAccessKey");
                throw new HttpResponseException(createresponse);
            }

        }
        private void GetAllDocs(ref List<OrgDocTyp> docTypes, Org org)
        {
            docTypes = docTypes.Union(org.OrgDocTyps).ToList();
            if (org.OrgOrgs1.Count > 0)
            {
                GetAllDocs(ref docTypes, org.OrgOrgs1.FirstOrDefault().Org);
            }
        }


        [Authorize]
        [HttpPost]
        public HttpResponseMessage CreateUploadWorkflow(BatchRequestDTO request)
        {
            DateTime start = DateTime.Now;
            int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

            var result = _IDocService.CreateUploadWorkflow(userID, request);

            ApiLog.Write("CreateUploadWorkflow", start);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage UpdateUploadStatus(BatchRequestDTO request)
        {
            DateTime start = DateTime.Now;

            int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());
            request.UserId = userID;

            var result = _IDocService.UpdateUploadStatus(request);

            if (result == null)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Failed to Update");
                throw new HttpResponseException(createresponse);
            }
            ApiLog.Write("UpdateUploadStatus", start);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage QueryWorkItemState(string soKey)
        {
            DateTime start = DateTime.Now;
            try
            {
                var doc = _IDocService.GetSouploads().Where(p => p.soKey == Guid.Parse(soKey)).FirstOrDefault();


                if (doc == null)
                {
                    var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "WorkItem does not exist");
                    throw new HttpResponseException(createresponse);
                }

                var result = new { Status = doc.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Code, InstanceID = doc.LockID, FileType = doc.FileType.Name };
                ApiLog.Write("QueryWorkItemStatus", start);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Failed to Query Work Item");
                throw new HttpResponseException(createresponse);
            }

        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage ClearSelections(WorkItemDTO WorkItem)
        {
            DateTime start = DateTime.Now;
            bool sucess = true;

            try
            {

                var docs = _IDocService.GetSouploads().Where(p => p.LockID == WorkItem.instanceID && p.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Code == "SCSELECTED");

                var batchProcessingWorkflow =
                        _IWkflowdefService.GetWkflowdefs().Where(p => p.Code == "FPW").FirstOrDefault();
                var statusID = batchProcessingWorkflow.WkflowDefWkflowStats.Where(p => p.WkflowStat.Code == "UPLOADED").FirstOrDefault().WkflowStatId;

                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                if (docs != null)
                {
                    foreach (soUpload doc in docs)
                    {

                        // Set INIT state for new workflow instance.
                        doc.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.WkflowStepHists.Add(new WkflowStepHist
                        {
                            CreateDate = DateTime.UtcNow,
                            DateLastMaint = DateTime.UtcNow,
                            WkflowStatId = statusID,
                            CreatedUserId = userID
                        });

                        doc.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.CurrWkflowStatId = statusID;
                        doc.LockID = null;

                        _IDocService.UpdateSoupload(doc);
                    }

                }
                ApiLog.Write("ClearSelections", start);
                return Request.CreateResponse<bool>(HttpStatusCode.OK, sucess);

            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Failed to Clear Selections");
                throw new HttpResponseException(createresponse);
            }
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage ClearDownloaded(string wiKey)
        {
            DateTime start = DateTime.Now;
            try
            {

                var docs = _IDocService.GetSouploads().Where(p => p.soKey == Guid.Parse(wiKey) && p.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Code == "SCDOWNLOADED");
                bool result = false;
                if (docs.Count() > 0)
                {
                    var batchProcessingWorkflow =
                        _IWkflowdefService.GetWkflowdefs().Where(p => p.Code == "FPW").FirstOrDefault();

                    var statusID = batchProcessingWorkflow.WkflowDefWkflowStats.Where(p => p.WkflowStat.Code == "UPLOADED").FirstOrDefault().WkflowStatId;
                    int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                    var doc = docs.FirstOrDefault();

                    doc.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.WkflowStepHists.Add(new WkflowStepHist
                    {
                        CreateDate = DateTime.UtcNow,
                        DateLastMaint = DateTime.UtcNow,
                        WkflowStatId = statusID,
                        CreatedUserId = userID
                    });

                    doc.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.CurrWkflowStatId = statusID;
                    doc.LockID = null;
                    _IDocService.UpdateSoupload(doc);
                    result = true;
                }
                ApiLog.Write("ClearDownloaded", start);
                return Request.CreateResponse<bool>(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Failed to Clear Selections");
                throw new HttpResponseException(createresponse);
            }
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage SelectWorkItems(string InstanceID, int MaxNumber)
        {
            DateTime start = DateTime.Now;
            try
            {

                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                var result = _IDocService.LockDocs(userID, InstanceID, MaxNumber);

                ApiLog.Write("SelectWorkItems", start, result.Count);
                return Request.CreateResponse<object>(HttpStatusCode.OK, result);

            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Failed to Select WorkItems");
                throw new HttpResponseException(createresponse);
            }

        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage DownloadedWorkItems(string InstanceID)
        {
            DateTime start = DateTime.Now;
            try
            {
                var docs = _IDocService.GetSouploads().Where(p => p.LockID == InstanceID && p.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Code == "SCDOWNLOADED").Select(s => new { wiKey = s.soKey, fileTypID = s.FileTypeId });
                ApiLog.Write("DownloadedWorkItems", start);
                return Request.CreateResponse<object>(HttpStatusCode.OK, docs);

            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Failed to Select WorkItems");
                throw new HttpResponseException(createresponse);
            }

        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage UploadingWorkItems(string custguid)
        {
            DateTime start = DateTime.Now;
            try
            {
                Guid guid = Guid.Parse(custguid);

                var docs = _IDocService.GetSouploads().Where(p => p.soOrganizationsKey == guid && p.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Code == "UPLOADING").Select(s => new { soFileName = s.soFileName, Descript = s.Descript, soFormTypesKey = s.soFormTypesKey, soKey = s.soKey });
                ApiLog.Write("UploadingWorkItems", start);
                return Request.CreateResponse<object>(HttpStatusCode.OK, docs);

            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Failed to Select WorkItems");
                throw new HttpResponseException(createresponse);
            }

        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage WorkItemDownloaded(WorkItemDTO WorkItem)
        {
            DateTime start = DateTime.Now;
            int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

            bool sucess = _IDocService.WorkItemDownloaded(userID, WorkItem);
            ApiLog.Write("WorkItemDownloaded", start);
            return Request.CreateResponse<bool>(HttpStatusCode.OK, sucess);

        }


        [Authorize]
        [HttpPost]
        public HttpResponseMessage ZipFileBreakOut(ZipFileImagesDTO request)
        {
            DateTime start = DateTime.Now;
            int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

            var result = _IDocService.ZipFileBreakOut(userID, request);
            if (result == null)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Failed to Create Request");
                throw new HttpResponseException(createresponse);
            }
            ApiLog.Write("ZipFileBreakOut", start, result.Count);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Authorize]
        [HttpPut]
        public HttpResponseMessage UpdateZipUploadStatus(ZipFileImagesDTO request)
        {
            DateTime start = DateTime.Now;
            int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

            var result = _IDocService.UpdateZipUploadStatus(userID, request);

            if (result == null)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Failed to Update");
                throw new HttpResponseException(createresponse);
            }
            ApiLog.Write("UpdateZipUploadStatus", start);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [Authorize]
        [HttpPost]
        public HttpResponseMessage ImageFileBreakOut(ImageFileDocs request)
        {
            DateTime start = DateTime.Now;
            int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

            var result = _IDocService.ImageFileBreakOut(userID, request);

            //var result = batchWkflowInstance;

            if (result == null)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Failed to Create Request");
                throw new HttpResponseException(createresponse);
            }
            ApiLog.Write("ImageFileBreakOut", start, result.Count);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Authorize]
        [HttpPut]
        public HttpResponseMessage setFileException(WorkItemDTO WorkItem)
        {
            DateTime start = DateTime.Now;
            try
            {
                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());


                var doc = _IDocService.GetSouploads().Where(p => p.soKey == Guid.Parse(WorkItem.wiKey)).FirstOrDefault();

                var batchProcessingWorkflow =
                                _IWkflowdefService.GetWkflowdefs().Where(p => p.Code == "FPW").FirstOrDefault();

                var statusID = batchProcessingWorkflow.WkflowDefWkflowStats.Where(p => p.WkflowStat.Code == "EXCP").FirstOrDefault().WkflowStatId;


                doc.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.CurrWkflowStatId = statusID;

                var ccare = doc.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.Org.OrgUsers.Where(p => p.Type == "CustomerCare");

                if (ccare.Count() > 0)
                {
                    doc.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.CCUserId = ccare.FirstOrDefault().UserId;
                }



            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            ApiLog.Write("setFileException", start);
            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage WorkItemInPE(WorkItemDTO WorkItem)
        {
            DateTime start = DateTime.Now;
            int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

            bool sucess = _IDocService.WorkItemInPE(userID, WorkItem);

            ApiLog.Write("WorkItemInPE", start);
            return Request.CreateResponse<bool>(HttpStatusCode.OK, sucess);

        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetPendingEmails(int count)
        {
            DateTime start = DateTime.Now;
            try
            {

                var msgUsers = _IMessageuserService.GetMessageusers().Where(u => u.DeliveryMethod.Name == "EMail" && u.Message.DeliveredDate == null).Take(count);

                var result = PlatformMappingHelper.Map<IList<MessageUser>, IList<MessageDTO>>(msgUsers.ToList()).ToList();

                ApiLog.Write("GetPendingEmails", start);
                return Request.CreateResponse<IList<MessageDTO>>(HttpStatusCode.OK, result);

            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error Retriving Version Number/StorageAccessKey");
                throw new HttpResponseException(createresponse);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage UpdateDeliveryDate(UpdateDelivery Delivered)
        {
            DateTime start = DateTime.Now;
            try
            {
                var msgIDs = Delivered.Ids.Split(',').Select(Int32.Parse).ToList();
                var msgs = _IMessageService.GetMessages().Where(p => msgIDs.Contains(p.Id));

                var currdate = DateTime.UtcNow;

                foreach (var msg in msgs)
                {
                    msg.DeliveredDate = currdate;
                    _IMessageService.UpdateMessage(msg);
                }

                //_UnitOfWork.Commit();

                ApiLog.Write("UpdateDeliveryDate", start);
                return Request.CreateResponse<bool>(HttpStatusCode.OK, true);

            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error Retriving Version Number/StorageAccessKey");
                throw new HttpResponseException(createresponse);
            }

        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetOrganizationInfo(string soOrgKey)
        {
            DateTime start = DateTime.Now;
            try
            {
                var org = _IOrgService.GetOrgs().Where(p => p.soKey == Guid.Parse(soOrgKey)).FirstOrDefault();
                if (org != null)
                {
                    CustomerDTO Result = new CustomerDTO() { soKey = org.soKey.ToString(), OtherAccountNbr = org.OtherAccountNbr };
                    ApiLog.Write("GetOrganizationInfo", start);
                    return Request.CreateResponse<CustomerDTO>(HttpStatusCode.OK, Result);
                }
                else
                {
                    var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Org Key");
                    throw new HttpResponseException(createresponse);
                }
            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error Retriving Organization Info");
                throw new HttpResponseException(createresponse);
            }
        }


        [Authorize]
        [HttpPost]
        public HttpResponseMessage UpdateOrgDocTypeWork(DateRange dates)
        {
            DateTime start = DateTime.Now;
            try
            {
                //Get Distinct DocTypes for a Organization in a date range
                // Get the count
                // Get the Price for that Unit
                // Add entry in the table
                foreach (var org in _IOrgService.GetOrgs().Where(p => p.OrgTyp.TypCd == "CUST"))
                {
                    var docs = _IDocService.GetSouploads().Where(q => q.OrgId == org.Id && q.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.WkflowDef.Code == "DPW" && q.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.WkflowStepHists.OrderByDescending(o=>o.Id).FirstOrDefault().WkflowStat.Code=="CMPL" && q.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().CreateDate >= dates.StartDate && q.WkflowInstanceDocs.FirstOrDefault().WkflowInstance.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().CreateDate <= dates.EndDate).GroupBy(g => g.soFormTypesKey)
                        .Select(cl => new 
                        {
                            Key = cl.Key,
                            Count = cl.Sum(c=>c.soItems)
                        }).ToList(); 
                    foreach(var doc in docs)
                    {
                        var docKey = doc.Key.Value;
                        var count = doc.Count;
                        var docid = _IOrgdoctypService.GetOrgdoctyps().Where(p => p.soKey == docKey).FirstOrDefault().Id;
                        var price =_IOrgdoctypService.GetOrgdoctyps().Where(p => p.soKey == docKey).FirstOrDefault().SowWkflowDocSetup.FirstOrDefault().ListPrice;
                        //var exist = _IOrgdoctypmonthService.GetOrgdoctypmonths().Where(om => om.MonthId == month.Id && om.OrgDocTypId == docid).Count();
                        var exist = _IOrgdoctypdailyuploadService.GetOrgdoctypdailyuploads().Where(od => od.Day == dates.StartDate && od.OrgId== org.Id && od.OrgDocTypId == docid).Count();
                        if (exist<=0)
                        {
                            _IOrgdoctypdailyuploadService.AddOrgdoctypdailyupload(new OrgDocTypDailyUpload { Day = dates.StartDate, Org=org, OrgDocTypId = docid, Price = (decimal)price, Images = (int)count, Revenue = (decimal)price * (int)count });
                           // _IOrgdoctypmonthService.AddOrgdoctypmonth(new OrgDocTypMonth { Month = month, OrgDocTypId = docid, Price = (decimal)price, Images = (int)count, Revenue = (decimal) price * (int)count });
                        }
                    }
                }
                return Request.CreateResponse<bool>(HttpStatusCode.OK, true);

            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error UpdateOrgDocTypeWork");
                throw new HttpResponseException(createresponse);
            }
        }


        [Authorize]
        [HttpPost]
        public HttpResponseMessage UpdateOrgDocTypeMonthlyWork(DateRange dates)
        {
            DateTime start = DateTime.Now;
            try
            {
                //Get Distinct DocTypes for a Organization in a date range
                // Get the count
                // Get the Price for that Unit
                // Add entry in the table
                var pMonth = dates.StartDate.Month.ToString("00") + dates.StartDate.Year.ToString("0000");
                var months = _IMonthService.GetMonths().Where(m => m.Name == pMonth);
                if (months.Count() <= 0)
                {
                    _IMonthService.AddMonth(new Month { Name = pMonth });
                    months = _IMonthService.GetMonths().Where(m => m.Name == pMonth);
                }

                var month = months.FirstOrDefault();
                foreach (var org in _IOrgService.GetOrgs().Where(p => p.OrgTyp.TypCd == "CUST"))
                {
                    var docs = _IOrgdoctypdailyuploadService.GetOrgdoctypdailyuploads().Where(od => od.OrgId == org.Id && od.Day >= dates.StartDate && od.Day <= dates.EndDate).GroupBy(g => g.OrgDocTypId)
                        .Select(cl => new
                        {
                            Key = cl.Key,
                            Count = cl.Sum(c=>c.Images),
                            Revenue = cl.Sum(c=>c.Revenue)
                        }).ToList();

                    foreach (var doc in docs)
                    {
                        var docid = doc.Key;
                        var count = doc.Count;
                        var revenue = doc.Revenue;
                        var price = _IOrgdoctypService.GetOrgdoctyps().Where(p => p.Id== docid).FirstOrDefault().SowWkflowDocSetup.FirstOrDefault().ListPrice;
                        var exist = _IOrgdoctypmonthService.GetOrgdoctypmonths().Where(om => om.MonthId == month.Id && om.OrgId == org.Id && om.OrgDocTypId == docid).Count();
                        if (exist <= 0)
                        {
                            _IOrgdoctypmonthService.AddOrgdoctypmonth(new OrgDocTypMonth { Month = month,Org=org, OrgDocTypId = docid, Price = (decimal)price, Images = (int)count, Revenue = revenue});
                        }
                    }
                }

                return Request.CreateResponse<bool>(HttpStatusCode.OK, true);

            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error UpdateOrgDocTypeWork");
                throw new HttpResponseException(createresponse);
            }

        }
        [Authorize]
        [HttpPost]
        public HttpResponseMessage UpdateOrgMonthCommitments(DateRange dates)
        {
            DateTime start = DateTime.Now;
            try
            {
                //Invalidate Any SOWs that have amandment if EffectiveDate is Begain of the Month
                //Invalidate Any Reseller Aggrement that have amandment if EffectiveDate is Begain of the Month
                //Check If the CurrentMonth is Populated for a Org. 
                //If Yes = Ignore
                //If No = Update
                
                var pMonth = dates.StartDate.Month.ToString("00") + dates.StartDate.Year.ToString("0000");
                var months = _IMonthService.GetMonths().Where(m => m.Name == pMonth);
                if (months.Count() <= 0)
                {
                    _IMonthService.AddMonth(new Month { Name = pMonth });
                    months = _IMonthService.GetMonths().Where(m => m.Name == pMonth);
                }

                var month = months.FirstOrDefault();
                //Inactivate old SOWs
                foreach (var org in _IOrgService.GetOrgs().Where(p => p.OrgTyp.TypCd == "CUST"))
                {
                    var sowWkflow = org.WkflowInstances.Where(w => w.WkflowDef.Code == "SOW");
                    if (sowWkflow.Count() > 0)
                    {
                        var sows = sowWkflow.FirstOrDefault().SowWkflows.Where(i => i.InactiveDate == null && i.EffectiveDate <= start).OrderByDescending(o => o.EffectiveDate).Select(re=>re);
                        if (sows.Count() > 1)
                        {
                            int I = 0;
                            foreach(var sow in sows)
                            {
                                if (I>0)
                                {
                                    sow.InactiveDate = start;
                                }
                                I++;
                            }
                        }
                        if (sows.Count()>0)
                        {
                            var commit = org.OrgMonthCommitments.Where(p => p.MonthId == month.Id);
                            if (commit.Count()<=0)
                            {
                                _IOrgmonthcommitmentService.AddOrgmonthcommitment(new OrgMonthCommitment { Month = month, Org = org, Commitment = (decimal)sows.FirstOrDefault().MonthlyCommitment });
                            }
                        }
                    }
                }

                //Invalidate Reseller aggrement
                foreach (var org in _IOrgService.GetOrgs().Where(p => p.OrgTyp.TypCd == "RESE"))
                {
                    var reseAg= org.OrgReseller.OrgResellerDiscHists.Where(i => i.InActiveDate == null && i.EffectiveDate <= start).OrderByDescending(o => o.EffectiveDate).Select(re => re);
                    if (reseAg.Count() > 0)
                    {
                        int I = 0;
                        foreach (var rese in reseAg)
                        {
                            if (I > 0)
                            {
                                rese.InActiveDate = start;
                            }
                            I++;
                        }
                    }
                }

                return Request.CreateResponse<bool>(HttpStatusCode.OK, true);

            }
            catch (Exception e)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error UpdateOrgDocTypeWork");
                throw new HttpResponseException(createresponse);
            }
        }


    }

    public class ApiLog
    {
        static object Locker = new object();
        static public void Write(string ApiName, DateTime start, int? Items = null)
        {
            //DateTime now = DateTime.Now;
            //TimeSpan span = now - start;
            //string FileName = string.Format("C:\\soLogs\\API-{0}.Log", now.ToString("yyyyMMdd"));
            //string itemStr = (Items == null) ? "          " : string.Format("(items={0,2})", Items);
            //string Msg = now.ToString("hh:mm:ss: ")
            //            + string.Format("T{0,02} ", System.Threading.Thread.CurrentThread.ManagedThreadId)
            //            + string.Format("{0,-22} ", ApiName + ":")
            //            + itemStr
            //            + string.Format("{0,5:N1}s", span.TotalSeconds);
            //lock (Locker)
            //{
            //    StreamWriter wr = new StreamWriter(FileName, true);
            //    wr.WriteLine(Msg);
            //    wr.Close();
            //}
        }
    }

}