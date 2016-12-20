using System.Collections.Generic;
using System.Linq;

namespace BlockChain {

    public class TransactionInContainer : List<TransactionIn>{
        public BitcoinValue Amount => BitcoinValue.FromSatoshis(this.Where(x => x.TxOut != null).Sum(o => o.TxOut.Value.Satoshis));
    }
}
