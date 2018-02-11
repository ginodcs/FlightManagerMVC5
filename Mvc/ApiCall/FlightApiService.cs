using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ARQ.Maqueta.Entities;
using ARQ.Maqueta.Presentation.Mvc.Extensions.Helpers;
using Newtonsoft.Json;

namespace ARQ.Maqueta.Presentation.Mvc.ApiCall
{
    public class FlightApiService
    {
        private readonly HttpComposer httpComposer;

        #region Constructor 

        public FlightApiService()
        {
            this.httpComposer = new HttpComposer();
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// Get the Flights
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public async Task<List<Flight>> GetFlights(string searchValue)
        {
            var client = this.httpComposer.GetHttpClient();
            string requestUrl = string.Format(Constants.GetFlightsSearchValueUrl, searchValue);
            var task = client.GetAsync(requestUrl);

            task.Wait();
            var response = task.Result;

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw Common.GetException(response, json);
            }

            return JsonConvert.DeserializeObject<List<Flight>>(json);
        }

        public async Task<Flight> GetFlight(int id)
        {
            var client = this.httpComposer.GetHttpClient();
            string requestUrl = string.Format(Constants.GetFlightById, id);
            var task = client.GetAsync(requestUrl);

            task.Wait();
            var response = task.Result;

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw Common.GetException(response, json);
            }

            return JsonConvert.DeserializeObject<Flight>(json);
        }

        /// <summary>
        /// Post the Flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public async Task<int> PostFlight(Flight flight)
        {
            var client = this.httpComposer.GetHttpClient();
            var task = Task.Factory.StartNew(() => JsonConvert.SerializeObject(flight));
            task.Wait();
            var serialized = task.Result;

            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            var uri = string.Format(Constants.PostFlightUrl);

            var response = await client.PostAsync(uri, stringContent);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw Common.GetException(response, json);
            }

            return JsonConvert.DeserializeObject<int>(json);
        }

        /// <summary>
        /// Put the Flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public async Task<bool> PutFlight(Flight flight)
        {
            var client = this.httpComposer.GetHttpClient();
            var task = Task.Factory.StartNew(() => JsonConvert.SerializeObject(flight));
            task.Wait();
            var serialized = task.Result;

            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            var uri = string.Format(Constants.PutFlightUrl);

            var response = await client.PutAsync(uri, stringContent);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw Common.GetException(response, json);
            }

            return JsonConvert.DeserializeObject<bool>(json);
        }

        /// <summary>
        /// Delete the Flight
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFlight(int id)
        {
            var client = this.httpComposer.GetHttpClient();
            var task = client.DeleteAsync(string.Format(Constants.DeleteFlightUrl, id));
            task.Wait();
            var response = task.Result;

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw Common.GetException(response, json);
            }

            return JsonConvert.DeserializeObject<bool>(json);
        }

        #endregion
    }
}