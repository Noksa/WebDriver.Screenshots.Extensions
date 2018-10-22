using System.Drawing;
using ImageMagick;
using OpenQA.Selenium;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    public class HeadCutDecorator : BaseScreenshotDecorator
    {
        private IWebElement _headElement;
        private int _headHeight;

        public HeadCutDecorator(IScreenshotStrategy strategy) : base(strategy)
        {
        }

        public override IMagickImage MakeScreenshot(IWebDriver driver)
        {
            return CutHead(driver, Strategy.MakeScreenshot(driver));
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

        public HeadCutDecorator SetHead(IWebElement element)
        {
            _headElement = element;
            return this;
        }

        public HeadCutDecorator SetHead(int headHeight)
        {
            _headHeight = headHeight;
            return this;
        }

        private int GetHeadHeight(IWebDriver driver)
        {
            if (_headElement == null) return _headHeight;
            var coords = driver.GetElementCoordinates(_headElement);
            _headHeight = coords.height;
            return _headHeight;
        }
    }
}