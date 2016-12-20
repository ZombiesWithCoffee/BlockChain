using System.Windows;
using BlockChain.WPF.ViewModels;

namespace BlockChain.WPF.Dialogs {

    /// <summary>
    /// Interaction logic for PrintCardDialog.xaml
    /// </summary>
    
    public partial class SettingsDialog {
     
        public SettingsDialog() {
            InitializeComponent();

            ViewModel = new SettingsViewModel();
        }

        public SettingsViewModel ViewModel
        {
            get { return DataContext as SettingsViewModel; }
            set { DataContext = value; }
        }

        void OK_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }
    }
}
