using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTestSample;
using GPSNET.IntegrationTest;

namespace IntegrationTestTests
{
    [TestClass]
    public class ColorTest : BaseIntegrationTest
    {
        [TestMethod]
        public void InsertColor()
        {
            var color = new Colors();
            color.AddColor("testcolor");
            var colors = color.GetAll();
            Assert.IsTrue(colors.Contains("testcolor"));            
        }


    }
}
