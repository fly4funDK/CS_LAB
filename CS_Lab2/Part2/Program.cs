using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
{
    class Program
    {
        static void Main(string[] args)
        {
            uint dividend = 2341, divisor = 24;
            ulong result, remainder = 0;
            bool leftShift1 = false;

            result = dividend;
            for (int i = 0; i <= 32; i++)
            {
                result = result << 1;
                if (leftShift1)
                    result |= 1;
                remainder = result >> 32;
                leftShift1 = false;
                if (i == 32)
                {
                    Output(i, dividend, divisor, result);
                    remainder = remainder >> 1;
                    result &= 4294967295;
                    result |= remainder << 32;
                    Output(33, dividend, divisor, result);
                }
                else if (remainder >= divisor)
                {
                    remainder -= divisor;
                    result &= 4294967295;
                    result |= remainder << 32;
                    leftShift1 = true;
                }
                if (i != 32)
                    Output(i, dividend, divisor, result);
            }

            Console.ReadKey();
        }

        static void Output(int step, uint dvd, uint dvs, ulong res)
        {
            if (step == 33)
                Console.WriteLine(new string('*', 50) + $" RESULT " + new string('*', 50));
            else
                Console.WriteLine(new string('*', 50) + $" STEP {step} " + new string('*', 50));
            Console.WriteLine($"Dividend : { ToBinaryString(dvd, true), 66 }- {dvd}");
            Console.WriteLine($"Divisor  : { ToBinaryString(dvs, true), 66 }- {dvs}");
            Console.WriteLine($"Result   : { ToBinaryString(res, false) } - {res & 4294967295}, remainder {res >> 32}");
        }

        static string ToBinaryString(ulong value, bool flag)
        {
            string res = "";
            while (value != 1)
            {
                res = Convert.ToString(value % 2) + res;
                value /= 2;
            }
            res = "1" + res;
            if (flag)
                res = res.PadLeft(32, '0');
            else
                res = res.PadLeft(64, '0');
            res = res.Insert(32, " ");
            return res;
        }
    }
}
