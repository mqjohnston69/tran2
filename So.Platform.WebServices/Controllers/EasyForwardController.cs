using RAMLSharp;
using Oc.Carbon.Common.Contracts;
using Oc.Carbon.DataAccess;
using Oc.Carbon.DTO.Mapping.Core;
using Oc.Carbon.DTO.PlatformDTO;
using Oc.Carbon.DTO.SolutionDTO;
using Oc.Carbon.ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Oc.Carbon.WebServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EasyForwardController : ApiController
    {
        ILogService _loggerService;
        IPackageService _PackageService;

        public EasyForwardController(ILogService loggerService, IPackageService packageService)
        {
            this._loggerService = loggerService;
            this._PackageService = packageService;
        }

        public HttpResponseMessage GetPackages()
        {
            var packages = _PackageService.GetPackages();

            var result = PlatformMappingHelper.Map<IList<Package>, IList<PackageDTO>>(packages.ToList()).ToList();

            if (result == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse<IEnumerable<PackageDTO>>(HttpStatusCode.OK, result);

            
        }

        // GET api/RAML
        /// <summary>
        /// Gets Raml
        /// </summary>
        /// <returns>Returns a string of RAML</returns>
        [Route("api/RAML"), HttpGet]
        public HttpResponseMessage Raml()
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var r = new RAMLMapper(this);
            var data = r.WebApiToRamlModel(new Uri("http://www.google.com"), "Test API", "1", "application/json", "This is a test")
                        .ToString();
            result.Content = new StringContent(data);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/Raml+yaml");
            result.Content.Headers.ContentLength = data.Length;
            return result;
        }



    }
}