using System;
using System.Collections.Generic;

namespace pw2.conversions
{
    public class Converter
    {
        private List<Conversion> operations;

        public Converter()
        {
            operations = new List<Conversion>
            {
                new DecimalToBinary("Binary", "Decimal to Binary"),
                new DecimalToOctal("Octal", "Decimal to Octal"),
                new DecimalToHexadecimal("Hexadecimal", "Decimal to Hexadecimal"),
                new DecimalToTwoComplement("TwoComplement", "Decimal to Binary (TwoComplement)"),
                new BinaryToDecimal("Decimal", "Binary to Decimal"),
                new TwoComplementToDecimal("Decimal", "Binary (TwoComplement) to Decimal"),
                new OctalToDecimal("Octal", "Octal to Decimal"),
                new HexadecimalToDecimal("Hexadecimal", "Hexadecimal to Decimal")
            };
        }

        public int Exit()
        {
            return operations.Count + 1;
        }

        public int GetNumberOperations()
        {
            return operations.Count;
        }

        public int PrintOperations()
        {
            Console.WriteLine("---------------------------------");
            for (int i = 0; i < operations.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {operations[i].GetDefinition()}");
            }
            Console.WriteLine($" {Exit()}. Exit");
            Console.WriteLine("---------------------------------");

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int option) || option < 1 || option > Exit())
            {
                return Exit();
            }
            return option;
        }

        public string PerformConversion(int op, string input)
        {
            operations[op - 1].Validate(input);

            if (operations[op - 1].NeedBitSize())
            {
                Console.Write("Enter number of bits: ");
                string bitsInput = Console.ReadLine();
                if (!int.TryParse(bitsInput, out int bits))
                {
                    bits = 8; // default bits if invalid input
                }
                return operations[op - 1].Change(input, bits);
            }
            return operations[op - 1].Change(input);
        }

        public void PrintConversion(int op, string input, string output)
        {
            operations[op - 1].PrintConversion(input, output);
        }
    }
}
