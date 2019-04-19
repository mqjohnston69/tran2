using RAMLSharp.Attributes;
using Oc.Carbon.Common.Contracts;
using Oc.Carbon.DataAccess;
using Oc.Carbon.DTO.Mapping.Core;
using Oc.Carbon.DTO.SolutionDTO;
using Oc.Carbon.ServiceLayer.Contracts;
using Oc.Carbon.WebServices.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Oc.Carbon.WebServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        ILogService _loggerService;
        IOrgService _IOrgService;
        IOrgcustService _IOrgcustService;
        IOrgresellerService _IOrgresellerService;
        IOrgorgService _IOrgorgService;
        IOrgtyporgstatusService _IOrgtyporgstatusService;
        IUserService _IUserService;
        IUserloginhistService _IUserloginhistService;
        IWkflowinstanceService _IWkflowinstanceService;
        IOrgdoctypService _IOrgdoctypService;
        IOrguserService _IOrguserService;
        IOrgtypService _IOrgtypService;
        IAuthrolService _IAuthrolService;
        IMessagetemplateService _IMessagetemplateService;
        IOrglocnService _IOrglocnService;

        public CustomerController(ILogService loggerService, IOrgService orgService, IOrgcustService orgCustService,
           IOrgresellerService orgResellerService, IOrgorgService orgorgService, IOrgtyporgstatusService orgtyporgstatusService,
           IUserService userService, IUserloginhistService userloginhistService, IWkflowinstanceService wkflowinstanceService,
           IOrgdoctypService orgdoctypService, IOrguserService orguserService, IOrgtypService orgtypService,
           IAuthrolService authrolService, IMessagetemplateService messagetemplateService,
           IOrglocnService orglocnService)
        {
            this._loggerService = loggerService;
            this._IOrgService = orgService;
            _IOrgcustService = orgCustService;
            _IOrgresellerService = orgResellerService;
            _IOrgorgService = orgorgService;
            _IOrgtyporgstatusService = orgtyporgstatusService;
            _IUserService = userService;
            _IUserloginhistService = userloginhistService;
            _IWkflowinstanceService = wkflowinstanceService;
            _IOrgdoctypService = orgdoctypService;
            _IOrguserService = orguserService;
            _IOrgtypService = orgtypService;

            _IAuthrolService = authrolService;
            _IMessagetemplateService = messagetemplateService;
            _IOrglocnService = orglocnService;


        }



        [Authorize]
        [HttpPost]
        public HttpResponseMessage Validate(CustomerDTO customerDTO)
        {
            try
            {
                if (customerDTO.parentId==null)
                {
                    customerDTO.parentId = 1;
                }

                var customer = _IOrgcustService.GetOrgcusts().Where(p => p.Org.Name == customerDTO.Name);
                if (customer.Count() <= 0)
                {
                    int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                    var curDateTime = DateTime.UtcNow;

                    var guid = Guid.NewGuid();

                    Org newOrg = new Org {
                        Name = customerDTO.Name,
                        Agreement =  customerDTO.Agreement,
                        ApprovedUserId = userID,
                        BillingInfo =  customerDTO.BillingInfo,
                        ApprovedDate = DateTime.UtcNow,
                        Comments =  customerDTO.Comments,
                        CreateDate = DateTime.UtcNow,
                        CreatedUserId = userID,
                        Descript = customerDTO.Descript,
                        GotAgreement = customerDTO.GotAgreement,
                        ImageCleanUp = customerDTO.ImageCleanUp,
                        ModifiedDate = DateTime.UtcNow,
                        ModifiedUserId = userID,

                    };

                    newOrg.OrgOrgs.Add(new OrgOrg() { OrgId = customerDTO.parentId, AssociatedOrgId = newOrg.Id });

                    OrgCust newCustomer = new OrgCust()
                    {
                       Org = newOrg,
                       SubmissionOpts = customerDTO.SubmissionOpts,
                       RemoveBlank = customerDTO.RemoveBlank                        
                    };                  

                    _IOrgcustService.AddOrgcust(newCustomer);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Customer Needs to be Unique");
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            

            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }
       
        [Authorize]
        [HttpPost]
        public HttpResponseMessage search(CustomerSearchRequest requestInfo)
        {
            var orgs = _IOrgcustService.GetCustomersByFilter(requestInfo);
            var result = PlatformMappingHelper.Map<IList<OrgCust>, IList<CustomerDTO>>(orgs.ToList()).ToList();

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<CustomerDTO>>(HttpStatusCode.OK, result);

        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage MetricsByDocType(CustomerSearchRequest requestInfo)
        {
            var docTypeList = _IOrgdoctypService.GetOrgdoctyps().Where(p => p.OrgId == requestInfo.orgId).Select(s => s.Id).ToList();

            var result = _IWkflowinstanceService.GetWkflowinstances().Where(p => p.WkflowDefId==4 && p.OrgId == requestInfo.orgId && (requestInfo.StartDate==null || p.CreateDate> requestInfo.StartDate) && (requestInfo.EndDate == null || p.CreateDate > requestInfo.EndDate)).Where(q=> docTypeList.Contains((int)q.WkflowInstanceDocs.FirstOrDefault().Doc.OrgDocTypId)).GroupBy(g => g.WkflowInstanceDocs.FirstOrDefault().Doc.OrgDocTyp.Descript).Select(s => new { Name = s.Key, Value = s.Count() });
            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, result);

        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage Statuses()
        {

            var otoss = _IOrgtyporgstatusService.GetOrgtyporgstatus().FindAll(p => p.OrgTyp.Name == "Customer").Select(o=> new {Id = o.Id, StatusCd = o.OrgStatus.StatusCd, Name = o.OrgStatus.DisplayText });

            return Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, otoss);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage ReSellers()
        {

            var otoss = _IOrgService.GetOrgs().Where(p => p.OrgTyp.TypCd != "CUST").Select(o=> new { id = o.Id, Name = o.Name });

            return Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, otoss);
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage SoSalesReps(int? org_id)
        {
            org_id = 1; //Scanoptics 

            var salesReps = _IOrguserService.GetOrgusers().Where(p=> p.OrgId==org_id).Select(a => new { id = a.UserId, name = a.User.Per.LastName + ", " + a.User.Per.FirstName });

            return Request.CreateResponse(HttpStatusCode.OK, salesReps);
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage CustomerCareReps(int? org_id)
        {
            org_id = 1;  //Scanoptics 

            var custCare = _IOrguserService.GetOrgusers().Where(p => p.OrgId == org_id).Select(a => new { id = a.UserId, name = a.User.Per.LastName + ", " + a.User.Per.FirstName });

            return Request.CreateResponse(HttpStatusCode.OK, custCare);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage ResellerUsers(int org_Id)
        {
            //Reseller OrgID
            var resellerusers = _IOrgService.GetOrg(org_Id).OrgUsers.Where(q=> q.Type == "Primary" || q.Type== null).Select(p=> new { id = p.UserId, name = p.User.Per.LastName + ", " + p.User.Per.FirstName });

            return Request.CreateResponse(HttpStatusCode.OK, resellerusers);
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage Customer(int custId)
        {

            CustomerDTO search = null;

            var customer = _IOrgcustService.GetOrgcusts().Where(p=>p.Org.Id==custId).FirstOrDefault(); 

            if (customer != null)
            {
                search = PlatformMappingHelper.Map<OrgCust, CustomerDTO>(customer);

                if (search == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Customer could not be found");
            }           

            return Request.CreateResponse<CustomerDTO>(HttpStatusCode.OK, search);
        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage LoginHist(int custId)
        {
            var logins = _IOrgService.GetOrg(custId).OrgUsers.Where(q => q.Type == "Primary" || q.Type == null).Where(r=>r.User.UserLoginHists.Count() > 0).Select(p => p.User.UserLoginHists.OrderByDescending(o => o.LoginDate).FirstOrDefault()).Select(s => new { UserName = s.User.UserName, Name = s.User.Per.FirstName + ", " + s.User.Per.LastName, LastLogin = s.LoginDate }).ToList();

            if (logins == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, logins);

        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage RecentActivity(int custId)
        {
            var uploadActivities = _IWkflowinstanceService.GetRecentUploadActivity(custId,10);

            var result = PlatformMappingHelper.Map<IList<WkflowInstance>, IList<BatchProcessingWorkflowDTO>>(uploadActivities).ToList();

            if (uploadActivities == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<BatchProcessingWorkflowDTO>>(HttpStatusCode.OK, result);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage ActivityCounts(int? orgId)
        {
            if (orgId == null)
            {
                orgId = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());
            }
            var finalreturn = _IWkflowinstanceService.ActivityCounts((int)orgId);

            return Request.CreateResponse<List<object>>(HttpStatusCode.OK, finalreturn);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage ActivityByWrkflowDef(int custId, int? defId,DateTime? startDate,DateTime? endDate)
        {
            IEnumerable<object> finalreturn;

            if (startDate==null)
            {
                startDate = DateTime.UtcNow.AddDays(-30);
                endDate = DateTime.UtcNow;
            }

            if (defId != null)
            {
                finalreturn = _IWkflowinstanceService.ActivityByWrkflowDef(custId, defId,startDate,endDate).Select(i => new
                {
                    Status = i.Key.Descript,
                    Code = i.Key.Code,
                    Color = i.FirstOrDefault().WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Color,
                    Count = i.Count()
                });
            }
            else
            {
                finalreturn = _IWkflowinstanceService.ActivityByWrkflowDef(custId, 3, startDate, endDate).Select(i => new
                {
                    DefName = "DocTypes",
                    Status = i.Key.Descript,
                    Code = i.Key.Code,
                    Color = i.FirstOrDefault().WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Color,
                    Count = i.Count()
                }).Union(_IWkflowinstanceService.ActivityByWrkflowDef(custId, 4, startDate, endDate).Select(i => new
                {
                    DefName = "Documents",
                    Status = i.Key.Descript,
                    Code = i.Key.Code,
                    Color = i.FirstOrDefault().WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Color,
                    Count = i.Count()
                })).Union(_IWkflowinstanceService.ActivityByWrkflowDef(custId, 5, startDate, endDate).Select(i => new
                {
                    DefName = "Files",
                    Status = i.Key.Descript,
                    Code = i.Key.Code,
                    Color = i.FirstOrDefault().WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Color,
                    Count = i.Count()
                })).Union(_IWkflowinstanceService.ActivityByWrkflowDef(custId, 6, startDate, endDate).Select(i => new
                {
                    DefName = "Supports",
                    Status = i.Key.Descript,
                    Code = i.Key.Code,
                    Color = i.FirstOrDefault().WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Color,
                    Count = i.Count()
                }));  
            }

            if (finalreturn == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, finalreturn);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage ImageCountByStatus(int custId, DateTime? startDate, DateTime? endDate)
        {
            IEnumerable<object> finalreturn;

            if (startDate == null)
            {
                startDate = DateTime.UtcNow.AddDays(-30);
                endDate = DateTime.UtcNow;
            }

            finalreturn = _IWkflowinstanceService.ActivityByWrkflowDef(custId, 5, startDate, endDate).Select(i => new
            {
                Status = i.Key.Descript,
                Code = i.Key.Code,
                Color = i.FirstOrDefault().WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Color,
                Images = i.Sum(o=>o.WkflowInstanceDocs.FirstOrDefault().Doc.soPages)
            });

            if (finalreturn == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, finalreturn);
        }

       

        [Authorize]
        [HttpGet]
        public HttpResponseMessage ActivityByDay(int custId)
        {

            var uploadActivities = _IWkflowinstanceService.GetDocActivityByFilter(new UploadActivitySearchRequest { OrgId = custId, StartDate = DateTime.UtcNow.AddDays(-30) }).OrderBy(o=>o.CreateDate).GroupBy(g=>((DateTime)g.CreateDate).ToString("dd-MMM-yyyy")).Select(i => new
            {               
                Date = i.Key.Substring(0,6),
                Count = i.Count()
            });

            List<object> finalwork = new List<object>();

            for (int day = -30; day <= 0; day++)
            {
                int count = 0;
                var datecal = DateTime.UtcNow.AddDays(day).ToString("dd-MMM");
               
                var find = uploadActivities.Where(p => p.Date == datecal);
                if (find.Count() > 0)
                {
                    count = find.FirstOrDefault().Count;
                }

                finalwork.Add(new { Date = datecal, Count = count});
            }

            if (finalwork == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, finalwork);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage SupportActivityByDay(SupportSearchRequest requestInfo)
        {

            int? org_id = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());

            if (requestInfo.OrgId == null)
            {
                requestInfo.OrgId = org_id;
            }

            if (requestInfo.StartDate == null)
            {
                requestInfo.StartDate = DateTime.UtcNow.AddDays(-30);
                requestInfo.EndDate = DateTime.UtcNow;
            }

            List<string> statusCodes = null;
            statusCodes = new List<string> { "Created", "EXCP", "Assigned to CustomerCare", "Assigned to Customer" };

            var data = _IWkflowinstanceService.GetWkflowinstances().Where(p =>
                                                                         (p.WkflowStepHists.Count > 0 ? statusCodes.Contains(p.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStat.Code) : false) &&
                                                                         (requestInfo.OrgId == null || requestInfo.OrgId == p.OrgId) && 
                                                                         (requestInfo.StartDate == null || p.CreateDate >= requestInfo.StartDate) && 
                                                                         (requestInfo.EndDate == null || p.CreateDate <= requestInfo.EndDate));

            var allwork = data.OrderBy(o => o.CreateDate).GroupBy(g => ((DateTime)g.CreateDate).ToString("dd-MMM-yyyy")).Select(i => new
            {
                Date = i.Key.Substring(0, 6),
                Count = i.Count()
            });

            var closedwork = data.Where(q=>q.WkflowStepHists.OrderByDescending(o=>o.Id).FirstOrDefault().WkflowStat.Code=="Closed").GroupBy(g => ((DateTime)g.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().CreateDate).ToString("dd-MMM")).Select(i => new
            {
                Date = i.Key.Substring(0, 6),
                Count = i.Count()
            });

            List<object> finalwork = new List<object>();

            for (int day = -30; day <= 0; day++)
            {
                int opencount = 0;
                int closedcount = 0;
                var datecal = DateTime.UtcNow.AddDays(day).ToString("dd-MMM");              
                var find = allwork.Where(p => p.Date == datecal);
                if (find.Count()>0)
                {
                    opencount = find.FirstOrDefault().Count;
                }

                var findclosed = closedwork.Where(p => p.Date == datecal);
                if (findclosed.Count() > 0)
                {
                    closedcount = findclosed.FirstOrDefault().Count;
                }

                finalwork.Add(new { Date=datecal, Open = opencount, Closed = closedcount});
            }

            if (finalwork == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, finalwork);

        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage SLAByDocType(int? custId)
        {

            DateTime startDate = DateTime.UtcNow.AddDays(-30);
            DateTime endDate = DateTime.UtcNow;

            var uploadActivities = _IWkflowinstanceService.GetSLAByDocType(custId, startDate, endDate);

            List<object> finalwork = new List<object>();

            foreach (var activity in uploadActivities)
            {
                string name = "";
                int totalCount = 0;
                int goodCount = 0;
                int badCount = 0;
                goodCount=activity.Select(p => p.WkflowInstanceDocs.FirstOrDefault().Doc.SLAInHours <= activity.Key.SLAInHours).Count();
                totalCount = activity.Count();

                badCount = totalCount - goodCount;
                finalwork.Add(new {CustId= activity.Key.Org.Id, CustomerName =activity.Key.Org.Name, DocType=activity.Key.Descript,Total= totalCount, good= goodCount,bad= badCount });
            }

            if (finalwork == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, finalwork);
        }


        [HttpPut]
        [Authorize]
        public HttpResponseMessage Add(CustomerDTO customerDTO)
        {
            try
            {
                var curDateTime = DateTime.UtcNow;
                var customer = new OrgCust();
                customer.Org = new Org();
                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                if (customer != null)
                {
                    customer.Org.Name = customerDTO.Name;
                    customer.Org.Descript = customerDTO.Descript;
                    customer.Org.Comments = customerDTO.Comments;
                    customer.Org.SOAccountNbr = customerDTO.CustomerNbr;
                    customer.Org.OtherAccountNbr = customerDTO.OtherAccountNbr;

                    customer.Org.Agreement = customerDTO.Agreement;
                    customer.Org.Logo = customerDTO.Logo;

                    customer.Org.PromoCode = customerDTO.PromoCode;
                    customer.Org.BillMe = customerDTO.BillMe;
                    customer.Org.BillingInfo = customerDTO.BillingInfo;
                    customer.Org.ImageCleanUp = customerDTO.ImageCleanUp;
                    customer.SLA = customerDTO.SLA;
                    customer.RemoveBlank = customerDTO.RemoveBlank;
                    customer.SubmissionOpts = customerDTO.SubmissionOpts;

                    customer.Org.SOW = customerDTO.SOW;
                    customer.Org.GotAgreement = customerDTO.GotAgreement;

                    customer.Org.ModifiedDate = DateTime.UtcNow;
                    customer.Org.ModifiedUserId = userID;

                    customer.Org.OrgLocns.Add(new OrgLocn() { Org = customer.Org, Locn = new Locn() { AddressLine1 = customerDTO.AddressLine1, AddressLine2 = customerDTO.AddressLine2, City = customerDTO.City, State = customerDTO.State, ZipCode = customerDTO.ZipCode } });


                    if (customerDTO.SalesRepId == 0)
                    {
                        if (customer.Org.OrgUsers.Where(p => p.Type == "SalesRep").Count() > 0)
                        {
                            customer.Org.OrgUsers.Remove(customer.Org.OrgUsers.Where(p => p.Type == "SalesRep").FirstOrDefault());
                        }
                    }
                    else if (customer.Org.OrgUsers.Where(p => p.Type == "SalesRep").FirstOrDefault() != null)
                    {
                        customer.Org.OrgUsers.Where(p => p.Type == "SalesRep").FirstOrDefault().UserId = (int)customerDTO.SalesRepId;
                    }
                    else
                    {
                        customer.Org.OrgUsers.Add(new OrgUser() { Org = customer.Org, UserId = (int)customerDTO.SalesRepId, Type = "SalesRep" });
                    }


                    if (customerDTO.CustomerCareId == 0)
                    {
                        if (customer.Org.OrgUsers.Where(p => p.Type == "CustomerCare").Count() > 0)
                        {
                            customer.Org.OrgUsers.Remove(customer.Org.OrgUsers.Where(p => p.Type == "CustomerCare").FirstOrDefault());
                        }
                    }
                    else if (customer.Org.OrgUsers.Where(p => p.Type == "CustomerCare").FirstOrDefault() != null)
                    {
                        customer.Org.OrgUsers.Where(p => p.Type == "CustomerCare").FirstOrDefault().UserId = (int)customerDTO.CustomerCareId;
                    }
                    else
                    {
                        customer.Org.OrgUsers.Add(new OrgUser() { Org = customer.Org, UserId = (int)customerDTO.CustomerCareId, Type = "CustomerCare" });
                    }

                    if (customerDTO.ResellerRepId == 0 || customerDTO.ResellerRepId == null)
                    {
                        if (customer.Org.OrgUsers.Where(p => p.Type == "ResellerRep").Count() > 0)
                        {
                            customer.Org.OrgUsers.Remove(customer.Org.OrgUsers.Where(p => p.Type == "ResellerRep").FirstOrDefault());
                        }
                    }
                    else if (customer.Org.OrgUsers.Where(p => p.Type == "ResellerRep").FirstOrDefault() != null)
                    {
                        customer.Org.OrgUsers.Where(p => p.Type == "ResellerRep").FirstOrDefault().UserId = (int)customerDTO.ResellerRepId;
                    }
                    else
                    {
                        customer.Org.OrgUsers.Add(new OrgUser() { Org = customer.Org, UserId = (int)customerDTO.ResellerRepId, Type = "ResellerRep" });
                    }

                    var curOrgStatus = _IOrgtyporgstatusService.GetOrgtyporgstatus().Where(p => p.OrgStatus.DisplayText == customerDTO.StatusName).FirstOrDefault();
                    if (curOrgStatus != null)
                    {
                        customer.Org.OrgStatusHists.Add(new OrgStatusHist() { Org = customer.Org, OrgTypOrgStatu = curOrgStatus, CreateDate = DateTime.UtcNow });
                    }
                    if (customerDTO.StatusName == "Active")
                    {
                        customer.Org.ApprovedUserId = userID;
                        customer.Org.ApprovedDate = DateTime.UtcNow;
                        customer.Org.InactiveUserId = null;
                        customer.Org.InactiveDate = null;

                        customer.Org.WkflowInstances.FirstOrDefault().WkflowStepHists.Add(new WkflowStepHist
                        {
                            CreateDate = DateTime.UtcNow,
                            DateLastMaint = DateTime.UtcNow,
                            WkflowStatId = 1,
                            CreatedUserId = userID
                        });
                    }
                    else if (customerDTO.StatusName == "Suspended")
                    {
                        customer.Org.InactiveUserId = userID;
                        customer.Org.InactiveDate = DateTime.UtcNow;
                    }

                    _IOrgcustService.AddOrgcust(customer);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Customer could not be found");
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }

        [HttpPut]
        [Authorize]
        public HttpResponseMessage Update(CustomerDTO customerDTO)
        {
            try
            {
                var curDateTime = DateTime.UtcNow;
                var customer = _IOrgcustService.GetOrgcusts().Where(p=>p.Org.Id==customerDTO.Id).FirstOrDefault();
                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                if (customer != null)
                {
                    customer.Org.Name = customerDTO.Name;
                    customer.Org.Descript = customerDTO.Descript;
                    customer.Org.Comments = customerDTO.Comments;
                    customer.Org.SOAccountNbr = customerDTO.CustomerNbr;
                    customer.Org.OtherAccountNbr = customerDTO.OtherAccountNbr;

                    customer.Org.Agreement = customerDTO.Agreement;
                    customer.Org.Logo = customerDTO.Logo;

                    customer.Org.PromoCode= customerDTO.PromoCode;
                    customer.Org.BillMe = customerDTO.BillMe;
                    customer.Org.BillingInfo = customerDTO.BillingInfo;
                    customer.Org.ImageCleanUp = customerDTO.ImageCleanUp;
                    customer.SLA = customerDTO.SLA;
                    customer.RemoveBlank= customerDTO.RemoveBlank;
                    customer.SubmissionOpts = customerDTO.SubmissionOpts;

                    customer.Org.SOW = customerDTO.SOW;
                    customer.Org.GotAgreement = customerDTO.GotAgreement;

                    customer.Org.ModifiedDate = DateTime.UtcNow;
                    customer.Org.ModifiedUserId = userID;

                    var locn = _IOrglocnService.GetOrglocns().Where(p => p.Org.Id == customer.Org.Id);
                    if (locn.Count() > 0)
                    {
                    customer.Org.OrgLocns.FirstOrDefault().Locn.AddressLine1 = customerDTO.AddressLine1;
                    customer.Org.OrgLocns.FirstOrDefault().Locn.AddressLine2 = customerDTO.AddressLine2;
                    customer.Org.OrgLocns.FirstOrDefault().Locn.City = customerDTO.City;
                    customer.Org.OrgLocns.FirstOrDefault().Locn.State = customerDTO.State;
                    customer.Org.OrgLocns.FirstOrDefault().Locn.ZipCode = customerDTO.ZipCode;
                    }
                    else
                    {
                        customer.Org.OrgLocns.Add(new OrgLocn() { Org = customer.Org, Locn = new Locn() { AddressLine1 = customerDTO.AddressLine1, AddressLine2 = customerDTO.AddressLine2, City = customerDTO.City, State = customerDTO.State, ZipCode = customerDTO.ZipCode } } );
                    }

                    if (customer.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault() != null)
                    {
                        if (customer.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().UserId == customerDTO.ContactId)
                        {
                            customer.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.Title = customerDTO.Title;
                        }
                        else
                        {
                            customer.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().Type= null;
                            customer.Org.OrgUsers.Where(p => p.UserId == customerDTO.ContactId).FirstOrDefault().Type = "Primary";
                        customer.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.Title = customerDTO.Title;
                    }
                    }
                    else if (customer.Org.OrgUsers.Where(p => p.Type == null).FirstOrDefault() != null)
                    {
                        customer.Org.OrgUsers.Where(p => p.Type == null).FirstOrDefault().UserId = customerDTO.ContactId;
                        customer.Org.OrgUsers.Where(p => p.Type == null).FirstOrDefault().User.Per.Title = customerDTO.Title;
                        customer.Org.OrgUsers.Where(p => p.Type == null).FirstOrDefault().Type = "Primary";
                    }

                    if (customer.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.PersContacts.Where(b => b.Contact.ContactTyp.DisplayName == "Phone").Count()>0)
                    {
                        customer.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.PersContacts.Where(b => b.Contact.ContactTyp.DisplayName == "Phone").FirstOrDefault().Contact.Value = customerDTO.Phone;
                    }
                    else
                    {
                        customer.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.PersContacts.Add(new PersContact() { Per= customer.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per, Contact = new Contact() { ContactTypId=2, Value= customerDTO.Phone } });
                    }



                    if (customerDTO.SalesRepId == 0)
                    {
                        if (customer.Org.OrgUsers.Where(p => p.Type == "SalesRep").Count() > 0)
                        {
                            customer.Org.OrgUsers.Remove(customer.Org.OrgUsers.Where(p => p.Type == "SalesRep").FirstOrDefault());
                        }
                    }
                    else if (customer.Org.OrgUsers.Where(p => p.Type == "SalesRep").FirstOrDefault() !=null)
                    {
                        customer.Org.OrgUsers.Where(p => p.Type == "SalesRep").FirstOrDefault().UserId = (int) customerDTO.SalesRepId;
                    }
                    else
                    {
                        customer.Org.OrgUsers.Add(new OrgUser() {Org=customer.Org, UserId= (int) customerDTO.SalesRepId, Type= "SalesRep" });
                    }


                    if (customerDTO.CustomerCareId == 0)
                    {
                        if (customer.Org.OrgUsers.Where(p => p.Type == "CustomerCare").Count() > 0)
                        {
                            customer.Org.OrgUsers.Remove(customer.Org.OrgUsers.Where(p => p.Type == "CustomerCare").FirstOrDefault());
                        }
                    }
                    else if (customer.Org.OrgUsers.Where(p => p.Type == "CustomerCare").FirstOrDefault() != null)
                    {
                        customer.Org.OrgUsers.Where(p => p.Type == "CustomerCare").FirstOrDefault().UserId = (int)customerDTO.CustomerCareId;
                    }
                    else
                    {
                        customer.Org.OrgUsers.Add(new OrgUser() { Org = customer.Org, UserId = (int)customerDTO.CustomerCareId, Type = "CustomerCare" });
                    }

                    if (customerDTO.ResellerRepId == 0 || customerDTO.ResellerRepId == null)
                    {
                        if (customer.Org.OrgUsers.Where(p => p.Type == "ResellerRep").Count() > 0)
                        {
                            customer.Org.OrgUsers.Remove(customer.Org.OrgUsers.Where(p => p.Type == "ResellerRep").FirstOrDefault());
                        }
                    }
                    else if (customer.Org.OrgUsers.Where(p => p.Type == "ResellerRep").FirstOrDefault() != null)
                    {
                        customer.Org.OrgUsers.Where(p => p.Type == "ResellerRep").FirstOrDefault().UserId = (int)customerDTO.ResellerRepId;
                    }
                    else
                    {
                        customer.Org.OrgUsers.Add(new OrgUser() { Org = customer.Org, UserId = (int)customerDTO.ResellerRepId, Type = "ResellerRep" });
                    }

                    var OrgStatHist = customer.Org.OrgStatusHists.OrderByDescending(s => s.Id).FirstOrDefault();
                    if (OrgStatHist != null && (OrgStatHist.OrgTypOrgStatu.OrgStatus.DisplayText != customerDTO.StatusName))
                    {
                        var curOrgStatus = _IOrgtyporgstatusService.GetOrgtyporgstatus().Where(p => p.OrgStatus.DisplayText == customerDTO.StatusName).FirstOrDefault();
                        if (curOrgStatus != null)
                        {
                            customer.Org.OrgStatusHists.Add(new OrgStatusHist() { Org = customer.Org, OrgTypOrgStatu = curOrgStatus, CreateDate = DateTime.UtcNow });
                        }
                        if (customerDTO.StatusName == "Active")
                        {
                            customer.Org.ApprovedUserId = userID;
                            customer.Org.ApprovedDate = DateTime.UtcNow;
                            customer.Org.InactiveUserId = null;
                            customer.Org.InactiveDate = null;

                            customer.Org.WkflowInstances.FirstOrDefault().WkflowStepHists.Add(new WkflowStepHist
                            {
                                CreateDate = DateTime.UtcNow,
                                DateLastMaint = DateTime.UtcNow,
                                WkflowStatId = 1,
                                CreatedUserId = userID
                            });

                        }
                        else if (customerDTO.StatusName == "Suspended")
                        {
                            customer.Org.InactiveUserId = userID;
                            customer.Org.InactiveDate = DateTime.UtcNow;
                        }
                    }

                    _IOrgcustService.UpdateOrgcust(customer);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Customer could not be found");
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        

            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }


    }
}