namespace BlockChain.Enums {
    public static class Op{
        public const byte OP_SEPARATOR = 0x041;
        public const byte OP_1 = 0x051;
        public const byte OP_2 = 0x052;
        public const byte OP_3 = 0x053;
        public const byte OP_DUP = 0x76;
        public const byte OP_EQUAL = 0x87;
        public const byte OP_HASH160 = 0xa9;
        public const byte OP_CHECKSIG = 0xac;
        public const byte OP_CHECKMULTISIG = 0xae;
    }
}
