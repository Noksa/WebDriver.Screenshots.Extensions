using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using WDSE;
using WDSE.Helpers;
using WDSE.ScreenshotMaker;
using WDSETests.Properties;

// ReSharper disable InconsistentNaming

namespace WDSETests.RemoveElementsFromDOMTests
{
    [TestFixture(TestName = "Remove elements from DOM tests")]
    [NonParallelizable]
    public class RemoveElementsFromDOMTests : TestsInit
    {
        [Test]
        public void TestRemoveElementsFromDOM1280x720()
        {
            Driver.Manage().Window.Size = new Size(1280, 720);
            Driver.Navigate().GoToUrl(PagePathWithHr);
            var screenMaker = new ScreenshotMaker();
            var by = By.Id("hrId");
            screenMaker.SetElementsToHide(new[] { by });
            var arr = Driver.TakeScreenshot(screenMaker);
            CompareAndTest(arr, Resources.RemoveElementShouldBe1280x720);
        }

        [Test]
        public void TestRemoveElementsFromDOM1920x1080()
        {
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Navigate().GoToUrl(PagePathWithHr);
            var screenMaker = new ScreenshotMaker();
            screenMaker.SetElementsToHide(new[] { By.Id("hrId") });
            var arr = Driver.TakeScreenshot(screenMaker);
            CompareAndTest(arr, Resources.RemoveElementShouldBe1920x1080);
        }
    }
}