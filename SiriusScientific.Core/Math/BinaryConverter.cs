using System;
using System.Collections;

namespace SiriusScientific.Core.Math
{
    public static class BinaryConverter
    {
        public static BitArray ToBinary(this int numeral)
        {
            return new BitArray(new[] { numeral });
        }

        public static int ToNumeral(this BitArray binary)
        {
            if (binary == null)
                throw new ArgumentNullException("binary");


            var result = new int[3];
            binary.CopyTo(result, 0);
            return result[0] + (result[1] << 32) + (result[2] << 64);
        }
    }
}
