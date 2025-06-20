using System;

namespace pw2.conversions
{
    /// <summary>
    /// Clase estática que actúa como fachada para todas las conversiones.
    /// </summary>
    public static class ConversionHelper
    {
        public static string DecimalToBinary(string input)
        {
            var conv = new DecimalToBinary(
                name: "Decimal → Binario",
                definition: "Convierte un número decimal a su representación binaria"
            );
            return conv.Change(input);
        }

        public static string DecimalToTwoComplement(string input, int bits = 8)
        {
            var conv = new DecimalToTwoComplement(
                name: "Decimal → Complemento a 2",
                definition: "Convierte un número decimal a su representación en complemento a dos"
            );
            return conv.Change(input, bits);
        }

        public static string DecimalToOctal(string input)
        {
            var conv = new DecimalToOctal(
                name: "Decimal → Octal",
                definition: "Convierte un número decimal a su representación octal"
            );
            return conv.Change(input);
        }

        public static string DecimalToHexadecimal(string input)
        {
            var conv = new DecimalToHexadecimal(
                name: "Decimal → Hexadecimal",
                definition: "Convierte un número decimal a su representación hexadecimal"
            );
            return conv.Change(input);
        }

        public static string BinaryToDecimal(string input)
        {
            var conv = new BinaryToDecimal(
                name: "Binario → Decimal",
                definition: "Convierte un número binario a su representación decimal"
            );
            return conv.Change(input);
        }

        public static string OctalToDecimal(string input)
        {
            var conv = new OctalToDecimal(
                name: "Octal → Decimal",
                definition: "Convierte un número octal a su representación decimal"
            );
            return conv.Change(input);
        }

        public static string HexadecimalToDecimal(string input)
        {
            var conv = new HexadecimalToDecimal(
                name: "Hexadecimal → Decimal",
                definition: "Convierte un número hexadecimal a su representación decimal"
            );
            return conv.Change(input);
        }

        public static string TwoComplementToDecimal(string input, int bits = 8)
        {
            var conv = new TwoComplementToDecimal(
                name: "Complemento a 2 → Decimal",
                definition: "Convierte un número en complemento a dos a decimal"
            );
            return conv.Change(input, bits);
        }
    }
}
