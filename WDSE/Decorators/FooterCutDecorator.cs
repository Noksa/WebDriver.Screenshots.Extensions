using System.Drawing;
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

        public override Bitmap MakeScreenshot(IWebDriver driver)
        {
            return RemoveFooter(driver, Strategy.MakeScreenshot(driver));
        }

        private Bitmap RemoveFooter(IWebDriver driver, Bitmap bmp)
        {
            var height = bmp.Height;
            var width = bmp.Width;
            _footerHeight = GetFooterHeight(driver);
            var rectangle = new Rectangle(0, 0, width, height - _footerHeight);
            var image = bmp.Clone(rectangle, bmp.PixelFormat);
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