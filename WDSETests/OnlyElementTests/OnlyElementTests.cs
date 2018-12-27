using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;
using WDSETests.Properties;

// ReSharper disable InconsistentNaming

namespace WDSETests.OnlyElementTests
{
    [TestFixture(TestName = "Only element screenshot tests")]
    [NonParallelizable]
    public class OnlyElementTests : TestsInit
    {
        [Test]
        public void TestOnlyElementImage1920x1080()
        {
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Navigate().GoToUrl(PagePathWithHr);
            var ele = By.Id("hrId");
            var arr = Driver.TakeScreenshot(new OnlyElementDecorator(new ScreenshotMaker()).SetElement(ele));
            CompareAndTest(arr, Resources.OnlyElementShouldBe1920x1080);
        }
    }
}