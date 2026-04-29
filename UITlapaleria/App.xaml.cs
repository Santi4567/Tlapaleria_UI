using System.Configuration;
using System.Data;
using System.Windows;
using dotenv.net;

namespace UITlapaleria
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Este método se ejecuta automáticamente antes de abrir cualquier ventana
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 2. Cargamos el archivo .env con el método de dotenv.net
            DotEnv.Load();
        }
    }

}
