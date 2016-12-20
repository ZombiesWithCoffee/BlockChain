namespace BlockChain.Headers {

    class Pdf2 : IExtensions {
        public byte[] Header => new byte[] {0x26, 0x23, 0x32, 0x30, 0x35 };
        public byte[] Footer => new byte[] { 0x25, 0x25, 0x45, 0x4f, 0x46 };
        public string Extension => "pdf";
    }
}
