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
    public class SubscriptionController : ApiController
    {
        ILogService _loggerService;
        IWkflowdefService _IWkflowdefService;
        ISowwkflowService _ISowwkflowService;
        IUserService _IUserService;
        IOrgService _IOrgService;
        ISowattributeService _ISowattributeService;
        IWkflowinstanceService _IWkflowinstanceService;
        public SubscriptionController(ILogService loggerService, IWkflowdefService wkflowdefService, ISowwkflowService sowwkflowService,
                                    IUserService userService, IOrgService orgService, IWkflowinstanceService wkflowinstanceService,
                                    ISowattributeService sowattributeService)
        {
            this._loggerService = loggerService;
            this._IWkflowdefService = wkflowdefService;
            this._IUserService = userService;
            this._IOrgService = orgService;
            this._IWkflowinstanceService = wkflowinstanceService;
            this._ISowattributeService = sowattributeService;
            _ISowwkflowService = sowwkflowService;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage SOWAttributes()
        {

            var sowAttributes = _ISowattributeService.GetSowattributes().ToList().Select(p=> new {Id = p.Id, Name = p.Name });

            if (sowAttributes == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Request.CreateResponse<object>(HttpStatusCode.OK, sowAttributes);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Create(SOWDTO requestInfo)
        {

            try
            {
                var SOWWorkflow =
                    _IWkflowdefService.GetWkflowdefs().Find(p => p.Code == "SOW");

                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                var CreatedstatusID = SOWWorkflow.WkflowDefWkflowStats.Where(p => p.WkflowStat.Code.Contains("Created")).FirstOrDefault().WkflowStatId;

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
                int? ccuserid = org.OrgUsers.Where(p => p.Type == "CustomerCare").Count() > 0 ? (int?)org.OrgUsers.Where(p => p.Type == "CustomerCare").FirstOrDefault().UserId : null;

                var WKflow = new WkflowInstance()
                {
                    Org = org,
                    CreateDate = DateTime.UtcNow,
                    DateLastMaint = DateTime.UtcNow,
                    WkflowDef = SOWWorkflow,
                    UserId = userID,
                    CurrWkflowStatId = CreatedstatusID,
                    CCUserId = ccuserid
                };

                var hist = new WkflowStepHist
                {
                    CreateDate = DateTime.UtcNow,
                    DateLastMaint = DateTime.UtcNow,
                    WkflowStatId = CreatedstatusID,
                    CreatedUserId = userID
                };
                var sowWkflow = new SowWkflow
                {
                    WkflowInstance = WKflow,
                    ProjectName = requestInfo.ProjectName,
                    Amend = 0,
                    EffectiveDate = DateTime.UtcNow,
                    LastReviewDate = DateTime.UtcNow,
                    NextReviewDate = DateTime.UtcNow.AddMonths(6),
                    MonthlyCommitment = requestInfo.Commitment
                };

                foreach (var attrib in requestInfo.Attribs)
                {
                    //var newatt = new SowWklowSowAttribute() { SowAttributeId = attrib.Id, SowWkflow = sowWkflow };
                    //newatt.SowAttributeValueHists.Add(new SowAttributeValueHist() { SowWklowSowAttribute = newatt, EffectiveDate = DateTime.UtcNow, SowAttributeId = attrib.Id, Qty = attrib.Qty, UnitPrice = attrib.UnitPrice, ExtendedPrice = attrib.ExtendedPrice });
                    //sowWkflow.SowWklowSowAttributes.Add(newatt);

                    var newatt = new SowWklowSowAttribute() { SowAttributeId = attrib.Id, SowWkflow = sowWkflow };
                    newatt.SowAttributeValueHists.Add(new SowAttributeValueHist() { SowWklowSowAttribute = newatt, EffectiveDate = DateTime.UtcNow, Qty = attrib.Qty, UnitPrice = attrib.UnitPrice, ExtendedPrice = attrib.ExtendedPrice });
                    sowWkflow.SowWklowSowAttributes.Add(newatt);


                }

                foreach (var docSetup in requestInfo.DocSetups)
                {
                    sowWkflow.SowWkflowDocSetups.Add(new SowWkflowDocSetup() { DocumentName = docSetup.Name, SowWkflow = sowWkflow, NoIndexes = docSetup.NoIndexes, NoDataFields = docSetup.NoDataFields, SLA = docSetup.SLA, Volume = docSetup.CommitVolume, ListPrice = docSetup.UnitPrice });
                }

                if (requestInfo.RebateOverride)
                {
                    var reseller = org.OrgOrgs1.FirstOrDefault().Org.OrgReseller;
                    sowWkflow.SOWWkflowOrgResellerDiscOverrides.Add(new SOWWkflowOrgResellerDiscOverride { Discount = requestInfo.ResellerRebate, EffectiveDate = DateTime.UtcNow, SowWkflow = sowWkflow, OrgReseller = reseller });
                }

                WKflow.SowWkflows.Add(sowWkflow);
                WKflow.WkflowStepHists.Add(hist);
                _IWkflowinstanceService.AddWkflowinstance(WKflow);
                return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Update(SOWDTO requestInfo)
        {

            try
            {
                var SOWWorkflow =
                    _IWkflowdefService.GetWkflowdefs().Find(p => p.Code == "SOW");

                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                var CreatedstatusID = SOWWorkflow.WkflowDefWkflowStats.Where(p => p.WkflowStat.Code.Contains("Created")).FirstOrDefault().WkflowStatId;

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
                int? ccuserid = org.OrgUsers.Where(p => p.Type == "CustomerCare").Count() > 0 ? (int?)org.OrgUsers.Where(p => p.Type == "CustomerCare").FirstOrDefault().UserId : null;

                var WKflow = _IWkflowinstanceService.GetWkflowinstance(requestInfo.WkflowInstanceId);
                
                var sowWkflow = new SowWkflow
                {
                    WkflowInstance = WKflow,
                    ProjectName = requestInfo.ProjectName,
                    Amend =  requestInfo.AmentId,
                    EffectiveDate = DateTime.UtcNow,
                    NextReviewDate = DateTime.UtcNow.AddMonths(6),
                    MonthlyCommitment = requestInfo.Commitment
                };
                if (requestInfo.RebateOverride)
                {
                    var reseller = org.OrgOrgs1.FirstOrDefault().Org.OrgReseller;
                    sowWkflow.SOWWkflowOrgResellerDiscOverrides.Add(new SOWWkflowOrgResellerDiscOverride { Discount = requestInfo.ResellerRebate, EffectiveDate = DateTime.UtcNow, SowWkflow = sowWkflow, OrgReseller = reseller });
                }

                foreach (var attrib in requestInfo.Attribs)
                {
                    //var newatt = new SowWklowSowAttribute() { SowAttributeId = attrib.Id, SowWkflow = sowWkflow };
                    //newatt.SowAttributeValueHists.Add(new SowAttributeValueHist() { SowWklowSowAttribute = newatt, EffectiveDate = DateTime.UtcNow, SowAttributeId = attrib.Id, Qty = attrib.Qty, UnitPrice = attrib.UnitPrice, ExtendedPrice = attrib.ExtendedPrice });
                    //sowWkflow.SowWklowSowAttributes.Add(newatt);

                    var newatt = new SowWklowSowAttribute() { SowAttributeId = attrib.Id, SowWkflow = sowWkflow };
                    newatt.SowAttributeValueHists.Add(new SowAttributeValueHist() { SowWklowSowAttribute = newatt, EffectiveDate = DateTime.UtcNow, Qty = attrib.Qty, UnitPrice = attrib.UnitPrice, ExtendedPrice = attrib.ExtendedPrice });
                    sowWkflow.SowWklowSowAttributes.Add(newatt);
                }

                foreach (var docSetup in requestInfo.DocSetups)
                {
                    sowWkflow.SowWkflowDocSetups.Add(new SowWkflowDocSetup() { SowWkflow = sowWkflow, NoIndexes = docSetup.NoIndexes, NoDataFields = docSetup.NoDataFields, SLA = docSetup.SLA, Volume = docSetup.CommitVolume, ListPrice = docSetup.UnitPrice });
                }

                WKflow.SowWkflows.Add(sowWkflow);
                _IWkflowinstanceService.AddWkflowinstance(WKflow);
                return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetSOW(int wkflowId)
        {
            var sow = _IWkflowinstanceService.GetWkflowinstance(wkflowId);
            var result = PlatformMappingHelper.Map<IList<SowWkflow>, IList<SOWDTO>>(sow.SowWkflows.ToList()).ToList();

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<SOWDTO>>(HttpStatusCode.OK, result);

        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Search(SubscriptionSearchRequest requestInfo)
        {
            try
            {
                int? orgid;
                DateTime? reviewEndDate;
                DateTime? reviewStartDate;

                List<SowWkflow> sows = new List<SowWkflow>();

                orgid = requestInfo.OrgId != null ? requestInfo.OrgId : null;
                reviewEndDate = requestInfo.ReviewEndDate != null ? requestInfo.ReviewEndDate : null;
                reviewStartDate = requestInfo.ReviewStartDate != null ? requestInfo.ReviewStartDate : null;


                if (orgid != null && reviewEndDate != null && reviewStartDate != null)
                {
                    //  All Search Params Search
                    sows = _ISowwkflowService.GetSowwkflows().Where(p => p.WkflowInstance.OrgId == orgid && p.EffectiveDate >= requestInfo.ReviewStartDate && p.EffectiveDate <= requestInfo.ReviewEndDate).ToList();

                }
                else if (orgid != null)
                {
                    // Search only by orgId
                    sows = _ISowwkflowService.GetSowwkflows().Where(p => p.WkflowInstance.OrgId == orgid).ToList();
                }
                else
                {
                    sows = _ISowwkflowService.GetSowwkflows().Where(p => p.WkflowInstance.OrgId > 0).ToList();
                }

                //if (requestInfo.OrgId != null)
                //{
                //    orgid = requestInfo.OrgId;

                //    sows = _ISowwkflowService.GetSowwkflows().Where(p => p.WkflowInstance.OrgId == orgid && p.EffectiveDate >= requestInfo.ReviewStartDate && p.EffectiveDate <= requestInfo.ReviewEndDate).ToList();

                //}
                //else
                //{
                //    sows = _ISowwkflowService.GetSowwkflows().Where(p => p.EffectiveDate >= requestInfo.ReviewStartDate && p.EffectiveDate <= requestInfo.ReviewEndDate).ToList();

                //}


                var result = PlatformMappingHelper.Map<IList<SowWkflow>, IList<SOWDTO>>(sows.ToList()).ToList();

                if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
                return Request.CreateResponse<IEnumerable<SOWDTO>>(HttpStatusCode.OK, result);
            }
            catch (Exception daExp)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
           

        }


    }
}