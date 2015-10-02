namespace UmbracoBase.Tests.Tests
{
    using System.Linq;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [DeploymentItem("UmbracoBase.Web.dll")]
    public class AssemblyInfoTests : BaseTests
    {
        private const string WebAssemblyName = "UmbracoBase.Web";

        [TestMethod]
        public void TestAssemblyInfoTitleExists()
        {
            var titleAttribute =
                (AssemblyTitleAttribute)Assembly.Load(WebAssemblyName)
                    .GetCustomAttributes(typeof(AssemblyTitleAttribute), false)
                    .FirstOrDefault();

            Assert.IsNotNull(titleAttribute);
            Assert.IsNotNull(titleAttribute.Title);
        }

        [TestMethod]
        public void TestAssemblyInfoCompanyExists()
        {
            var companyAttribute =
                (AssemblyCompanyAttribute)Assembly.Load(WebAssemblyName)
                    .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)
                    .FirstOrDefault();

            Assert.IsNotNull(companyAttribute);
            Assert.IsNotNull(companyAttribute.Company);
        }

        [TestMethod]
        public void TestAssemblyInfoProductExists()
        {
            var productAttribute =
                (AssemblyProductAttribute)Assembly.Load(WebAssemblyName)
                    .GetCustomAttributes(typeof(AssemblyProductAttribute), false)
                    .FirstOrDefault();

            Assert.IsNotNull(productAttribute);
            Assert.IsNotNull(productAttribute.Product);
        }

        [TestMethod]
        public void TestAssemblyInfoCopyrightExists()
        {
            var copyrightAttribute =
                (AssemblyCopyrightAttribute)Assembly.Load(WebAssemblyName)
                    .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)
                    .FirstOrDefault();

            Assert.IsNotNull(copyrightAttribute);
            Assert.IsNotNull(copyrightAttribute.Copyright);
        }        
    }
}
