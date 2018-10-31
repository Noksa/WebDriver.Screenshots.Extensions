using System.Drawing;
using NUnit.Framework;
using WDSE;
using WDSE.ScreenshotMaker;
using WDSETests.Properties;

// ReSharper disable InconsistentNaming

namespace WDSETests.BasicTests
{
    [TestFixture(TestName = "Basic screenshot tests")]
    [NonParallelizable]
    public class BasicTests : TestsInit
    {
        [Test]
        public void TestBasicImage1920x1080()
        {
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Navigate().GoToUrl(PagePath);
            var arr = Driver.TakeScreenshot(new ScreenshotMaker());
            CompareAndTest(arr, Resources.BasicImageShouldBe1920x1080);
        }
    }
}