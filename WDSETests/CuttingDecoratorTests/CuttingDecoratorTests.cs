using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using WDSE;
using WDSE.Decorators;
using WDSE.Decorators.CuttingStrategies;
using WDSE.ScreenshotMaker;
using WDSETests.Properties;

// ReSharper disable InconsistentNaming

namespace WDSETests.CuttingDecoratorTests
{
    [TestFixture(TestName = "Cutting decorator tests")]
    [NonParallelizable]
    public class CuttingDecoratorTests : TestsInit
    {
        [Test]
        public void TestCutBigElement1920x1080()
        {
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Url = PagePathWithBigElement;
            var scMaker = new ScreenshotMaker();
            var cut = new CutterDecorator(scMaker).SetCuttingStrategy(
                new CutElementHeightOnEntireWidthThenCombine(By.Id("hrId")));
            var vcd = new VerticalCombineDecorator(cut);
            var screenArrBytes = Driver.TakeScreenshot(vcd);
            CompareAndTest(screenArrBytes, Resources.RemoveBigElementShouldBe);
        }

        [Test]
        public void TestCutElementImageWithPixels1920x1080()
        {
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Navigate().GoToUrl(PagePathWithHr);
            var by = By.Id("hrId");
            var arr = Driver.TakeScreenshot(
                new CutterDecorator(new ScreenshotMaker()).SetCuttingStrategy(
                    new CutElementHeightOnEntireWidthThenCombine(by)));
            CompareAndTest(arr, Resources.EleCuttingShouldBe1920x1080);
        }
    }
}