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
            subject.ValidateGet(0);
            subject.ValidateGet(1);
            subject.ValidateGet(3);
            subject.ValidateGet(5);
            subject.ValidateGet(12);

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
            CombinationApprovals.VerifyAllCombinations(subject.ValidateGet, counts);
        }

        protected override string BaseUrl
        {
            get { return null; }
        }
    }
}
