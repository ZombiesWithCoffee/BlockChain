using BlockChain.WPF.Properties;
using GalaSoft.MvvmLight;

namespace BlockChain.WPF.ViewModels {

    public class OpenBlocksViewModel : ViewModelBase {

        public int Start
        {
            get { return _start; }
            set
            {
                Set(ref _start, value);
                Settings.Default.StartBlock = value;
                Settings.Default.Save();
            }
        }

        int _start = Settings.Default.StartBlock;

        public int Stop
        {
            get { return _stop; }
            set
            {
                Set(ref _stop, value);
                Settings.Default.StopBlock = value;
                Settings.Default.Save();
            }
        }

        int _stop = Settings.Default.StopBlock;
    }
}
