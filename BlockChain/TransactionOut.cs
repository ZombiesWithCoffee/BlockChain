using System.Security.Cryptography;
using BlockChain.Extensions;

namespace BlockChain
{
    public class TransactionOut {

        public BitcoinValue Value { get; }
        public PublicKeyScript Script { get; }
        public int N { get; set; }
        public TransactionIn TxIn { get; set; }

        public TransactionOut(byte[] buffer, ref int index, int count){
            Value = buffer.ToBitcoin(ref index);
            Script = buffer.ToPublicKeyScript(ref index);
            N = count;
        }

        public override string ToString() {
            return $"TxOut: {Value}";
        }
    }
}