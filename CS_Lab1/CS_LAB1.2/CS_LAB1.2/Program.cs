using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CS_LAB1._2
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] base64Table = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O',
                                  'P','Q','R','S','T','U','V','W','X','Y','Z','a','b','c','d',
                                  'e','f','g','h','i','j','k','l','m','n','o','p','q','r','s',
                                  't','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
                                  '8','9','+','/','=' };

            //string filePath = @"D:\C#\ComputerSystems\CS_Lab1.1\text1.txt";
            //string filePath = @"D:\C#\ComputerSystems\CS_Lab1.1\text2.txt";
            //string filePath = @"D:\C#\ComputerSystems\CS_Lab1.1\text3.txt";
            //string filePath = @"D:\C#\ComputerSystems\CS_Lab1.1\text1.txt.bz2";
            //string filePath = @"D:\C#\ComputerSystems\CS_Lab1.1\text2.txt.bz2";
            string filePath = @"D:\C#\ComputerSystems\CS_Lab1.1\text3.txt.bz2";

            byte[] bytes = File.ReadAllBytes(filePath);

            int countGroupOfThreeBytes = bytes.Length / 3;
            int countSymbolEqual = bytes.Length % 3;
            string res = "";

            byte symbolA;
            byte symbolB;
            byte symbolC;
            byte sixBits;
            byte partOfSixBits;

            for (int i = 0; i < countGroupOfThreeBytes * 3; i += 3)
            {
                symbolA = bytes[i];
                symbolB = bytes[i + 1];
                symbolC = bytes[i + 2];

                sixBits = symbolA;
                res += base64Table[sixBits >> 2];

                partOfSixBits = Convert.ToByte((int)sixBits & 3);
                sixBits = symbolB;
                res += base64Table[(int)partOfSixBits << 4 | (int)sixBits >> 4];

                partOfSixBits = Convert.ToByte((int)sixBits & 15);
                sixBits = symbolC;
                res += base64Table[(int)partOfSixBits << 2 | (int)sixBits >> 6];

                partOfSixBits = Convert.ToByte((int)sixBits & 63);
                res += base64Table[(int)partOfSixBits];
            }

            if (countSymbolEqual == 1)
            {
                symbolA = bytes[bytes.Length - 1];
                sixBits = symbolA;
                res += base64Table[sixBits >> 2];

                partOfSixBits = Convert.ToByte((int)sixBits & 3);
                res += base64Table[(int)partOfSixBits << 4];

                res += base64Table[64];
                res += base64Table[64];
            }            
            else if (countSymbolEqual == 2)
            {
                symbolA = bytes[bytes.Length - 2];
                symbolB = bytes[bytes.Length - 1];

                sixBits = symbolA;
                res += base64Table[sixBits >> 2];

                partOfSixBits = Convert.ToByte((int)sixBits & 3);
                sixBits = symbolB;
                res += base64Table[(int)partOfSixBits << 4 | (int)sixBits >> 4];

                partOfSixBits = Convert.ToByte((int)sixBits & 15);
                res += base64Table[(int)partOfSixBits << 2];

                res += base64Table[64];
            }

            string newFilePath = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(filePath) + "base64.txt";
           
            using (StreamWriter sw = new StreamWriter(newFilePath, false, Encoding.Default))
            {
                sw.WriteLine(res);
            }

            Console.WriteLine("Succefully encoded!");
            Console.ReadKey();
        }
    }
}
