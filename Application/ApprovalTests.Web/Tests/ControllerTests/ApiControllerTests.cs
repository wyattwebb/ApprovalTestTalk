﻿using System.Collections.Generic;
using System.Linq;
using ApprovalTests.Combinations;
using ApprovalTests.Reporters;
using ApprovalTests.Web.Controllers;
using ApprovalTests.Web.Models;
using ApprovalTests.Web.PersistenceModels;
using ApprovalTests.Web.Services;
using ApprovalTests.Web.Tests.Setup;
using ApprovalUtilities.SimpleLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace ApprovalTests.Web.Tests.ControllerTests
{
    [TestClass]
    public class ApiControllerTests : ApiTestBase
    {

        private readonly Fixture _fixture;

        public ApiControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new ApprovalTestCustomization());

            //_fixture.Customize(new AutoMoqCustomization());

            // NEEDED FOR API CONTROLLER ACTION CREATION
            //_fixture.Customize(new HttpRequestMessageCustomization());
            //_fixture.Customize(new ApiControllerCustomization());

            // NEEDED FOR CONTROLLER ACTION CREATION
            //_fixture.Customize(new MvcCustomization());
        }

        protected override string BaseUrl
        {
            get { return "http://localhost:8000/api/"; }
        }

        [TestMethod]
        public void GET_Get__Given_10__When_Mapper_Succeeds__Then_Expected_Mapped_Result()
        {
            // Arrange
            var count = 10;
            var items = _fixture.CreateMany<Team>(10).ToArray();

            var dataService = new Mock<IGetDataService>();
            dataService.Setup(x => x.GetGeneratedTeams(count))
                .Returns(() => items);

            var subject = new ApiController(
                dataService: dataService.Object);

            // Act
            var result = subject.Get(count);

            // Assert
            Assert.AreEqual(items[0].RunningBack, result.Teams[0].RunningBack);
            Assert.AreEqual(items[0].DefensiveTackle, result.Teams[0].DefensiveTackle);
            Assert.AreEqual(items[0].LineBacker, result.Teams[0].LineBacker);
            Assert.AreEqual(items[0].OffensiveGuard, result.Teams[0].OffensiveGuard);
            Assert.AreEqual(items[0].OffensiveTackle, result.Teams[0].OffensiveTackle);
            Assert.AreEqual(items[0].QuarterBack, result.Teams[0].QuarterBack);
            Assert.AreEqual(items[0].Safety, result.Teams[0].Safety);
            Assert.AreEqual(items[0].Center, result.Teams[0].Center);
            // ... Assert the rest
            Assert.AreEqual(items[1].RunningBack, result.Teams[0].RunningBack);
            Assert.AreEqual(items[1].DefensiveTackle, result.Teams[0].DefensiveTackle);
            Assert.AreEqual(items[1].LineBacker, result.Teams[0].LineBacker);
            Assert.AreEqual(items[1].OffensiveGuard, result.Teams[0].OffensiveGuard);
            Assert.AreEqual(items[1].OffensiveTackle, result.Teams[0].OffensiveTackle);
            Assert.AreEqual(items[1].QuarterBack, result.Teams[0].QuarterBack);
            Assert.AreEqual(items[1].Safety, result.Teams[0].Safety);
            Assert.AreEqual(items[1].Center, result.Teams[0].Center);
            // ... Assert the rest up till 10

        }

        [TestMethod, Description("Approval Tests Assert")]
        public void GET_Get_Ten()
        {
            // Arrange
            var count = 10;
            var items = _fixture.CreateMany<Team>(10).ToArray();

            var dataService = new Mock<IGetDataService>();
            dataService.Setup(x => x.GetGeneratedTeams(count))
                .Returns(() => items);

            var subject = new ApiController(
                dataService: dataService.Object);
            
            // Act
            var result = subject.Get(count);

            // Assert
            VerifyApprovedJsonResult(result);
        }

        [TestMethod, Description("No AutoFixture")]
        public void GET_Given_Manual_Setups__When_Get_Three__ThenAsserts()
        {
            // Arrange
            var count = 3;
            var items = new[]
            {
                new Team
                {
                    RunningBack = "RunningBack",
                    OffensiveGuard = "OffensiveGuard",
                    QuarterBack = "QuarterBack",
                    OffensiveTackle = "OffensiveTackle",
                    Safety = "Safety",
                    TightEnd = "TightEnd",
                    WideReciever = "WideReciever",
                    DefensiveTackle = "DefensiveTackle",
                    LineBacker = "LineBacker",
                    Center = "Center"
                    // TODO the rest..
                },
                new Team
                {
                    RunningBack = "RunningBack",
                    OffensiveGuard = "OffensiveGuard",
                    QuarterBack = "QuarterBack",
                    OffensiveTackle = "OffensiveTackle",
                    Safety = "Safety",
                    TightEnd = "TightEnd",
                    WideReciever = "WideReciever",
                    DefensiveTackle = "DefensiveTackle",
                    LineBacker = "LineBacker",
                    Center = "Center"
                    // TODO the rest..
                },
                new Team
                {
                    RunningBack = "RunningBack",
                    OffensiveGuard = "OffensiveGuard",
                    QuarterBack = "QuarterBack",
                    OffensiveTackle = "OffensiveTackle",
                    Safety = "Safety",
                    TightEnd = "TightEnd",
                    WideReciever = "WideReciever",
                    DefensiveTackle = "DefensiveTackle",
                    LineBacker = "LineBacker",
                    Center = "Center"
                    // TODO the rest..
                }
            };

            var dataService = new Mock<IGetDataService>();
            dataService.Setup(x => x.GetGeneratedTeams(count))
                .Returns(() => items);

            var subject = new ApiController(
                dataService: dataService.Object);

            // Act
            var result = subject.Get(count);

            // Assert
            VerifyApprovedJsonResult(result);
        }

        [TestMethod, Description("Approval Tests Assert With AutoFixture")]
        public void GET_Get_Ten_UsingAutoFixture()
        {
            // Arrange
            var count = 10;
            var items = _fixture.CreateMany<Team>(10).ToArray();

            var dataService = new Mock<IGetDataService>();
            dataService.Setup(x => x.GetGeneratedTeams(count))
                .Returns(() => items);

            var subject = new ApiController(
                dataService: dataService.Object);

            // Act
            var result = subject.Get(count);

            // Assert
            VerifyApprovedJsonResult(result);
        }

        [TestMethod, Description("Manual Asserts on null Validation")]
        public void GET_Get_Given_Null_Parameter__When_Executed__Then_Return_Validation_Error_ManualAssertion()
        {
            // Arrange
            int? count = null;

            var expected = "Count is Required";
            var mockValidation = new Mock<IValidateInput>();
            mockValidation.Setup(x => x.ValidateGet(count))
                .Returns(expected);

            var subject = new ApiController(
                validateInput: mockValidation.Object);

            // Act
            var result = subject.Get(count);

            // Assert
            Assert.AreEqual(result.ErrorMessage, expected);
        }

        [TestMethod, Description("Manual Asserts on min Validation")]
        public void GET_Get_Given_Min_Parameter__When_Executed__Then_Return_Validation_Error_ManualAssertion()
        {
            // Arrange
            int? count = 5;

            var expected = "FIVE is OUTRIGHT!";
            var mockValidation = new Mock<IValidateInput>();
            mockValidation.Setup(x => x.ValidateGet(count))
                .Returns(expected);

            var subject = new ApiController(
                validateInput: mockValidation.Object);

            // Act
            var result = subject.Get(count);

            // Assert
            Assert.AreEqual(result.ErrorMessage, expected);
        }

        [TestMethod, Description("Multiple Approval Asserts")]
        public void GET_Get_Given_Parameter__When_Executed__Then_Return_Validation_Errors()
        {
            // Arrange
            int? nullCount = null;
            var fiveCount = 5;

            var nullExpected = "Count is Required";
            var mockValidation = new Mock<IValidateInput>();
            mockValidation.Setup(x => x.ValidateGet(nullCount))
                .Returns(nullExpected);

            var fiveExpected = "FIVE is OUTRIGHT!";
            mockValidation.Setup(x => x.ValidateGet(fiveCount))
                .Returns(fiveExpected);

            var subject = new ApiController(
                validateInput: mockValidation.Object);
            var results = new List<TeamsViewModel>();

            // Act
            results.Add(subject.Get(nullCount));
            results.Add(subject.Get(fiveCount));

            // Assert
            VerifyApprovedJsonResult(results);
        }

        [TestMethod, Description("Multiple Approval Asserts HTML")]
        public void GET_Get_Given_Parameter__When_Executed__Then_Return_Validation_Errors_Html()
        {
            // Arrange
            int? nullCount = null;
            var fiveCount = 5;

            var nullExpected = "Count is Required";
            var mockValidation = new Mock<IValidateInput>();
            mockValidation.Setup(x => x.ValidateGet(nullCount))
                .Returns(nullExpected);

            var fiveExpected = "FIVE is OUTRIGHT!";
            mockValidation.Setup(x => x.ValidateGet(fiveCount))
                .Returns(fiveExpected);

            var subject = new ApiController(
                validateInput: mockValidation.Object);
            var results = new List<TeamsViewModel>();

            // Act
            var htmlReport = new BuildHtml("Multiple Approval Test Count Input");
            htmlReport.AddLine(subject.Get(nullCount).ErrorMessage);
            htmlReport.AddLine(subject.Get(fiveCount).ErrorMessage);

            // Assert
            VerifyApprovedHtmlResult(htmlReport.GetHtml());
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void TestMultiple()
        {
            // Arrange
            var subject = new ValidateInput();

            var counts = new[] { null as int?, 0, 1, 3, 5, 12 };

            // Act
            // Assert
            CombinationApprovals.VerifyAllCombinations(subject.ValidateGet, counts);
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void GET_Get_API_JsonTest()
        {
            var result = ExecuteGetRequest<TeamsViewModel>("Data/1");

            VerifyApprovedJsonResult(result);
        }

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
        public void TestContructorWithMoq_Test()
        {
            // Arrange
            var dataService = new Mock<IGetDataService>();
            var mockMapper = new Mock<IMapperService>();
            var mockValidation = new Mock<IValidateInput>();

            var subject = new ApiController(
                dataService.Object,
                mockMapper.Object,
                mockValidation.Object);
            // Act
            subject.Get(1);

            // Assert
            Assert.IsTrue(true);
            // Just and stopping point for presentation purposes
        }

        [TestMethod]
        public void TestContructorWithMoq_And_AutoMoq_Test()
        {
            // Arrange
            _fixture.Customize(new ApiControllerCustomization());      // This could also be done in initilization of this object
            _fixture.Customize(new HttpRequestMessageCustomization()); // This could also be done in initilization of this object
            _fixture.Customize(new AutoMoqCustomization());

            var subject = _fixture.Create<ApiController>();

            // Act
            subject.Get(1);

            // Assert
            Assert.IsTrue(true);
            // Just and stopping point for presentation purposes
        }

        [TestMethod]
        public void Sample_TestContructorWithMoq_Test()
        {
            // Arrange
            var dataService = new Mock<IGetDataService>();
            var mockMapper = new Mock<IMapperService>();
            var mockValidation = new Mock<IValidateInput>();

            var subject = new SampleService(
                dataService.Object,
                mockMapper.Object,
                mockValidation.Object);
            // Act
            var result = subject.IsSample(true);

            // Assert
            Assert.IsTrue(result);
            // Just and stopping point for presentation purposes
        }

        [TestMethod]
        public void Sample_TestContructorWithMoq_And_AutoMoq_Test()
        {
            // Arrange
            //_fixture.Customize(new AutoMoqCustomization()); // This could also be done in initilization of this object

            var subject = _fixture.Create<SampleService>();

            // Act
            var result = subject.IsSample(true);

            // Assert
            Assert.IsTrue(result);
            // Just and stopping point for presentation purposes
        }

    }
}
