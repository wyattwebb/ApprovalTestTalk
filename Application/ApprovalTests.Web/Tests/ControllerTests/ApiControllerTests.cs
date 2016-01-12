using System.Collections.Generic;
using System.Linq;
using ApprovalTests.Reporters;
using ApprovalTests.Web.Controllers;
using ApprovalTests.Web.Models.BaconViewModels;
using ApprovalTests.Web.PersistanceModels.BaconModels;
using ApprovalTests.Web.Services;
using ApprovalTests.Web.Tests.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

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
            var items = _fixture.CreateMany<Pig>(10).ToArray();

            var dataService = new Mock<IGetDataService>();
            dataService.Setup(x => x.GetGeneratedPigs(count))
                .Returns(() => items);

            var subject = new ApiController(
                dataService: dataService.Object);

            // Act
            var result = subject.Get(count);

            // Assert
            Assert.AreEqual(items[0].Belly, result.Pigs[0].Belly);
            Assert.AreEqual(items[0].Back, result.Pigs[0].Back);
            Assert.AreEqual(items[0].Cannon, result.Pigs[0].Cannon);
            Assert.AreEqual(items[0].Cheek, result.Pigs[0].Cheek);
            Assert.AreEqual(items[0].Coffin, result.Pigs[0].Coffin);
            Assert.AreEqual(items[0].Crops, result.Pigs[0].Crops);
            Assert.AreEqual(items[0].Dewclaw, result.Pigs[0].Dewclaw);
            Assert.AreEqual(items[0].Ear, result.Pigs[0].Ear);
            // ... Assert the rest
            Assert.AreEqual(items[1].Belly, result.Pigs[0].Belly);
            Assert.AreEqual(items[1].Back, result.Pigs[0].Back);
            Assert.AreEqual(items[1].Cannon, result.Pigs[0].Cannon);
            Assert.AreEqual(items[1].Cheek, result.Pigs[0].Cheek);
            Assert.AreEqual(items[1].Coffin, result.Pigs[0].Coffin);
            Assert.AreEqual(items[1].Crops, result.Pigs[0].Crops);
            Assert.AreEqual(items[1].Dewclaw, result.Pigs[0].Dewclaw);
            Assert.AreEqual(items[1].Ear, result.Pigs[0].Ear);
            // ... Assert the rest up till 10

        }

        [TestMethod, Description("Approval Tests Assert")]
        public void GET_Get_Ten()
        {
            // Arrange
            var count = 10;
            var items = _fixture.CreateMany<Pig>(10).ToArray();

            var dataService = new Mock<IGetDataService>();
            dataService.Setup(x => x.GetGeneratedPigs(count))
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
                new Pig
                {
                    Back = "Back",
                    Belly = "Belly",
                    Cannon = "Cannon",
                    Cheek = "Cheek",
                    Coffin = "Coffin",
                    Crops = "Crops",
                    Dewclaw = "Dewclaw",
                    Ear = "Ear",
                    FetLock = "FetLock",
                    ForeFlank = "ForFlank"
                    // TODO the rest..
                },
                new Pig
                {
                    Back = "Back",
                    Belly = "Belly",
                    Cannon = "Cannon",
                    Cheek = "Cheek",
                    Coffin = "Coffin",
                    Crops = "Crops",
                    Dewclaw = "Dewclaw",
                    Ear = "Ear",
                    FetLock = "FetLock",
                    ForeFlank = "ForFlank"
                    // TODO the rest..
                },
                new Pig
                {
                    Back = "Back",
                    Belly = "Belly",
                    Cannon = "Cannon",
                    Cheek = "Cheek",
                    Coffin = "Coffin",
                    Crops = "Crops",
                    Dewclaw = "Dewclaw",
                    Ear = "Ear",
                    FetLock = "FetLock",
                    ForeFlank = "ForFlank"
                    // TODO the rest..
                }
            };

            var dataService = new Mock<IGetDataService>();
            dataService.Setup(x => x.GetGeneratedPigs(count))
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
            var items = _fixture.CreateMany<Pig>(10).ToArray();

            var dataService = new Mock<IGetDataService>();
            dataService.Setup(x => x.GetGeneratedPigs(count))
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
            mockValidation.Setup(x => x.ValidateGetPigs(count))
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
            mockValidation.Setup(x => x.ValidateGetPigs(count))
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
            mockValidation.Setup(x => x.ValidateGetPigs(nullCount))
                .Returns(nullExpected);

            var fiveExpected = "FIVE is OUTRIGHT!";
            mockValidation.Setup(x => x.ValidateGetPigs(fiveCount))
                .Returns(fiveExpected);

            var subject = new ApiController(
                validateInput: mockValidation.Object);
            var results = new List<PigsViewModel>();

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
            mockValidation.Setup(x => x.ValidateGetPigs(nullCount))
                .Returns(nullExpected);

            var fiveExpected = "FIVE is OUTRIGHT!";
            mockValidation.Setup(x => x.ValidateGetPigs(fiveCount))
                .Returns(fiveExpected);

            var subject = new ApiController(
                validateInput: mockValidation.Object);
            var results = new List<PigsViewModel>();

            // Act
            var htmlReport = new BuildHtml("Multiple Approval Test Count Input");
            htmlReport.AddLine(subject.Get(nullCount).ErrorMessage);
            htmlReport.AddLine(subject.Get(fiveCount).ErrorMessage);

            // Assert
            VerifyApprovedHtmlResult(htmlReport.GetHtml());
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void GET_Get_API_JsonTest()
        {
            var result = ExecuteGetRequest<PigsViewModel>("Data/1");

            VerifyApprovedJsonResult(result);
        }

    }
}
