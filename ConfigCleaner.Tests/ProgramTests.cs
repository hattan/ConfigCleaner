using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigCleaner.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Main_WithNoArguments_ReturnsHelpText()
        {
            using (var sw = new StringWriter())
            {   
                //Arrange
                Console.SetOut(sw);
                string helpTextStart = "Config Cleaner - ";

                //Act
               Program.Main(new string[0]);

                //Assert
                Assert.IsTrue(sw.ToString().StartsWith(helpTextStart));
            }
        }

        [TestMethod]
        public void Main_WithHelpSwitch_ReturnsHelpText()
        {
            using (var sw = new StringWriter())
            {
                //Arrange
                Console.SetOut(sw);
                string helpTextStart = "Config Cleaner - ";

                //Act
                Program.Main(new[] { "-help" });

                //Assert
                Assert.IsTrue(sw.ToString().StartsWith(helpTextStart));
            }
        }

        [TestMethod]
        public void Main_InvalidInputFolder_WritesFriendlyErrorToConsole()
        {
            using (var sw = new StringWriter())
            {
                //Arrange
                Console.SetOut(sw);
                string helpTextStart = "Error: Input folder not found" + Environment.NewLine;

                //Act
                Program.Main(new[] { "notARealfolder" });

                //Assert
                Assert.AreEqual(helpTextStart,sw.ToString());
            }
        }
    }
}
