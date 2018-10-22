using ImageMagick;
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
            var magickImage = strategy.MakeScreenshot(driver);
            return magickImage.ToByteArray();
        }

        public static IMagickImage ToMagickImage(this byte[] arr)
        {
            return new MagickImage(arr);
        }
    }
}