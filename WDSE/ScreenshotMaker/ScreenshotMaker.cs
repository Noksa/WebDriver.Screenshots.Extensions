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
    /// <summary>
    /// <para>The default core strategy.</para>
    /// <para>This class takes a screenshot and has additional settings, such as hiding scrollbars, hiding elements when taking a screenshot, and others.</para>
    /// </summary>
    public class ScreenshotMaker : IScreenshotStrategy
    {
        #region Private fields

        private IWebDriver _driver;
        private List<By> _elementsToRemoveBys;
        private List<IWebElement> _hiddenElements;
        private bool _scrollBarsNeedToBeHidden;

        #endregion

        public IMagickImage MakeScreenshot(IWebDriver driver)
        {
            _driver = driver;
            HideElements().HideScrollBars();
            var screenshot = driver.TakeScreenshot();
            var ms = new MemoryStream(screenshot.AsByteArray);
            RestoreScrollBars().RestoreHiddenElements();
            return new MagickImage(ms);
        }

        /// <summary>
        ///     <para>Sets which elements will be hidden from the DOM before taking the screenshot.</para>
        ///     <para>Elements will be hidden if they are in the viewport.</para>
        ///     <para>After taking the screenshot, the hidden elements will become visible again.</para>
        ///     <para>Be careful, if any of the Bys finds more than one element, they all will be hidden.</para>
        /// </summary>
        /// <param name="Bys">
        ///     Bys collection, how to find the elements that need to be hidden from the DOM until screenshot was
        ///     taken.
        /// </param>
        /// <returns></returns>
        public ScreenshotMaker SetElementsToHide(IEnumerable<By> Bys)
        {
            _elementsToRemoveBys = Bys.ToList();
            return this;
        }

        /// <summary>
        ///     <para>While taking a screenshot, the scrollbars will be hidden.</para>
        ///     <para>After taking the screenshot, the scrollbars will be visible again.</para>
        /// </summary>
        public ScreenshotMaker RemoveScrollBarsWhileShooting()
        {
            _scrollBarsNeedToBeHidden = true;
            return this;
        }


        #region Privates

        private ScreenshotMaker HideElements()
        {
            if (_elementsToRemoveBys != null && _elementsToRemoveBys.Count > 0)
                _elementsToRemoveBys.ForEach(by =>
                {
                    var elements = _driver.GetElementsFromDOM(by).ToList();
                    if (elements.Count > 0)
                        elements.ForEach(element =>
                        {
                            var visibleState = _driver.IsElementInViewPort(element);
                            if (visibleState)
                            {
                                if (_hiddenElements == null) _hiddenElements = new List<IWebElement>();
                                _driver.SetElementHidden(element);
                                _hiddenElements.Add(element);
                            }
                        });
                });

            return this;
        }

        private ScreenshotMaker RestoreHiddenElements()
        {
            _hiddenElements?.ForEach(_driver.SetElementVisible);
            return this;
        }

        private ScreenshotMaker HideScrollBars()
        {
            if (_scrollBarsNeedToBeHidden) _driver.HideScrollBar();
            return this;
        }

        private ScreenshotMaker RestoreScrollBars()
        {
            if (_scrollBarsNeedToBeHidden) _driver.ShowScrollBar();
            return this;
        }

        #endregion
    }
}