using System;

namespace ConsoleAppDM
{
    public static class Evaluation
    {
        private static string[] Symbols { get; } =
        {
            "x",
            "y",
            "z",
            "!x",
            "!y",
            "!z"
        };

        public static void GetTruthTable(int columns, int rows, string[][] array)
        {
            for (var i = 0; i < columns; i++)
            for (var j = 0; j < rows; j++)
                if ((j + 1) % (int) Math.Pow(2, columns - i) <= (int) Math.Pow(2, columns - i) / 2 &&
                    (j + 1) % (int) Math.Pow(2, columns - i) != 0)
                    array[j][i] = "0";
                else
                    array[j][i] = "1";
        }

        public static string GetPDNF(string[] funcValues, string[][] truthTable, int columns)
        {
            var result = string.Empty;
            for (var i = 0; i < funcValues.Length; i++)
            for (var j = 0; funcValues[i].Equals("1") && j < columns; j++)
            {
                result += truthTable[i][j].Equals("0") ? Symbols[j + Symbols.Length / 2] : Symbols[j];
                if (j == columns - 1) result += " | ";
            }

            return string.IsNullOrEmpty(result) ? result : result.Substring(0, result.Length - 3);
        }

        public static string GetPCNF(string[] funcValues, string[][] truthTable, int columns)
        {
            var result = string.Empty;
            var k = 1;
            for (var i = 0; i < funcValues.Length; i++)
            {
                for (var j = 0; funcValues[i].Equals("0") && j < columns; j++)
                {
                    if (k == 1)
                    {
                        result += "(";
                        k = 0;
                    }

                    result += truthTable[i][j].Equals("1") ? Symbols[j + Symbols.Length / 2] : Symbols[j];
                    if (j == columns - 1)
                        result += ") & ";
                    else
                        result += " | ";
                }

                k = 1;
            }

            return string.IsNullOrEmpty(result) ? result : result.Substring(0, result.Length - 3);
        }

        public static void WriteArray(int columns, int rows, string[][] truthTable, string functionVector)
        {
            for (var i = 0; i < columns; i++) Console.Write(Symbols[i] + " ");

            Console.WriteLine("f");
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++) Console.Write(truthTable[i][j] + " ");

                Console.WriteLine(functionVector[i]);
            }
        }
    }
}