namespace pw2.conversions
{
    public static class conversionHelper
    {
        public static string DecimalToBinary(string input)
        {
            return new DecimalToBinary("", "").Change(input);
        }

        public static string DecimalToTwoComplement(string input)
        {
            return new DecimalToTwoComplement("", "").Change(input);
        }

        public static string DecimalToOctal(string input)
        {
            return new DecimalToOctal("", "").Change(input);
        }

        public static string DecimalToHexadecimal(string input)
        {
            return new DecimalToHexadecimal("", "").Change(input);
        }

        public static string BinaryToDecimal(string input)
        {
            return new BinaryToDecimal("", "").Change(input);
        }

        public static string TwoComplementToDecimal(string input)
        {
            return new TwoComplementToDecimal("", "").Change(input);
        }

        public static string OctalToDecimal(string input)
        {
            return new OctalToDecimal("", "").Change(input);
        }

        public static string HexadecimalToDecimal(string input)
        {
            return new HexadecimalToDecimal("", "").Change(input);
        }
    }
}
