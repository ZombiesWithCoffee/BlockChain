using System;
using System.Collections.Generic;
using BlockChain.Extensions;

namespace BlockChain
{
    public class Block{

        public Hash BlockHash;

        public int Height = -1; // this is set afterward parsing because the .dat files ordering != Height ordering
        public int ByteLength;
        public uint Version { get; }
        public Hash PrevBlockHash { get; }
        public Hash MerkleRoot { get; }
        public DateTime Timestamp { get; }
        public uint Bits { get; }
        public uint Nonce { get; }
        public List<Transaction> Transactions { get; }

        public Block(byte[] buffer){

            var index = 0;
            BlockHash = buffer.ToSha256_2(ref index, 80);
            Version = buffer.ToUInt32(ref index);
            PrevBlockHash = buffer.ToHash(ref index, 32);
            MerkleRoot = buffer.ToHash(ref index, 32);
            Timestamp = buffer.ToDateTime(ref index);
            Bits = buffer.ToUInt32(ref index);
            Nonce = buffer.ToUInt32(ref index);

            var transactionCount = buffer.ToVariableInt(ref index);

            Transactions = new List<Transaction>();

            for (var counter = 0ul; counter < transactionCount; counter++){
                var transaction = new Transaction(buffer, ref index);
                Transactions.Add(transaction);
            }

            if (index != buffer.Length)
                throw new ApplicationException("parse error");
        }
    }
}
