using System;
using System.Linq;
using System.Security.Cryptography;
using System.Numerics;
using System.Text;

namespace BlockChain{

    public class ArrayHelpers{
        public static T[] ConcatArrays<T>(params T[][] arrays){
            var result = new T[arrays.Sum(arr => arr.Length)];
            var offset = 0;
            for (var i = 0; i < arrays.Length; i++){
                var arr = arrays[i];
                Buffer.BlockCopy(arr, 0, result, offset, arr.Length);
                offset += arr.Length;
            }
            return result;
        }

        public static T[] ConcatArrays<T>(T[] arr1, T[] arr2){
            var result = new T[arr1.Length + arr2.Length];
            Buffer.BlockCopy(arr1, 0, result, 0, arr1.Length);
            Buffer.BlockCopy(arr2, 0, result, arr1.Length, arr2.Length);
            return result;
        }

        public static T[] SubArray<T>(T[] arr, int start, int length){
            var result = new T[length];
            Buffer.BlockCopy(arr, start, result, 0, length);
            return result;
        }

        public static T[] SubArray<T>(T[] arr, int start){
            return SubArray(arr, start, arr.Length - start);
        }
    }

    public static class Base58Encoding{
        public const int CheckSumSizeInBytes = 4;

        public static byte[] AddCheckSum(byte[] data){
            var checkSum = GetCheckSum(data);
            var dataWithCheckSum = ArrayHelpers.ConcatArrays(data, checkSum);
            return dataWithCheckSum;
        }

        //Returns null if the checksum is invalid
        public static byte[] VerifyAndRemoveCheckSum(byte[] data){
            var result = ArrayHelpers.SubArray(data, 0, data.Length - CheckSumSizeInBytes);
            var givenCheckSum = ArrayHelpers.SubArray(data, data.Length - CheckSumSizeInBytes);
            var correctCheckSum = GetCheckSum(result);

            if (givenCheckSum.SequenceEqual(correctCheckSum))
                return result;
            else
                return null;
        }

        private const string Digits = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        public static string Encode(byte[] data) {
            
            BigInteger intData = 0;

            foreach (var @byte in data){
                intData = intData * 256 + @byte;
            }

            var result = new StringBuilder();

            while (intData > 0) {
                var remainder = (int)(intData % 58);
                intData /= 58;
                result.Insert(0, Digits[remainder]);
            }

            // Append '1' for each leading 0 byte

            for (var i = 0; i < data.Length && data[i] == 0; i++) {
                result.Insert(0, "1");
            }

            return result.ToString();
        }

        public static string EncodeWithCheckSum(byte[] data){
            return Encode(AddCheckSum(data));
        }

        public static byte[] GetCheckSum(byte[] data){
            SHA256 sha256 = new SHA256Managed();
            var hash1 = sha256.ComputeHash(data);
            byte[] hash2 = sha256.ComputeHash(hash1);

            var result = new byte[CheckSumSizeInBytes];
            Buffer.BlockCopy(hash2, 0, result, 0, result.Length);

            return result;
        }
        
    }
}