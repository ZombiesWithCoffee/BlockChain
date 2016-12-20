using System.Windows.Data;
using BlockChain.WPF.Properties;

namespace BlockChain.WPF.Extensions {
    public class SettingBindingExtension : Binding {

        public SettingBindingExtension() {
            Initialize();
        }

        public SettingBindingExtension(string path) : base(path) {
            Initialize();
        }

        void Initialize() {
            Source = Settings.Default;
            Mode = BindingMode.TwoWay;
        }
    }
}
