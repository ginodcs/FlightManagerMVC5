using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ARQ.Maqueta.Entities;
using Resources;

namespace ARQ.Maqueta.Presentation.Mvc.ViewModels
{
    public class FlightViewModel
    {
        [Display(Name = "Id", ResourceType = typeof(FlightResource))]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Shared), ErrorMessageResourceName = "RequiredField")]
        [StringLength(255)]
        [Display(Name = "Airline", ResourceType = typeof(FlightResource))]
        public string Airline { get; set; }

        [Required(ErrorMessageResourceType = typeof(Shared), ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "SourceAirportID", ResourceType = typeof(FlightResource))]
        public string SourceAirportID { get; set; }

        [Display(Name = "SourceAirportName", ResourceType = typeof(FlightResource))]
        public string SourceAirportName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Shared), ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "DestinationAirportID", ResourceType = typeof(FlightResource))]
        public string DestinationAirportID { get; set; }

        [Display(Name = "DestinationAirportName", ResourceType = typeof(FlightResource))]
        public string DestinationAirportName { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        [Display(Name = "FuelNeeded", ResourceType = typeof(FlightResource))]
        public decimal FuelNeeded { get; set; }

        [Display(Name = "Stops", ResourceType = typeof(FlightResource))]
        public int Stops { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        [Display(Name = "Distance", ResourceType = typeof(FlightResource))]
        public decimal Distance { get; set; }

        [Display(Name = "Active", ResourceType = typeof(FlightResource))]
        public bool Active { get; set; }

        [Display(Name = "LastModifiedUser", ResourceType = typeof(FlightResource))]
        public string LastModifiedUser { get; set; }

        [Display(Name = "LastModifiedDate", ResourceType = typeof(FlightResource))]
        public DateTime LastModifiedDate { get; set; }

        public string Description
        {
            get
            {
                return SourceAirportName + " > " + DestinationAirportName;
            }
        }

        public IEnumerable<AiportViewModel> AiportList { get; set; }
    }
}