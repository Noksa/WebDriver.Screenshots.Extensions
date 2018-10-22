using System.Drawing;
using OpenQA.Selenium;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    public abstract class BasicStrategyDecorator : IScreenshotStrategy
    {
        protected BasicStrategyDecorator(IScreenshotStrategy strategy)
        {
            Strategy = strategy;
        }

        public IScreenshotStrategy Strategy { get; }
        public abstract Bitmap MakeScreenshot(IWebDriver driver);
    }
}