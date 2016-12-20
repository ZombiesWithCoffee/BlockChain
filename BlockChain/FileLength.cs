using System;

namespace BlockChain {
    public class FileLength{

        // 204E00005cFcCF40

        public bool LengthTest(byte[] content){

            // The length and CDC are contained in the headers

            var length = BitConverter.ToInt32(content, 0);
            var cdc = BitConverter.ToInt32(content, 4);

            return false;
        }

    }
}

