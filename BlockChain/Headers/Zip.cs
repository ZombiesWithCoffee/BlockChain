namespace BlockChain.Headers {

    class Zip : IExtensions {

        public byte[] Header => new byte[]{ 0x50, 0x4b, 0x03, 0x04, 0x14 };
        public byte[] Footer => new byte[] { 0x50, 0x4b, 0x05, 0x06, 0x00 };
        public string Extension => "zip";
    }
}
