using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    class Program
    {
        static void Main(string[] args)
        {
            uint multiplicand = 25, multiplier = 138;
            ulong product = 0, highbits = 0; //B-multc, Q - multr
            string multpr;

            Output(0, multiplicand, multiplier, product);

            for (int i = 0; i < 32; i++)
            {
                multpr = Convert.ToString(multiplier, 2);
                if (multpr[multpr.Length - 1] == '1')
                {
                    highbits = product >> 32;
                    highbits += multiplicand;
                    product = product >> 1;
                    product &= 2147483647;
                    product |= highbits << 31;
                }
                else
                    product = product >> 1;
                multiplier = multiplier >> 1;
                Output(i + 1, multiplicand, multiplier, product);
            }

            Output(33, multiplicand, multiplier, product);
            Console.ReadKey();
        }

        static void Output(int step, uint multc, uint multr, ulong prod)
        {
            if (step == 33)
                Console.WriteLine(new string('*', 50) + $" RESULT " + new string('*', 50));
            else
                Console.WriteLine(new string('*', 50) + $" STEP {step} " + new string('*', 50));
            Console.WriteLine($"Multiplicand: { ToBinaryString(multc, true), 66 }- {multc}");
            Console.WriteLine($"Multiplier  : { ToBinaryString(multr, true), 66 }- {multr}");
            Console.WriteLine($"Product     : { ToBinaryString(prod, false), 64 } - {prod}");
        }

        static string ToBinaryString(ulong value, bool flag)
        {
            string res = "";
            if (value != 0)
            {
                while (value != 1)
                {
                    res = Convert.ToString(value % 2) + res;
                    value /= 2;
                }
                res = "1" + res;
            }
            if (flag)
                res = res.PadLeft(32, '0');
            else
                res = res.PadLeft(64, '0');
            res = res.Insert(32, " ");
            return res;
        }
    }
}
