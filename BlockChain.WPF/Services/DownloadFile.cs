using System.IO;
using BlockChain.WPF.Messaging;
using BlockChain.WPF.Properties;

namespace BlockChain.WPF.Services {
    public class DownloadFile{

        public DownloadFile(BlockContainer block, MessageCollection messages){
            _block = block;
            _messages = messages;
        }

        readonly BlockContainer _block;
        readonly MessageCollection _messages;

        public void Download(string[] txIds) {

            var fileData = _block.GetFile(txIds);
            var fileName = Path.Combine(Settings.Default.OutputPath, Path.ChangeExtension(txIds[0], fileData.Extension));

            File.Delete(fileName);
            File.WriteAllBytes(fileName, fileData.Data);

            _messages.Add($"File saved to {fileName}");
        }
    }
}
