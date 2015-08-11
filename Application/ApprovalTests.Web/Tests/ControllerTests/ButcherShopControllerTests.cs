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
    public class ButcherShopControllerTests : ApiTestBase
    {

        private readonly Fixture _fixture;

        public ButcherShopControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new ApprovalTestCustomization());
        }

        protected override string BaseUrl
        {
            get { return "http://localhost:8000/api/"; }
        }

        [TestMethod]
        public void GET_GetPigs__Given_10Pigs__When_Mapper_Succeeds__Then_Expected_Mapped_Result()
        {
            // Arrange
            var count = 10;
            var pigs = _fixture.CreateMany<Pig>(10).ToArray();

            var turkeyBaconService = new Mock<IBaconService>();
            turkeyBaconService.Setup(x => x.GetGeneratedPigs(count))
                .Returns(() => pigs);

            var subject = new ButcherShopController(
                baconService: turkeyBaconService.Object);

            // Act
            var result = subject.GetPigs(count);

            // Assert
            Assert.AreEqual(pigs[0].Belly, result.Pigs[0].Belly);
            Assert.AreEqual(pigs[0].Back, result.Pigs[0].Back);
            Assert.AreEqual(pigs[0].Cannon, result.Pigs[0].Cannon);
            Assert.AreEqual(pigs[0].Cheek, result.Pigs[0].Cheek);
            Assert.AreEqual(pigs[0].Coffin, result.Pigs[0].Coffin);
            Assert.AreEqual(pigs[0].Crops, result.Pigs[0].Crops);
            Assert.AreEqual(pigs[0].Dewclaw, result.Pigs[0].Dewclaw);
            Assert.AreEqual(pigs[0].Ear, result.Pigs[0].Ear);
            // ... Assert the rest
            Assert.AreEqual(pigs[1].Belly, result.Pigs[0].Belly);
            Assert.AreEqual(pigs[1].Back, result.Pigs[0].Back);
            Assert.AreEqual(pigs[1].Cannon, result.Pigs[0].Cannon);
            Assert.AreEqual(pigs[1].Cheek, result.Pigs[0].Cheek);
            Assert.AreEqual(pigs[1].Coffin, result.Pigs[0].Coffin);
            Assert.AreEqual(pigs[1].Crops, result.Pigs[0].Crops);
            Assert.AreEqual(pigs[1].Dewclaw, result.Pigs[0].Dewclaw);
            Assert.AreEqual(pigs[1].Ear, result.Pigs[0].Ear);
            // ... Assert the rest up till 10

        }

        [TestMethod, Description("Approval Tests Assert")]
        public void GET_GetPigs_Ten()
        {
            // Arrange
            var count = 10;
            var pigs = _fixture.CreateMany<Pig>(10).ToArray();

            var turkeyBaconService = new Mock<IBaconService>();
            turkeyBaconService.Setup(x => x.GetGeneratedPigs(count))
                .Returns(() => pigs);

            var subject = new ButcherShopController(
                baconService: turkeyBaconService.Object);
            
            // Act
            var result = subject.GetPigs(count);

            // Assert
            VerifyApprovedJsonResult(result);
        }

        [TestMethod, Description("No AutoFixture")]
        public void GET_Given_Manual_Setups__When_GetPigs_Three__ThenAsserts()
        {
            // Arrange
            var count = 3;
            var pigs = new[]
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

            var turkeyBaconService = new Mock<IBaconService>();
            turkeyBaconService.Setup(x => x.GetGeneratedPigs(count))
                .Returns(() => pigs);

            var subject = new ButcherShopController(
                baconService: turkeyBaconService.Object);

            // Act
            var result = subject.GetPigs(count);

            // Assert
            VerifyApprovedJsonResult(result);
        }

        [TestMethod, Description("Approval Tests Assert With AutoFixture")]
        public void GET_GetPigs_Ten_UsingAutoFixture()
        {
            // Arrange
            var count = 10;
            var pigs = _fixture.CreateMany<Pig>(10).ToArray();

            var turkeyBaconService = new Mock<IBaconService>();
            turkeyBaconService.Setup(x => x.GetGeneratedPigs(count))
                .Returns(() => pigs);

            var subject = new ButcherShopController(
                baconService: turkeyBaconService.Object);

            // Act
            var result = subject.GetPigs(count);

            // Assert
            VerifyApprovedJsonResult(result);
        }

        [TestMethod, Description("Manual Asserts on null Validation")]
        public void GET_GetPigs_Given_Null_Parameter__When_Executed__Then_Return_Validation_Error_ManualAssertion()
        {
            // Arrange
            int? count = null;

            var expected = "Count is Required";
            var mockValidation = new Mock<IValidateInput>();
            mockValidation.Setup(x => x.ValidateGetPigs(count))
                .Returns(expected);

            var subject = new ButcherShopController(
                validateInput: mockValidation.Object);

            // Act
            var result = subject.GetPigs(count);

            // Assert
            Assert.AreEqual(result.ErrorMessage, expected);
        }

        [TestMethod, Description("Manual Asserts on min Validation")]
        public void GET_GetPigs_Given_Min_Parameter__When_Executed__Then_Return_Validation_Error_ManualAssertion()
        {
            // Arrange
            int? count = 5;

            var expected = "FIVE is OUTRIGHT!";
            var mockValidation = new Mock<IValidateInput>();
            mockValidation.Setup(x => x.ValidateGetPigs(count))
                .Returns(expected);

            var subject = new ButcherShopController(
                validateInput: mockValidation.Object);

            // Act
            var result = subject.GetPigs(count);

            // Assert
            Assert.AreEqual(result.ErrorMessage, expected);
        }

        [TestMethod, Description("Multiple Approval Asserts")]
        public void GET_GetPigs_Given_Parameter__When_Executed__Then_Return_Validation_Errors()
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

            var subject = new ButcherShopController(
                validateInput: mockValidation.Object);
            var results = new List<PigsViewModel>();

            // Act
            results.Add(subject.GetPigs(nullCount));
            results.Add(subject.GetPigs(fiveCount));

            // Assert
            VerifyApprovedJsonResult(results);
        }

        [TestMethod, Description("Multiple Approval Asserts HTML")]
        public void GET_GetPigs_Given_Parameter__When_Executed__Then_Return_Validation_Errors_Html()
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

            var subject = new ButcherShopController(
                validateInput: mockValidation.Object);
            var results = new List<PigsViewModel>();

            // Act
            var htmlReport = new BuildHtml("Multiple Approval Test Count Input");
            htmlReport.AddLine(subject.GetPigs(nullCount).ErrorMessage);
            htmlReport.AddLine(subject.GetPigs(fiveCount).ErrorMessage);

            // Assert
            VerifyApprovedHtmlResult(htmlReport.GetHtml());
        }

        [TestMethod]
        [UseReporter(typeof(DiffReporter))]
        public void GET_GetPigs_API_JsonTest()
        {
            var result = ExecuteGetRequest<PigsViewModel>("ButcherShop/GetPigs?id=1");

            VerifyApprovedJsonResult(result);
        }

    }
}
