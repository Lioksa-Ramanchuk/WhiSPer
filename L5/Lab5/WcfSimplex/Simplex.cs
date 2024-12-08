using WcfSimplex.Models;

namespace WcfSimplex
{
    public class Simplex : ISimplex
    {
        public int Add(int x, int y) => x + y;

        public string Concat(string s, double d) => s + d.ToString();

        public A Sum(A a1, A a2)
        {
            return new A
            {
                s = a1.s + a2.s,
                k = a1.k + a2.k,
                f = a1.f + a2.f,
            };
        }
    }
}
