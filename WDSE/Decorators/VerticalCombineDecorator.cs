using System;
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
            int totalHeight;
            var beforeActionsDocumentHeight = driver.GetHeight(SizesHelper.Entity.Document);
            var elementWithScrollBar = driver.GetElementWithActiveScrollBar();
            if (elementWithScrollBar.TagName.ToLower() == "body" ||
                elementWithScrollBar.TagName.ToLower() == "html" || elementWithScrollBar.Equals(driver.GetDocumentScrollingElement()))
            {
                totalHeight = driver.GetHeight(SizesHelper.Entity.Document);
            }
            else
            {
                totalHeight = driver.GetElementScrollBarHeight(elementWithScrollBar);
            }

            var totalWidth = driver.GetWidth(SizesHelper.Entity.Document);
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
                    var screenshot = new MagickImage(NestedStrategy.MakeScreenshot(driver));
                    imagesCollection.Add(screenshot);
                }

                if (footer > 0)
                {
                    var actualDocumentHeight = driver.GetHeight(SizesHelper.Entity.Document);
                    if (actualDocumentHeight != beforeActionsDocumentHeight &&
                        actualDocumentHeight < beforeActionsDocumentHeight)
                        footer = footer - (beforeActionsDocumentHeight - actualDocumentHeight);

                    driver.ScrollTo(elementWithScrollBar,
                        totalHeight);
                    WaitAfterScrolling();
                    var screenshot = new MagickImage(NestedStrategy.MakeScreenshot(driver));
                    var footerImage = screenshot.Clone(0, screenshot.Height - footer, totalWidth, footer);
                    imagesCollection.Add(footerImage);
                }

                var overAllImage = imagesCollection.AppendVertically();
                return overAllImage;
            }
        }

        #endregion
    }
}