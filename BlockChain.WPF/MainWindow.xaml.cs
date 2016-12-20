using System.Text;
using System.Windows;
using System.Windows.Controls;
using BlockChain.WPF.ViewModels;

namespace BlockChain.WPF{

    public partial class MainWindow{

        public MainWindow(){
            InitializeComponent();

            ViewModel = new MainWindowViewModel();
        }

        public MainWindowViewModel ViewModel
        {
            get { return DataContext as MainWindowViewModel; }
            set { DataContext = value; }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e){

            var listView = sender as ListView;
            var text = new StringBuilder();

            foreach (var item in listView.SelectedItems){
                text.AppendLine(item.ToString());
            }

            Clipboard.SetText(text.ToString());
        }
    }
}
