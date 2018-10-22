using System.Drawing;
using OpenQA.Selenium;

namespace WDSE.Interfaces
{
    public interface IScreenshotStrategy
    {
        Bitmap MakeScreenshot(IWebDriver driver);
    }
}