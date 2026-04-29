using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UITlapaleria.Models;

namespace UITlapaleria.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public AuthService()
        {
            _httpClient = new HttpClient();
            // Leemos la URL del .env
            _baseUrl = Environment.GetEnvironmentVariable("API_URL") ?? "https://localhost:7183/api";
        }

        public async Task<ApiResponse<LoginData>> LoginAsync(string usuario, string password)
        {
            try
            {
                var requestBody = new LoginRequest
                {
                    usuarioOCorreo = usuario,
                    password = password
                };

                // Convertir a JSON
                string json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Hacer la petición POST
                HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}/Auth/login", content);

                // Leer la respuesta
                string responseString = await response.Content.ReadAsStringAsync();

                // Deserializar el JSON al modelo
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<ApiResponse<LoginData>>(responseString, options);

                return result;
            }
            catch (Exception ex)
            {
                // Si el servidor está apagado o hay error de red
                return new ApiResponse<LoginData>
                {
                    success = false,
                    message = "Error de conexión con el servidor."
                };
            }
        }
    }
}