using System;
using System.Globalization;
using BlockChain.Extensions;

namespace BlockChain
{
    public struct BitcoinValue : IComparable, IComparable<BitcoinValue>, IEquatable<BitcoinValue> {
        public const int SatoshisPerBitcoin  = 100000000;

        public static readonly BitcoinValue Zero = new BitcoinValue();

        public BitcoinValue(decimal btc) {
            Btc = btc;
        }

        public decimal Btc { get; set; }

        public long Satoshis     => (long)(Btc * SatoshisPerBitcoin);

        public static BitcoinValue FromSatoshis(long satoshis) => new BitcoinValue((decimal)satoshis / SatoshisPerBitcoin);

        public static BitcoinValue Read(byte[] buffer, ref int index){
            return FromSatoshis(buffer.ToInt64(ref index));
        }

        public static BitcoinValue operator +(BitcoinValue x, BitcoinValue y) {
            return new BitcoinValue(x.Btc + y.Btc);
        }
        public static BitcoinValue operator -(BitcoinValue x, BitcoinValue y) {
            return new BitcoinValue(x.Btc - y.Btc);
        }

        public static bool operator >(BitcoinValue x, BitcoinValue y) {
            return x.Btc > y.Btc;
        }

        public static bool operator <(BitcoinValue x, BitcoinValue y) {
            return x.Btc < y.Btc;
        }

        public static bool operator ==(BitcoinValue x, BitcoinValue y) {
            return x.Btc == y.Btc;
        }

        public static bool operator !=(BitcoinValue x, BitcoinValue y) {
            return x.Btc != y.Btc;
        }

        public static BitcoinValue operator /(BitcoinValue x, int y) {
            return new BitcoinValue(x.Btc / y);
        }

        public bool Equals(BitcoinValue other) {
            return Btc == other.Btc;
        }

        public override bool Equals(object obj) {
            if(obj is BitcoinValue)
                return this.Equals((BitcoinValue)obj);

            return false;
        }
        public override int GetHashCode() {
            return Btc.GetHashCode();
        }

        public int CompareTo(object obj) {
            return CompareTo((BitcoinValue)obj);
        }

        public int CompareTo(BitcoinValue other) {
            return Btc.CompareTo(other.Btc);
        }

        public override string ToString() {
            return $"{Btc:###0.000000} btc";
        }
    }

}
