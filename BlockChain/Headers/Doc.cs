namespace BlockChain.Headers {

    // public const string DocHeader = "d0cf11e0a1b11ae1";
    // public const string DocFooter = "576f72642e446f63756d656e742e";

    class Doc : IExtensions {
        public byte[] Header => new byte[]{ 0xd0, 0xcf, 0x11, 0xe0, 0xa1, 0xb1, 0x1e, 0xe1 };
        public byte[] Footer => new byte[] { 0x57, 0x6f, 0x72, 0x64, 0x2e, 0x44, 0x6f, 0x63, 0x75, 0x6d, 0x65, 0x6e, 0x74, 0x2e };
        public string Extension => "doc";
    }
}
