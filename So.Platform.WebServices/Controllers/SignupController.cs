using Oc.Carbon.Auth.Api.Password;
using Oc.Carbon.Common.Constants;
using Oc.Carbon.Common.Contracts;
using Oc.Carbon.DataAccess;
using Oc.Carbon.DTO.Mapping.Core;
using Oc.Carbon.DTO.PlatformDTO;
using Oc.Carbon.DTO.SolutionDTO;
using Oc.Carbon.ServiceLayer.Contracts;
using Oc.Carbon.WebServices.Models;
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
    public class SignupController : ApiController
    {
        ILogService _loggerService;
        IOrgService _IOrgService;
        IOrgcustService _IOrgcustService;
        IOrgresellerService _IOrgresellerService;
        IOrgorgService _IOrgorgService;
        IOrgtypService _IOrgtypService;
        IUserService _IUserService;
        IWkflowdefService _IWkflowdefService;
        IWkflowinstanceService _IWkflowinstanceService;
        IAuthrolService _IAuthrolService;
        IMessagetemplateService _IMessagetemplateService;
        IContacttypService _IContacttypService;
        IOrgtyporgstatusService _IOrgtyporgstatusService;


        public SignupController(ILogService loggerService, IOrgService orgService, IOrgcustService orgCustService,
           IOrgresellerService orgResellerService, IOrgorgService orgorgService, IOrgtypService orgtypService,          
           IUserService userService, IWkflowdefService wkflowdefService, IWkflowinstanceService wkflowinstanceService,
           IAuthrolService authrolService, IMessagetemplateService messagetemplateService, IContacttypService contacttypService,
           IOrgtyporgstatusService orgtyporgstatusService)
        {
            this._loggerService = loggerService;
            this._IOrgService = orgService;

            _IOrgcustService = orgCustService;
            _IOrgresellerService = orgResellerService;
            _IOrgorgService = orgorgService;
            _IOrgtypService = orgtypService;
            _IUserService = userService;

            _IWkflowdefService = wkflowdefService;
            _IWkflowinstanceService = wkflowinstanceService;
            _IAuthrolService = authrolService;
            _IMessagetemplateService = messagetemplateService;
            _IContacttypService = contacttypService;
            _IOrgtyporgstatusService = orgtyporgstatusService;
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Invite(InviteRequestInfo customer)
        {
            try
            {
                if (customer.ParentId == null)
                {
                    customer.ParentId = 1;
                }
                int? org_id = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());

                var user = _IUserService.GetUsers().Where(p => p.UserName == customer.Email);
                if (user.Count() > 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Customer Email already exists in our system");
                }
                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());
                var excustomer = _IOrgService.GetOrgs().Where(p => p.Name == customer.Name);

                var curDateTime = DateTime.UtcNow;

                if (excustomer.Count() <= 0)
                {
                    OrgTyp custType;
                    if (customer.TypeId == 6)
                    {
                        custType = _IOrgtypService.GetOrgtyps().Where(p => p.TypCd == "CUST").FirstOrDefault();
                    }
                    else
                    {
                        custType = _IOrgtypService.GetOrgtyps().Where(p => p.TypCd == "RESE").FirstOrDefault();
                    }

                    var cguid = Guid.NewGuid();

                    Org newOrg = new Org
                    {
                        Name = customer.Name,
                        CreateDate = DateTime.UtcNow,
                        CreatedUserId = userID,
                        ModifiedDate = DateTime.UtcNow,
                        ModifiedUserId = userID,
                        InviteDate = DateTime.UtcNow,
                        OrgTypId = custType.Id,
                        soKey = cguid,
                    };


                    var parentOrg = _IOrgService.GetOrg(customer.ParentId);


                    if (customer.TypeId == 6)
                    {
                        OrgCust newCustomer = new OrgCust()
                        {
                            Org = newOrg,
                        };
                        _IOrgcustService.AddOrgcust(newCustomer);
                    }
                    else
                    {
                        OrgReseller newReseller = new OrgReseller()
                        {
                            Org = newOrg,
                        };
                        _IOrgresellerService.AddOrgreseller(newReseller);
                    }

                    _IOrgorgService.AddOrgorg(new OrgOrg() { Org = parentOrg, AssociatedOrgId = newOrg.Id });

                    var orgTypeStatus = _IOrgtyporgstatusService.GetOrgtyporgstatus().Where(p => p.OrgTyp.TypCd == "CUST" && p.OrgStatus.StatusCd == "INVITED").FirstOrDefault();

                    newOrg.OrgStatusHists.Add(new OrgStatusHist() { Org = newOrg,
                        OrgTypOrgStatu = orgTypeStatus,
                        CreateDate = DateTime.UtcNow});


                    var guid = Guid.NewGuid();

                    var newuser = new User() { UserName = customer.Email, Password = "", Per = new Per() { FirstName = "", LastName = "", MiddleName = "", Createdate = curDateTime }, CreateDate = curDateTime, IsProcess = false, soKey = guid, CreateUserId = userID, InviteDate = curDateTime, IsSuperAdmin = false };

                    var authrole = _IAuthrolService.GetAuthrols().Where(p => p.Name == "Customer Admin").FirstOrDefault();

                    newuser.UserAuthRols.Add(new UserAuthRol() { User = newuser, AuthRol = authrole });

                    newuser.PortUsers.Add(new PortUser() { User = newuser, PortId = 1 });

                    newuser.OrgUsers.Add(new OrgUser() { User = newuser, Org = newOrg, Type="Primary"});


                    var Singupmessagetemp = _IMessagetemplateService.GetMessagetemplates().Where(p => p.Name == "Signup").FirstOrDefault();

                    var messagetemp = _IMessagetemplateService.GetMessagetemplates().Where(p => p.Name == "InviteUser").FirstOrDefault();

                    var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                    string Singupurl;

                    if (customer.TypeId == 6)
                    {
                        Singupurl = baseUrl + "/cust/" + cguid.ToString() + "/signup";
                    }
                    else
                    {
                        Singupurl = baseUrl + "/resel/" + cguid.ToString() + "/signup";
                    }

                    if (Singupmessagetemp != null)
                    {
                        newuser.MessageUsers.Add(new MessageUser() { User = newuser, DeliveryMethodId = 1, Message = new Message() { CreateDate = curDateTime, HeaderText = Singupmessagetemp.HeaderText, MessageBody = string.Format(Singupmessagetemp.TemplateText, Singupurl) } });
                    }

                    _IUserService.AddUser(newuser);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Customer Name already exists in our system");
                }


            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }


        [HttpPost]
        public HttpResponseMessage Signup(SignupRequestInfo signupRequestInfo)
        {
            OrgCust newCustomer = new OrgCust();
            OrgReseller newReseller = new OrgReseller();


            try
            {
                var rolename = 0;

                bool isNewCustomer = signupRequestInfo.userType == "RESE" ? false : true;

                var orgCustomerType = _IOrgtypService.GetOrgtyps().Where(p => p.TypCd == SystemConstants.ORG_TYPE_CD_CUSTOMER).FirstOrDefault();
                var orgResellerType = _IOrgtypService.GetOrgtyps().Where(p => p.TypCd == SystemConstants.ORG_TYPE_CD_RESELLER).FirstOrDefault();

                var newOrg = _IOrgService.GetOrgs().Where(p => p.soKey == Guid.Parse(signupRequestInfo.id)).FirstOrDefault();
                if (newOrg == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Invalid Invitation");
                }

                Locn newLocn = new Locn
                {
                    AddressLine1 = signupRequestInfo.addressStreet1,
                    AddressLine2 = signupRequestInfo.addressStreet2,
                    City = signupRequestInfo.addressCity,
                    DateLastMaint = DateTime.UtcNow,
                    CreateDate = DateTime.UtcNow,
                    State = signupRequestInfo.addressState,
                    ZipCode = signupRequestInfo.addressZip
                };

                var os = _IOrgtyporgstatusService.GetOrgtyporgstatus().Where(p => p.OrgStatus.StatusCd == "INREVIEW").FirstOrDefault();
                newOrg.OrgStatusHists.Add(new OrgStatusHist() { Org = newOrg, OrgTypOrgStatu = os, CreateDate=DateTime.UtcNow });

                //  Associate Organization to Location
                newOrg.OrgLocns.Add(new OrgLocn { Locn = newLocn, Org = newOrg });

                //  Created New OrgCust or OrgReseller based on request
                if (isNewCustomer)
                {
                   
                    rolename = 2;
                }
                else
                {
                    rolename = 3;
                }

                _IOrgService.UpdateOrg(newOrg);

                // Create User
                var newuser = _IUserService.GetUsers().Where(p => p.UserName == signupRequestInfo.email).FirstOrDefault();
                var ctype = _IContacttypService.GetContacttyps().Find(p => p.ContactTypCd == "PHON");
                var contact = new Contact { ContactTyp = ctype, Value = signupRequestInfo.phone }; 

                var curDateTime = DateTime.UtcNow;
                var guid = Guid.NewGuid();

                if (newuser.Per == null)
                {
                    Per per = new Per() { FirstName = signupRequestInfo.firstName, LastName = signupRequestInfo.lastName, MiddleName = signupRequestInfo.middleName, Createdate = curDateTime, Title = signupRequestInfo.contactTitle };
                    newuser.Per = per;
                }
                else
                {
                    newuser.Per.FirstName = signupRequestInfo.firstName;
                    newuser.Per.MiddleName = signupRequestInfo.middleName;
                    newuser.Per.LastName = signupRequestInfo.lastName;
                    newuser.Per.Title = signupRequestInfo.contactTitle;
                }

                newuser.Per.PersContacts.Add(new PersContact() { Per = newuser.Per, Contact = contact });

                var authrole = _IAuthrolService.GetAuthrol(rolename);

                newuser.UserAuthRols.Add(new UserAuthRol() { User = newuser, AuthRol = authrole });
                newuser.InactiveDate = null;
                newuser.InviteDate = null;

                var messagetemp = _IMessagetemplateService.GetMessagetemplates().Where(p => p.Name == "SignupAccepted").FirstOrDefault();
                var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                if (messagetemp != null)
                {
                    newuser.MessageUsers.Add(new MessageUser() { User = newuser, DeliveryMethodId = 1, Message = new Message() { CreateDate = curDateTime, HeaderText = messagetemp.HeaderText, MessageBody = messagetemp.TemplateText + "\n" + baseUrl + "/login" } });
                }

                _IUserService.UpdateUser(newuser);
                _IUserService.UpdatePassword(signupRequestInfo.email, PasswordHash.CreateHash(signupRequestInfo.password));

                // Create Sign Up Workflow
                WkflowDef signUpWorkflow;
                int wkflowDefId = isNewCustomer ? 1 : 2; // 1= customer signup , 2 = reseller signup
                signUpWorkflow = _IWkflowdefService.GetWkflowdefs().Where(p => p.Id == wkflowDefId).FirstOrDefault();

                var signUpWorkflowInstance = new WkflowInstance
                {
                    CreateDate = DateTime.UtcNow,
                    DateLastMaint = DateTime.UtcNow,
                    WkflowDefId = signUpWorkflow.Id,
                    OrgId = newOrg.Id,
                    UserId =1,
                    CurrWkflowStatId = 5//InReview State
                };

                signUpWorkflowInstance.WkflowStepHists.Add(new WkflowStepHist
                {
                    CreateDate = DateTime.UtcNow,
                    DateLastMaint = DateTime.UtcNow,
                    WkflowStatId = 5,
                    CreatedUserId = 1
                });


                _IWkflowinstanceService.AddWkflowinstance(signUpWorkflowInstance);

                return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
            }
            catch (Exception daExp)
            {
                return Request.CreateResponse<bool>(HttpStatusCode.InternalServerError, false);
            }
           
        }

        [HttpGet]
        public HttpResponseMessage GetSignupData(string type, string id)
        {

            try
            {

                if (type == "user")
                {
                    var user = _IUserService.GetUsers().Where(p=>p.soKey== Guid.Parse(id)).FirstOrDefault();
                    if (user != null)
                    {                       
                        if (user.InviteDate!=null)
                        {
                            var days = DateTime.Compare(((DateTime)user.InviteDate).AddDays(7), DateTime.UtcNow);
                            if (days >= 0)
                            {
                                var Type = "USER";
                                if (type == "reset")
                                {
                                    Type = "RESET";
                                }

                                if (user.Per != null)
                                {
                                    var result = new { id = user.soKey, type = Type, parentName = "", name = "", email = user.UserName, firstName = user.Per.FirstName, middleName = user.Per.MiddleName, lastName = user.Per.LastName };
                                    return Request.CreateResponse(HttpStatusCode.OK, result);
                                } else
                                {
                                    var result = new { id = user.soKey, type = Type, parentName = "", name = "", email = user.UserName, firstName = "", middleName = "", lastName = "" };
                                    return Request.CreateResponse(HttpStatusCode.OK, result);
                                }
                            }
                            else
                            {
                                var createresponse = Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invitation Expired, Contact Customer Service.");
                                throw new HttpResponseException(createresponse);
                            }
                        }
                        else
                        {
                            var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid invitation");
                            throw new HttpResponseException(createresponse);
                        }
                    }
                    else
                    {
                        var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid invitation");
                        throw new HttpResponseException(createresponse);
                    }
                }
                else
                {
                    var org = _IOrgService.GetOrgs().Where(p => p.soKey == Guid.Parse(id)).FirstOrDefault();

                    if (org != null)
                    {
                        if (org.OrgStatusHists.OrderByDescending(o => o.Id).FirstOrDefault().OrgTypOrgStatu.OrgStatus.StatusCd == "INVITED")
                        {
                            var days = DateTime.Compare(((DateTime)org.InviteDate).AddDays(7), DateTime.UtcNow);
                            if (days >= 0)
                            {
                                var result = new { id = org.soKey, type = org.OrgTyp.TypCd, parentName = org.OrgOrgs1.FirstOrDefault().Org.Name, name = org.Name, email = org.OrgUsers.Where(p => p.Type == "Primary").FirstOrDefault().User.UserName, firstName = "", middleName = "", lastName = "" };
                                return Request.CreateResponse(HttpStatusCode.OK, result);
                            }
                            else
                            {
                                var createresponse = Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invitation Expired, Contact Customer Service.");
                                throw new HttpResponseException(createresponse);
                            }
                        }
                        else
                        {
                            var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid invitation");
                            throw new HttpResponseException(createresponse);
                        }
                    }
                    else
                    {
                        var createresponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid invitation");
                        throw new HttpResponseException(createresponse);
                    }
                }

            }
            catch (Exception daExp)
            {
                return Request.CreateResponse<bool>(HttpStatusCode.InternalServerError, false);
            }
          }

        }

    }