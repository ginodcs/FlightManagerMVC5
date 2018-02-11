
namespace ARQ.Maqueta.Presentation.Mvc.ApiCall
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the HttpComposer type.
    /// </summary>
    public class HttpComposer
    {
        /// <summary>
        /// The dictionary
        /// </summary>
        private static Dictionary<string, string> dictionary;

        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// The token.
        /// </returns>
        /// <exception cref="System.Exception">Throw if not IsSuccessStatusCode</exception>
        public async Task InitializeAsync(string username, string password)
        {
            if (null != dictionary && !string.IsNullOrWhiteSpace(dictionary["access_token"]))
            {
                return;
            }

            HttpResponseMessage response;

            var pairs = new List<KeyValuePair<string, string>>
                            {
                                new KeyValuePair<string, string>("grant_type", "password"),
                                new KeyValuePair<string, string>("username", username),
                                new KeyValuePair<string, string>("password", password),
                            };

            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var tokenEndpoint = new Uri(ConfigurationManager.AppSettings["TokenURLApi"]);
                var responseTask = client.PostAsync(tokenEndpoint, content);
                responseTask.Wait();

                response = responseTask.Result;
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            dictionary = GetTokenDictionary(responseContent);
        }

        /// <summary>
        /// Gets the HTTP client.
        /// </summary>
        /// <returns>
        /// The HTTP client.
        /// </returns>
        public HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseURLApi"])
            };

            if (null != dictionary && !string.IsNullOrWhiteSpace(dictionary["access_token"]))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", dictionary["access_token"]);
            }

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        /// <summary>
        /// Gets the token dictionary.
        /// </summary>
        /// <param name="responseContent">Content of the response.</param>
        /// <returns>
        /// The response dictionary.
        /// </returns>
        private static Dictionary<string, string> GetTokenDictionary(string responseContent)
        {
            var tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

            return tokenDictionary;
        }
    }
}