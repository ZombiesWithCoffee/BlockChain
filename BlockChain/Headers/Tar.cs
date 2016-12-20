namespace BlockChain.Headers {

    class Tar : IExtensions {

        public byte[] Header => new byte[] { 0x1f, 0x8b, 0x08, 0x00 };
        public byte[] Footer => null;
        public string Extension => "tar";
    }
}
