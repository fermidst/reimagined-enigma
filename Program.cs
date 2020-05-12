using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDM
{
    internal class Program
    {
        private static async Task Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("введите любой вектор булевой функции (8 чисел)");
            var funcVector = Console.ReadLine();

            var columns = (int) (Math.Log(funcVector.Length) / Math.Log(2));
            var rows = (int) Math.Pow(2, columns);

            var truthTable = new string[rows][];
            for (var i = 0; i < rows; i++) truthTable[i] = new string[columns];

            var functionValues = funcVector.Select(c => c.ToString()).ToArray();
            Evaluation.GetTruthTable(columns, rows, truthTable);

            Console.WriteLine("номер в группе");
            var numberInGroup = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("последняя цифра в группе");
            var groupLastDigit = Convert.ToInt32(Console.ReadLine());

            var n1 = Convert.ToString(numberInGroup + groupLastDigit, 2);
            var n2 = Convert.ToString(256 - (numberInGroup + groupLastDigit), 2);
            if (n1.Length < 8 || n2.Length < 8)
            {
                for (var i = 8 - n1.Length; i > 0; i--) n1 = "0" + n1;

                for (var i = 8 - n2.Length; i > 0; i--) n2 = "0" + n2;
            }

            var n1FunctionValues = n1.Select(c => c.ToString()).ToArray();
            var n2FunctionValues = n2.Select(c => c.ToString()).ToArray();

            // todo: add string builder
            var builder = new StringBuilder();
            builder.Append(
                $"N1 ({numberInGroup}+{groupLastDigit}) {n1} PDNF: {Evaluation.GetPdnf(n1FunctionValues, truthTable, columns)}\n");
            builder.Append(
                $"N1 ({numberInGroup}+{groupLastDigit}) {n1} PCNF: {Evaluation.GetPcnf(n1FunctionValues, truthTable, columns)}\n");
            builder.Append(
                $"N2 (256-({numberInGroup}+{groupLastDigit})) {n2} PDNF: {Evaluation.GetPdnf(n2FunctionValues, truthTable, columns)}\n");
            builder.Append(
                $"N2 (256-({numberInGroup}+{groupLastDigit})) {n2} PCNF: {Evaluation.GetPcnf(n2FunctionValues, truthTable, columns)}\n");
            builder.Append($"\n{funcVector}\n");
            builder.Append($"PDNF: {Evaluation.GetPdnf(functionValues, truthTable, columns)}\n");
            builder.Append($"PCNF: {Evaluation.GetPcnf(functionValues, truthTable, columns)}{Environment.NewLine}");

            var output = builder.ToString();
            Console.Write(output);

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory ?? throw new InvalidOperationException();

            await File.WriteAllTextAsync(Path.Combine(baseDirectory, "output.txt"), output,
                Encoding.UTF8);
        }
    }
}