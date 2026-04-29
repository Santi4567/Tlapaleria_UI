using System.Text.Json.Serialization;

namespace UITlapaleria.Models
{
    // El cuerpo de la petición (POST)
    public class LoginRequest
    {
        public string usuarioOCorreo { get; set; }
        public string password { get; set; }
    }

    // El objeto "data" cuando es exitoso
    public class LoginData
    {
        public string usuario { get; set; }
        public string token { get; set; }
    }

    // La respuesta general de tu API
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}