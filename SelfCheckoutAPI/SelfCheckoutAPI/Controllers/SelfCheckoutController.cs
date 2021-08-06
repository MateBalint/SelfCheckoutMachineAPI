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
        private static Dictionary<string, int> AvailableCurrency;
        private static List<int> Money;

        public SelfCheckoutController()
        {
            AvailableCurrency = new Dictionary<string, int>();
            Money = new List<int>() { 5, 10, 20, 50, 100, 200, 500, 1000, 5000, 10000, 20000 };
        }

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
        public HttpResponseMessage StockMachine(JObject data)
        {
            HttpResponseMessage response;
            try
            {
                string json = data.ToString(Formatting.None);
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

        [HttpPost]
        [Route("api/v1/Checkout")]
        public HttpResponseMessage ProcessPayment(JObject data)
        {
            HttpResponseMessage response;

            try
            {
                string inserted = data.SelectToken("inserted").ToString();
                string price = data.SelectToken("price").ToString();
                var values = JsonConvert.DeserializeObject<Dictionary<string, int>>(inserted);
                DictionaryService.Merge(AvailableCurrency, values);
                response = Request.CreateResponse(HttpStatusCode.OK, "success");
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return response;
        }
    }
}