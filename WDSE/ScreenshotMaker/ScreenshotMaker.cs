using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImageMagick;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE.Helpers;
using WDSE.Interfaces;

// ReSharper disable InconsistentNaming

namespace WDSE.ScreenshotMaker
{
    public class ScreenshotMaker : IScreenshotStrategy
    {
        private List<By> _elementsToRemoveBys;

        public IMagickImage MakeScreenshot(IWebDriver driver)
        {
            List<IWebElement> hiddenElements = null;
            if (_elementsToRemoveBys != null && _elementsToRemoveBys.Count > 0)
            {
                _elementsToRemoveBys.ForEach(by =>
                {
                    var element = driver.GetElementFromDOM(by);
                    if (element != null)
                    {
                        if (hiddenElements == null) hiddenElements = new List<IWebElement>();
                        var visibleState = driver.IsElementInViewPort(element);
                        if (visibleState)
                        {
                            driver.SetElementHidden(element);
                            hiddenElements.Add(element);
                        }
                    }
                });
            }

            var screenshot = driver.TakeScreenshot();
            var ms = new MemoryStream(screenshot.AsByteArray);

            hiddenElements?.ForEach(driver.SetElementVisible);

            return new MagickImage(ms);
        }

        /// <summary>
        /// <para>Method sets which elements will be hidden from the DOM before taking the screenshot.</para>
        /// <para>Elements will be hidden if they are in the viewport.</para>
        /// <para>After taking the screenshot, the hidden elements will become visible again.</para>
        /// </summary>
        /// <param name="Bys">Bys collection, how to find the elements that need to be hidden from the DOM until screenshot was taken.</param>
        /// <returns></returns>
        public ScreenshotMaker SetElementsToHide(IEnumerable<By> Bys)
        {
            _elementsToRemoveBys = Bys.ToList();
            return this;
        }
    }
}