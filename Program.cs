using System;
using System.Linq;

namespace ConsoleAppDM
{
    internal class Program
    {
        private static void Main()
        {
            var funcVector = Console.ReadLine();

            var columns = (int) (Math.Log(funcVector.Length) / Math.Log(2));
            var rows = (int) Math.Pow(2, columns);

            var truthTable = new string[rows][];
            for (var i = 0; i < rows; i++) truthTable[i] = new string[columns];

            var functionValues = funcVector.Select(c => c.ToString()).ToArray();
            Evaluation.GetTruthTable(columns, rows, truthTable);
            Console.WriteLine($"PDNF: {Evaluation.GetPDNF(functionValues, truthTable, columns)}");
            Console.WriteLine($"PCNF: {Evaluation.GetPCNF(functionValues, truthTable, columns)}{Environment.NewLine}");

            const int numberInGroup = 3;
            const int groupLastDigit = 5;
            var n1 = Convert.ToString(numberInGroup + groupLastDigit, 2);
            var n2 = Convert.ToString(256 - (numberInGroup + groupLastDigit), 2);
            if (n1.Length < 8 || n2.Length < 8)
            {
                for (var i = 8 - n1.Length; i > 0; i--) n1 = "0" + n1;

                for (var i = 8 - n2.Length; i > 0; i--) n2 = "0" + n2;
            }

            var n1FunctionValues = n1.Select(c => c.ToString()).ToArray();
            var n2FunctionValues = n2.Select(c => c.ToString()).ToArray();

            Console.WriteLine(
                $"N1 ({numberInGroup}+{groupLastDigit}) {n1} PDNF: {Evaluation.GetPDNF(n1FunctionValues, truthTable, columns)}");
            Console.WriteLine(
                $"N1 ({numberInGroup}+{groupLastDigit}) {n1} PCNF: {Evaluation.GetPCNF(n1FunctionValues, truthTable, columns)}");
            Console.WriteLine(
                $"N2 (256-({numberInGroup}+{groupLastDigit})) {n2} PDNF: {Evaluation.GetPDNF(n2FunctionValues, truthTable, columns)}");
            Console.WriteLine(
                $"N2 (256-({numberInGroup}+{groupLastDigit})) {n2} PCNF: {Evaluation.GetPCNF(n2FunctionValues, truthTable, columns)}");
        }
    }
}