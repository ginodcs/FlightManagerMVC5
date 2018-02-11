using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARQ.Maqueta.Entities
{
    public class Flight
    {
        #region Public Properties 

        public int Id { get; set; }

        public string Airline { get; set; }

        public string SourceAirportID { get; set; }

        public string SourceAirportName { get; set; }

        public string DestinationAirportID { get; set; }

        public string DestinationAirportName { get; set; }

        public decimal FuelNeeded { get; set; }

        public int Stops { get; set; }

        public decimal Distance { get; set; }

        public bool Active { get; set; }

        public string LastModifiedUser { get; set; }

        public DateTime LastModifiedDate { get; set; }

        [NotMapped]
        public Aiport Source { get; set; }

        [NotMapped]
        public Aiport Destination { get; set; }

        #endregion

        #region Constructor 

        public Flight()
        {
            this.Airline = string.Empty;
            this.SourceAirportID = string.Empty;
            this.DestinationAirportID = string.Empty;
            this.Stops = 0;
            this.Active = true;
            this.Source = new Aiport();
            this.Destination = new Aiport();
        }

        #endregion
    }
}