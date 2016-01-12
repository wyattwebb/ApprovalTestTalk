using System;
using System.Text;
using System.Collections.Generic;
using ApprovalTests.Combinations;
using ApprovalTests.Reporters;
using ApprovalTests.Web.Services;
using ApprovalTests.Web.Tests.Setup;
using ApprovalUtilities.SimpleLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.Web.Tests.ServiceTests
{
    /// <summary>
    /// Summary description for ValidateInputTests
    /// </summary>
    [TestClass]
    public class ValidateInputTests : ApiTestBase
    {
        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void TestLogger()
        {
            // Arrange
            var logger = Logger.LogToStringBuilder();
            var subject = new ValidateInput();

            // Act
            subject.ValidateGetPigs(0);
            subject.ValidateGetPigs(1);
            subject.ValidateGetPigs(3);
            subject.ValidateGetPigs(5);
            subject.ValidateGetPigs(12);

            // Assert
            Approvals.Verify(logger.ToString());
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void TestMultiple()
        {
            // Arrange
            var subject = new ValidateInput();

            var counts = new[] {null as int?, 0, 1, 3, 5, 12};

            // Act
            // Assert
            CombinationApprovals.VerifyAllCombinations(subject.ValidateGetPigs, counts);
        }

        protected override string BaseUrl
        {
            get { return null; }
        }
    }
}
