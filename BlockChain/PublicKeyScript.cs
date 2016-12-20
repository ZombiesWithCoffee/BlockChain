using System.Collections.Generic;
using BlockChain.Extensions;

namespace BlockChain
{
    public class PublicKeyScript : ByteArray {

        public PublicKeyScript(byte[] buffer) : base(buffer){
            
        }

        public PublicKeyScript(byte[] buffer, ref int index, int bytes) : base(buffer, ref index, bytes){
            
        }

        public string Address {
            get {
                var byteArray = new List<byte>();

                if (Raw.IsOpDupCheckSig()){
                    byteArray.Add(0x00);
                }
                else if (Raw.IsOpHashEqual()){
                    byteArray.Add(0x05);
                }
                else{
                    return string.Empty;
                }

                byteArray.AddRange(ToInnerBytes());
                byteArray.AddRange(Base58Encoding.GetCheckSum(byteArray.ToArray()));

                return Base58Encoding.Encode(byteArray.ToArray());
            }
        }
    }
}