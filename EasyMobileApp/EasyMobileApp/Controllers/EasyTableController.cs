using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyMobileApp.Services;
using EasyMobileApp.Models;

namespace EasyMobileApp.Controllers
{

    [RoutePrefix("api/table")]
    public class EasyTableController : ApiController
    {

        private EasyTableService _tableService;

        private HttpResponseMessage PrepareResponse(Tuple<int, object> tableResponse)
        {

            HttpResponseMessage responseMessage = null;

            if (tableResponse.Item1 == -1)
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                                                              string.Empty);
            else
                responseMessage = Request.CreateResponse(HttpStatusCode.OK, tableResponse.Item2);

            return responseMessage;

        }

        public EasyTableController()
        {

            _tableService = new EasyTableService();

        }

        [Route("{tableNameString}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string tableNameString)
        {

            var fetchedResult = await _tableService.FetchAllAsync();

            var status = fetchedResult.Item1;
            var fetchedResultsList = fetchedResult.Item2.Cast<object>().ToList();
            var responseResult = new Tuple<int, object>(status, fetchedResultsList);

            var responseMessage = PrepareResponse(responseResult);
            return responseMessage;

        }

        [Route("{tableNameString}")]
        [HttpPut]
        public async Task<HttpResponseMessage> Put(string tableNameString,
                                                   [FromBody] EasyTableModel easyTableModel)
        {

            var insertResult = await _tableService.InsertRowAsync(easyTableModel);
            var responseMessage = PrepareResponse(insertResult);
            return responseMessage;

        }

    }
}
