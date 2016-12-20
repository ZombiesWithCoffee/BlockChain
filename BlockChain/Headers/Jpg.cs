namespace BlockChain.Headers {

    class Jpg : IExtensions {
        public byte[] Header => new byte[]{ 0xff, 0xd8, 0xff, 0xe0, 0x00, 0x10, 0x4a, 0x46, 0x49, 0x46, 0x00, 0x01, 0x01 };
        public byte[] Footer => null;
        public string Extension => "jpg";
    }
}
