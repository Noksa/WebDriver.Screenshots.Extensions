using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using WDSE;
using WDSE.Decorators;
using WDSE.Decorators.CuttingStrategies;
using WDSE.ScreenshotMaker;
using WDSETests.Properties;

// ReSharper disable InconsistentNaming

namespace WDSETests.VerticalCombineAndElementCuttingTests
{
    [TestFixture(TestName = "Vertical Combine Decorator and Cutter Decorator tests")]
    [NonParallelizable]
    public class VerticalCombineAndElementCuttingTests : TestsInit
    {
        [Test]
        public void TestCutElementWithVcsImageWithPixels1280x720()
        {
            Driver.Manage().Window.Size = new Size(1280, 720);
            Driver.Navigate().GoToUrl(PagePathWithHr);
            var ele = Driver.FindElement(By.Id("hrId"));
            var arr = Driver.TakeScreenshot(new VerticalCombineDecorator(
                new CutterDecorator(new ScreenshotMaker()).SetCuttingStrategy(
                    new CutElementHeightOnEntireWidthThenCombine(ele))));
            CompareAndTest(arr, Resources.VcsEleCuttingShouldBe1280x720);
        }

        [Test]
        public void TestCutElementWithVcsImageWithPixels1920x1080()
        {
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Navigate().GoToUrl(PagePathWithHr);
            var ele = Driver.FindElement(By.Id("hrId"));
            var arr = Driver.TakeScreenshot(new VerticalCombineDecorator(
                new CutterDecorator(new ScreenshotMaker()).SetCuttingStrategy(
                    new CutElementHeightOnEntireWidthThenCombine(ele))));
            CompareAndTest(arr, Resources.VcsEleCuttingShouldBe1920x1080);
        }
    }
}