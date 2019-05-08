// ReSharper disable InconsistentNaming

using System.Drawing;
using ImageMagick;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace WDSETests.Debugging
{
    [TestFixture(TestName = "DEBUG SUITE")]
    [NonParallelizable]
    public class DebuggingTests : TestsInit
    {
        [Test]
        public void Debugging()
        {
            //Driver.Manage().Window.Maximize();
            Driver.Manage().Window.Size = new Size(640, 480);
            Driver.Navigate().GoToUrl("http://docker.com");
            var ele = Driver.FindElement(By.TagName("html"));
            var screenMaker = new ScreenshotMaker();
            var arr = Driver.TakeScreenshot(new VerticalCombineDecorator(screenMaker));
        }
    }
}