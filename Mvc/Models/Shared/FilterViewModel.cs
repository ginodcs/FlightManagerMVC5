using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ARQ.Maqueta.Presentation.Mvc.Extensions.ModelBinders;
using ARQ.Maqueta.Presentation.Mvc.Models.Shared;
using Resources;

namespace ARQ.Maqueta.Presentation.Mvc.ViewModels
{
    public partial class FilterViewModel
    {
       
        [Display(Name = "Flight", ResourceType = typeof(FlightResource))]
        public string Flight { get; set; }
    }
}
