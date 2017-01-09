using System;
using System.Threading.Tasks;
using BlockChain.WPF.Messaging;
using Info.Blockchain.API;

namespace BlockChain.WPF.Services {

    public class WalkTransaction{

        public WalkTransaction(MessageCollection messages){
            _messages = messages;
            _api = new BlockchainApiHelper();
        }

        readonly MessageCollection _messages;
        private readonly BlockchainApiHelper _api;

        public async Task Search(string txId){

            txId = txId.Trim(' ', '\r', '\n');

            _messages.NewLine();
            _messages.Add("Walking down Transactions", MessageType.Heading);
            _messages.Cancel = false;

            try{
                await SearchTransaction(txId);
            }
            catch (Exception ex){
                _messages.Add(ex.Message, MessageType.Error);
            }

            if (_messages.Cancel){
                _messages.Add("Search Canceled", MessageType.Error);
                _messages.Cancel = false;
            }
            else{
                _messages.Add("Search Complete", MessageType.Heading);
            }
        }

        async Task SearchTransaction(string txId){

            if (_messages.Cancel){
                return;
            }

            var transaction = await _api.BlockExpolorer.GetTransactionAsync(txId);

            _messages.Add($"{transaction.Hash}");

            var largest = transaction.Outputs[0];

            foreach (var txOut in transaction.Outputs)
            {
                if (largest.Value.Bits < txOut.Value.Bits){
                    largest = txOut;
                }
            }

            var address = await _api.BlockExpolorer.GetAddressAsync(largest.Address);

            foreach (var txIn in address.Transactions){
                if (txIn.Hash != transaction.Hash){
                    await SearchTransaction(txIn.Hash);
                }
            }
        }
    }
}
