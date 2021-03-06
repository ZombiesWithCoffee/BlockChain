﻿using BlockChain.WPF.Messaging;

namespace BlockChain.WPF.Services {
    public class WalkAddress{

        public WalkAddress(BlockContainer block, MessageCollection messages){
            _block = block;
            _messages = messages;
        }

        readonly BlockContainer _block;
        readonly MessageCollection _messages;

        public void Execute(string txId) {

            txId = txId.Trim('\r', '\n');

            _messages.NewLine();
            _messages.Add("Walking Transaction");

            var addresses = _block.WalkAddresses(txId);

            if (addresses == null){
                _messages.Add("Transaction not found");
                return;
            }

            foreach (var result in addresses){
                _messages.Add($"{result}");
            }
        }
    }
}
