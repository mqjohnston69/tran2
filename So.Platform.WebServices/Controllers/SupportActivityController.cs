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
using RAMLSharp.Attributes;

namespace Oc.Carbon.WebServices.Controllers
{
    public class SupportActivityController : ApiController
    {
        ILogService _LoggerService;
        IUserService _IUserService;
        IUnitOfWork _UnitOfWork;

        IWkflowdefService _IWkflowdefService;
        IWkflowinstanceService _IWkflowinstanceService;
        IDpworkflowService _IDpworkflowService;
        IOrgdoctypdataelmService _IOrgdoctypdataelmService;
        IOrgService _IOrgService;
        IWkflowstatService _IWkflowstatService;
        IOrguserService _IOrguserService;
        IUserauthrolService _IUserauthrolService;
        IOrgorgService _IOrgorgService;
        IOrgcustService _IOrgcustService;
        IOrgresellerService _IOrgresellerService;
        IOrgdoctypService _IOrgdoctypService;
        ISouploadService _ISouploadService;
        IOrgdoctypdailyuploadService _IOrgdoctypdailyuploadService;

        public SupportActivityController(ILogService loggerService, IUserService userService, IUnitOfWork unitOfWork,
            IWkflowdefService wkflowdefService, IWkflowinstanceService wkflowinstanceService,
            IDpworkflowService dpworkflowService, IOrgdoctypdataelmService orgdoctypdataelmService, IOrgService orgService, 
            IWkflowstatService wkflowstatService, IOrguserService orguserService, IUserauthrolService userauthrolService,
            IOrgorgService orgorgService, IOrgcustService orgcustService, IOrgresellerService orgresellerService,
            IOrgdoctypService orgdoctypService, ISouploadService souploadService, IOrgdoctypdailyuploadService orgdoctypdailyuploadService)
        {

            this._LoggerService = loggerService;
            this._IUserService = userService;
            this._IWkflowdefService = wkflowdefService;
            this._IWkflowinstanceService = wkflowinstanceService;
            this._IDpworkflowService = dpworkflowService;
            this._IOrgdoctypdataelmService = orgdoctypdataelmService;
            this._UnitOfWork = unitOfWork;
            this._IOrgService = orgService;
            this._IWkflowstatService = wkflowstatService;
            this._IOrguserService = orguserService;
            this._IUserauthrolService = userauthrolService;
            this._IOrgorgService = orgorgService;
            this._IOrgcustService = orgcustService;
            this._IOrgresellerService = orgresellerService;
            this._IOrgdoctypService = orgdoctypService;
            this._ISouploadService = souploadService;
            this._IOrgdoctypdailyuploadService = orgdoctypdailyuploadService;
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage ActivityTypes()
        {

            var activityTypes = _IWkflowdefService.GetWkflowdefs().Select(u => new { id = u.Id, Name = u.Name});

            if (activityTypes == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Request.CreateResponse<object>(HttpStatusCode.OK, activityTypes);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage OrgList()
        {

            List<dynamic> orgs = new List<dynamic>();
            int org_id = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());

            if (org_id == 1)
            {
                orgs.AddRange(_IOrgService.GetOrgs().Where(o => o.Id != 1).Select(p => new { Id = p.Id, Name = p.Name }).ToList());
            }
            else
            {
                var reseller = _IOrgService.GetOrg(org_id);
                orgs.Add(new { Id = reseller.Id, Name = reseller.Name }); 
               orgs.AddRange(_IOrgorgService.GetOrgorgs().Where(o => o.OrgId == org_id).Select(p => new { Id = p.Org1.Id, Name = p.Org1.Name }));
            }


            return Request.CreateResponse(HttpStatusCode.OK, orgs);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage CustomerCareReps()
        {
            var custCare = _IUserauthrolService.GetUserauthrols().Where(p => p.AuthRol.Name.Contains("Customer Care Admin")).Select(a => new { id = a.User.Id, name = a.User.Per.LastName + ", " + a.User.Per.FirstName });

            return Request.CreateResponse(HttpStatusCode.OK, custCare);
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage SupportRequestReasons()
        {

            var supportRequestTypes = _IWkflowdefService.GetWkflowdefs().Find(p => p.Code == "SRW").WkflowDefWkflowStats.Where(q => q.WkflowStat.Code == "Created").FirstOrDefault().WkflowDefStatReas.Select(r => new { id = r.WkflowStatRea.Id, Name = r.WkflowStatRea.Descript }).ToList();

            if (supportRequestTypes == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Request.CreateResponse<object>(HttpStatusCode.OK, supportRequestTypes);
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage NextAllowedSupportStates(int id)
        {

            var work = _IWkflowinstanceService.GetWkflowinstance(id);
            var currentStateId = work.CurrWkflowStatId;

            var supportRequestTypes = _IWkflowdefService.GetWkflowdefs().Find(p => p.Id == work.WkflowDefId).WkflowDefWkflowStats.Where(q => q.WkflowStat.Id == currentStateId).FirstOrDefault().WkflowDefWkflowStatWkflowStats.Select(r => new { id = r.WkflowStat.Id, Name = r.WkflowStat.Descript}).ToList();

            if (supportRequestTypes == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Request.CreateResponse<object>(HttpStatusCode.OK, supportRequestTypes);
        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage TicketsOpenedSinceLastLogin()
        {
            int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

            var loginHist = _IUserService.GetUser(userID).UserLoginHists.OrderByDescending(o => o.Id);

            var userLastLogin = loginHist.Count() > 0 ? loginHist.FirstOrDefault().LoginDate : null;

            var data = _IWkflowinstanceService.GetWkflowinstances().Where(p => p.WkflowDef.Code.Contains("SRW") && p.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Code != "Closed" && (userLastLogin == null || p.CreateDate >= userLastLogin)).Select(u=> new {Id = u.Id, CustomerName = u.Org.Name, Reason = u.WkflowStepHists.OrderBy(j=>j.Id).FirstOrDefault().WkflowStatRea.Descript});

            return Request.CreateResponse<object>(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage OpenRequests()
        {
            List<int> Orgs = new List<int>();
            int CustomerCareId = 0;
            int orgid = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());
            var org = _IOrgService.GetOrg(orgid);

            if (org.OrgTyp.TypCd == "RESE")
            {
                // Add the reseller org and all their customers to the orglist
                Orgs.Add(orgid);
                var custOrgs = _IOrgorgService.GetOrgorgs().Where(p => p.OrgId == orgid).Select(s => s.AssociatedOrgId).ToList();
                Orgs.AddRange(custOrgs);
            }
            else if (org.OrgTyp.TypCd == "CUST")
            {
                Orgs.Add(orgid);
            }
            else
            {
                Orgs = null;
                CustomerCareId = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());
            }

            List<string> statusCodes = null;
            statusCodes = new List<string> { "Created", "EXCP", "Assigned to CustomerCare", "Assigned to Customer" };



            int data = _IWkflowinstanceService.GetWkflowinstances().Where(p =>
                      (Orgs == null || Orgs.Contains((int)p.OrgId)) &&                  
                      (CustomerCareId == 0 || p.CCUserId == CustomerCareId) &&
                      (p.WkflowStepHists.Count > 0 ? statusCodes.Contains(p.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStat.Code) : false)).Count();

            return Request.CreateResponse<object>(HttpStatusCode.OK, data);
        }
       

        [Authorize]
        [HttpGet]
        public HttpResponseMessage OpenRequestsThisMonth()
        {
            List<int> Orgs = new List<int>();
            int CustomerCareId = 0;
            int orgid = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());
            var org = _IOrgService.GetOrg(orgid);

            if (org.OrgTyp.TypCd == "RESE")
            {
                // Add the reseller org and all their customers to the orglist
                Orgs.Add(orgid);
                var custOrgs = _IOrgorgService.GetOrgorgs().Where(p => p.OrgId == orgid).Select(s => s.AssociatedOrgId).ToList();
                Orgs.AddRange(custOrgs);                
            }
            else if (org.OrgTyp.TypCd == "CUST")
            {
                Orgs.Add(orgid);               
            }
            else
            {
                Orgs = null;
                CustomerCareId = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());
            } 

            List<string> statusCodes = null;
            statusCodes = new List<string> { "Created", "EXCP", "Assigned to CustomerCare", "Assigned to Customer" };
                        
            var StartDate = DateTime.UtcNow.AddDays(-30);
            var EndDate = DateTime.UtcNow;
            
            int data = _IWkflowinstanceService.GetWkflowinstances().Where(p =>
                      (Orgs == null || Orgs.Contains((int)p.OrgId)) &&
                       p.CreateDate >= StartDate &&
                       p.CreateDate <= EndDate &&
                      (CustomerCareId == 0 || p.CCUserId == CustomerCareId) &&
                      (p.WkflowStepHists.Count > 0 ? statusCodes.Contains(p.WkflowStepHists.OrderByDescending(s => s.Id).FirstOrDefault().WkflowStat.Code) : false)).Count();
            
            return Request.CreateResponse<object>(HttpStatusCode.OK, data);
        }
       

        [Authorize]
        [HttpGet]
        public HttpResponseMessage ClosedRequestsThisMonth()
        {
            List<int> Orgs = new List<int>();
            int CustomerCareId = 0;
            int orgid = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());
            var org = _IOrgService.GetOrg(orgid);

            if (org.OrgTyp.TypCd == "RESE")
            {
                // Add the reseller org and all their customers to the orglist
                Orgs.Add(orgid);
                var custOrgs = _IOrgorgService.GetOrgorgs().Where(p => p.OrgId == orgid).Select(s => s.AssociatedOrgId).ToList();
                Orgs.AddRange(custOrgs);
            }
            else if (org.OrgTyp.TypCd == "CUST")
            {
                Orgs.Add(orgid);
            }
            else
            {
                Orgs = null;
                CustomerCareId = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());
            }

            var StartDate = DateTime.UtcNow.AddDays(-30);
            var EndDate = DateTime.UtcNow;         


            int data = _IWkflowinstanceService.GetWkflowinstances().Where(p =>
                      (Orgs == null || Orgs.Contains((int)p.OrgId)) &&
                       p.CreateDate >= StartDate &&
                       p.CreateDate <= EndDate &&
                      (CustomerCareId == 0 || p.CCUserId == CustomerCareId) &&
                      (p.WkflowStepHists.Count > 0 ? p.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Code == "Closed" : false)).Count();
            
            return Request.CreateResponse<object>(HttpStatusCode.OK, data);
        }


        [Authorize]
        [HttpGet]
        public HttpResponseMessage RequestsByReasonThisMonth()
        {

            DateTime firstDayOfTheMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            DateTime lastDayOfTheMonth = firstDayOfTheMonth.AddMonths(1).AddDays(-1);

            var data = _IWkflowinstanceService.GetWkflowinstances().Where(p => p.WkflowDef.Code.Contains("SRW") && p.CreateDate >= firstDayOfTheMonth && p.CreateDate <= lastDayOfTheMonth).GroupBy(g=>g.WkflowStepHists.OrderBy(o=>o.Id).FirstOrDefault().WkflowStatRea.Descript).Select(
                s=> new {Name = s.Key, Count = s.Count()  }
                );

            return Request.CreateResponse<object>(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage RequestsByStateThisMonth()
        {

            DateTime firstDayOfTheMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            DateTime lastDayOfTheMonth = firstDayOfTheMonth.AddMonths(1).AddDays(-1);

            var data = _IWkflowinstanceService.GetWkflowinstances().Where(p => p.WkflowDef.Code.Contains("SRW") && p.CreateDate >= firstDayOfTheMonth && p.CreateDate <= lastDayOfTheMonth).GroupBy(g => g.WkflowStepHists.OrderByDescending(o => o.Id).FirstOrDefault().WkflowStat.Descript).Select(
                s => new { Name = s.Key, Count = s.Count() }
                );

            return Request.CreateResponse<object>(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage RequestsByCustomerThisMonth()
        {

            DateTime firstDayOfTheMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            DateTime lastDayOfTheMonth = firstDayOfTheMonth.AddMonths(1).AddDays(-1);

            var data = _IWkflowinstanceService.GetWkflowinstances().Where(p => p.WkflowDef.Code.Contains("SRW") && p.CreateDate >= firstDayOfTheMonth && p.CreateDate <= lastDayOfTheMonth).GroupBy(g => g.Org.Name).Select(
                s => new { Name = s.Key, Count = s.Count() }
                );

            return Request.CreateResponse<object>(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage CustomerReSellerSignupByDay()
        {

            var StartDate = DateTime.UtcNow.AddDays(-30);
            var EndDate = DateTime.UtcNow;

            var custwork = _IWkflowinstanceService.GetWkflowinstances().Where(p=>p.WkflowDefId==1 && p.CreateDate>= StartDate && p.CreateDate<=EndDate).GroupBy(g => ((DateTime)g.CreateDate).ToString("dd-MMM-yyyy")).Select(i => new
            {
                Date = i.Key.Substring(0, 6),
                Count = i.Count()
            });

            var rework = _IWkflowinstanceService.GetWkflowinstances().Where(p => p.WkflowDefId == 2 && p.CreateDate >= StartDate && p.CreateDate <= EndDate).GroupBy(g => ((DateTime)g.CreateDate).ToString("dd-MMM-yyyy")).Select(i => new
            {
                Date = i.Key.Substring(0, 6),
                Count = i.Count()
            });

            List<object> finalwork = new List<object>();

            for (int day = -30; day <= 0; day++)
            {
                int custcount = 0;
                int recount = 0;
                var datecal = DateTime.UtcNow.AddDays(day).ToString("dd-MMM");
                
                var find = custwork.Where(p => p.Date == datecal);
                if (find.Count() > 0)
                {
                    custcount = find.FirstOrDefault().Count;
                }

                var findre = rework.Where(p => p.Date == datecal);
                if (findre.Count() > 0)
                {
                    recount = findre.FirstOrDefault().Count;
                }

                finalwork.Add(new { Date = datecal, customer = custcount, reseller = recount });
            }

            if (finalwork == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, finalwork);

        }



        [Authorize]
        [HttpPost]
        public HttpResponseMessage Search(SupportSearchRequest requestInfo)
        {
            int org_id = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());
            requestInfo.OrgIds = new List<int>();

            if (requestInfo.OrgId == null)
            {
                var org = _IOrgService.GetOrg(org_id);
                if (org.OrgTyp.TypCd == "RESE")
                {
                    requestInfo.OrgIds.Add(org_id);
                    var custOrgs = _IOrgorgService.GetOrgorgs().Where(p => p.OrgId == org_id).Select(s => s.AssociatedOrgId).ToList();
                    requestInfo.OrgIds.AddRange(custOrgs);
                    requestInfo.OrgId = 0;
                }
                else if (org.OrgTyp.TypCd == "CUST")
                {
                    requestInfo.OrgIds.Add(org_id);
                    requestInfo.OrgId = 0;
                }
                else if (org.OrgTyp.TypCd == "TENA")
                {

                }
                else
                {
                    requestInfo.OrgId = 0;
                }
            }
            else
            {
                requestInfo.OrgIds.Add((int)requestInfo.OrgId);
            }


            var work = _IWkflowinstanceService.GetSupportActivityByFilter(requestInfo);

            var result = PlatformMappingHelper.Map<IList<WkflowInstance>, IList<SupportActivityDTO>>(work.ToList()).ToList();

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<SupportActivityDTO>>(HttpStatusCode.OK, result);

        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            var work = _IWkflowinstanceService.GetWkflowinstance(id);

            var result = PlatformMappingHelper.Map<WkflowInstance, SupportActivityDTO>(work);

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<SupportActivityDTO>(HttpStatusCode.OK, result);

        }



        [Authorize]
        [HttpPost]
        public HttpResponseMessage Create(SupportActivityDTO requestInfo)
        {

            try
            {
                var supportRequestWorkflow =
                    _IWkflowdefService.GetWkflowdefs().Find(p => p.Code == "SRW");

                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                //var createdstatusID = supportRequestWorkflow.WkflowDefWkflowStats.Where(p => p.WkflowStat.Code.Contains("Created")).FirstOrDefault().WkflowStatId;

                var ccstatusID = supportRequestWorkflow.WkflowDefWkflowStats.Where(p => p.WkflowStat.Code.Contains("Assigned to CustomerCare")).FirstOrDefault().WkflowStatId;
                var cstatusID = supportRequestWorkflow.WkflowDefWkflowStats.Where(p => p.WkflowStat.Code.Contains("Assigned to Customer")).FirstOrDefault().WkflowStatId;

                int? orgid;

                if (requestInfo.OrgId != null)
                {
                    orgid = requestInfo.OrgId;
                }
                else
                {
                    orgid = _IUserService.GetUser(userID).OrgUsers.Where(p=>p.Type==null || p.Type.Contains("Primary")).FirstOrDefault().OrgId;
                }

                var statusID = 0;

                var org = _IOrgService.GetOrg((int)orgid);

                if (org.OrgTyp.TypCd == "TENA")
                {
                    statusID = cstatusID;
                }
                else
                {
                    statusID = ccstatusID;
                }

                int? ccuserid = org.OrgUsers.Where(p => p.Type == "CustomerCare").Count() > 0 ? (int?) org.OrgUsers.Where(p => p.Type == "CustomerCare").FirstOrDefault().UserId : null;

                // Create a new batch processing workflow 
                var supportWkflowInstance = new WkflowInstance
                {
                    CreateDate = DateTime.UtcNow,
                    DateLastMaint = DateTime.UtcNow,
                    WkflowDefId = supportRequestWorkflow.Id,
                    OrgId = orgid,
                    UserId = userID,
                    CurrWkflowStatId = statusID,
                    Summary = requestInfo.Summary,
                    Priority = requestInfo.Priority,
                    CCUserId = ccuserid
                };

                var hist = new WkflowStepHist
                {
                    CreateDate = DateTime.UtcNow,
                    DateLastMaint = DateTime.UtcNow,
                    WkflowStatId = statusID,
                    CreatedUserId = userID,
                    WkflowStatReasId = requestInfo.ReasonId
                };

                hist.WkflowStepNotes.Add(new WkflowStepNote() { CreatedDate = DateTime.UtcNow, NoteText = requestInfo.NewMessage});
                supportWkflowInstance.WkflowStepHists.Add(hist);
                _IWkflowinstanceService.AddWkflowinstance(supportWkflowInstance);
                return Request.CreateResponse<bool>(HttpStatusCode.OK, true);

            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }


        [Authorize]
        [HttpPost]
        public HttpResponseMessage Update(SupportActivityDTO requestInfo)
        {

            try
            {
                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

              
                int? orgid;

                if (requestInfo.OrgId != null)
                {
                    orgid = requestInfo.OrgId;
                }
                else
                {
                    orgid = _IUserService.GetUser(userID).OrgUsers.Where(p => p.Type == null || p.Type.Contains("Primary")).FirstOrDefault().OrgId;
                }

                var org = _IOrgService.GetOrg((int)orgid);

                var supportWkflowInstance = _IWkflowinstanceService.GetWkflowinstance((int) requestInfo.Id);

                if (supportWkflowInstance.CCUserId != requestInfo.CCUserId)
                {
                    supportWkflowInstance.CCUserId = requestInfo.CCUserId;
                }

                if (supportWkflowInstance.Priority != requestInfo.Priority)
                {
                    supportWkflowInstance.Priority = requestInfo.Priority;
                }

                supportWkflowInstance.DateLastMaint = DateTime.UtcNow;

                if (requestInfo.NextStepId != null)
                {

                    supportWkflowInstance.CurrWkflowStatId = (int)requestInfo.NextStepId;
                                        
                    var hist = new WkflowStepHist
                    {
                        CreateDate = DateTime.UtcNow,
                        DateLastMaint = DateTime.UtcNow,
                        WkflowStatId = (int)requestInfo.NextStepId,
                        CreatedUserId = userID,
                        WkflowStatReasId = requestInfo.ReasonId
                    };
                    if (requestInfo.NewMessage != null && requestInfo.NewMessage != "")
                    {
                        hist.WkflowStepNotes.Add(new WkflowStepNote() { CreatedDate = DateTime.UtcNow, NoteText = requestInfo.NewMessage });
                    }
                    if (requestInfo.Resolution != null && requestInfo.Resolution != "")
                    {
                        hist.WkflowStepNotes.Add(new WkflowStepNote() { CreatedDate = DateTime.UtcNow, NoteText = requestInfo.Resolution });
                    }

                    supportWkflowInstance.WkflowStepHists.Add(hist);
                }

                _IWkflowinstanceService.UpdateWkflowinstance(supportWkflowInstance);
                return Request.CreateResponse<bool>(HttpStatusCode.OK, true);

            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage Customers()
        {
            var otoss = _IOrgcustService.GetOrgcusts().Select(o=>o.Org);
            var result = otoss.Select(s => new { s.Id, s.Name, s.SOAccountNbr, SalesRep = (s.OrgUsers.Where(u => u.Type == "SalesRep").Count() > 0 ? s.OrgUsers.Where(u => u.Type == "SalesRep").FirstOrDefault().User.UserName : ""), Commitment = (s.WkflowInstances.Where(p => p.WkflowDef.Code == "SOW").FirstOrDefault().SowWkflows.Where(w => w.InactiveDate == null).FirstOrDefault().MonthlyCommitment) });
            return Request.CreateResponse<Object>(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage NewCustomers()
        {
            var date = DateTime.UtcNow;

            var CurrStartDate = new DateTime(date.Year, date.Month, 1);
            var CurrEndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            var otoss = _IOrgService.GetOrgs().Where(r => r.InactiveDate == null && r.CreateDate >= CurrStartDate && r.CreateDate <= CurrEndDate).Count();
            return Request.CreateResponse<Object>(HttpStatusCode.OK, otoss);
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage NewResellers()
        {
            var date = DateTime.UtcNow;

            var CurrStartDate = new DateTime(date.Year, date.Month, 1);
            var CurrEndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            var otoss = _IOrgresellerService.GetOrgresellers().Where(r => r.Org.InactiveDate == null && r.Org.CreateDate >= CurrStartDate && r.Org.CreateDate <= CurrEndDate).Count();
            return Request.CreateResponse<Object>(HttpStatusCode.OK, otoss);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage MonthlySubscriptionRevenue()
        {
            var date = DateTime.UtcNow;

            var CurrStartDate = new DateTime(date.Year, date.Month, 1);
            var CurrEndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            var otoss = _IOrgService.GetOrgs().Where(r => r.OrgTyp.TypCd=="CUST" && r.InactiveDate == null).Sum(s => s.WkflowInstances.Where(w => w.WkflowDef.Code == "SOW").Sum(ss => ss.SowWkflows.OrderByDescending(sw => sw.Amend).FirstOrDefault().MonthlyCommitment));
            return Request.CreateResponse<Object>(HttpStatusCode.OK, otoss);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage MonthlyRebate()
        {
            var date = DateTime.UtcNow;

            var CurrStartDate = new DateTime(date.Year, date.Month, 1);
            var CurrEndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            var otoss = _IOrgresellerService.GetOrgresellers().Where(r => r.Org.InactiveDate == null && r.OrgResellerDiscHists.Count > 0).Sum(rs => rs.Org.OrgOrgs.Sum(s => s.Org1.WkflowInstances.Where(w => w.WkflowDef.Code == "SOW").Sum(ss => ss.SowWkflows.OrderByDescending(sw => sw.Amend).FirstOrDefault().MonthlyCommitment)) * rs.OrgResellerDiscHists.OrderByDescending(rd => rd.Amend).FirstOrDefault().Discount / 100);
            return Request.CreateResponse<Object>(HttpStatusCode.OK, otoss);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage CompareLastCurrentMonthByDay()
        {

            var date = DateTime.UtcNow;

            var CurrStartDate = new DateTime(date.Year, date.Month, 1);
            var CurrEndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            var month = new DateTime(date.Year, date.Month, 1);
            var LastStartDate = month.AddMonths(-1);
            var LastEndDate = month.AddDays(-1);

            var curwork = _IWkflowinstanceService.GetWkflowinstances().Where(q => q.WkflowDefId == 4 && q.Org.OrgOrgs1.Count > 0).Where(p =>  p.CreateDate >= CurrStartDate && p.CreateDate <= CurrEndDate).GroupBy(g => ((DateTime)g.CreateDate).ToString("dd-MMM")).Select(i => new
            {
                Date = i.Key.Substring(0, 6),
                Sum = i.Sum(s => s.WkflowInstanceDocs.FirstOrDefault().Doc.soPages)
            });

            var lastwork = _IWkflowinstanceService.GetWkflowinstances().Where(q => q.WkflowDefId == 4 && q.Org.OrgOrgs1.Count > 0).Where(p =>  p.CreateDate >= LastStartDate && p.CreateDate <= LastEndDate).GroupBy(g => ((DateTime)g.CreateDate).ToString("dd-MMM")).Select(i => new
            {
                Date = i.Key.Substring(0, 6),
                Sum = i.Sum(s => s.WkflowInstanceDocs.FirstOrDefault().Doc.soPages)
            });

            var curDays = DateTime.DaysInMonth(date.Year, date.Month);
            var lastDays = DateTime.DaysInMonth(LastStartDate.Year, LastStartDate.Month);

            var days = 0;

            if (curDays > lastDays)
            {
                days = curDays;
            }
            else
            {
                days = lastDays;
            }


            List<object> finalwork = new List<object>();
            List<object> cwork = new List<object>();
            List<object> lwork = new List<object>();

            for (int day = 0; day < days; day++)
            {
                int? currcount = 0;
                int? lastcount = 0;
                var curdatecal = CurrStartDate.AddDays(day).ToString("dd-MMM");
                var lastdatecal = LastStartDate.AddDays(day).ToString("dd-MMM");
                var key = curdatecal.Substring(0, 6);
                var find = curwork.Where(p => p.Date == key);
                if (find.Count() > 0)
                {
                    currcount = find.FirstOrDefault().Sum;
                }
                key = lastdatecal.Substring(0, 6);
                find = lastwork.Where(p => p.Date == key);
                if (find.Count() > 0)
                {
                    lastcount = find.FirstOrDefault().Sum;
                }
                finalwork.Add(new { Date = curdatecal, CurCount = currcount, LastCount = lastcount });
            }

            if (finalwork == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, finalwork);

        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage CustomerCurrentMonthVolume()
        {


            var finalwork = _IOrgService.GetOrgs().Where(p=>p.OrgTyp.TypCd=="CUST").Select(s =>
            new {
                Id = s.Id,
                Name = s.Name,
                Commitment = s.WkflowInstances.Where(w => w.WkflowDef.Code == "SOW").Count() > 0 ? s.WkflowInstances.Where(w => w.WkflowDef.Code == "SOW").FirstOrDefault().SowWkflows.Where(so => so.InactiveDate == null).FirstOrDefault().MonthlyCommitment : 0,
                Revenue = GetRevenue(s)
            });

            if (finalwork == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, finalwork);

        }

        private decimal GetRevenue(Org org)
        {
            var date = DateTime.UtcNow;

            var CurrStartDate = new DateTime(date.Year, date.Month, 1);
            var CurrEndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            //When Nightly Job is running, get the data from Daily table
            //Loop thru all the dates
            var StartDate = CurrStartDate;
            decimal revenue = 0;
            while (StartDate.Date < CurrEndDate.Date)
            {
                revenue += _IOrgdoctypdailyuploadService.GetOrgdoctypdailyuploads().Where(p => p.OrgId == org.Id && p.Day.Date == StartDate.Date).Sum(s => s.Revenue);
                StartDate = StartDate.AddDays(1);
            };

            return revenue;

        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage CustomerTwelveMonthCommitment()
        {

            var date = DateTime.UtcNow;
            var CurrStartDate = new DateTime(date.Year, date.Month, 1);
            IList<Object> result = new List<object>();
            for (int i = -12; i < 0; i++)
            {
                var calDate = CurrStartDate.AddMonths(i);
                var month = calDate.Month.ToString("00") + calDate.Year.ToString();
                var commitment = _IOrgService.GetOrgs().Where(p=>p.OrgTyp.TypCd=="CUST").Sum(o => o.OrgMonthCommitments.Where(p => p.Month.Name == month).FirstOrDefault().Commitment);
                result.Add(new
                {
                    Month = calDate.Month.ToString("00") + "\\" + calDate.Year.ToString().Substring(2, 2),
                    Commitment = commitment
                });
            }

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, result);

        }


    }

}