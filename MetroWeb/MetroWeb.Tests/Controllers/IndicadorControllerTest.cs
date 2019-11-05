using System;
using System.Web.Mvc;
using MetroWeb.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetroWeb.Tests.Controllers
{
    [TestClass]
    public class IndicadorControllerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            IndicadorController controller = new IndicadorController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
