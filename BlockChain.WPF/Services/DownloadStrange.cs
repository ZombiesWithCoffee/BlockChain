using System;
using System.IO;
using System.Threading.Tasks;
using BlockChain.WPF.Messaging;
using BlockChain.WPF.Properties;
using Info.Blockchain.API;

namespace BlockChain.WPF.Services {

    public class DownloadStrange{

        public DownloadStrange(BlockContainer block, MessageCollection messages)
        {
            _messages = messages;
            _block = block;
        }

        readonly BlockContainer _block;
        readonly MessageCollection _messages;

        public void Download(string txId){

            txId = txId.Trim(' ', '\r', '\n');

            _messages.AddHeading("Combining all strange transactions");

            var fileData = _block.DownloadStrangeFile(txId);

            if (fileData == null)
            {
                throw new InvalidDataException($"The transaction ID {txId} was not found");
            }

            // Now check for headers

            var fileName = Path.Combine(Settings.Default.OutputPath, Path.ChangeExtension(txId, fileData.Extension));

            File.Delete(fileName);
            File.WriteAllBytes(fileName, fileData.Data);

            _messages.Add($"File saved to {fileName}");

            _messages.AddCompletion();
        }
    }
}
