
using Oc.Carbon.Auth.Api.Models;
using Oc.Carbon.Common.Contracts;
using Oc.Carbon.DataAccess.Contracts;
using Oc.Carbon.DataAccess;
using Oc.Carbon.ServiceLayer.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data.Entity;
using Oc.Carbon.DTO.PlatformDTO;
using Oc.Carbon.DTO.Requests;
using Oc.Carbon.DTO.Mapping.Core;

namespace Oc.Carbon.WebServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlatformAuthController : ApiController
    {
        ILogService _LoggerService;
        IUserService _IUserService;
        IPortService _IPortalService;
        IPortuserService _IPortalUserService;
        //ISessionService _ISessionService;
        //ISessionTypeService _ISessionTypeService;
        IUnitOfWork _UnitOfWork;

        public PlatformAuthController(ILogService loggerService, IUserService userAccountService, IPortService portalService, IPortuserService portalUserService,
            IUnitOfWork unitOfWork)//,  ISessionService sessionService,ISessionTypeService sessionTypeService
        {
            this._LoggerService = loggerService;
            this._IUserService = userAccountService;
            this._IPortalService = portalService;
            this._IPortalUserService = portalUserService;
            //this._ISessionService = sessionService;
            this._UnitOfWork = unitOfWork;
            //this._ISessionTypeService = sessionTypeService;
        }

        [AllowAnonymous]
        public HttpResponseMessage UserLogin(UserPasswordModel userPasswordModel)
        {

            var securityUrl = ConfigurationManager.AppSettings["AuthServer"];
            var clientID = ConfigurationManager.AppSettings["ClientID"];

            HttpWebRequest httpWReq =
                (HttpWebRequest)WebRequest.Create( securityUrl + @"/oauth2/token");

            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "Username=" + userPasswordModel.UserName;
            postData += "&Password=" + userPasswordModel.Password ;
            postData += "&grant_type=password";
            postData += "&client_id=" + clientID;

            try
            {
                byte[] data = encoding.GetBytes(postData);

                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;

                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                AuthTokenDTO authToken = JsonConvert.DeserializeObject<AuthTokenDTO>(responseString);


                var user = _IUserService.GetUserByUserName(userPasswordModel.UserName);

                var userLookedUp = user;

                var portUser = _IPortalUserService.QueryData().Where(p => p.User.Id == user.Id).ToList();
                var userPort = portUser[0].Port;

                //var portalDefinition = _IPortalService.GetPortalDefinition(userPort.Id);

                var portalUser = PlatformMappingHelper.Map<User, PortalUserDTO>(user);

                //AutherizationResponseDTO autherizationResponse = new AutherizationResponseDTO
                //{ AuthToken = authToken, PortalId = userPort.Id, PortalUser = portalUser };

               var autherizationResponse = new
                { data = authToken, error="" };

                var userOrg = user.Orgs.FirstOrDefault();


                portalUser.Org = PlatformMappingHelper.Map<Org, OrgDTO>(userOrg);


                try
                {
                    if (user != null)
                    {
                        user.UserLoginHists.Add(new UserLoginHist { LoginDate = DateTime.UtcNow });
                        _UnitOfWork.Commit();
                    }
                }
                catch (Exception daExp)
                {

                    var x = daExp; 
                }

                return Request.CreateResponse<dynamic>(HttpStatusCode.OK, autherizationResponse);
            }
            catch (Exception exp)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            
        }


        [HttpGet]
        public string Ping()
        {
            return "pong";
        }
    }
}