using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3
{
    class Program
    {
        static void Main(string[] args)
        {
            float value1 = 9.75F, value2 = 0.5625F;
            if (Math.Abs(value2) > Math.Abs(value1))
            {
                //value2 += value1;
                //value1 = value2 - value1;
                //value2 -= value1;
                (value1, value2) = (value2, value1);
            }

            byte[] byteArray1 = BitConverter.GetBytes(value1);
            uint x1 = BitConverter.ToUInt32(byteArray1, 0);
            byte[] byteArray2 = BitConverter.GetBytes(value2);
            uint x2 = BitConverter.ToUInt32(byteArray2, 0);

            bool x1sign = Convert.ToBoolean(x1 >> 31);
            bool x2sign = Convert.ToBoolean(x2 >> 31);
            bool x3sign;

            uint x1mant = (1u << 23) | (x1 & ((1u << 23) - 1));
            uint x2mant = (1u << 23) | (x2 & ((1u << 23) - 1));
            uint x3mant;

            uint x1E = (x1 >> 23) & ((1u << 8) - 1);
            uint x2E = (x2 >> 23) & ((1u << 8) - 1);
            uint x3E;

            Console.WriteLine(new string('*', 50) + $" INITIALIZATION " + new string('*', 50));
            Console.WriteLine("Value  Sign    Exponent           Mantiss");
            Output("x1", x1sign, x1E, x1mant);
            Output("x2", x2sign, x2E, x2mant);

            x3sign = x1sign | x2sign;
            x3E = x1E;
            x2mant = x2mant >> (int)(x1E - x2E);
            Console.WriteLine(new string('*', 46) + $" M2 AFTER NORMALIZATION " + new string('*', 46));
            Console.WriteLine("Value  Sign    Exponent           Mantiss");
            Output("x1", x1sign, x1E, x1mant);
            Output("x2", x2sign, x2E, x2mant);
            Output("x3", x3sign, x3E, 0);
            x3mant = x1mant + x2mant;

            if ((x3mant >> 23) > 1)
            {
                int counter = 0;
                while ((x3mant >> 23) > 1)
                {
                    x3mant = x3mant >> 1;
                    counter++;
                }
                for (int i = 0; i < counter; i++)
                    x3E += 1;
            }

            Console.WriteLine(new string('*', 54) + $" RESULT " + new string('*', 54));
            Console.WriteLine("Value  Sign    Exponent           Mantiss");
            Output("x1", x1sign, x1E, x1mant);
            Output("x2", x2sign, x2E, x2mant);
            Output("x3", x3sign, x3E, x3mant);

            uint x3 = (Convert.ToUInt32(x3sign) << 31) | (x3E << 23) | (x3mant & ((1u << 23) - 1));

            Console.WriteLine(new string('-', 116));
            Console.WriteLine("x1: " + value1 + "\n" + "x2: " + value2 + "\n" + "x3: " + BitConverter.ToSingle(BitConverter.GetBytes(x3), 0));

            Console.ReadKey();
        }

        static void Output(string label, bool sign, uint exp, uint mant)
        {
            Console.WriteLine(label + ":      " + Convert.ToInt32(sign) + "     " + Convert.ToString(exp, 2).PadLeft(8, '0') + "   " + Convert.ToString(mant & ((1u << 23) - 1), 2).PadLeft(23, '0'));
        }
    }
}
