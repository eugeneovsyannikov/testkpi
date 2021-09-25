using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpi
{
    public class StairsAlgorithm : IAlgorithm
    {
        class Range
        {
            public uint Left { get; set; }
            public uint? Right { get; set; } = null;
            public double Value { get; set; }
        }

        private List<Range> ranges = new();

        public StairsAlgorithm AddRange(uint left, uint right, double value)
        {
            ranges.Add(new Range() { Left = left, Right = right, Value = value });
            return this;
        }
        public StairsAlgorithm AddRangeRightPlus(uint left, double value)
        {
            ranges.Add(new Range() { Left = left, Right = null, Value = value });
            return this;
        }

        public double Calculate(int spent, int plan)
        {
            int spentPercent = (int)((double)spent / (double)plan * 100.0);
            Range? range = ranges.FirstOrDefault(range =>
                range.Right == null ?
                    spentPercent >= range.Left : 
                    spentPercent >= range.Left && spentPercent <= range.Right
            );
            return range?.Value ?? 0.0;
        }
    }
}
