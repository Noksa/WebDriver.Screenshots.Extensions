using ImageMagick;
using OpenQA.Selenium;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Makes a screenshot of the browser and performs additional actions if decorators are used.
        /// <para></para>
        /// For simple screenshot use strategy: new ScreenshotMaker()
        /// <para></para>
        /// More info can be founded at github page: https://github.com/Noksa/WebDriver.Screenshots.Extensions
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static byte[] TakeScreenshot(this IWebDriver driver, IScreenshotStrategy strategy)
        {
            driver.CheckJQueryOnPage();
            var magickImage = strategy.MakeScreenshot(driver);
            return magickImage.ToByteArray();
        }

        /// <summary>
        /// Bytes array to IMagickImage.
        /// </summary>
        /// <param name="arr">Bytes array.</param>
        /// <returns></returns>
        public static IMagickImage ToMagickImage(this byte[] arr)
        {
            return new MagickImage(arr);
        }
    }
}