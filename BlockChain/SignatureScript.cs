namespace BlockChain
{
    public class SignatureScript : ByteArray {
        public SignatureScript(byte[] buffer, ref int index, int size) : base(buffer, ref index, size) { }
    }
}