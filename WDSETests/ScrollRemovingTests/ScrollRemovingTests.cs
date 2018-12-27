using System.Drawing;
using NUnit.Framework;
using WDSE;
using WDSE.ScreenshotMaker;
using WDSETests.Properties;

// ReSharper disable InconsistentNaming

namespace WDSETests.ScrollRemovingTests
{
    [TestFixture(TestName = "Removing scrollbar tests")]
    [NonParallelizable]
    public class ScrollRemovingTests : TestsInit
    {
        [Test]
        public void RemoveScrollBar1920x1080()
        {
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Navigate().GoToUrl(PagePath);
            var scmkr = new ScreenshotMaker();
            scmkr.RemoveScrollBarsWhileShooting();
            var arr = Driver.TakeScreenshot(scmkr);
            CompareAndTest(arr, Resources.RemovingScrollBarsShouldBe1920x1080);
        }
    }
}