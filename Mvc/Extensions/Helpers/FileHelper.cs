using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Ionic.Zip;
using System.Web.Mvc;
using System.Configuration;
using ARQ.Maqueta.Presentation.Mvc.ViewModels;
using Newtonsoft.Json;

namespace ARQ.Maqueta.Presentation.Mvc.Extensions.Helpers
{
    public class FileHelper
    {

        public static IEnumerable<AiportViewModel> GetAiportsListItemsFromJsonFile()
        {
            string jsonFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AiportsJson"]);

            using (StreamReader r = new StreamReader(jsonFile))
            {
                string json = r.ReadToEnd();
                var aiports = JsonConvert.DeserializeObject<List<AiportViewModel>>(json);

                var resultList = aiports
                     .Where(m => m.Name != string.Empty)
                     .Take(100);

                return resultList;
            }
          
        }
       
    }
}