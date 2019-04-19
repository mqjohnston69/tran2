using Oc.Carbon.Common.Contracts;
using Oc.Carbon.DataAccess;
using Oc.Carbon.DTO.Mapping.Core;
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
    public class ResellerController : ApiController
    {
        ILogService _loggerService;
        IOrgService _OrgService;
        IOrgcustService _IOrgcustService;
        IOrgresellerService _IOresellerService;
        IOrgorgService _IOrgorgService;
        IOrgtyporgstatusService _IOrgtyporgstatusService;
        IUserService _IUserService;
        IWkflowinstanceService _IWkflowinstanceService;
        IOrgdoctypService _IOrgdoctypService;
        ISouploadService _ISouploadService;
        IOrgdoctypdailyuploadService _IOrgdoctypdailyuploadService;

        public ResellerController(ILogService loggerService, IOrgService orgService, IOrgcustService orgCustService,
           IOrgresellerService orgResellerService, IOrgorgService orgorgService, IOrgtyporgstatusService orgtyporgstatusService,
           IUserService userService, IWkflowinstanceService wkflowinstanceService, IOrgdoctypService orgdoctypService,
           ISouploadService souploadService, IOrgdoctypdailyuploadService orgdoctypdailyuploadService)
        {
            this._loggerService = loggerService;
            this._OrgService = orgService;

            _IOrgcustService = orgCustService;
            _IOresellerService = orgResellerService;
            _IOrgorgService = orgorgService;
            _IOrgtyporgstatusService = orgtyporgstatusService;
            _IUserService = userService;
            _IWkflowinstanceService = wkflowinstanceService;
            _IOrgdoctypService = orgdoctypService;
            _ISouploadService = souploadService;
            _IOrgdoctypdailyuploadService = orgdoctypdailyuploadService;
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage Statuses()
        {

            var otoss = _IOrgtyporgstatusService.GetOrgtyporgstatus().FindAll(p => p.OrgTyp.Name == "Reseller").Select(o=> new { StatusCd = o.OrgStatus.StatusCd, Name = o.OrgStatus.DisplayText } );

            return Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, otoss);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage SoSalesReps(int? org_id)
        {
            org_id = 1;  //Scanoptics 

            var salesReps = _IUserService.GetUsers().Where(p => p.OrgUsers.FirstOrDefault().OrgId == org_id).Select(a => new { id = a.Id, name = a.Per.LastName + ", " + a.Per.FirstName });

            return Request.CreateResponse(HttpStatusCode.OK, salesReps);
        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage CustomerCareReps(int? org_id)
        {
            org_id = 1;  //Scanoptics 

            var custCare = _IUserService.GetUsers().Where(p => p.OrgUsers.FirstOrDefault().OrgId == org_id).Select(a => new { id = a.Id, name = a.Per.LastName + ", " + a.Per.FirstName });

            return Request.CreateResponse(HttpStatusCode.OK, custCare);
        }


        [Authorize]
        [HttpPost]
        public HttpResponseMessage search(ResellerSearchRequest requestInfo)
        {

            var orgs = _IOresellerService.GetResellersByFilter(requestInfo);

            var result = PlatformMappingHelper.Map<IList<OrgReseller>, IList<ResellerDTO>>(orgs.ToList()).ToList();

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<ResellerDTO>>(HttpStatusCode.OK, result);

        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage searchRebates(ResellerSearchRequest requestInfo)
        {

            var orgs = _IOresellerService.GetResellersRebateByFilter(requestInfo);

            var result = PlatformMappingHelper.Map<IList<OrgReseller>, IList<ResellerDTO>>(orgs.ToList()).ToList();

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<ResellerDTO>>(HttpStatusCode.OK, result);

        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage CustomerSignupByDay(int resellerId)
        {

            var date = DateTime.UtcNow;

            var CurrStartDate = new DateTime(date.Year, date.Month, 1);
            var CurrEndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            var curDays = DateTime.DaysInMonth(date.Year, date.Month);

            var custwork = _IWkflowinstanceService.GetWkflowinstances().Where(q => q.WkflowDefId == 1 && q.Org.OrgOrgs1.Count > 0).Where (p=> p.Org.OrgOrgs1.FirstOrDefault().Org.Id == resellerId &&  p.CreateDate >= CurrStartDate && p.CreateDate <= CurrEndDate).GroupBy(g => ((DateTime)g.CreateDate).ToString("dd-MMM")).Select(i => new
            {
                Date = i.Key.Substring(0, 6),
                Count = i.Count()
            });


            List<object> finalwork = new List<object>();

            for (int day = 0; day < curDays; day++)
            {
                int custcount = 0;
                var datecal = CurrStartDate.AddDays(day).ToString("dd-MMM");
                var key = datecal.Substring(0, 6);
                var find = custwork.Where(p => p.Date == key);
                if (find.Count() > 0)
                {
                    custcount = find.FirstOrDefault().Count;
                }

                finalwork.Add(new { Date = key, Count = custcount });
            }

            if (finalwork == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, finalwork);

        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage CompareLastCurrentMonthByDay()
        {

            var resellerId = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());

            var date = DateTime.UtcNow;

            var CurrStartDate = new DateTime(date.Year, date.Month, 1);
            var CurrEndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            var month = new DateTime(date.Year, date.Month, 1);
            var LastStartDate = month.AddMonths(-1);
            var LastEndDate = month.AddDays(-1);

            var curwork =
                _IWkflowinstanceService.GetWkflowinstances()
                    .Where(q => q.WkflowDefId == 4 && q.Org.OrgOrgs1.Count > 0)
                    .Where(p => p.Org.OrgOrgs1.FirstOrDefault().OrgId == resellerId && p.CreateDate >= CurrStartDate && p.CreateDate <= CurrEndDate)
                    .GroupBy(g => ((DateTime)g.CreateDate).ToString("dd-MMM"))
                    .Select(i => new
                    {
                        Date = i.Key,
                        Sum = i.Sum(s => s.WkflowInstanceDocs.FirstOrDefault() != null ? s.WkflowInstanceDocs.FirstOrDefault().Doc.soPages : 0)
                    });

            var lastwork =
                _IWkflowinstanceService.GetWkflowinstances().
                    Where(q => q.WkflowDefId == 4 && q.Org.OrgOrgs1.Count > 0).
                    Where(p => p.Org.OrgOrgs1.FirstOrDefault().OrgId == resellerId && p.CreateDate >= LastStartDate && p.CreateDate <= LastEndDate).
                    GroupBy(g => ((DateTime)g.CreateDate).ToString("dd-MMM"))
                    .Select(i => new
                    {
                        Date = i.Key,
                        Sum = i.Sum(s => s.WkflowInstanceDocs.FirstOrDefault() != null ? s.WkflowInstanceDocs.FirstOrDefault().Doc.soPages : 0)
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
                if (curwork != null)
                {
                    var find = curwork.Where(p => p.Date == key);
                    if (find.Count() > 0)
                    {
                        currcount = find.FirstOrDefault().Sum;
                    }
                }
                key = lastdatecal.Substring(0, 6);
                if (lastwork != null)
                {
                    var find = lastwork.Where(p => p.Date == key);
                    if (find.Count() > 0)
                    {
                        lastcount = find.FirstOrDefault().Sum;
                    }
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

            var resellerId = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());

            var finalwork = _OrgService.GetOrg(resellerId).OrgOrgs.Select(s =>
            new {
                Id = s.Org1.Id,
                Name = s.Org1.Name,
                Commitment = s.Org1.WkflowInstances.Where(w => w.WkflowDef.Code == "SOW").Count() > 0 ? s.Org1.WkflowInstances.Where(w => w.WkflowDef.Code == "SOW").FirstOrDefault().SowWkflows.Where(so => so.InactiveDate == null).FirstOrDefault().MonthlyCommitment : 0,
                Revenue = GetRevenue(s.Org1)
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
                revenue += _IOrgdoctypdailyuploadService.GetOrgdoctypdailyuploads().Where(p => p.OrgId == org.Id && p.Day.Date == StartDate.Date).Sum(s=>s.Revenue);
                StartDate = StartDate.AddDays(1);
            };

            return revenue;
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage CustomerTwelveMonthCommitment()
        {
            var resellerId = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());

            var date = DateTime.UtcNow;
            var CurrStartDate = new DateTime(date.Year, date.Month, 1);
            IList<Object> result = new List<object>();
            for (int i=-12; i<0; i++)
            {
                var calDate = CurrStartDate.AddMonths(i);
                var month = calDate.ToString("MMM") + calDate.Year.ToString();
                var commitment = _IOrgorgService.GetOrgorgs().Where(oo => oo.Org.Id == resellerId && oo.Org1.OrgMonthCommitments.Count > 0).Sum(o => o.Org1.OrgMonthCommitments.Where(p => p.Month.Name == month).FirstOrDefault().Commitment);
                result.Add(new {
                    Month = calDate.ToString("MMM") + "-" + calDate.Year.ToString().Substring(2, 2),
                    Commitment = commitment
                });
            }

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<object>>(HttpStatusCode.OK, result);

        }


        [HttpGet]
        [Authorize]
        public HttpResponseMessage Customers()
        {
            var orgId = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());
            
            var otoss = _IOresellerService.GetOrgresellers().Where(p => p.Org.Id == orgId).FirstOrDefault().Org.OrgOrgs.Select(s=>s.Org1);
            var result = otoss.Select(s => new { s.Id, s.Name, s.SOAccountNbr, SalesRep = (s.OrgUsers.Where(u => u.Type == "SalesRep").Count() > 0 ? s.OrgUsers.Where(u => u.Type == "SalesRep").FirstOrDefault().User.UserName: "" ),
                Commitment = (s.WkflowInstances.Where(p=>p.WkflowDef.Code== "SOW").FirstOrDefault().SowWkflows.Where(w=>w.InactiveDate == null).FirstOrDefault().MonthlyCommitment)} );
            
            return Request.CreateResponse<Object>(HttpStatusCode.OK, result);
        }

        #region Moved to SupportActivity

        //[HttpGet]
        //[Authorize]
        //public HttpResponseMessage NewCustomers()
        //{
        //    var date = DateTime.UtcNow;

        //    var CurrStartDate = new DateTime(date.Year, date.Month, 1);
        //    var CurrEndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

        //    var otoss = _IOresellerService.GetOrgresellers().Where(r => r.Org.InactiveDate == null).Sum(c => c.Org.OrgOrgs.Count(cs => cs.Org1.CreateDate >= CurrStartDate && cs.Org1.CreateDate <= CurrEndDate));
        //    return Request.CreateResponse<Object>(HttpStatusCode.OK, otoss);
        //}


        //[HttpGet]
        //[Authorize]
        //public HttpResponseMessage NewResellers()
        //{
        //    var date = DateTime.UtcNow;

        //    var CurrStartDate = new DateTime(date.Year, date.Month, 1);
        //    var CurrEndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

        //    var otoss = _IOresellerService.GetOrgresellers().Where(r => r.Org.InactiveDate == null && r.Org.CreateDate >= CurrStartDate && r.Org.CreateDate <= CurrEndDate).Count();
        //    return Request.CreateResponse<Object>(HttpStatusCode.OK, otoss);
        //}

        //[HttpGet]
        //[Authorize]
        //public HttpResponseMessage MonthlySubscriptionRevenue()
        //{
        //    var date = DateTime.UtcNow;

        //    var CurrStartDate = new DateTime(date.Year, date.Month, 1);
        //    var CurrEndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

        //    var otoss = _IOresellerService.GetOrgresellers().Where(r => r.Org.InactiveDate == null).Sum(r=>r.Org.OrgOrgs.Sum(s => s.Org1.WkflowInstances.Where(w => w.WkflowDef.Code == "SOW").Sum(ss => ss.SowWkflows.OrderByDescending(sw => sw.Amend).FirstOrDefault().MonthlyCommitment)));
        //    return Request.CreateResponse<Object>(HttpStatusCode.OK, otoss);
        //}

        //[HttpGet]
        //[Authorize]
        //public HttpResponseMessage MonthlyRebate()
        //{
        //    var date = DateTime.UtcNow;

        //    var CurrStartDate = new DateTime(date.Year, date.Month, 1);
        //    var CurrEndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

        //    var otoss = _IOresellerService.GetOrgresellers().Where(r => r.Org.InactiveDate == null && r.OrgResellerDiscHists.Count > 0).Sum(rs => rs.Org.OrgOrgs.Sum(s => s.Org1.WkflowInstances.Where(w => w.WkflowDef.Code == "SOW").Sum(ss => ss.SowWkflows.OrderByDescending(sw => sw.Amend).FirstOrDefault().MonthlyCommitment)) * rs.OrgResellerDiscHists.OrderByDescending (rd=>rd.Amend).FirstOrDefault().Discount/100);
        //    return Request.CreateResponse<Object>(HttpStatusCode.OK, otoss);
        //}

        #endregion

        [HttpPut]
        [Authorize]
        public HttpResponseMessage Update(ResellerDTO ResellerDTO)
        {
            try
            {
                var curDateTime = DateTime.UtcNow;
                var reseller = _IOresellerService.GetOrgreseller(ResellerDTO.Id);
                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                if (reseller != null)
                {
                    reseller.Org.Name = ResellerDTO.Name;
                    reseller.Org.Descript = ResellerDTO.Descript;
                    reseller.Org.SOAccountNbr = ResellerDTO.SOAccountNbr;

                    reseller.Org.Agreement = ResellerDTO.Agreement;
                    reseller.Org.Logo = ResellerDTO.Logo;

                    reseller.Org.BillMe = ResellerDTO.BillMe;
                    reseller.Org.BillingInfo = ResellerDTO.BillingInfo;
                    reseller.Org.SOW = ResellerDTO.SOW;
                    reseller.Org.GotAgreement = ResellerDTO.GotAgreement;

                    reseller.Org.ModifiedDate = DateTime.UtcNow;
                    reseller.Org.ModifiedUserId = userID;

                    reseller.Org.OrgLocns.FirstOrDefault().Locn.AddressLine1 = ResellerDTO.AddressLine1;
                    reseller.Org.OrgLocns.FirstOrDefault().Locn.AddressLine2 = ResellerDTO.AddressLine2;
                    reseller.Org.OrgLocns.FirstOrDefault().Locn.City = ResellerDTO.City;
                    reseller.Org.OrgLocns.FirstOrDefault().Locn.State = ResellerDTO.State;
                    reseller.Org.OrgLocns.FirstOrDefault().Locn.ZipCode = ResellerDTO.ZipCode;

                    if (reseller.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault() != null)
                    {
                        reseller.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().UserId = ResellerDTO.ContactId;
                        reseller.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.Title = ResellerDTO.Title;
                    }
                    else if (reseller.Org.OrgUsers.Where(p => p.Type == null).FirstOrDefault() != null)
                    {
                        reseller.Org.OrgUsers.Where(p => p.Type == null).FirstOrDefault().UserId = ResellerDTO.ContactId;
                        reseller.Org.OrgUsers.Where(p => p.Type == null).FirstOrDefault().User.Per.Title = ResellerDTO.Title;
                        reseller.Org.OrgUsers.Where(p => p.Type == null).FirstOrDefault().Type = "Primary";
                    }


                    if (reseller.Org.OrgUsers.Where(p => p.Type == "SalesRep").FirstOrDefault() != null)
                    {
                        reseller.Org.OrgUsers.Where(p => p.Type == "SalesRep").FirstOrDefault().UserId = ResellerDTO.SalesRepId;
                    }

                    if (reseller.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.PersContacts.Where(b => b.Contact.ContactTyp.DisplayName == "Phone") != null)
                    {
                        reseller.Org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.Per.PersContacts.Where(b => b.Contact.ContactTyp.DisplayName == "Phone").FirstOrDefault().Contact.Value = ResellerDTO.Phone;
                    }

                    if (reseller.Org.OrgStatusHists.OrderByDescending (s=>s.Id).FirstOrDefault().OrgTypOrgStatu.Id != ResellerDTO.StatusID)
                    {
                        reseller.Org.OrgStatusHists.OrderByDescending (s=>s.Id).FirstOrDefault().OrgTypOrgStatusId = ResellerDTO.StatusID;
                        if (ResellerDTO.StatusName == "Active")
                        {
                            reseller.Org.ApprovedUserId = userID;
                            reseller.Org.ApprovedDate = DateTime.UtcNow;
                            reseller.Org.InactiveUserId = null;
                            reseller.Org.InactiveDate = null;
                        }
                        else if (ResellerDTO.StatusName == "Suspended")
                        {
                            reseller.Org.InactiveUserId = userID;
                            reseller.Org.InactiveDate = DateTime.UtcNow;
                        }
                    }

                    _IOresellerService.UpdateOrgreseller(reseller);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "reseller could not be found");
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage Reseller(int resellerId)
        {

            ResellerDTO search = null;

            var reseller = _IOresellerService.GetOrgresellers().Where(p => p.Org.Id == resellerId).FirstOrDefault();

            if (reseller != null)
            {
                search = PlatformMappingHelper.Map<OrgReseller, ResellerDTO>(reseller);

                if (search == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Reseller could not be found");
            }

            return Request.CreateResponse<ResellerDTO>(HttpStatusCode.OK, search);
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage RebateDetail(int resellerId)
        {

            var reseller = _IOresellerService.GetOrgresellers().Where(p => p.Org.Id == resellerId).FirstOrDefault();
            List<Object> RebateHist = new List<Object>();

            if (reseller != null)
            {
                foreach (var disc in reseller.OrgResellerDiscHists.OrderByDescending(o=>o.Id))
                {
                    RebateHist.Add(new { DiscId = disc.Id, AgreementNumber = disc.AgreementNum, AmmendmentId = disc.Amend,AnnualRevenue = disc.AnnualRevenue, EffectiveDate = disc.EffectiveDate, InactiveDate = disc.InActiveDate, Rebate = disc.Discount, PDFDoc = disc.PDFDoc });
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Reseller could not be found");
            }

            return Request.CreateResponse<IList<Object>>(HttpStatusCode.OK, RebateHist);
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage UpdateRebate(int resellerId)
        {

            RebateDTO rebate = new RebateDTO();
            try
            {
                rebate.EffectiveDate = Convert.ToDateTime(HttpContext.Current.Request.Form["effectiveDate"]);
                var LastReviewDate = Convert.ToDateTime(HttpContext.Current.Request.Form["lastReviewDate"]);
                var NextReviewDate = Convert.ToDateTime(HttpContext.Current.Request.Form["nextReviewDate"]);
                rebate.AgreementNumber = HttpContext.Current.Request.Form["agreementNumber"];
                var Disc = HttpContext.Current.Request.Form["discount"];
                rebate.Discount = Disc != null ? Int32.Parse(Disc) : 0;                    
                var AnnRev = HttpContext.Current.Request.Form["annualRevenue"];
                rebate.AnnualRevenue = AnnRev != null ? decimal.Parse(AnnRev) : 0;

                var reseller = _IOresellerService.GetOrgresellers().Where(p => p.Org.Id == resellerId).FirstOrDefault();

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
                
                if (reseller != null)
                {
                    var hists = reseller.OrgResellerDiscHists.Where(p => p.InActiveDate == null);
                    foreach (var hist in hists)
                    {
                        hist.InActiveDate = rebate.EffectiveDate;
                    }
                    var maxamend = reseller.OrgResellerDiscHists.OrderByDescending(o => o.Amend).FirstOrDefault();
                    int amend = 0;
                    if (maxamend != null)
                    {
                        amend = maxamend.Amend + 1;
                        maxamend.InActiveDate = DateTime.UtcNow;
                    }
                    reseller.OrgResellerDiscHists.Add(new OrgResellerDiscHist() { AgreementNum = int.Parse(rebate.AgreementNumber), Amend = amend,AnnualRevenue = rebate.AnnualRevenue, EffectiveDate = (DateTime)rebate.EffectiveDate, Discount = rebate.Discount,
                        PDFDoc = binData});

                    reseller.LastReviewDate = LastReviewDate;
                    reseller.NextReviewDate = NextReviewDate;

                    _IOresellerService.UpdateOrgreseller(reseller);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Reseller could not be found");
                }

                return Request.CreateResponse<bool>(HttpStatusCode.OK, true);                
            }
            catch
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Reseller Rebate Create Failed");
                throw new HttpResponseException(createresponse);
            }
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage CurrentAnnualRevenue(int orgId)
        {
            decimal annualRevenue=0;
            try
            { 

                var otoss = _IOresellerService.GetOrgresellers().Where(p => p.Org.Id == orgId).FirstOrDefault().Org.OrgOrgs.Where(r => r.Org1.InactiveDate == null);
                //var otoss = _IOresellerService.GetOrgreseller(orgId).Org.OrgOrgs.Where(r => r.Org1.InactiveDate == null).ToList();
                foreach (var orgorg in otoss)
                {
                   var commitment = orgorg.Org1.WkflowInstances.Where(w => w.WkflowDef.Code == "SOW").Sum(ss => ss.SowWkflows.OrderByDescending(sw => sw.Amend).FirstOrDefault().MonthlyCommitment);
                   if (commitment == null || commitment<=0)
                    {
                        var histRevenue = orgorg.Org1.OrgDocTyps.Sum(s => s.OrgDocTypMonths.OrderByDescending(d => d.Id).Take(3).Sum(s1 => s1.Revenue));
                        annualRevenue = histRevenue * 4;
                    }
                   else
                    {
                        annualRevenue = (decimal) commitment * 12;
                    }
                }
                return Request.CreateResponse<Object>(HttpStatusCode.OK, annualRevenue);
            }
            catch(Exception ex)
            {
                var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Reseller Not Found");
                throw new HttpResponseException(createresponse);
            }
        }

    }
}