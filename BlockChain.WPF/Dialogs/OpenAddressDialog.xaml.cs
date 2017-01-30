using System.Windows;
using BlockChain.WPF.ViewModels;

namespace BlockChain.WPF.Dialogs {

    /// <summary>
    /// Interaction logic for PrintCardDialog.xaml
    /// </summary>
    
    public partial class OpenAddressDialog {
     
        public OpenAddressDialog() {
            InitializeComponent();

            ViewModel = new OpenAddressViewModel();
        }

        public OpenAddressViewModel ViewModel
        {
            get { return DataContext as OpenAddressViewModel; }
            set { DataContext = value; }
        }

        void Test_Click(object sender, RoutedEventArgs e){
            DialogResult = true;
        }

        void OK_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }
    }
}
