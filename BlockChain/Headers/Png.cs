namespace BlockChain.Headers {

    class Png : IExtensions {
        public byte[] Header => new byte[]{0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a};
        public byte[] Footer => null;
        public string Extension => "png";
    }
}
