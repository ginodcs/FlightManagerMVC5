using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ARQ.Maqueta.Presentation.Mvc.ViewModels
{
    public class AiportViewModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}