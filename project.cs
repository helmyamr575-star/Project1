using System;
using System.Collections.Generic;
using System.Linq;

class Task
{
    static void Main()
    {
        double[] numbers = { 115, 182, 191, 31, 196, 1099, 5, 172, 10, 179, 83, 21, 20, 21, 186, 177, 195, 193, 188, 199, 62, 109, 105, 183, 110 };
        
        Array.Sort(numbers);
        int n = numbers.Length;

        double sum = numbers.Sum();
        double mean = sum / n;
        double range = numbers.Max() - numbers.Min();

        var common = numbers.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;

        double totalVariance = numbers.Select(x => Math.Pow(x - mean, 2)).Sum();
        double variance = totalVariance / n;
        double standardDev = Math.Sqrt(variance);

        double p20 = Calculate(numbers, 20);
        double p50 = Calculate(numbers, 50);
        double q1 = Calculate(numbers, 25);
        double q2 = Calculate(numbers, 50);
        double q3 = Calculate(numbers, 75);
        double iqr = q3 - q1;

        double diffSum = numbers.Select(x => x - mean).Sum();

        Console.WriteLine($"(i)   {mean:F2}");
        Console.WriteLine($"(ii)  {common}");
        Console.WriteLine($"(iii) {q2}");
        Console.WriteLine($"(iv)  {variance:F2}");
        Console.WriteLine($"(v)   {p20}");
        Console.WriteLine($"(vi)  {p50}");
        Console.WriteLine($"(vii) {q1}");
        Console.WriteLine($"(viii){q2}");
        Console.WriteLine($"(ix)  {q3}");
        Console.WriteLine($"(x)   {range}");
        Console.WriteLine($"(xi)  {iqr}");
        Console.WriteLine($"(xii) {standardDev:F2}");
        Console.WriteLine($"(xiii){diffSum:F10}");

        double low = q1 - (1.5 * iqr);
        double high = q3 + (1.5 * iqr);

        foreach (var num in numbers)
        {
            string result = (num < low || num > high) ? "Outlier" : "Normal";
            Console.WriteLine($"{num} -> {result}");
        }
    }

    static double Calculate(double[] sortedData, double p)
    {
        double pos = (p / 100) * (sortedData.Length - 1);
        int idx = (int)pos;
        double frac = pos - idx;
        if (idx + 1 < sortedData.Length)
            return sortedData[idx] + (frac * (sortedData[idx + 1] - sortedData[idx]));
        return sortedData[idx];
    }
}