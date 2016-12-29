using System;
using BlockChain.Enums;

namespace BlockChain.Extensions {
    static class AddressExtensions {

        public static byte[] ToInnerBytes(this byte[] array)
        {
            if (array.Length < 8)
                return array;

            if (array.IsOpDupCheckSig()) {
                return array.ToOpDupCheckBytes();
            }

            if (array.IsOpHashEqual()) {
                return array.ToOpHashBytes();
            }

            if (array.IsOp1() || array.IsOp1V2()) {
                return array.ToOp1Bytes();
            }

            if (array.IsOp2()) {
                return array.ToOp2Bytes();
            }

            if (array.IsOp3()) {
                return array.ToOp3Bytes();
            }

            if (array.IsOp3V2()) {
                return array.ToOp3Bytes();
            }

            if (array.IsOpCheckSig()) {
                return array.ToOpCheckSigBytes();
            }

            // TODO: Find out why it goes down this far
            return array;
        }
            //            throw new InvalidDataException();

        public static byte[] ToOpDupCheckBytes(this byte[] array) {

            var data = new byte[array.Length - 5];

            Buffer.BlockCopy(array, 3, data, 0, array.Length - 5);

            return data;
        }

        public static byte[] ToOpCheckSigBytes(this byte[] array) {
            var data = new byte[array.Length - 1];

            Buffer.BlockCopy(array, 0, data, 0, array.Length - 1);

            return data;
        }

        public static byte[] ToOpHashBytes(this byte[] array) {

            var data = new byte[array.Length - 3];

            Buffer.BlockCopy(array, 2, data, 0, array.Length - 3);

            return data;
        }

        public static byte[] ToOp1Bytes(this byte[] array) {

            var data = new byte[array.Length - 4];

            Buffer.BlockCopy(array, 2, data, 0, array.Length - 4);

            return data;
        }

        public static byte[] ToOp2Bytes(this byte[] array) {

            var data = new byte[array.Length - 5];

            Buffer.BlockCopy(array, 2, data, 0, 65);
            Buffer.BlockCopy(array, 68, data, 65, array.Length - 70);

            return data;
        }

        public static byte[] ToOp3Bytes(this byte[] array) {

            var data = new byte[array.Length - 6];

            Buffer.BlockCopy(array, 2, data, 0, 65);
            Buffer.BlockCopy(array, 68, data, 65, 65);
            Buffer.BlockCopy(array, 134, data, 130, array.Length - 136);

            return data;
        }

        // OP_HASH160 f80ffc734c5723bf5caa012a0835fa4f8d5e5155 OP_EQUAL 

        public static bool IsOpHashEqual(this byte[] array) {
            return array[0] == Op.OP_HASH160
                && array[1] == 0x14
                && array[array.Length - 1] == 0x87;
        }

        // OP_DUP OP_HASH160 27a1f12771de5cc3b73941664b2537c15316be43 OP_EQUALVERIFY OP_CHECKSIG 

        internal static bool IsOpHashEqualCheckSig(this byte[] array) {
            return array[0] == 0xae
                && array[1] == 0x5c
                && array[array.Length - 2] == 0x16
                && array[array.Length - 1] == 0x04;
        }

        // 0427f6457716e57352b3d071c451cc4f2c56704344029c0e87e6417bcd47dbe8f172a4e1169b0b138051993d2e2d685bba1382f6c5e9f50b8d9cdb367a1b549cbb OP_CHECKSIG 

        public static bool IsOpCheckSig(this byte[] array) {
            return array[array.Length - 1] == Op.OP_CHECKSIG;
        }

        // OP_DUP OP_HASH160 ac42e0f55f4f26cb4d350add34ae8f15aa95382b OP_EQUALVERIFY OP_CHECKSIG

        public static bool IsOpDupCheckSig(this byte[] array) {
            return array[0] == Op.OP_DUP
                && array[1] == Op.OP_HASH160
                && array[2] == 0x14
                && array[array.Length - 2] == 0x88
                && array[array.Length - 1] == Op.OP_CHECKSIG;
        }

        public static bool IsOp1(this byte[] array) {
            return array[0] == Op.OP_1
                && array[1] == Op.OP_SEPARATOR
                && array[array.Length - 2] == Op.OP_1
                && array[array.Length - 1] == Op.OP_CHECKMULTISIG;
        }

        public static bool IsOp1V2(this byte[] array) {
            return array[0] == Op.OP_1
                && array[1] == 0x21
                && array[array.Length - 2] == Op.OP_1
                && array[array.Length - 1] == Op.OP_CHECKMULTISIG;
        }

        // OP_1 41a6736b31faa19d88f30f8ebfe8cc8a24236c02bd7151038816c814a1b706f30bcb91028432f69a2ffcb83553666946555b4d5f5cb316444e51499ce8c77e287b fd45efd28550ed5aa782687be240387c85148609cc3dc556d88a023d3d24ce2d8d048c0d0db99b4360cb1565d24706df207573d5a1000000000000000000000000 OP_2 OP_CHECKMULTISIG

        public static bool IsOp2(this byte[] array) {
            return array[0] == Op.OP_1
                && array[1] == Op.OP_SEPARATOR
                && array[array.Length - 2] == Op.OP_2
                && array[array.Length - 1] == Op.OP_CHECKMULTISIG
                && array[67] == Op.OP_SEPARATOR;
        }

        // OP_1 1c2200007353455857696b696c65616b73204361626c6567617465204261636b75700a0a6361626c65676174652d3230313031323034313831312e377a0a0a446f 776e6c6f61642074686520666f6c6c6f77696e67207472616e73616374696f6e732077697468205361746f736869204e616b616d6f746f277320646f776e6c6f61 6420746f6f6c2077686963680a63616e20626520666f756e6420696e207472616e73616374696f6e20366335336364393837313139656637393764356164636364 OP_3 OP_CHECKMULTISIG

        internal static bool IsOp3(this byte[] array) {
            return array[0] == Op.OP_1
                && array[1] == Op.OP_SEPARATOR
                && array[array.Length - 2] == Op.OP_3
                && array[array.Length - 1] == Op.OP_CHECKMULTISIG
                && array[67] == Op.OP_SEPARATOR
                && array[133] == Op.OP_SEPARATOR;
        }

        // Not sure why, but a record came with a 0x21 instead of a 0x41 as it's second separator.  Maybe that it was a variable length?

        internal static bool IsOp3V2(this byte[] array) {
            return array[0] == Op.OP_1
                && array[1] == Op.OP_SEPARATOR
                && array[array.Length - 2] == Op.OP_3
                && array[array.Length - 1] == Op.OP_CHECKMULTISIG
                && array[67] == Op.OP_SEPARATOR
                && array[133] == 0x21;
        }


    }
}
