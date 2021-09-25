using Microsoft.VisualStudio.TestTools.UnitTesting;
using kpi;
using System;

namespace kpitests
{
    [TestClass]
    public class KpiTests
    {
        readonly double kpiDelta = 0.0001;

        [TestMethod]
        public void MainTest()
        {
            Employee employee = new();
            employee
                .AppendKpi("#1", 100, 0.2, new BinaryAlgorithm())
                .AppendKpi("#2", 200, 0.8, new StairsAlgorithm()
                                            .AddRange(0, 79, 0.0)
                                            .AddRange(80, 99, 0.8)
                                            .AddRangeRightPlus(100, 1.0));

            employee
                .SetSpent("#1", 95)
                .SetSpent("#2", 210);

            Assert.AreEqual(0.8, employee.CalculateKpi(), kpiDelta);
        }

        [TestMethod]
        public void TestAppend()
        {
            Employee employee = new();
            employee
                .AppendKpi("#1", 20, 0.5, new BinaryAlgorithm())
                .AppendKpi("#2", 30, 0.1, new StairsAlgorithm());

            Assert.AreEqual(employee.Kpis.Count, 2);

            (var kpi1, var kpi2) = (employee.Kpis[0], employee.Kpis[1]);
            
            Assert.AreEqual(kpi1.Name, "#1");
            Assert.AreEqual(kpi1.Plan, 20);
            Assert.AreEqual(kpi1.Weight, 0.5);
            Assert.IsInstanceOfType(kpi1.Algorithm, typeof(BinaryAlgorithm));

            Assert.AreEqual(kpi2.Name, "#2");
            Assert.AreEqual(kpi2.Plan, 30);
            Assert.AreEqual(kpi2.Weight, 0.1);
            Assert.IsInstanceOfType(kpi2.Algorithm, typeof(StairsAlgorithm));
        }

        [TestMethod]
        public void TestSpent()
        {
            Employee employee = new();
            employee
                .AppendKpi("#1", 20, 0.5, new BinaryAlgorithm())
                .AppendKpi("#2", 30, 0.1, new StairsAlgorithm());

            employee.SetSpent("#1", 13).SetSpent("#2", 15);

            Assert.AreEqual(employee.Kpis[0].Spent, 13);
            Assert.AreEqual(employee.Kpis[1].Spent, 15);
        }

        [TestMethod]
        public void TestBinaryAlgorithm()
        {
            BinaryAlgorithm binaryAlgorithm = new BinaryAlgorithm();
            Assert.AreEqual(binaryAlgorithm.Calculate(120, 100), 1.0);
            Assert.AreEqual(binaryAlgorithm.Calculate(50, 100), 0.0);
        }

        [TestMethod]
        public void TestBinaryAlgorithm2()
        {
            double kpi1 = new Employee()
                .AppendKpi("#0", 20, 0.5, new BinaryAlgorithm())
                .SetSpent("#0", 30)
                .CalculateKpi();

            Assert.AreEqual(kpi1, 0.5, kpiDelta);
            
            double kpi2 = new Employee()
                .AppendKpi("#0", 20, 0.5, new BinaryAlgorithm())
                .SetSpent("#0", 10)
                .CalculateKpi();

            Assert.AreEqual(kpi2, 0.0, kpiDelta);
        }

        [TestMethod]
        public void TestStairsAlgorithm()
        {
            var alg = new StairsAlgorithm()
                .AddRange(0, 20, 0.7)
                .AddRange(21, 89, 0.8)
                .AddRangeRightPlus(90, 0.9);

            Assert.AreEqual(0.7, alg.Calculate(0, 100));
            Assert.AreEqual(0.7, alg.Calculate(19, 100));
            Assert.AreEqual(0.8, alg.Calculate(30, 100));
            Assert.AreEqual(0.9, alg.Calculate(96, 100));
            Assert.AreEqual(0.9, alg.Calculate(140, 100));
        }
    }
}
