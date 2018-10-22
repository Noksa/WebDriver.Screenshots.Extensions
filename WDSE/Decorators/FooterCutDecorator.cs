using System.Drawing;
using ImageMagick;
using OpenQA.Selenium;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    internal class FooterCutDecorator : BaseScreenshotDecorator
    {
        #region Private fields

        private IWebElement _footerElement;
        private int _footerHeight;

        #endregion

        #region Ctor

        public FooterCutDecorator(IScreenshotStrategy strategy) : base(strategy)
        {
        }

        #endregion

        #region Override

        public override IMagickImage MakeScreenshot(IWebDriver driver)
        {
            var screen = Strategy.MakeScreenshot(driver);
            return RemoveFooter(driver, screen);
        }

        #endregion

        #region Privates

        private IMagickImage RemoveFooter(IWebDriver driver, IMagickImage magickImage)
        {
            var height = magickImage.Height;
            var width = magickImage.Width;
            _footerHeight = GetFooterHeight(driver);
            if (_footerHeight == 0) return magickImage;
            var rectangle = new Rectangle(0, 0, width, height - _footerHeight);
            var image = magickImage.Clone(new MagickGeometry(rectangle));
            return image;
        }

        private int GetFooterHeight(IWebDriver driver)
        {
            if (_footerElement == null) return _footerHeight;
            var coords = driver.GetElementCoordinates(_footerElement);
            _footerHeight = coords.height;
            return _footerHeight;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets element for remove from the screenshot in the footer. 
        /// </summary>
        /// <param name="ele">Element</param>
        /// <returns></returns>
        public FooterCutDecorator SetFooter(IWebElement ele)
        {
            _footerElement = ele;
            return this;
        }

        /// <summary>
        /// Sets height in pixels for remove from the screenshot in the footer. 
        /// </summary>
        /// <param name="footerHeight">Height</param>
        /// <returns></returns>
        public FooterCutDecorator SetFooter(int footerHeight)
        {
            _footerHeight = footerHeight;
            return this;
        }

        #endregion
    }
}