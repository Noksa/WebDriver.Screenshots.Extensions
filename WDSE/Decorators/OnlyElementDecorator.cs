using System;
using System.Drawing;
using ImageMagick;
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

        public override IMagickImage MakeScreenshot(IWebDriver driver)
        {
            return CutElementFromScreenshot(driver, Strategy.MakeScreenshot(driver));
        }

        private IMagickImage CutElementFromScreenshot(IWebDriver driver, IMagickImage magickImage)
        {
            if (_element == null)
                throw new ArgumentNullException(
                    $"Element is not setted. Before using this decorator, call the method SetElement(IWebElement element).");
            var coords = driver.GetElementCoordinates(_element);
            var rectangle = new Rectangle(coords.x, coords.y, coords.width, coords.height);
            var image = magickImage.Clone(new MagickGeometry(rectangle));
            return image;
        }

        public OnlyElementDecorator SetElement(IWebElement element)
        {
            _element = element;
            return this;
        }
    }
}