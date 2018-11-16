using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyMobileApp.Services;
using EasyMobileApp.Models;

namespace EasyMobileApp.Controllers
{
    [RoutePrefix("api/blob")]
    public class EasyBlobController : ApiController
    {

        private EasyBlobService _blobService;

        private HttpResponseMessage PrepareResponse(EasyBlobModel easyBlobModel)
        {

            HttpResponseMessage responseMessage = null;

            if (string.IsNullOrEmpty(easyBlobModel.BlobExceptionMessage) == false)
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                              easyBlobModel.BlobExceptionMessage);
            else
                responseMessage = Request.CreateResponse(HttpStatusCode.OK, easyBlobModel);

            return responseMessage;

        }

        public EasyBlobController()
        {

            _blobService = new EasyBlobService();

        }

        [Route("{blobNameString}/{typeString}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string blobNameString, string typeString)
        {

            var imageNameString = string.Concat(blobNameString, ".", typeString);
            var easyBlobModel = await _blobService.DownloadBlobAsync(imageNameString);
            var responseMessage = PrepareResponse(easyBlobModel);
            return responseMessage;

        }

        [Route("{blobNameString}/{typeString}")]
        [HttpPut]
        public async Task<HttpResponseMessage> Put(string blobNameString, string typeString,
                                                   [FromBody] EasyBlobModel uploadBlobModel)
        {

            var blobBytes = Convert.FromBase64String(uploadBlobModel.BlobContents);
            var imageNameString = string.Concat(blobNameString, ".", typeString);
            var easyBlobModel = await _blobService.UploadBlobAsync(imageNameString, blobBytes);
            var responseMessage = PrepareResponse(easyBlobModel);
            return responseMessage;

        }
    }
}
