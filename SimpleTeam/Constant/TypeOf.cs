namespace SimpleTeam.Constant
{
    class TypeOf
    {
        /**
         * Null
         */
        public const int NULL = 0x00;

        /**
         * Boolean
         */
        public const int BOOLEAN = 0x01;
        /**
         * Byte
         */
        public const int BYTE = 0x02;
        /**
         * Short
         */
        public const int SHORT = 0x03;
        /**
         * Char
         */
        public const int CHAR = 0x04;
        /**
         * Integer
         */
        public const int INTEGER = 0x05;
        /**
         * Long
         */
        public const int LONG = 0x06;
        /**
         * Float
         */
        public const int FLOAT = 0x07;
        /**
         * Double
         */
        public const int DOUBLE = 0x08;

        /**
         * Bytes
         */
        public const int BYTES = 0x10;

        /**
         * String
         */
        public const int STRING = 0x20;
        /**
         * File Reference
         */
        public const int FILE_REFERENCE = 0x21;

        /**
         * Object
         */
        public const int OBJECT = 0xF0;
        /**
         * Serializable
         */
        public const int SERIALIZABLE = 0xF1;
        /**
         * Externalizable
         */
        public const int EXTERNALIZABLE = 0xF2;
    }
}
