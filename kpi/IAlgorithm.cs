using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpi
{
    public interface IAlgorithm
    {
        public double Calculate(int spent, int plan);
    }
}
