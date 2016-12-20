using System.Text;
using BlockChain.Headers;

namespace BlockChain {

    public class FileData {

        public FileData(byte[] data){
            Data = data;
        }

        public byte[] Data { get; }

        static readonly ExtensionFinder ExtensionFinder = new ExtensionFinder();

        public string Extension {
            get
            {
                if (Data.Length < 10)
                    return ".dat";

                return ExtensionFinder.Find(Data);
            }
        }

        public string Text => Encoding.ASCII.GetString(Data);
    }
}
