using System.Drawing;
using OpenQA.Selenium;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    public class HeadCutterDecorator : BasicStrategyDecorator
    {
        private IWebElement _headElement;
        private int _headHeight;

        public HeadCutterDecorator(IScreenshotStrategy strategy) : base(strategy)
        {
        }

        public override Bitmap MakeScreenshot(IWebDriver driver)
        {
            return CutHead(driver, Strategy.MakeScreenshot(driver));
        }


        private Bitmap CutHead(IWebDriver driver, Bitmap bmp)
        {
            var height = bmp.Height;
            var width = bmp.Width;
            _headHeight = GetHeadHeight(driver);
            var rectangle = new Rectangle(0, _headHeight, width, height - _headHeight);
            return bmp.Clone(rectangle, bmp.PixelFormat);
        }

        public HeadCutterDecorator SetHead(IWebElement element)
        {
            _headElement = element;
            return this;
        }

        public HeadCutterDecorator SetHead(int headHeight)
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