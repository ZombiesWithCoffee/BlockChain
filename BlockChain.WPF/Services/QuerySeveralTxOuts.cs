using BlockChain.WPF.Messaging;

namespace BlockChain.WPF.Services {
    public class QuerySeveralTxOuts {

        public QuerySeveralTxOuts(BlockContainer block, MessageCollection messages){
            _block = block;
            _messages = messages;
        }

        readonly BlockContainer _block;
        readonly MessageCollection _messages;

        public void Execute(){

            _messages.NewLine();
            _messages.Add("Finding Transactions that have a high number of TxOut Transaction");

            foreach (var block in _block) {
                foreach (var transaction in block.Transactions) {

                    if (transaction.Outs.Unique < 30) {
                        continue;
                    }

                    if (transaction.Outs.SpentCount > 2) {
                        continue;
                    }

                    _messages.Add($"{transaction} - {transaction.Outs.Count}");
                }
            }
        }
    }
}
