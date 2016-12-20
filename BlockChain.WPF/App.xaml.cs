using System.Windows;
using BlockChain.WPF.Properties;

namespace BlockChain.WPF {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {

        void Application_Exit(object sender, ExitEventArgs e) {
            Settings.Default.Save();
        }

        public MainWindow StartWindow
        {
            get { return Current?.MainWindow as MainWindow; }
            set { Current.MainWindow = value; }
        }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            StartWindow = new MainWindow();
            StartWindow.Show();
        }
    }
}
