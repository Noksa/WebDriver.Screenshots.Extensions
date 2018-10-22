using System;
using System.Drawing;
using OpenQA.Selenium;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    internal class OnlyElementDecorator : BaseScreenshotDecorator
    {
        private IWebElement _element;

        public OnlyElementDecorator(IScreenshotStrategy strategy) : base(strategy)
        {
        }

        public override Bitmap MakeScreenshot(IWebDriver driver)
        {
            return CutElementFromScreenshot(driver, Strategy.MakeScreenshot(driver));
        }

        private Bitmap CutElementFromScreenshot(IWebDriver driver, Bitmap bmp)
        {
            if (_element == null) throw new ArgumentNullException($"Element is not setted. Before using this decorator, call the method SetElement(IWebElement element).");
            var coords = driver.GetElementCoordinates(_element);
            var rectangle = new Rectangle(coords.x, coords.y, coords.width, coords.height);
            var image = bmp.Clone(rectangle, bmp.PixelFormat);
            return image;
        }

        public OnlyElementDecorator SetElement(IWebElement element)
        {
            _element = element;
            return this;
        }
    }
}