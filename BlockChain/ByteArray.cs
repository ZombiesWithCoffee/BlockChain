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

        public byte[] Inner{

            get{
                if (Raw.Length < 8)
                    return Raw;

                if (Raw.IsOpDupCheckSig()){
                    return Raw.ToOpDupCheckBytes();
                }

                if (Raw.IsOpHashEqual()){
                    return Raw.ToOpHashBytes();
                }

                if (Raw.IsOp1() || Raw.IsOp1V2()){
                    return Raw.ToOp1Bytes();
                }

                if (Raw.IsOp2()){
                    return Raw.ToOp2Bytes();
                }

                if (Raw.IsOp3()){
                    return Raw.ToOp3Bytes();
                }

                if (Raw.IsOp3V2()){
                    return Raw.ToOp3Bytes();
                }

                if (Raw.IsOpCheckSig()){
                    return Raw.ToOpCheckSigBytes();
                }

                // TODO: Find out why it goes down this far
                return Raw;
            }
//            throw new InvalidDataException();
        }

        public string OpString{
            get{
                if (Raw.IsOpDupCheckSig()){
                    return "OP_DUP OP_HASH160 " + Inner.ToHex() + " OP_EQUALVERIFY OP_CHECKSIG";
                }

                if (Raw.IsOpHashEqual()){
                    return "<" + Inner.ToHex() + ">";
                }

                if (Raw.IsOp1() || Raw.IsOp1V2()){
                    return "OP_1 " + Raw.ToOp1Bytes().ToHex() + " OP_1 OP_CHECKMULTISIG";
                }

                if (Raw.IsOp2()){
                    return "OP_1 " + Raw.ToOp2Bytes().ToHex() + " OP_2 OP_CHECKMULTISIG";
                }

                if (Raw.IsOp3()){
                    return "OP_1 " + Raw.ToOp3Bytes().ToHex() + " OP_3 OP_CHECKMULTISIG";
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
