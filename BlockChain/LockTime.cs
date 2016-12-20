using System;
using System.Globalization;
using BlockChain.Extensions;

namespace BlockChain
{
    public struct LockTime {

        public LockTime(byte[] buffer, ref int index, bool isLockTimeIrrelevant){
            _value = buffer.ToUInt32(ref index);
            IsIrrelevant = isLockTimeIrrelevant;
        }
    
        readonly uint _value;
        public bool IsIrrelevant;

        // If all TxIn inputs have final (0xffffffff) sequence numbers then lock_time is irrelevant. Otherwise, the transaction may not be added to a block until after lock_time (see NLockTime).

        public bool ContainsHiddenData => IsIrrelevant && _value != 0;

        public uint GetRaw() {
            return _value;
        }

        public bool IsLocked => _value != 0;

        /// <summary>
        ///     Returns -1 if it doesnt unlock at a block.
        /// </summary>

        public int UnlocksAtBlock {
            get {
                if(_value < 500000000)
                    return (int)_value;
                return -1;
            }
        }

        /// <summary>
        ///     Returns the date at which it unlocks.
        ///     Returns MinValue if not applicable.
        /// </summary>

        public DateTime UnlocksAt {
            get {
                if(_value >= 500000000)
                    return Helper.ReadUnixTimestamp(_value);

                return DateTime.MinValue;
            }
        }

        public override string ToString() {
            if(ContainsHiddenData)
                return $"{{HIDDEN DATA FOUND: (uint32) 0x{_value.ToString("x2", CultureInfo.InvariantCulture)}}}";

            if(IsIrrelevant)
                return "{irrelevant}";

            if(!IsLocked)
                return "{not locked}";

            if(UnlocksAtBlock >= 0)
                return $"{{unlocks at block {UnlocksAtBlock.ToString(CultureInfo.InvariantCulture)}}}";

            return $"{{unlocks at {UnlocksAt}}}";
        }
    }
}