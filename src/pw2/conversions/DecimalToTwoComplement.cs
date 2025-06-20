using System;

namespace pw2.conversions
{
    public class DecimalToTwoComplement : Conversion
    {
        private int size = 16;

        public DecimalToTwoComplement(string name, string definition) : base(name, definition, new DecimalInputValidator(), true)
        {

        }

        public void SetSize(int size) 
        {
            this.size = size;
        }

        public override string Change(string input) 
        {
            int number = Int32.Parse(input);

            int minVal = -(1 << (this.size - 1));
            int maxVal = (1 << (this.size - 1)) - 1;
            
            if (number < minVal || number > maxVal)
                throw new ArgumentOutOfRangeException(nameof(input), $"Number must fit within {this.size} bits.");

            // 1. Converts the decimal (signed) number into an unsigned integer,
            //    removing any negative sign and making it non-negative.
            // 2. Shifts the binary value 1 left by size bits, effectively 
            //    creating a number that represents 2^size.
            // 3. Subtracts 1 from the shifted value, creating a mask with all bits
            //    set to 1 up to the size bit, and 0 beyond that.

            uint unsignedValue = (uint) number & ((1u << this.size) - 1);

            // 1. Converts the unsigned integer value into its binary string representation (base 2).
            // 2. Pads the resulting binary string to the left with 0 characters, ensuring
            //    that the string has exactly size bits.

            string binaryString = Convert.ToString(unsignedValue, 2).PadLeft(this.size, '0'); 

            return binaryString.PadLeft(this.size, '0');
        }

        public override string Change(string input, int bits) 
        {
            int number = Int32.Parse(input);

            int minVal = -(1 << (bits - 1));
            int maxVal = (1 << (bits - 1)) - 1;
            
            if (number < minVal || number > maxVal)
                throw new ArgumentOutOfRangeException(nameof(input), $"Number must fit within {bits} bits.");

            // 1. Converts the decimal (signed) number into an unsigned integer,
            //    removing any negative sign and making it non-negative.
            // 2. Shifts the binary value 1 left by size bits, effectively 
            //    creating a number that represents 2^size.
            // 3. Subtracts 1 from the shifted value, creating a mask with all bits
            //    set to 1 up to the size bit, and 0 beyond that.

            uint unsignedValue = (uint) number & ((1u << bits) - 1);

            // 1. Converts the unsigned integer value into its binary string representation (base 2).
            // 2. Pads the resulting binary string to the left with 0 characters, ensuring
            //    that the string has exactly size bits.

            string binaryString = Convert.ToString(unsignedValue, 2).PadLeft(bits, '0'); 

            return binaryString.PadLeft(bits, '0');
        }        
    }
}