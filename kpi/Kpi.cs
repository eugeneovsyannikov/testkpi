using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpi
{
    public class Kpi
    {
        public string Name { get; set; } = string.Empty;
        public int Plan { get; set; }
        public double Weight { get; set; }
        public int Spent { get; set; }
        public IAlgorithm? Algorithm { get; set; } = null;
    }
}
