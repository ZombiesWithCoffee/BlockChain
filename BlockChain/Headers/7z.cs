namespace BlockChain.Headers {

    class Z7 : IExtensions {

        public byte[] Header => new byte[]{ 0x37, 0x7a, 0xbc, 0xaf, 0x27, 0x1c };
        public byte[] Footer => new byte[] { 0x00, 0x00, 0x00, 0x17, 0x06 };
        public string Extension => "7z";
    }
}
