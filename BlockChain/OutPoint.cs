using BlockChain.Extensions;

namespace BlockChain
{
    public class OutPoint {

        public Hash TransactionHash;
        public uint N;

        public OutPoint(byte[] buffer, ref int index){
            TransactionHash = buffer.ToHash(ref index, 32);
            N = buffer.ToUInt32(ref index);
        }

        public override string ToString(){
            if (N == 0xFFFFFFFF)
                return "(none)";

            return $"{N}";
        }
    }
}