using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARQ.Maqueta.Entities;
using System.Data.Entity;
using ARQ.Maqueta.Services.Domain;

namespace ARQ.Maqueta.Services
{

    public class FlightService: Service, IFlightService
    {
        private FlightDomain flightDomain = new FlightDomain();

        public FlightService(IEntitiesDB entitiesDB) : base(entitiesDB) {  }

        /// <summary>
        /// Add a new flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public virtual Flight Add(Flight flight)
        {
            flight.SourceAirportName = flight.Source.Name;
            flight.DestinationAirportName = flight.Destination.Name;

            flightDomain.FlightCalculator(flight);

            EntitiesDB.FlightSet.Add(flight);
            EntitiesDB.SaveChanges();

            return flight;
        }

        /// <summary>
        /// Modified the flight
        /// </summary>
        /// <param name="flight"></param>
        public virtual void Change(Flight flight)
        {
            flight.SourceAirportName = flight.Source.Name;
            flight.DestinationAirportName = flight.Destination.Name;

            flightDomain.FlightCalculator(flight);

            EntitiesDB.Entry(flight).State = EntityState.Modified;

            EntitiesDB.SaveChanges();
        }

        /// <summary>
        /// Remove the flight
        /// </summary>
        /// <param name="Id"></param>
        public virtual void Remove(int Id)
        {
            Flight flight = EntitiesDB.FlightSet.Find(Id);

            EntitiesDB.FlightSet.Remove(flight);

            EntitiesDB.SaveChanges();
        }

        /// <summary>
        /// Find flights by airline, source aiport and destination aiport
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public virtual List<Flight> Search(string searchValue)
        {
            IQueryable<Flight> query = EntitiesDB.FlightSet.Where(x => string.IsNullOrEmpty(searchValue)
                || x.Airline.Contains(searchValue)
                || x.SourceAirportName.Contains(searchValue)
                || x.DestinationAirportName.Contains(searchValue));

            List<Flight> result = query.ToList();

            return result;
        }

        /// <summary>
        /// Find a fligh by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Flight Find(int id)
        {
            Flight flight = EntitiesDB.FlightSet.Find(id);

            return flight;
        }

    }
}