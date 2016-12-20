using BlockChain.WPF.Properties;
using GalaSoft.MvvmLight;

namespace BlockChain.WPF.ViewModels {

    public class SettingsViewModel : ViewModelBase {

        public string InputPath
        {
            get { return _inputPath; }
            set
            {
                Set(ref _inputPath, value);
                Settings.Default.InputPath = value;
                Settings.Default.Save();
            }
        }

        string _inputPath = Settings.Default.InputPath;

        public string OutputPath
        {
            get { return _outputPath; }
            set
            {
                Set(ref _outputPath, value);
                Settings.Default.OutputPath = value;
                Settings.Default.Save();
            }
        }

        string _outputPath = Settings.Default.OutputPath;
    }
}
