using System;
using System.IO;
using System.Threading.Tasks;
using BlockChain.WPF.Messaging;
using BlockChain.WPF.Properties;

namespace BlockChain.WPF.Services {
    public class SatoshiUploads{

        public SatoshiUploads(MessageCollection messages){
            _messages = messages;
        }

        private readonly MessageCollection _messages;

        public BlockContainer Blocks { get; } = new BlockContainer();

        public async Task Search(int start, int stop) {

            _messages.Add($"{Environment.NewLine}");
            _messages.Add("Finding Transactions that used Shatoshi's Upload File Transaction");

            for (var blockNumber = start; blockNumber < stop; blockNumber++) {

                var fileName = Path.Combine(Settings.Default.InputPath, $"blk{blockNumber:D5}.dat");

                _messages.Add($"File: {Path.GetFileNameWithoutExtension(fileName)}", MessageType.Block);

                if (Path.GetFileName(fileName) == "blk00000.dat") {
                    continue;
                }

                Blocks.Clear();
                await Blocks.Add(fileName);

                foreach (var block in Blocks) {
                    foreach (var transaction in block.Transactions) {

                        if (transaction.GetSatoshiUploadedBytes() != null) {
                            _messages.Add($"{transaction}", MessageType.Transaction);
                        }
                    }
                }
            }
        }
    }
}
