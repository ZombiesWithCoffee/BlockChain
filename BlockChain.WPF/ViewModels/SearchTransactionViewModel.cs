using BlockChain.WPF.Properties;
using GalaSoft.MvvmLight;

namespace BlockChain.WPF.ViewModels {

    public class SearchTransactionViewModel : OpenBlocksViewModel {

        public string Transaction
        {
            get { return _transaction; }
            set
            {
                Set(ref _transaction, value);

                Settings.Default.TxId = value;
                Settings.Default.Save();
            }
        }

        string _transaction = Settings.Default.TxId;
    }
}
