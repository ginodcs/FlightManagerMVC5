using System;
using System.Collections.Generic;
using System.Web.Http;
using ARQ.Maqueta.Entities;
using ARQ.Maqueta.Entities.Entities;
using ARQ.Maqueta.Services;
using Newtonsoft.Json;

namespace ApiServices.Controllers
{
    /// <summary>
    /// Api Controller Flight
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    //[Authorize]
    [RoutePrefix("api/Flight")]
    public class FlightController : ApiController
    {
        #region Private Fields 

        private readonly IFlightService flightService;

        #endregion

        #region Constructors

        public FlightController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// Get the flights
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [Route("Search")]
        public List<Flight> GetFlights(string searchValue)
        {
            return this.flightService.Search(searchValue);
        }

        /// <summary>
        /// Get a Flight
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Find")]
        public Flight GetFlight(int id)
        {
            return this.flightService.Find(id);
        }

        /// <summary>
        /// Post the flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public int PostFlight(Flight flight)
        {
            var result = this.flightService.Add(flight);

            return result.Id;
        }

        /// <summary>
        /// Put the flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public bool PutFlight(Flight flight)
        {
            this.flightService.Change(flight);

            return true;
        }

        /// <summary>
        /// Delete the flight
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteFlight(int id)
        {
            this.flightService.Remove(id);

            return true;
        }

        #endregion
    }
}