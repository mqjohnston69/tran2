using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Newtonsoft.Json;
using Oc.Carbon.Common.Implementations;
using Oc.Carbon.DataAccess;
using Oc.Carbon.WebServices.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Routing;

namespace Oc.Carbon.WebServices
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            // WebApiConfig.Register(GlobalConfiguration.Configuration);

            ConfigureWindsor(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(c => WebApiConfig.Register(c, _container));

            //Register the Authentication Delegation Handler
            GlobalConfiguration.Configuration.MessageHandlers.Add(new TokenValidationHandler());
            DbInterception.Add(new PlatformInterceptorLogging());
        }

        public static void ConfigureWindsor(HttpConfiguration configuration)
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));
            var dependencyResolver = new WindsorDependencyResolver(_container);
            configuration.DependencyResolver = dependencyResolver;
        }

        protected void Application_End()
        {
            _container.Dispose();
            base.Dispose();
        }

    }

    internal class localClaims
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    internal class TokenValidationHandler : DelegatingHandler
    {

        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;

            try
            {
                var securityUrl = ConfigurationManager.AppSettings["ResourceServer"];

                HttpWebRequest httpWReq =
                    (HttpWebRequest)WebRequest.Create(securityUrl + @"/api/protected");

                IEnumerable<string> authzHeaders;
                request.Headers.TryGetValues("Authorization", out authzHeaders);
                httpWReq.Method = "Get";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                string auth;
                string responseString = "";
                if (authzHeaders != null)
                {
                    auth = authzHeaders.ElementAt(0);
                    httpWReq.Headers.Add("Authorization", auth);
                    HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }


                if (responseString != "")
                {
                    var responseTxt = JsonConvert.DeserializeObject<IEnumerable<localClaims>>(responseString);

                    var claimsIdentityNew = new ClaimsIdentity("CustomApiKeyAuth");
                    var principal = new ClaimsPrincipal(new[] { claimsIdentityNew });
                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;

                    request.Headers.Add("userId", responseTxt.FirstOrDefault(p => p.Type == "userId").Value);
                    request.Headers.Add("portId", responseTxt.FirstOrDefault(p => p.Type == "portId").Value);
                    request.Headers.Add("authRols", responseTxt.FirstOrDefault(p => p.Type == "authRols").Value);
                    request.Headers.Add("orgId", responseTxt.FirstOrDefault(p => p.Type == "orgId").Value);
                }

                #region Cert purpose
                //request.Headers.Add("jsonPayload", responseTxt.ToString());


                // X509Store store = new X509Store(StoreName.TrustedPeople, 
                //                                      StoreLocation.LocalMachine);
                //store.Open(OpenFlags.ReadOnly);
                // X509Certificate2 cert = store.Certificates.Find(
                //                 X509FindType.FindByThumbprint, 
                //                 "C1677FBE7BDD6B131745E900E3B6764B4895A226",
                //                 false)[0];
                // store.Close();


                // JWTSecurityTokenHandler tokenHandler = 
                //        new JWTSecurityTokenHandler();
                // TokenValidationParameters validationParameters = 
                //        new TokenValidationParameters()
                // {
                //     AllowedAudience = "urn:poormansactassample",
                //     ValidIssuer = "https://lefederateur.accesscontrol.windows.net/",                    
                //     SigningToken = new X509SecurityToken(cert)
                //};

                // Thread.CurrentPrincipal = 
                //          tokenHandler.ValidateToken(token, validationParameters);


                //string baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/";

                //HttpWebRequest requestAuth = WebRequest.Create(baseUrl + "Ev.Platform.ResourceServer.Api/api/Protected") as HttpWebRequest;
                //requestAuth.Method = "GET";
                //requestAuth.Accept = "application/json";
                //requestAuth.ContentType = "application/json";
                //requestAuth.Headers.Add("Authorization", "Bearer " + token);
                //string responseTxt = String.Empty;
                //using (HttpWebResponse response = requestAuth.GetResponse() as HttpWebResponse)
                //{
                //    var reader = new StreamReader(response.GetResponseStream());
                //    responseTxt = reader.ReadToEnd();
                //    response.Close();
                //}

                //var identity = new ClaimsIdentity(HttpContext.Current.User.Identity);
                //identity.Actor = new ClaimsIdentity();
                //identity.Actor.AddClaim(new Claim("jsonPayload", responseTxt));
                //var principal = new ClaimsPrincipal(identity);

                //Thread.CurrentPrincipal = principal;
                //HttpContext.Current.User = principal;

                //  request.Headers.Add("jsonPayload", responseTxt);
                #endregion

                return base.SendAsync(request, cancellationToken);

            }
            catch (Exception e)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            return Task<HttpResponseMessage>.Factory.StartNew(() =>
                new HttpResponseMessage(statusCode));
        }

    }
}
