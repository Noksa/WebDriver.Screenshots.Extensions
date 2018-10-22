using System.Drawing;
using ImageMagick;
using OpenQA.Selenium;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    public class HeadCutDecorator : BaseScreenshotDecorator
    {
        #region Private fields

        private IWebElement _headElement;
        private int _headHeight;

        #endregion

        #region Ctor

        public HeadCutDecorator(IScreenshotStrategy strategy) : base(strategy)
        {
        }

        #endregion

        #region Override

        public override IMagickImage MakeScreenshot(IWebDriver driver)
        {
            return CutHead(driver, Strategy.MakeScreenshot(driver));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets element for remove from the screenshot in the top. 
        /// </summary>
        /// <param name="ele">Element</param>
        /// <returns></returns>
        public HeadCutDecorator SetHead(IWebElement ele)
        {
            _headElement = ele;
            return this;
        }

        /// <summary>
        /// Sets height for remove from the screenshot in the top. 
        /// </summary>
        /// <param name="headHeight">Height</param>
        /// <returns></returns>
        public HeadCutDecorator SetHead(int headHeight)
        {
            _headHeight = headHeight;
            return this;
        }

        #endregion

        #region Privates

        private int GetHeadHeight(IWebDriver driver)
        {
            if (_headElement == null) return _headHeight;
            var coords = driver.GetElementCoordinates(_headElement);
            _headHeight = coords.height;
            return _headHeight;
        }

        private IMagickImage CutHead(IWebDriver driver, IMagickImage magickImage)
        {
            var height = magickImage.Height;
            var width = magickImage.Width;
            _headHeight = GetHeadHeight(driver);
            if (_headHeight == 0) return magickImage;
            var rectangle = new Rectangle(0, _headHeight, width, height - _headHeight);
            return magickImage.Clone(new MagickGeometry(rectangle));
        }

        #endregion
    }
}