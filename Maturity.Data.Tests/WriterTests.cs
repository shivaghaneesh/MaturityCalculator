using Maturity.Data.Implementation;
using Maturity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maturity.Data.Tests
{
    /// <summary>
    /// Test class for writer.
    /// </summary>
    [TestClass]
    public class WriterTests
    {
        private Writer writer;

        [TestInitialize]
        [Description("Test Initialize")]
        public void Test_Intialize()
        {
            writer = new Writer();
        }

        [TestMethod]
        [Description("Test method with MaturityDetails instance without data and returns as not null.")]
        public void XmlReader_WithoutData_CheckResultLength()
        {
            var result = writer.XmlWriter(new List<MaturityDetail>());

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        [Description("Test method with Maturity Details instance with data and returns as not null.")]
        public void XmlReader_WithData_CheckResultLength()
        {
            var maturityDetails = new List<MaturityDetail>()
            {
                new MaturityDetail() {PolicyNumber ="A100100", MaturityValue = "100" },
                new MaturityDetail() {PolicyNumber ="B100200", MaturityValue = "200" },
            };
            var result = writer.XmlWriter(maturityDetails);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }
    }
}

