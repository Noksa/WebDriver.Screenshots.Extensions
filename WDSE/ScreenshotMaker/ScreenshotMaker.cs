using System.IO;
using ImageMagick;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE.Interfaces;

namespace WDSE.ScreenshotMaker
{
    public class ScreenshotMaker : IScreenshotStrategy
    {
        public IMagickImage MakeScreenshot(IWebDriver driver)
        {
            var screenShot = driver.TakeScreenshot();
            var ms = new MemoryStream(screenShot.AsByteArray);
            return new MagickImage(ms);
        }
    }
}