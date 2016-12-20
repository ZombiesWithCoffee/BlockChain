using System;
using System.IO;
using BlockChain.Extensions;

namespace BlockChain 
{
    public abstract class ByteArray : IComparable, IComparable<ByteArray>, IEquatable<ByteArray> {

        public byte[] Raw { get; }

        protected ByteArray(byte[] raw, bool isLittleEndian = false) {
            Raw = raw;

            if(isLittleEndian)
                Array.Reverse(Raw);
        }

        protected ByteArray(byte[] buffer, ref int index, int bytes, bool isLittleEndian = false) {
            Raw = new byte[bytes];

            if (isLittleEndian){
                for (var i = 0; i < bytes; i++)
                    Raw[i] = buffer[index + bytes - i - 1];
            }
            else{
                Buffer.BlockCopy(buffer, index, Raw, 0, bytes);
            }

            index += bytes;
        }

        public byte[] ToInnerBytes(){

            if (Raw.Length < 8)
                return Raw;

            if (Raw.IsOpDupCheckSig()){
                return ToOpDupCheckBytes();
            }

            if (Raw.IsOpHashEqual()) {
                return ToOpHashBytes();
            }

            if (Raw.IsOp1() || Raw.IsOp1V2()) {
                return ToOp1Bytes();
            }

            if (Raw.IsOp2()){
                return ToOp2Bytes();
            }

            if (Raw.IsOp3()){
                return ToOp3Bytes();
            }

            if (Raw.IsOp3V2()) {
                return ToOp3Bytes();
            }

            // TODO: Find out why it goes down this far
            return Raw;
//            throw new InvalidDataException();
        }

        byte[] ToOpDupCheckBytes(){

            var data = new byte[Raw.Length - 5];

            Buffer.BlockCopy(Raw, 3, data, 0, Raw.Length - 5);

            return data;
        }

        byte[] ToOpHashBytes() {

            var data = new byte[Raw.Length - 3];

            Buffer.BlockCopy(Raw, 2, data, 0, Raw.Length - 3);

            return data;
        }

        byte[] ToOp1Bytes() {

            var data = new byte[Raw.Length - 4];

            Buffer.BlockCopy(Raw, 2, data, 0, Raw.Length - 4);

            return data;
        }

        byte[] ToOp2Bytes(){

            var data = new byte[Raw.Length - 5];

            Buffer.BlockCopy(Raw,  2, data,  0, 65);
            Buffer.BlockCopy(Raw, 68, data, 65, Raw.Length - 70);

            return data;
        }

        byte[] ToOp3Bytes(){

            var data = new byte[Raw.Length - 6];

            Buffer.BlockCopy(Raw,   2, data,   0, 65);
            Buffer.BlockCopy(Raw,  68, data,  65, 65);
            Buffer.BlockCopy(Raw, 134, data, 130, Raw.Length - 136);

            return data;
        }

        public string OpString{
            get{
                if (Raw.IsOpDupCheckSig()){
                    return "OP_DUP OP_HASH160 " + ToInnerBytes().ToHex() + " OP_EQUALVERIFY OP_CHECKSIG";
                }

                if (Raw.IsOpHashEqual()){
                    return "<" + ToInnerBytes().ToHex() + ">";
                }

                if (Raw.IsOp1() || Raw.IsOp1V2()){
                    return "OP_1 " + ToOp1Bytes().ToHex() + " OP_1 OP_CHECKMULTISIG";
                }

                if (Raw.IsOp2()){
                    return "OP_1 " + ToOp2Bytes().ToHex() + " OP_2 OP_CHECKMULTISIG";
                }

                if (Raw.IsOp3()){
                    return "OP_1 " + ToOp3Bytes().ToHex() + " OP_3 OP_CHECKMULTISIG";
                }

                return "OP_??? " + Raw.ToHex();
            }
        }

        public override string ToString() {
            return Raw.ToHex();
        }

        public bool Equals(ByteArray other) {
            return CompareTo(other) == 0;
        }

        public override bool Equals(object obj) {
            var value = obj as ByteArray;

            if(value != null)
                return Equals(value);

            return false;
        }

        public override int GetHashCode() {
            return Raw.GetHashCode();
        }

        public int CompareTo(object obj) {
            return CompareTo((ByteArray)obj);
        }

        public int CompareTo(ByteArray other) {
            var diff = Raw.Length - other.Raw.Length;

            if(diff != 0)
                return diff;

            for(var i = 0; i < Raw.Length; i++) {
                diff = Raw[i] - other.Raw[i];
                if(diff != 0)
                    return diff;
            }

            return 0;
        }
    }

}
