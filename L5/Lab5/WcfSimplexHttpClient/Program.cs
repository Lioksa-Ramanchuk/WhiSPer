using System;
using WcfSimplexHttpClient.WcfSimplexHttpReference;

namespace WcfSimplexHttpClient
{
    internal static class Program
    {
        static void Main()
        {
            using (var httpSimplex = new SimplexClient())
            {
                const int v1 = 2;
                const int v2 = 3;
                Console.WriteLine($"{v1} add {v2} = {httpSimplex.Add(v1, v2)}");

                const string s1 = "Lab #";
                const double v3 = 5.6;
                Console.WriteLine($"\"{s1}\" concat {v3:F2} = \"{httpSimplex.Concat(s1, v3)}\"");

                var a1 = new A
                {
                    s = "A1",
                    k = 1,
                    f = 1.1f,
                };
                var a2 = new A
                {
                    s = "A2",
                    k = 2,
                    f = 2.2f,
                };
                var a3 = httpSimplex.Sum(a1, a2);

                Console.WriteLine(
                    $"A{{s=\"{a1.s}\", k={a1.k}, f={a1.f.ToString("F2")}}} sum A{{s=\"{a2.s}\", k={a2.k}, f={a2.f.ToString("F2")}}} = A{{s=\"{a3.s}\", k={a3.k}, f={a3.f.ToString("F2")}}}"
                );

                Console.WriteLine();
                Console.WriteLine("Press Enter to terminate the client.");
                Console.ReadLine();
            }
        }
    }
}
