namespace BlockChain 
{
    public class Hash : ByteArray {
        public Hash(byte[] buffer, ref int index, int bytes) : base(buffer, ref index, bytes, true) { }
        public Hash(byte[] hash) : base(hash, true) { }
    }
}
