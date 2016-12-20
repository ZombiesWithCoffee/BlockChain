using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BlockChain.WPF.Properties;

namespace BlockChain.WPF.Services {
    public class KnownExtensions{

        public KnownExtensions(ObservableCollection<string> messages) {
            _messages = messages;
        }

        readonly ObservableCollection<string> _messages;

        public BlockContainer Blocks { get; } = new BlockContainer();

        public async Task Search(int start, int stop){

            _messages.Add($"{Environment.NewLine}");
            _messages.Add("Searching for Known Extensions");

            for (var blockNumber = start; blockNumber < stop; blockNumber++) {

                var fileName = Path.Combine(Settings.Default.InputPath, $"blk{blockNumber:D5}.dat");

                _messages.Add($"File: {Path.GetFileNameWithoutExtension(fileName)}");

                if (Path.GetFileName(fileName) == "blk00000.dat") {
                    continue;
                }

                Blocks.Clear();
                await Blocks.Add(fileName);

                foreach (var block in Blocks) {
                    foreach (var transaction in block.Transactions){

                        var fileData = Blocks.GetFile(transaction.ToString());

                        if (fileData == null){
                            continue;
                        }

                        if (fileData.Extension != ".dat"){
                            _messages.Add($"{fileData.Extension}: {transaction}");
                        }

              //          if (fileData.Extension == ".txt"){
              //              _messages.Add(Encoding.ASCII.GetString(fileData.Data));
              //          }
                    }
                }
            }
        }
    }
}
