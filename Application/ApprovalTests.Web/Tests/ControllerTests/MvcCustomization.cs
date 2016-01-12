using System.Web.Mvc;
using Ploeh.AutoFixture;

namespace ApprovalTests.Web.Tests.ControllerTests
{
    public class MvcCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            // This is needed to Create<> an MVC Controller
            fixture.Customize<ControllerContext>(c => c
                .Without(x => x.DisplayMode));
        }
    }
}
