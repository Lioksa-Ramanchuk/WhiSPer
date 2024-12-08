using System.ServiceModel;
using WcfSimplex.Models;

namespace WcfSimplex
{
    [ServiceContract]
    public interface ISimplex
    {
        [OperationContract]
        int Add(int x, int y);

        [OperationContract]
        string Concat(string s, double d);

        [OperationContract]
        A Sum(A a1, A a2);
    }
}
