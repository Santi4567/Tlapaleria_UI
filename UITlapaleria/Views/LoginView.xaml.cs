using System.Windows;
using UITlapaleria.ViewModels;

namespace UITlapaleria.Views
{
    public partial class LoginView : Window
    {
        private LoginViewModel _viewModel;

        public LoginView()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            this.DataContext = _viewModel; // Conectamos la vista al estado
        }

        // Lógica para mostrar/ocultar contraseña
        private void btnShowPass_Click(object sender, RoutedEventArgs e)
        {
            if (btnShowPass.IsChecked == true)
            {
                // Mostrar texto plano
                txtPasswordVisible.Text = txtPasswordHidden.Password;
                txtPasswordVisible.Visibility = Visibility.Visible;
                txtPasswordHidden.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Ocultar texto plano
                txtPasswordHidden.Password = txtPasswordVisible.Text;
                txtPasswordHidden.Visibility = Visibility.Visible;
                txtPasswordVisible.Visibility = Visibility.Collapsed;
            }
        }

        // Lógica del botón Ingresar
        private async void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            // Sincronizar por si estaba visible u oculta
            string passwordActual = btnShowPass.IsChecked == true ? txtPasswordVisible.Text : txtPasswordHidden.Password;

            // Llamamos al ViewModel
            bool exito = await _viewModel.IniciarSesionAsync(passwordActual);

            if (exito)
            {
                MessageBox.Show("¡Login exitoso!", "Bienvenido", MessageBoxButton.OK, MessageBoxImage.Information);

                // Aquí haremos la transición a la pantalla de carga.
                // LoadingView loading = new LoadingView();
                // loading.Show();
                // this.Close();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}