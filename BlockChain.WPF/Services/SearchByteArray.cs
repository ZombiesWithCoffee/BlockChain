using System.IO;
using System.Threading.Tasks;
using BlockChain.Extensions;
using BlockChain.WPF.Messaging;
using BlockChain.WPF.Properties;

namespace BlockChain.WPF.Services {

    public class SearchByteArray{

        public SearchByteArray(MessageCollection messages){
            _messages = messages;
        }

        private readonly MessageCollection _messages;

        public BlockContainer Blocks { get; } = new BlockContainer();

        public async Task Search(string byteText, int start, int stop) {

            byteText = byteText.Trim(' ', '\r', '\n');

            _messages.AddHeading($"Searching For Byte Array {byteText}");

            var bytes = byteText.ToByteArray();

            for (var blockNumber = start; blockNumber <= stop; blockNumber++)
            {
                if (_messages.Cancel)
                    break;

                var fileName = Path.Combine(Settings.Default.InputPath, $"blk{blockNumber:D5}.dat");

                _messages.Add($"File: {Path.GetFileNameWithoutExtension(fileName)}", MessageType.Heading);

                if (Path.GetFileName(fileName) == "blk00000.dat") {
                    continue;
                }

                Blocks.ClearAll();
                await Blocks.Add(fileName);

                foreach (var transaction in Blocks.TransactionList){
                    SearchTransaction(transaction.Value, bytes);
                }
            }

            _messages.AddCompletion();
        }

        void SearchTransaction(Transaction transaction, byte[] bytes) {

            if (_messages.Cancel)
                return;

            var outputBytes = transaction.Outs.GetFileBytes();

            if (outputBytes.Search(bytes) > 0) {
                _messages.Add($"Byte Array found in Transaction Outs {transaction}");
            }

            var inputBytes = transaction.Ins.GetFileBytes();

            if (inputBytes.Search(bytes) > 0) {
                _messages.Add($"Byte Array found in Transaction Ins {transaction}");
            }
        }
    }
}
