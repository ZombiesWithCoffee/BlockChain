using System.Windows;
using BlockChain.WPF.ViewModels;

namespace BlockChain.WPF.Dialogs {

    /// <summary>
    /// Interaction logic for PrintCardDialog.xaml
    /// </summary>
    
    public partial class OpenTransactionsDialog {
     
        public OpenTransactionsDialog() {
            InitializeComponent();

            ViewModel = new OpenTransactionsViewModel();
        }

        public OpenTransactionsViewModel ViewModel{
            get { return DataContext as OpenTransactionsViewModel; }
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
