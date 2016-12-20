using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using BlockChain.WPF.Properties;

namespace BlockChain.WPF.Services {

    public class SearchTransactionId{

        public SearchTransactionId(ObservableCollection<string> messages){
            _messages = messages;
        }

        private readonly ObservableCollection<string> _messages;

        public BlockContainer Blocks { get; } = new BlockContainer();

        public async Task Search(string txId, int start, int stop) {

            _messages.Add($"{Environment.NewLine}");
            _messages.Add("Finding Transactions that used Shatoshi's Upload File Transaction");

            for (var blockNumber = start; blockNumber < stop; blockNumber++) {

                var fileName = Path.Combine(Settings.Default.InputPath, $"blk{blockNumber:D5}.dat");

                _messages.Add($"File: {Path.GetFileNameWithoutExtension(fileName)}");

                if (Path.GetFileName(fileName) == "blk00000.dat") {
                    continue;
                }

                Blocks.Clear();
                await Blocks.Add(fileName);

                Blocks.Clear();
                await Blocks.Add(fileName);

                if (Blocks[txId] != null) {
                    _messages.Add($"Transaction {txId} found in file {fileName}");
                    break;
                }
            }
        }
    }
}
