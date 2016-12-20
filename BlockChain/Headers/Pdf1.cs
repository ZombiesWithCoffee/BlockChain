namespace BlockChain.Headers {

    class Pdf1 : IExtensions {
        public byte[] Header => new byte[]{ 0x25, 0x50, 0x44, 0x46 };
        public byte[] Footer => new byte[] { 0x25, 0x25, 0x45, 0x4f, 0x46 };
        public string Extension => "pdf";
    }
}
