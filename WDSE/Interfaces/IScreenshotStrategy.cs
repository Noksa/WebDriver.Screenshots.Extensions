using ImageMagick;
using OpenQA.Selenium;

namespace WDSE.Interfaces
{
    public interface IScreenshotStrategy
    {
        IMagickImage MakeScreenshot(IWebDriver driver);
    }
}