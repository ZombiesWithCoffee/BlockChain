using System.IO;
using BlockChain.WPF.Properties;

namespace BlockChain.WPF.Services {
    public class DownloadFile{

        public DownloadFile(BlockContainer block){
            _block = block;
        }

        readonly BlockContainer _block;

        public string Download(string[] txIds) {

            var fileData = _block.GetFile(txIds);
            var fileName = Path.Combine(Settings.Default.OutputPath, Path.ChangeExtension(txIds[0], fileData.Extension));

            File.Delete(fileName);
            File.WriteAllBytes(fileName, fileData.Data);

            return fileName;
        }
    }
}
