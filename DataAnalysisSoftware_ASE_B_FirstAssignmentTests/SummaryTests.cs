using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAnalysisSoftware_ASE_B_FirstAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysisSoftware_ASE_B_FirstAssignment.Tests
{
    /// <summary>
    /// testing of the max value,min value ,average value,sum from the data
    /// </summary>
    [TestClass()]
    public class SummaryTests
    {
        //to test if the method find max or not
        [TestMethod()]
        public void FindMaxTest()
        {
            int maxValue = Summary.FindMax(new List<string> { "15", "10", "4", "18", "16" });
            Assert.AreEqual(18, maxValue);
        }

        //to test if the methods find min or not
        [TestMethod()]
        public void FindMinTest()
        {
            int val = Summary.FindMin(new List<string> { "15", "10", "4", "18", "16" });
            Assert.AreEqual(4, val);
        }

        //to test if the methods find average or not
        [TestMethod()]
        public void FindAverageTest()
        {
            double val = Summary.FindAverage(new List<string> { "15", "10", "4", "18", "16" });
            Assert.AreEqual(12, val);
        }

        //to test if the methods find sum or not
        [TestMethod()]
        public void FindSumTest()
        {
            double val = Summary.FindSum(new List<string> { "15", "10", "4", "18", "16" });
            Assert.AreEqual(63, val);
        }

        ////to test if the methods converts date into vaid format or not
        [TestMethod()]
        public void ConvertToDate()
        {
            string val = Summary.ConvertToDate("20120102");
            Assert.AreEqual("2012-01-02", val);
        }
    }
}