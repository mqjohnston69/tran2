using Oc.Carbon.Common.Contracts;
using Oc.Carbon.DataAccess;
using Oc.Carbon.DataAccess.Contracts;
using Oc.Carbon.DTO.Mapping.Core;
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
using Oc.Carbon.Auth.Api.Password;
using Oc.Carbon.WebServices.Models;

namespace Oc.Carbon.WebServices.Controllers
{
    public class UserController : ApiController
    {

        ILogService _LoggerService;
        IUserService _IUserService;
        IContacttypService _IContacttypService;
        IAuthrolService _IAuthrolService;
        IOrgService _IOrgService;
        IMessagetemplateService _IMessagetemplateService;
        IMessageuserService _IMessageuserService;
        IWkflowdefService _IWkflowdefService;
        IWkflowinstanceService _IWkflowinstanceService;
        IOrgtyporgstatusService _IOrgtyporgstatusService;
        IOrguserService _IOrguserService;

        IUnitOfWork _UnitOfWork;


        public UserController(ILogService loggerService, IUserService userService, IContacttypService contacttypService,
            IAuthrolService authrolService, IOrgService orgService, IMessagetemplateService messagetemplateService,
            IMessageuserService messageuserService, IWkflowdefService wkflowdefService, IWkflowinstanceService wkflowinstanceService,
            IOrgtyporgstatusService orgtyporgstatusService, IOrguserService _IOrguserService, IUnitOfWork unitOfWork)
        {

            this._LoggerService = loggerService;
            this._IUserService = userService;
            this._IContacttypService = contacttypService;
            this._IAuthrolService = authrolService;
            this._IOrgService = orgService;
            this._IMessagetemplateService= messagetemplateService;
            this._IMessageuserService = messageuserService;
            this._IWkflowdefService = wkflowdefService;
            this._IWkflowinstanceService = wkflowinstanceService;
            this._IOrgtyporgstatusService = orgtyporgstatusService;


        }

        /// <summary>
        /// UserSignup
        /// </summary>
        /// <param name="docTypeDTO">DocTypDTO</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "UserSignup"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage Invite(UserDTO userDTO)
        {

            try
            {

                var user = _IUserService.GetUsers().Where(p => p.UserName == userDTO.userName);
                if (user.Count() <= 0)
                {
                    int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                    var curDateTime = DateTime.UtcNow;

                    var guid = Guid.NewGuid();

                    var newuser = new User() { UserName = userDTO.userName, Password="",   Per = new Per() { FirstName = userDTO.firstName, LastName = userDTO.lastName, MiddleName = userDTO.middleName, Createdate = curDateTime }, CreateDate = curDateTime, IsProcess = false, soKey = guid, CreateUserId = userID, InviteDate = curDateTime, IsSuperAdmin = false };

                    var org = _IOrgService.GetOrg(userDTO.orgId);

                    AuthRol authrole;

                    if (org.OrgTyp.TypCd == "TENA")
                    {
                        authrole = _IAuthrolService.GetAuthrol(2);
                    }
                    else if (org.OrgTyp.TypCd == "RESE")
                    {
                        authrole = _IAuthrolService.GetAuthrol(4);
                    }
                    else
                    {
                        authrole = _IAuthrolService.GetAuthrol(3);
                    }

                    newuser.UserAuthRols.Add(new UserAuthRol() { User = newuser, AuthRol = authrole });
                    newuser.PortUsers.Add(new PortUser() { User = newuser, PortId = 1 });
                    newuser.OrgUsers.Add(new OrgUser() { User = newuser, Org = org });

                    var messagetemp = _IMessagetemplateService.GetMessagetemplates().Where(p => p.Name == "InviteUser").FirstOrDefault();

                    var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                    var url = baseUrl + "/user/" + guid.ToString() + "/signup";

                    if (messagetemp != null)
                    {
                        newuser.MessageUsers.Add(new MessageUser() { User = newuser, DeliveryMethodId = 1, Message = new Message() { CreateDate = curDateTime, HeaderText = messagetemp.HeaderText, MessageBody = string.Format(messagetemp.TemplateText, url) } });
                    }

                    _IUserService.AddUser(newuser);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "UserName Needs to be Unique");
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }

        /// <summary>
        /// UserSignup
        /// </summary>
        /// <param name="docTypeDTO">DocTypDTO</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "UserSignup"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage ReInvite(UserDTO userDTO)
        {

            try
            {
                var newuser = _IUserService.GetUser(userDTO.id);
                if (newuser != null)
                {

                    var curDateTime = DateTime.UtcNow;

                    var messagetemp = _IMessagetemplateService.GetMessagetemplates().Where(p => p.Name == "InviteUser").FirstOrDefault();

                    var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                    var guid = newuser.soKey.ToString();

                    var url = baseUrl + "/user/" + guid + "/signup";

                    newuser.UserName = userDTO.userName;
                    newuser.Per.FirstName = userDTO.firstName;
                    newuser.Per.MiddleName = userDTO.middleName;
                    newuser.Per.LastName = userDTO.lastName;
                    newuser.InviteDate = DateTime.UtcNow;

                    if (messagetemp != null)
                    {
                        newuser.MessageUsers.Add(new MessageUser() { User = newuser, DeliveryMethodId = 1, Message = new Message() { CreateDate = curDateTime, HeaderText = messagetemp.HeaderText, MessageBody = string.Format(messagetemp.TemplateText, url) } });
                    }

                    _IUserService.UpdateUser(newuser);


                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "User not found");
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }


        /// <summary>
        /// ValidateUserInvite
        /// </summary>
        /// <param name="docTypeDTO">DocTypDTO</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "ValidateUserInvite"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpPost]
        public HttpResponseMessage Validate(UserDTO userDTO)
        {

            try
            {
                var curDateTime = DateTime.UtcNow;

                var user = _IUserService.GetUsers().Where(p => p.soKey == Guid.Parse(userDTO.key)).FirstOrDefault();
                if (user!= null)
                {

                    if (user.InviteDate != null)
                    {
                        if (curDateTime.AddDays(-10) < user.InviteDate)
                        {
                            //int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                            if (userDTO.firstName != null)
                            {
                                user.Per.FirstName = userDTO.firstName;
                                user.Per.LastName = userDTO.lastName;
                                user.Per.MiddleName = userDTO.middleName;
                                user.Per.Title = userDTO.title;
                                user.DateLastMaint = curDateTime;
                                user.ModifiedUserId = 1;
                                user.Per.DateLastMaint = curDateTime;

                                if (user.Per.PersContacts.Where(p => p.Contact.ContactTyp.ContactTypCd == "PHON").Count() > 0)
                                {
                                    user.Per.PersContacts.Where(p => p.Contact.ContactTyp.ContactTypCd == "PHON").FirstOrDefault().Contact.Value = userDTO.phone;
                                }
                                else
                                {
                                    var ctype = _IContacttypService.GetContacttyps().Find(p => p.ContactTypCd == "PHON");
                                    user.Per.PersContacts.Add(new PersContact() { Per = user.Per, Contact = new Contact { ContactTyp = ctype, Value = userDTO.phone } });
                                }
                            }

                            user.InviteDate = null;
                            _IUserService.UpdateUser(user);
                            _IUserService.UpdatePassword(user.UserName, PasswordHash.CreateHash(userDTO.password));

                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Invitation Expired, Please contact your Administrator");
                        }

                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Invitation not found");
                    }

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "UserName could not be found");
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }


        /// <summary>
        /// Returns User List based on Filters
        /// </summary>
        /// <param name="orgIds">OrgID</param>
        /// <param name="UserName">UserName</param>
        /// <param name="FirstName">FirstName</param>
        /// <param name="LastName">LastName</param>
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
        public HttpResponseMessage Search(UserSearchRequest request)
        {
            //if (request.OrgId == null)
            //{
            //    request.OrgId = int.Parse(Request.Headers.GetValues("orgId").FirstOrDefault());
            //}

            request.UserName = request.UserName == null ? string.Empty : request.UserName;
            request.FirstName = request.FirstName == null ? string.Empty : request.FirstName;
            request.LastName = request.LastName == null ? string.Empty : request.LastName;


            var users = _IUserService.GetUsersByFilter(request);

            var search = PlatformMappingHelper.Map<IList<User>, IList<UserDTO>>(users.ToList()).ToList();

            if (search == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Request.CreateResponse < IList<UserDTO>>(HttpStatusCode.OK, search);
        }


        /// <summary>
        /// Returns User  based on Id
        /// </summary>
        /// <param name="userId">userId</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "GetUser"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage User(int userId)
        {

            UserDTO search = null;

            var user = _IUserService.GetUser(userId);

            if (user != null)
            {
                search = PlatformMappingHelper.Map<User, UserDTO>(user);

                if (search == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "UserName could not be found");
            }

            return Request.CreateResponse<UserDTO>(HttpStatusCode.OK, search);
        }

        /// <summary>
        /// Returns User  based on Id
        /// </summary>
        /// <param name="userId">userId</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "GetUserId"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpGet]
        public HttpResponseMessage GetUserId(string Id)
        {


            var user = _IUserService.GetUsers().Where(p=>p.soKey == Guid.Parse(Id)).FirstOrDefault();

            if (user != null)
            {
                var result = new {id = user.Id, userName = user.UserName, firstName = user.Per.FirstName, lastName = user.Per.LastName, middleName = user.Per.MiddleName};
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "UserName could not be found");
            }

        }


        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="UserDTO">UserDTO</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "UpdateUser"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpPut]
        [Authorize]
        public HttpResponseMessage Update(UserDTO userDTO)
        {

            try
            {
                var curDateTime = DateTime.UtcNow;

                var user = _IUserService.GetUser(userDTO.id);

                int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());

                if (user != null)
                {
                    if (user.UserName != userDTO.userName & user.InviteDate != null)
                    {
                        user.UserName = userDTO.userName;
                        var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                        var guid = user.soKey.ToString();
                        var url = baseUrl + "/user/" + guid + "/signup";

                        var messagetemp = _IMessagetemplateService.GetMessagetemplates().Where(p => p.Name == "InviteUser").FirstOrDefault();
                        if (messagetemp != null)
                        {
                            user.MessageUsers.Add(new MessageUser() { User = user, DeliveryMethodId = 1, Message = new Message() { CreateDate = curDateTime, HeaderText = messagetemp.HeaderText, MessageBody = string.Format(messagetemp.TemplateText, url) } });
                        }
                    }
                    user.Per.FirstName = userDTO.firstName;
                    user.Per.LastName = userDTO.lastName;
                    user.Per.MiddleName = userDTO.middleName;
                    user.Per.Title = userDTO.title;
                    if (userDTO.phone != null && userDTO.phone.Length > 0)
                    {
                        if (user.Per.PersContacts.Where(p => p.Contact.ContactTyp.ContactTypCd == "PHON").Count() > 0)
                        {
                            user.Per.PersContacts.Where(p => p.Contact.ContactTyp.ContactTypCd == "PHON").FirstOrDefault().Contact.Value = userDTO.phone;
                        }
                        else
                        {
                            var ctype = _IContacttypService.GetContacttyps().Find(p => p.ContactTypCd == "PHON");
                            user.Per.PersContacts.Add(new PersContact() { Per = user.Per, Contact = new Contact { ContactTyp = ctype, Value = userDTO.phone } });
                        }
                    }

                    var authrole = _IAuthrolService.GetAuthrol(userDTO.roleId);
                    var org = _IOrgService.GetOrg(userDTO.orgId);

                    if (user.OrgUsers.Count == 0)
                    {
                        user.OrgUsers.Add(new OrgUser() { User = user, Org = org });
                    }
                    else
                    {
                        user.OrgUsers.FirstOrDefault().OrgId = userDTO.orgId;
                    }
                    user.DateLastMaint = curDateTime;
                    user.UserAuthRols.FirstOrDefault().AuthRol = authrole;
                    _IUserService.UpdateUser(user);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "UserName could not be found");
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }


        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="userId">userId</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "InActivateUser"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpPut]
        [Authorize]
        public HttpResponseMessage InActivateUser(int userId)
        {

            try
            {
                var curDateTime = DateTime.UtcNow;

                var user = _IUserService.GetUser(userId);
                if (user != null)
                {
                    int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());
                    user.InactiveUserId = userID;
                    user.InactiveDate = curDateTime;
                    _IUserService.UpdateUser(user);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "UserName could not be found");
                }


            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse < bool>(HttpStatusCode.OK, true);
        }

        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpPut]
        [Authorize]
        public HttpResponseMessage ActivateUser(int userId)
        {

            try
            {
                var curDateTime = DateTime.UtcNow;

                var user = _IUserService.GetUser(userId);
                if (user != null)
                {
                    int userID = int.Parse(Request.Headers.GetValues("userId").FirstOrDefault());
                    user.InactiveUserId = null;
                    user.InactiveDate = null;
                    _IUserService.UpdateUser(user);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "UserName could not be found");
                }


            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="userId">userId</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "UpdatePwd"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpPut]
        public HttpResponseMessage UpdatePwd(UserDTO userDTO)
        {

            try
            {
                var curDateTime = DateTime.UtcNow;

                var user = _IUserService.GetUser(userDTO.id);
                if (user != null)
                {
                    if(user.InviteDate != null)
                    { 
                        user.InviteDate = null;
                        _IUserService.UpdateUser(user);
                    }
                    _IUserService.UpdatePassword(user.UserName, PasswordHash.CreateHash(userDTO.password));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "UserName could not be found");
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="userId">userId</param>
        [RequestHeaders(Name = "Accept",
            Example = "application/json",
            IsRequired = true,
            Type = typeof(string),
            Description = "SendResetPwdInvite"
        )]
        [ResponseBody(StatusCode = HttpStatusCode.OK, ContentType = "application/json", Example = "[should be the location of this test]", Description = "This is the standard request back.")]
        [ResponseBody(StatusCode = HttpStatusCode.BadRequest, ContentType = "application/json", Example = "[bad request]")]
        [ResponseBody(StatusCode = HttpStatusCode.InternalServerError, ContentType = "application/json", Example = "[internal server error]")]
        [HttpPost]
        public HttpResponseMessage SendResetPwdInvite(ResetPWDRequestInfo data)
        {

            try
            {
                var curDateTime = DateTime.UtcNow;

                var user = _IUserService.GetUsers().Where(p => p.UserName.ToLower() == data.userName.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    var messagetemp = _IMessagetemplateService.GetMessagetemplates().Where(p => p.Name == "ResetPwd").FirstOrDefault();

                    var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);

                    var url = baseUrl + "/" + user.soKey + "/changepwd";

                    user.InviteDate = curDateTime;

                    if (messagetemp != null)
                    {
                        user.MessageUsers.Add(new MessageUser() { User = user, DeliveryMethodId = 1, Message = new Message() { CreateDate = curDateTime, HeaderText = messagetemp.HeaderText, MessageBody = string.Format(messagetemp.TemplateText,user.UserName, url) } });
                    }

                    _IUserService.UpdateUser(user);

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "UserName could not be found");
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
        public HttpResponseMessage GetAuthRoles()
        {
            var authRoles = _IAuthrolService.GetAuthrols();

            var result = PlatformMappingHelper.Map<IList<AuthRol>, IList<DTO.PlatformDTO.AuthRolDTO>>(authRoles.ToList());

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Request.CreateResponse<IList<DTO.PlatformDTO.AuthRolDTO>>(HttpStatusCode.OK, result);

        }

    }
}