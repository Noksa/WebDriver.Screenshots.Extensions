using System.Drawing;
using ImageMagick;
using OpenQA.Selenium;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    internal class FooterCutDecorator : BaseScreenshotDecorator
    {
        private IWebElement _footerElement;
        private int _footerHeight;

        public FooterCutDecorator(IScreenshotStrategy strategy) : base(strategy)
        {
        }

        public override IMagickImage MakeScreenshot(IWebDriver driver)
        {
            var screen = Strategy.MakeScreenshot(driver);
            return RemoveFooter(driver, screen);
        }

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

        public FooterCutDecorator SetFooter(IWebElement ele)
        {
            _footerElement = ele;
            return this;
        }

        public FooterCutDecorator SetFooter(int footerHeight)
        {
            _footerHeight = footerHeight;
            return this;
        }

        private int GetFooterHeight(IWebDriver driver)
        {
            if (_footerElement == null) return _footerHeight;
            var coords = driver.GetElementCoordinates(_footerElement);
            _footerHeight = coords.height;
            return _footerHeight;
        }
    }
}