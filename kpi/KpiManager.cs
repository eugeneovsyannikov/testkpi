using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpi
{
    public static class KpiManagerExtension
    {
        public static double CalculateKpi(this Employee employee) => 
            employee.Kpis
                .Select(kpi => kpi.Weight * kpi.Algorithm?.Calculate(kpi.Spent, kpi.Plan) ?? 0.0)
                .Aggregate((res, kpi) => res + kpi);

        public static Employee AppendKpi(this Employee employee, string name, int plan, double weight, IAlgorithm algorithm)
        {
            employee.Kpis.Add(new Kpi() { 
                Name = name,
                Plan = plan,
                Weight = weight,
                Algorithm = algorithm
            });
            return employee;
        }

        public static Employee SetSpent(this Employee employee, string name, int value)
        {
            foreach (var kpi in employee.Kpis.Where(kpi => kpi.Name == name))
                kpi.Spent = value;

            return employee;
        }
    }
}
