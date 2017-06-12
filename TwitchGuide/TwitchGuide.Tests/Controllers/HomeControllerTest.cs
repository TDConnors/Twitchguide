using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.Mvc;
using TwitchGuide.DAL;
using TwitchGuide.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitchGuide;
using TwitchGuide.Controllers;

namespace TwitchGuide.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }     
    }
}
