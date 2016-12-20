using System.IO;
using BlockChain.WPF.Properties;

namespace BlockChain.WPF.Services {
    public class DownloadSatoshi{

        public DownloadSatoshi(BlockContainer block){
            _block = block;
        }

        readonly BlockContainer _block;

        public string Download(string txId) {

            var fileData = _block.GetSatoshiUploadedFile(txId);

            if (fileData == null) {
                throw new InvalidDataException("The transaction ID {txId} was not found");
            }

            // Now check for headers

            var fileName = Path.Combine(Settings.Default.OutputPath, Path.ChangeExtension(txId, fileData.Extension));

            File.Delete(fileName);
            File.WriteAllBytes(fileName, fileData.Data);

            return fileName;
        }
    }
}
