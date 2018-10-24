

// ReSharper disable InconsistentNaming

using System.Drawing;
using ImageMagick;
using NUnit.Framework;
using OpenQA.Selenium;
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
        public void TestRemoveElementsFromDOM1280x720()
        {
            Driver.Manage().Window.Size = new Size(1280, 720);
            Driver.Navigate().GoToUrl("http://docker.com");
            var screenMaker = new ScreenshotMaker();
            screenMaker.RemoveScrollBarsWhileShooting();
            var arr = Driver.TakeScreenshot(screenMaker);
            new MagickImage(arr).ToBitmap().Save(@"C:\png.png");
        }
    }
}