using BlockChain.WPF.Properties;
using GalaSoft.MvvmLight;

namespace BlockChain.WPF.ViewModels {

    public class OpenAddressViewModel : ViewModelBase {

        public string Address
        {
            get { return _address; }
            set
            {
                Set(ref _address, value);

                Settings.Default.Address = value;
                Settings.Default.Save();
            }
        }

        string _address = Settings.Default.Address;
    }
}
