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
    /// To test if splitting text by enter and space works or not.
    /// </summary>
    [TestClass()]
    public class FileConvertorTests
    {
        //split by enter
        [TestMethod()]
        public void SplitStringByEnterTest()
        {
            FileConvertor fileConvertor = new FileConvertor();
            string[] splittedString = fileConvertor.SplitStringByEnter("01 12 15 23\n14 14 05 23");
            CollectionAssert.AreEqual(new string[] { "01 12 15 23", "14 14 05 23" }, splittedString);
        }

        //split y space
        [TestMethod()]
        public void SplitStringBySpaceTest()
        {
            FileConvertor fileConvertor = new FileConvertor();
            string[] splittedString = fileConvertor.SplitStringBySpace("01 12 15 23");
            CollectionAssert.AreEqual(new string[] { "01", "12", "15", "23" }, splittedString);
        }
    }
}