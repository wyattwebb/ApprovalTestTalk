using System.IO;
using ApprovalTests.Web.Tests.Setup;
using ASP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorGenerator.Testing;

namespace ApprovalTests.Web.Tests.ControllerTests
{
    [TestClass]
    public class HomeControllerTests : ApiTestBase
    {

        protected override string BaseUrl
        {
            get { return "http://localhost:8000/api/"; }
        }

        [TestMethod]
        public void Index()
        {
            var result = new _Views_Home_Index_cshtml();

            var doc = result.RenderAsHtml();

            using (var s = new StringWriter())
            {
                doc.Save(s);

                VerifyApprovedHtmlResult(s.ToString());
            }

        }
    }
}
