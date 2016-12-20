namespace BlockChain.Headers {

    class GZip : IExtensions {

        public byte[] Header => new byte[] { 0x1f, 0x8b, 0x08, 0x08 };
        public byte[] Footer => null;
        public string Extension => "gzip";
    }
}
