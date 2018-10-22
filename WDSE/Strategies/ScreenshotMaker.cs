using System.Drawing;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE.Interfaces;

namespace WDSE.Strategies
{
    public class ScreenshotMaker : IScreenshotStrategy
    {
        public Bitmap MakeScreenshot(IWebDriver driver)
        {
            var screenShot = driver.TakeScreenshot();
            var ms = new MemoryStream(screenShot.AsByteArray);
            return new Bitmap(ms);
        }
    }
}