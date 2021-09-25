using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpi
{
    public class BinaryAlgorithm : IAlgorithm
    {
        public double Calculate(int spent, int plan) => spent >= plan ? 1.0 : 0.0;
    }
}
