using System;
using System.IO;
using System.Text;
using Maturity.Data.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Maturity.Data.Tests
{
    [TestClass]
    public class ReaderTests
    {
        private Reader reader;
        private StringBuilder stringBuilder;

        [TestInitialize]
        [Description("Test Initialize")]
        public void Test_Intialize()
        {
            reader = new Reader();
            stringBuilder = new StringBuilder();
        }

        [TestMethod]
        [Description("Test method with MaturityDetails instance without data and throws argument null exception.")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void XmlReader_WithoutData_CheckResultLength()
        {
            var result = reader.CsvReader(It.IsAny<Stream>());
        }
        [TestMethod]
        [Description("Test method with Maturity Details instance with data with missing/incorrect header and throws missing field exception.")]
        [ExpectedException(typeof(CsvHelper.MissingFieldException))]
        public void XmlReader_WithInValidHeaderValue_ThrowsMissingFieldException()
        {
            stringBuilder.Append("policy_no,policy_start_date,premiums,membership,discretionary_bonus,uplift_percentage");
            stringBuilder.AppendLine();
            stringBuilder.Append("A123123,01/01/1990,100,Y,550,50").AppendLine();

            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(stringBuilder.ToString()));

            var policyDetails = reader.CsvReader(contentStream);
        }

        [TestMethod]
        [Description("Test method with Maturity Details instance with data with incorrect policy value and throws reader exception.")]
        [ExpectedException(typeof(CsvHelper.ReaderException))]
        public void XmlReader_WithInValidPolicyValue_ThrowsMissingFieldException()
        {
            stringBuilder.Append("policy_number,policy_start_date,premiums,membership,discretionary_bonus,uplift_percentage");
            stringBuilder.AppendLine();
            stringBuilder.Append("123123,01/01/1990,100,Y,550,50").AppendLine();

            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(stringBuilder.ToString()));

            var policyDetails = reader.CsvReader(contentStream);
        }

        [TestMethod]
        [Description("Test method with Maturity Details instance with data with incorrect start date and throws reader exception.")]
        [ExpectedException(typeof(CsvHelper.ReaderException))]
        public void XmlReader_WithInValidStartDateValue_ThrowsMissingFieldException()
        {
            stringBuilder.Append("policy_number,policy_start_date,premiums,membership,discretionary_bonus,uplift_percentage");
            stringBuilder.AppendLine();
            stringBuilder.Append("A123123,01/45/2000,100,Y,550,50").AppendLine();

            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(stringBuilder.ToString()));

            var policyDetails = reader.CsvReader(contentStream);
        }

        [TestMethod]
        [Description("Test method with Maturity Details instance with data with incorrect premium and throws reader exception.")]
        [ExpectedException(typeof(CsvHelper.ReaderException))]
        public void XmlReader_WithInValidPremiumValue_ThrowsMissingFieldException()
        {
            stringBuilder.Append("policy_number,policy_start_date,premiums,membership,discretionary_bonus,uplift_percentage");
            stringBuilder.AppendLine();
            stringBuilder.Append("A123123,01/01/1990,NA,Y,550,50").AppendLine();

            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(stringBuilder.ToString()));

            var policyDetails = reader.CsvReader(contentStream);
        }

        [TestMethod]
        [Description("Test method with Maturity Details instance with data with incorrect membership and throws reader exception.")]
        [ExpectedException(typeof(CsvHelper.ReaderException))]
        public void XmlReader_WithInValidMembershipValue_ThrowsMissingFieldException()
        {
            stringBuilder.Append("policy_number,policy_start_date,premiums,membership,discretionary_bonus,uplift_percentage");
            stringBuilder.AppendLine();
            stringBuilder.Append("A123123,01/01/1990,100,NA,550,50").AppendLine();

            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(stringBuilder.ToString()));

            var policyDetails = reader.CsvReader(contentStream);
        }

        [TestMethod]
        [Description("Test method with Maturity Details instance with data with incorrect discretionary bonus and throws reader exception.")]
        [ExpectedException(typeof(CsvHelper.ReaderException))]
        public void XmlReader_WithInValidDiscretionaryBonusValue_ThrowsMissingFieldException()
        {
            stringBuilder.Append("policy_number,policy_start_date,premiums,membership,discretionary_bonus,uplift_percentage");
            stringBuilder.AppendLine();
            stringBuilder.Append("A123123,01/01/1990,100,Y,avd,50").AppendLine();

            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(stringBuilder.ToString()));

            var policyDetails = reader.CsvReader(contentStream);
        }

        [TestMethod]
        [Description("Test method with Maturity Details instance with data with incorrect uplift value and throws reader exception.")]
        [ExpectedException(typeof(CsvHelper.ReaderException))]
        public void XmlReader_WithInValidUpliftValue_ThrowsMissingFieldException()
        {
            stringBuilder.Append("policy_number,policy_start_date,premiums,membership,discretionary_bonus,uplift_percentage");
            stringBuilder.AppendLine();
            stringBuilder.Append("A123123,01/01/1990,100,Y,550,NA").AppendLine();

            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(stringBuilder.ToString()));

            var policyDetails = reader.CsvReader(contentStream);
        }

        [TestMethod]
        [Description("Test method with Maturity Details instance with data and returns as not null.")]
        public void XmlReader_WithValidData_CheckResultLength()
        {
            stringBuilder.Append("policy_number,policy_start_date,premiums,membership,discretionary_bonus,uplift_percentage");
            stringBuilder.AppendLine();
            stringBuilder.Append("A123123,01/01/1990,100,Y,550,50").AppendLine();

            var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(stringBuilder.ToString()));

            var policyDetails = reader.CsvReader(contentStream);

            Assert.IsNotNull(policyDetails);

            Assert.AreEqual(1, policyDetails.Count);

        }
    }
}
