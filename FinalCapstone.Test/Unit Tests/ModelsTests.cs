using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Linq;
using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace FinalCapstone.Test.Unit_Tests
{
    [TestClass]
    public class ModelsTests
    {
        [TestMethod]
        public void ToolImageNameTest()
        {
            Tool tool = new Tool()
            {
                Brand = "DewaltDisney",
                ToolName = "Table Saw"
            };

            Assert.AreEqual("DewaltDisneyTable Saw", tool.ImageName);
        }
    }
}
