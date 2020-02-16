using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CS_Lab1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            //string pathfile = @"D:\C#\ComputerSystems\CS_Lab1.1\text1.txt";
            //string pathfile = @"D:\C#\ComputerSystems\CS_Lab1.1\text2.txt";
            //string pathfile = @"D:\C#\ComputerSystems\CS_Lab1.1\text3.txt";
            //string pathfile = @"D:\C#\ComputerSystems\CS_Lab1.1\text1.txtbase64.txt";
            //string pathfile = @"D:\C#\ComputerSystems\CS_Lab1.1\text2.txtbase64.txt";
            string pathfile = @"D:\C#\ComputerSystems\CS_Lab1.1\text3.txtbase64.txt";

            Dictionary<char, int> symbols = new Dictionary<char, int>();
            int numberOfSymbols = 0;
            double H = 0;
            double countInform = 0;

            using (StreamReader reader = new StreamReader(pathfile))
            {
                foreach (char c in reader.ReadToEnd())
                {
                    bool isPresent = false;
                    foreach (char chr in symbols.Keys)
                    {
                        if (chr == c)
                        {
                            isPresent = true;
                            break;
                        }
                    }

                    if (isPresent)
                        symbols[c] += 1;
                    else
                        symbols[c] = 1;

                    numberOfSymbols += 1;
                }
            }

            foreach (int value in symbols.Values)
            {
                double probOfOccurrence = (double)value / numberOfSymbols;
                H -= probOfOccurrence * Math.Log(probOfOccurrence, 2);
            }

            countInform = H * numberOfSymbols;

            foreach (KeyValuePair<char, int> keyValue in symbols)
            {
                if (keyValue.Key == '\r')
                    Console.WriteLine(@"Symbol '\r' - " + keyValue.Value + " times, p = {0:F6}.", (double)keyValue.Value / numberOfSymbols);
                else if (keyValue.Key == '\n')
                    Console.WriteLine(@"Symbol '\n' - " + keyValue.Value + " times, p = {0:F6}.", (double)keyValue.Value / numberOfSymbols);
                else
                    Console.WriteLine(@"Symbol '" + keyValue.Key + "' - " + keyValue.Value + " times, p = {0:F6}.", (double)keyValue.Value / numberOfSymbols);
            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Number of characters - " + numberOfSymbols);
            Console.WriteLine("Alphabet`s entropy - {0:F4}", H);
            Console.WriteLine("Information content - {0:F2} bytes.", countInform / 8);
            Console.ReadKey();
        }
    }
}
