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
        public void Debugging()
        {
            Driver.Manage().Window.Size = new Size(1280, 720);
            Driver.Navigate().GoToUrl("http://pi-test2.iml.ru/setPoint");
            var screenMaker = new ScreenshotMaker();
            screenMaker.RemoveScrollBarsWhileShooting();
            screenMaker.SetElementsToHide(new[]
            {
                By.XPath("(//*[contains(@class, \'phpdebugbar\')]) [1]"),
                By.XPath("(//*[contains(@class, \'phpdebugbar\')]) [2]"),
                By.XPath("(//*[contains(@class, \'phpdebugbar\')]) [3]"),

            });
            var arr = Driver.TakeScreenshot(new VerticalCombineDecorator(screenMaker));
            new MagickImage(arr).ToBitmap().Save(@"C:\png.png");
        }
    }
}