namespace BlockChain.Headers {
    interface IExtensions {

        byte[] Header { get; }
        byte[] Footer { get; }
        string Extension { get; }
    }
}
