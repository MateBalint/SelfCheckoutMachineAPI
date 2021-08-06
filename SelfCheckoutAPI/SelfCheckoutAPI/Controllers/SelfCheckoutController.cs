using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SelfCheckoutAPI.Controllers
{
    public class SelfCheckoutController : ApiController
    {
        private static Dictionary<string, int> AvailableCurrency = new Dictionary<string, int>();

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
                response = Request.CreateResponse(HttpStatusCode.OK, storedItems);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return response;
        }

        /// <summary>
        /// Locates the given values into the AvailableCurrency collection.
        /// </summary>
        /// <param name="currencies">Values to store.</param>
        /// <returns>A 200 OK response along with the available currencies if no exception has happened, exception message otherwise.</returns>
        [HttpPost]
        [Route("api/v1/Stock")]
        public HttpResponseMessage StockMachine([FromBody] JObject data)
        {
            HttpResponseMessage response;
            try
            {
                string json = data.ToString(Newtonsoft.Json.Formatting.None);
                var values = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
                DictionaryService.Merge(AvailableCurrency, values);
                string storedItems = JsonConvert.SerializeObject(AvailableCurrency);
                response = Request.CreateResponse(HttpStatusCode.OK, storedItems);

            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return response;
        }
    }
}