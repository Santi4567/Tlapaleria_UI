using System.Threading.Tasks;
using UITlapaleria.Models;
using UITlapaleria.Services;

namespace UITlapaleria.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly AuthService _authService;

        private string _usuarioText;
        public string UsuarioText
        {
            get => _usuarioText;
            set => SetProperty(ref _usuarioText, value);
        }

        private string _mensajeError;
        public string MensajeError
        {
            get => _mensajeError;
            set => SetProperty(ref _mensajeError, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public LoginViewModel()
        {
            _authService = new AuthService();
        }

        // Método que llamaremos desde la vista
        public async Task<bool> IniciarSesionAsync(string password)
        {
            MensajeError = string.Empty;

            if (string.IsNullOrWhiteSpace(UsuarioText) || string.IsNullOrWhiteSpace(password))
            {
                MensajeError = "Por favor ingresa usuario y contraseña.";
                return false;
            }

            IsLoading = true; // Aquí podrías mostrar un spinner en el botón si quisieras

            ApiResponse<LoginData> respuesta = await _authService.LoginAsync(UsuarioText, password);

            IsLoading = false;

            if (respuesta != null && respuesta.success)
            {
                // ¡Éxito! Guardamos el token de forma global (puedes guardarlo en las Propiedades de la App)
                App.Current.Properties["AuthToken"] = respuesta.data.token;
                App.Current.Properties["UsuarioLogueado"] = respuesta.data.usuario;

                return true;
            }
            else
            {
                // Mostramos el mensaje de error del backend
                MensajeError = respuesta?.message ?? "Error desconocido.";
                return false;
            }
        }
    }
}