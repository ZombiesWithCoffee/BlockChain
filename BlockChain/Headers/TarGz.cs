namespace BlockChain.Headers {

    class TarGz : IExtensions {

        public byte[] Header => new byte[] { 0x1f, 0x9d, 0x90, 0x70 };
        public byte[] Footer => null;
        public string Extension => "tar.gz";
    }
}
