using System.Drawing;
using OpenQA.Selenium;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE
{
    public static class ExtensionMethods
    {
        public static byte[] TakeScreenshot(this IWebDriver driver, IScreenshotStrategy strategy)
        {
            driver.CheckJQueryOnPage();
            var bitmap = strategy.MakeScreenshot(driver);
            var converter = new ImageConverter();
            var arr = (byte[]) converter.ConvertTo(bitmap, typeof(byte[]));
            return arr;
        }
    }
}