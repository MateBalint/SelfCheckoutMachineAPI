using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelfCheckoutAPI.Exceptions;
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
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException);
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

        /// <summary>
        /// This method is responsible for the payment process.
        /// </summary>
        /// <param name="data">Contains the price and the amount of money that was inserted in the machine.</param>
        /// <returns>A 200 OK reponse along with the change if no exception happened.
        /// If an exception happened it returns a 400 Bad Request with the error description.
        /// </returns>
        [HttpPost]
        [Route("api/v1/Checkout")]
        public HttpResponseMessage ProcessPayment(JObject data)
        {
            HttpResponseMessage response;

            try
            {
                string inserted = data.SelectToken("inserted").ToString();
                string price = data.SelectToken("price").ToString();
                var insertedMoneyDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(inserted);
                DictionaryService.Merge(AvailableCurrency, insertedMoneyDict);
                int insertedMoneySum = PaymentService.CalculateInsertedMoney(insertedMoneyDict);
                Dictionary<string, int> changeDict = PaymentService.CalculateChange(insertedMoneySum, Int32.Parse(price));
                string changeJson = JsonConvert.SerializeObject(changeDict);
                response = Request.CreateResponse(HttpStatusCode.OK, changeJson);
            }
            catch(NotEnoughMoneyException ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return response;
        }

        [HttpPost]
        [Route("api/v1/BlockedBills")]
        public HttpResponseMessage GetAvailableDenominations(JObject data)
        {
            HttpResponseMessage response;

            try
            {
                string inserted = data.SelectToken("inserted").ToString();
                string price = data.SelectToken("price").ToString();
                var insertedMoneyDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(inserted);
                DictionaryService.Merge(AvailableCurrency, insertedMoneyDict);
                int insertedMoneySum = PaymentService.CalculateInsertedMoney(insertedMoneyDict);
                response = Request.CreateResponse(HttpStatusCode.OK, "success");
            }
            catch (NotEnoughMoneyException ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return response;
        }
    }
}