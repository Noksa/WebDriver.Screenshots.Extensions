using System.Drawing;
using ImageMagick;
using OpenQA.Selenium;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    public abstract class BaseScreenshotDecorator : IScreenshotStrategy
    {
        protected BaseScreenshotDecorator(IScreenshotStrategy strategy)
        {
            Strategy = strategy;
        }

        public IScreenshotStrategy Strategy { get; }
        public abstract IMagickImage MakeScreenshot(IWebDriver driver);
    }
}