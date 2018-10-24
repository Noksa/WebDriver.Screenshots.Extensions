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
        public void TestOnlyElementImage1280x720()
        {
            Driver.Manage().Window.Size = new Size(1280, 720);
            Driver.Navigate().GoToUrl(PagePathWithHr);
            var ele = By.Id("hrId");
            var screenMaker = new ScreenshotMaker();
            var onlyEleDecorator = new OnlyElementDecorator(screenMaker);
            onlyEleDecorator.SetElement(ele);
            var arr = Driver.TakeScreenshot(onlyEleDecorator);
            CompareAndTest(arr, Resources.OnlyElementShouldBe1280x720);
        }

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