using BlockChain.WPF.Messaging;

namespace BlockChain.WPF.Services {
    public class QueryTextMessages{

        public QueryTextMessages(BlockContainer block, MessageCollection messages){
            _block = block;
            _messages = messages;
        }

        readonly BlockContainer _block;
        readonly MessageCollection _messages;

        public void Execute(){

            _messages.NewLine();
            _messages.Add("Searching for text messages");

            foreach (var block in _block) {
                foreach (var transaction in block.Transactions) {

                    var fileData = _block.GetFile(transaction.ToString());

                    if (fileData == null) {
                        continue;
                    }

                    if (fileData.Extension == ".txt") {
                        _messages.Add($"{transaction} - {fileData.Text}");
                    }
                }
            }
        }
    }
}
