using ARQ.Maqueta.Entities;
using System.Device.Location;
using System;

namespace ARQ.Maqueta.Services.Domain
{
    public class FlightDomain
    {
        #region Private Fields 
        //Data for Boeing 747
        /// <summary>
        /// Liters per kilometer
        /// </summary>
        private const double consumption = 12;

        /// <summary>
        /// km/h
        /// </summary>
        private const double speed = 900;

        #endregion

        #region Constructor 

        public FlightDomain()
        { }

        #endregion

        #region Public Methods 
        
        public void FlightCalculator(Flight flight)
        {
            var distance = this.GetDistance(flight.Source, flight.Destination);

            flight.Distance = (decimal)distance;
            flight.FuelNeeded = (decimal)this.GetAircraftFuelConsumption(distance);
        }

        private TimeSpan GetFlighTime(Aiport source, Aiport destination)
        {
            return TimeSpan.FromHours(this.GetTime(source, destination));
        }

        #endregion

        #region Private Methods 

        private double GetDistance(Aiport source, Aiport destination)
        {
            var sourceCoord = new GeoCoordinate(source.Latitude, source.Longitude);
            var destinationCoord = new GeoCoordinate(destination.Latitude, destination.Longitude);

            return sourceCoord.GetDistanceTo(destinationCoord) / 1000;
        }

        private double GetTime(Aiport source, Aiport destination)
        {
            var distance = this.GetDistance(source, destination);

            return distance / speed;
        }

        private double GetAircraftFuelConsumption (double distance)
        {
            return distance * consumption;
        }

        #endregion
    }
}