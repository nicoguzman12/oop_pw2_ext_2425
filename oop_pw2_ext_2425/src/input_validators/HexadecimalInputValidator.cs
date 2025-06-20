using System;

namespace oop_pw2_ext_2425
{
    public class HexadecimnalInputValidator : InputValidator
    {
        public override void validate(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                bool isDigit = c >= '0' && c <= '9';
                bool isUpperHex = c >= 'A' && c <= 'F';
                bool isLowerHex = c >= 'a' && c <= 'f';

                if (!isDigit && !isUpperHex && !isLowerHex)
                {
                    throw new FormatException("Bad format: input is not a valid hexadecimal number.");
                }
            }
        }
    }
}