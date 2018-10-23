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
        #region Private fields

        private IWebElement _element;

        #endregion

        #region Ctor

        public OnlyElementDecorator(IScreenshotStrategy strategy) : base(strategy)
        {
        }

        #endregion

        #region Override

        public override IMagickImage MakeScreenshot(IWebDriver driver)
        {
            return TakeOnlyElementScreenshot(driver, Strategy.MakeScreenshot(driver));
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Sets the element whose screenshot is to be made.
        /// </summary>
        /// <param name="element">Element.</param>
        /// <returns></returns>
        public OnlyElementDecorator SetElement(IWebElement element)
        {
            _element = element;
            return this;
        }

        #endregion

        #region Privates

        private IMagickImage TakeOnlyElementScreenshot(IWebDriver driver, IMagickImage magickImage)
        {
            if (_element == null)
                throw new ArgumentNullException(
                    $"Element is not setted. Before using {nameof(OnlyElementDecorator)}, call the method {nameof(SetElement)}.");
            driver.ScrollToElement(_element);
            var coords = driver.GetElementCoordinates(_element);
            var rectangle = new Rectangle(coords.x, coords.y, coords.width, coords.height);
            var image = magickImage.Clone(new MagickGeometry(rectangle));
            return image;
        }

        #endregion
    }
}