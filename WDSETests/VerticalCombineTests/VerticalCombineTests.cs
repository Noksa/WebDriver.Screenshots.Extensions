using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;
using WDSETests.Properties;

// ReSharper disable InconsistentNaming

namespace WDSETests.VerticalCombineTests
{
    [TestFixture(TestName = "Vertical Combine Decorator tests")]
    [NonParallelizable]
    public class VerticalCombineTests : TestsInit
    {
        [Test]
        public void TestOnlyElementImage1280x720()
        {
            Driver.Manage().Window.Size = new Size(1280, 720);
            Driver.Navigate().GoToUrl(PagePathWithHr);
            var ele = Driver.FindElement(By.Id("hrId"));
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
            var ele = Driver.FindElement(By.Id("hrId"));
            var arr = Driver.TakeScreenshot(new OnlyElementDecorator(new ScreenshotMaker()).SetElement(ele));
            CompareAndTest(arr, Resources.OnlyElementShouldBe1920x1080);
        }


        [Test]
        public void TestVcsImage1280x720()
        {
            Driver.Manage().Window.Size = new Size(1280, 720);
            Driver.Navigate().GoToUrl(PagePath);
            var arr = Driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));
            CompareAndTest(arr, Resources.VcsImageShouldBe1280x720);
        }

        [Test]
        public void TestVcsImage1920x1080()
        {
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Navigate().GoToUrl(PagePath);
            var arr = Driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));
            CompareAndTest(arr, Resources.VcsImageShouldBe1920x1080);
        }
    }
}