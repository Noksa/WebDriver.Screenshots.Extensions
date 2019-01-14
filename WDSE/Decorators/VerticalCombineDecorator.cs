using System;
using System.Linq;
using System.Threading;
using ImageMagick;
using OpenQA.Selenium;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    /// <summary>
    ///     Maker of the entire page screenshot.
    /// </summary>
    public class VerticalCombineDecorator : BaseScreenshotDecorator
    {
        #region Private fields

        private TimeSpan _waitAfterScroll;

        #endregion

        #region Ctor

        public VerticalCombineDecorator(IScreenshotStrategy strategy) : base(strategy)
        {
        }

        #endregion

        #region Override

        public override IMagickImage MakeScreenshot(IWebDriver driver)
        {
            return CombineScreenshots(driver);
        }

        #endregion

        #region Public methods

        /// <summary>
        ///     Sets interval to wait after scrolling the page to take the screenshot.
        /// </summary>
        /// <param name="timeSpan">Time interval.</param>
        /// <returns></returns>
        public VerticalCombineDecorator SetWaitAfterScrolling(TimeSpan timeSpan)
        {
            _waitAfterScroll = timeSpan;
            return this;
        }

        #endregion

        #region Privates

        private void WaitAfterScrolling()
        {
            if (_waitAfterScroll == default) _waitAfterScroll = TimeSpan.FromMilliseconds(50);
            Thread.Sleep(_waitAfterScroll);
        }

        private IMagickImage CombineScreenshots(IWebDriver driver)
        {
            var elementWithScrollBar = driver.GetElementWithActiveScrollBar();
            var totalHeight = GetTotalHeight(driver, elementWithScrollBar);

            var windowHeight = driver.GetHeight(SizesHelper.Entity.Window);
            var totalScrolls = totalHeight / windowHeight;
            var footer = totalHeight - windowHeight * totalScrolls;
            using (var imagesCollection = new MagickImageCollection())
            {
                for (var i = 0; i < totalScrolls; i++)
                {
                    driver.ScrollTo(elementWithScrollBar,
                        windowHeight * i);
                    WaitAfterScrolling();
                    var image = NestedStrategy.MakeScreenshot(driver);
                    if (image != null)
                    {
                        image = new MagickImage(image);
                        imagesCollection.Add(image);
                    }
                }

                var realtimeTotalHeight = GetTotalHeight(driver, elementWithScrollBar);

                if (footer > 0)
                {
                    var currentScrollLocation = driver.GetCurrentScrollLocation(elementWithScrollBar);
                    driver.ScrollTo(elementWithScrollBar,
                        totalHeight);
                    WaitAfterScrolling();
                    var afterScrollingScrollLocation = driver.GetCurrentScrollLocation(elementWithScrollBar);
                    var realFooterSize = afterScrollingScrollLocation - currentScrollLocation;
                    var screenshot = new MagickImage(NestedStrategy.MakeScreenshot(driver));
                    var maxSize = imagesCollection.Max(q => q.Height);
                    if (maxSize != screenshot.Height)
                    {
                        realFooterSize = realFooterSize - (maxSize - screenshot.Height);
                    }
                    else
                    {
                        var realtimeTotalHeight2 = GetTotalHeight(driver, elementWithScrollBar);
                        var collapsed = realtimeTotalHeight - realtimeTotalHeight2;
                        if (collapsed > 0) realFooterSize = realFooterSize - collapsed;
                    }

                    if (realFooterSize > 0)
                    {
                        var footerImage = screenshot.Clone(0, screenshot.Height - realFooterSize, screenshot.Width,
                            realFooterSize);
                        imagesCollection.Add(footerImage);
                    }
                }

                var overallImage = imagesCollection.AppendVertically();
                return overallImage;
            }
        }

        private static int GetTotalHeight(IWebDriver driver, IWebElement elementWithScrollBar)
        {
            int totalHeight;
            if (elementWithScrollBar.TagName.ToLower() == "body" ||
                elementWithScrollBar.TagName.ToLower() == "html" ||
                elementWithScrollBar.Equals(driver.GetDocumentScrollingElement()))
                totalHeight = driver.GetHeight(SizesHelper.Entity.Document);
            else
                totalHeight = driver.GetElementScrollBarHeight(elementWithScrollBar);
            return totalHeight;
        }

        #endregion
    }
}