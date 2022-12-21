using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using static JSonManager.Definitions;

namespace JSonManager
{
    public static class ApiConnector
    {

        private static HttpClient _httpClient;

        public static async Task<string> Connect(Uri url)
        {
            // Generamos el objeto cliente HTTP con la dirección de la API correspondiente.
            HttpClient client = API_HttpClient(url, APIDataFormat.json);

            // Realizamos la llamada a la API.
            var response = await client.GetAsync(url);

            // Deserializamos la respuesta.
            if (response.IsSuccessStatusCode)            
                return await response.Content.ReadAsStringAsync();
            else
                return null;
        }

        public static async Task<HttpResponseMessage> PostAsync(Uri url, string content)
        {

            // Generamos el objeto cliente HTTP con la dirección de la API correspondiente.
            HttpClient client = API_HttpClient(url, APIDataFormat.xml);

            var httpContent = new StringContent(content, Encoding.UTF8, "text/xml");
            // Realizamos la llamada a la API.
            //using StringContent xmlContent = new(xmlSerializer.Serialize()
            return await client.PostAsync(url, httpContent);    //PostAsJsonAsync(url, requestObject);
            //return await client.PostAsync(url, requestObject);
        }

        /// <summary>
        /// Crea el objeto cliente HTTP y lo configura para la llamada a la API.
        /// </summary>
        /// <returns>Devuelve un objeto cliente HTTP correctamente configurado.</returns>
        private static HttpClient API_HttpClient(Uri baseUrl, APIDataFormat dataFormat )
        {
            //Patrón Singleton para no crear más de un cliente HTTP.
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = baseUrl;
            }

            // Creamos la configuración mínima para la conexión.
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Content - Type: application / soap + xml"));
            _httpClient.DefaultRequestHeaders.Accept.Add(GetMediaType(dataFormat));             
            
            return _httpClient;
        }

        private static MediaTypeWithQualityHeaderValue GetMediaType(APIDataFormat dataTransferFormat)
        {
            string contentType = "application/" + dataTransferFormat.ToString();
            return new MediaTypeWithQualityHeaderValue(contentType);
        }
    }
}
