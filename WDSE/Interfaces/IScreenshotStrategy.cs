using ImageMagick;
using OpenQA.Selenium;

namespace WDSE.Interfaces
{
    public interface IScreenshotStrategy
    {
        /// <summary>
        /// Make a screenshot. Create or change.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        IMagickImage MakeScreenshot(IWebDriver driver);
    }
}