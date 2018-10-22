using System.Drawing;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE.Interfaces;
using WDSE.Json;
using WDSE.Properties;

namespace WDSE.Decorators
{
    internal class FootCutterDecorator : BasicStrategyDecorator
    {
        private IWebElement _footerElement;
        private int _footerHeight;

        public FootCutterDecorator(IScreenshotStrategy strategy) : base(strategy)
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

        public FootCutterDecorator SetFooter(IWebElement ele)
        {
            _footerElement = ele;
            return this;
        }

        public FootCutterDecorator SetFooter(int footerHeight)
        {
            _footerHeight = footerHeight;
            return this;
        }

        private int GetFooterHeight(IWebDriver driver)
        {
            if (_footerElement == null) return _footerHeight;
            var coords =
                JsonConvert.DeserializeObject<ElementCoords>(
                    driver.ExecuteJavaScript<string>(Resources.GetElementCoordinates, _footerElement));
            _footerHeight = coords.height;

            return _footerHeight;
        }
    }
}