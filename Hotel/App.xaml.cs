using System.Windows;
using Unity;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            MainWindow window = container.Resolve<MainWindow>();
            window.Initialize();
            window.Show();
        }
    }
}

