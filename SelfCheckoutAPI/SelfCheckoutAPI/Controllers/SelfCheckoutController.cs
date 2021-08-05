using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SelfCheckoutAPI.Controllers
{
    public class SelfCheckoutController : ApiController
    {
        private static Dictionary<int, int> AvailableCurrency = new Dictionary<int, int>();

        /// <summary>
        /// Gets the currently stored currencies.
        /// </summary>
        /// <returns>A 200 OK response along with the available currencies if no exception has happened, exception message otherwise.</returns>
        [HttpGet]
        [Route("api/v1/Stock")]
        public HttpResponseMessage GetStock()
        {
            HttpResponseMessage response;
            try
            {
                string storedItems = JsonConvert.SerializeObject(AvailableCurrency);
                response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(storedItems);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent($"Cause of error: {ex.Message}");
            }

            return response;
        }
    }
}