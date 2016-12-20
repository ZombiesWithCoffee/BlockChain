namespace BlockChain.Headers {

    class Gif : IExtensions {

        public byte[] Header => new byte[]{0x47, 0x49, 0x46, 0x38, 0x39, 0x61};
        public byte[] Footer => new byte[] {0x21, 0x00, 0x00, 0x3b, 0x00};
        public string Extension => "gif";
    }
}
