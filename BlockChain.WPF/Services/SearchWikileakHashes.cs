using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlockChain.WPF.Data;
using BlockChain.WPF.Messaging;
using BlockChain.WPF.Properties;

namespace BlockChain.WPF.Services {

    public class SearchWikileakHashes {

        public SearchWikileakHashes(MessageCollection messages){
            _messages = messages;
        }

        readonly MessageCollection _messages;

        public BlockContainer Blocks { get; } = new BlockContainer();

        public async Task Search(int start, int stop) {

            _messages.NewLine();
            _messages.Add("Finding Wikileak Hashes stored in the BlockChain", MessageType.Heading);

            for (var blockNumber = start; blockNumber <= stop; blockNumber++) {

                var fileName = Path.Combine(Settings.Default.InputPath, $"blk{blockNumber:D5}.dat");

                _messages.Add($"File: {Path.GetFileNameWithoutExtension(fileName)}", MessageType.Heading);

                if (Path.GetFileName(fileName) == "blk00000.dat") {
                    continue;
                }

                Blocks.ClearAll();
                await Blocks.Add(fileName);
                SearchTransaction();
            }

            _messages.Add("Search Complete", MessageType.Heading);
        }

        void SearchTransaction(){

            foreach (var block in Blocks){

                foreach (var transaction in block.Transactions){

                    if (transaction.ToString() == "bdb67f3b003e2c3d06d6b8d314ca7b937f9ae7de20ed34baccaedcac62e6f414"){
                        int j = 0;
                    }

                    foreach (var txOut in transaction.Outs){

                        foreach (var hash in Wikileaks.Hashes){

                            if (!string.IsNullOrEmpty(hash.Transaction))
                                continue;

                            if (!txOut.Script.Inner.SequenceEqual(hash.RipeMd160))
                                continue;

                            _messages.Add($"Wikileaks Hash Found: {hash.Description}", MessageType.Error);
                            _messages.Add($"Transaction: {transaction}", MessageType.Transaction);

                            hash.Transaction = transaction.ToString();
                        }
                    }
                }
            }
        }
    }
}
