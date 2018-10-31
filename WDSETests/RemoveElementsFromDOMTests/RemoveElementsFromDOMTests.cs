using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using WDSE;
using WDSE.Decorators;
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
        public void TestRemoveElementsOneByFromDOM1920x1080()
        {
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Navigate().GoToUrl(PagePathWithHr);
            var screenMaker = new ScreenshotMaker();
            screenMaker.SetElementsToHide(new[] {By.Id("hrId")});
            var arr = Driver.TakeScreenshot(screenMaker);
            CompareAndTest(arr, Resources.RemoveElementShouldBe1920x1080);
        }

        [Test]
        public void TestRemoveTwoElementsOneByFromDOM1920x1080()
        {
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Navigate().GoToUrl(PagePath5Elements);
            var screenMaker = new ScreenshotMaker();
            var by = By.XPath("//*[@id = \'table1\' or @id = \'table4\']");
            screenMaker.SetElementsToHide(new[] {by});
            var vcd = new VerticalCombineDecorator(screenMaker);
            var arr = Driver.TakeScreenshot(vcd);
            CompareAndTest(arr, Resources.RemoveTwoElementShouldBe1920x1080);
        }

        [Test]
        public void TestRemoveTwoElementsTwoBysFromDOM1920x1080()
        {
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Navigate().GoToUrl(PagePath5Elements);
            var screenMaker = new ScreenshotMaker();
            var by = By.Id("table1");
            var by2 = By.Id("table4");
            screenMaker.SetElementsToHide(new[] {by, by2});
            var vcd = new VerticalCombineDecorator(screenMaker);
            var arr = Driver.TakeScreenshot(vcd);
            CompareAndTest(arr, Resources.RemoveTwoElementShouldBe1920x1080);
        }

        [Test]
        public void TestRemoveAllElementsOneByFromDOM1920x1080()
        {
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Navigate().GoToUrl(PagePath5Elements);
            var screenMaker = new ScreenshotMaker();
            var by = By.XPath("//*[contains(@id, \'table\')]");
            screenMaker.SetElementsToHide(new[] {by});
            var vcd = new VerticalCombineDecorator(screenMaker);
            var arr = Driver.TakeScreenshot(vcd);
            CompareAndTest(arr, Resources.RemoveAllElementByOneByShouldBe1920x1080);
        }
    }
}