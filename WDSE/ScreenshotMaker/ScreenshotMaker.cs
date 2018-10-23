using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImageMagick;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE.Helpers;
using WDSE.Interfaces;
using WDSE.Properties;

// ReSharper disable InconsistentNaming

namespace WDSE.ScreenshotMaker
{
    public class ScreenshotMaker : IScreenshotStrategy
    {
        private List<IWebElement> _elementsToRemove;

        public IMagickImage MakeScreenshot(IWebDriver driver)
        {
            if (_elementsToRemove != null && _elementsToRemove.Count > 0)
            {
                _elementsToRemove.ForEach(element =>
                {
                    var visibleState = driver.IsElementInViewPort(element);
                    if (visibleState)
                    {
                        driver.ExecuteJavaScript(Resources.RemoveElementFromDOM, element);
                    }
                });
            }

            var screenshot = driver.TakeScreenshot();
            var ms = new MemoryStream(screenshot.AsByteArray);
            return new MagickImage(ms);
        }

        public ScreenshotMaker RemoveElementsFromDOM(IEnumerable<IWebElement> elementsToRemove)
        {
            _elementsToRemove = elementsToRemove.ToList();
            return this;
        }
    }
}