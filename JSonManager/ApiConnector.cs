using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JSonManager
{
    public static class ApiConnector
    {

        private static HttpClient _httpClient;

        public static async Task<string>  Connect(Uri url)
        {
            // Generamos el objeto cliente HTTP con la dirección de la API correspondiente.
            HttpClient client = API_HttpClient(url);

            // Realizamos la llamada a la API.
            var response = await client.GetAsync(url);

            // Deserializamos la respuesta.
            if (response.IsSuccessStatusCode)            
                return await response.Content.ReadAsStringAsync();
            else
                return null;
        }

        /// <summary>
        /// Crea el objeto cliente HTTP y lo configura para la llamada a la API.
        /// </summary>
        /// <returns>Devuelve un objeto cliente HTTP correctamente configurado.</returns>
        private static HttpClient API_HttpClient(Uri baseUrl)
        {
            //Patrón Singleton para no crear más de un cliente HTTP.
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = baseUrl;
            }

            // Creamos la configuración mínima para la conexión.
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return _httpClient;
        }
    }
}
