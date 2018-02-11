using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web.Services;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using Microsoft.Owin.Security;

namespace ARQ.Maqueta.Presentation.Mvc.Extensions.Helpers
{
    public class Common : System.Web.UI.Page
    {
        #region Public Methods

        public static string ObtenerRuta(string ruta)
        {
          
            // Se comprueba si la ruta no termina con '\' para añadirlo
            if (!ruta.EndsWith(@"\"))
            {
                // Se añade el '\' al final de la ruta
                ruta += "\\";
            }
            // Se devuelve la ruta
            return ruta;
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="json">The json.</param>
        /// <returns>The api exception.</returns>
        public static ApiException GetException(HttpResponseMessage response, string json)
        {
            ApiException jsonException = null;

            try
            {
                dynamic exception = JsonConvert.DeserializeObject(json);

                jsonException = MapException(exception) as ApiException;
            }
            catch
            {
                jsonException = new ApiException();
            }

            if (null != response && null != jsonException)
            {
                if(HttpStatusCode.Unauthorized == response.StatusCode)
                {
                    jsonException.StatusCode = HttpStatusCode.Forbidden;
                    jsonException.Source = HttpStatusCode.Forbidden.ToString();
                }
                else
                {
                    jsonException.StatusCode = response.StatusCode;
                    jsonException.Source = response.StatusCode.ToString();
                }
            }

            return jsonException;
        }

        #endregion

        #region private Methods

        /// <summary>
        /// Maps the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        /// The exception.
        /// </returns>
        private static ApiException MapException(dynamic exception)
        {
            var obj = exception as JObject;

            var message = string.Empty;

            try
            {
                if (null != obj)
                {
                    var jproperties = obj.Children().Where(c => c is JProperty).ToList();

                    if (null != jproperties)
                    {
                        var messageProperty = jproperties.FirstOrDefault(p => ((JProperty)p).Name == "cod_respuesta") as JProperty;
                        var exceptionMessageProperty = jproperties.FirstOrDefault(p => ((JProperty)p).Name == "des_respuesta") as JProperty;

                        var apiException = null != messageProperty ? new ApiException(messageProperty.Value.ToString()) : new ApiException();

                        if (null != exceptionMessageProperty)
                        {
                            apiException.ExceptionMessage = exceptionMessageProperty.Value.ToString();
                        }

                        return apiException;
                    }
                }
            }
            catch (Exception)
            {
                return new ApiException(message);
            }

            return new ApiException(message);
        }

        #endregion
    }
}