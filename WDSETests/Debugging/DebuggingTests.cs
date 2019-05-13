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
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Navigate().GoToUrl("https://www.nordstromrack.com/");
            var screenMaker = new ScreenshotMaker();
            var arr = Driver.TakeScreenshot(new VerticalCombineDecorator(screenMaker));
        }
    }
}