using System;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    public class VerticalCombineDecorator : BaseScreenshotDecorator
    {
        private int _firstScreenshotHeight;
        private TimeSpan _waitAfterScroll;

        public VerticalCombineDecorator(IScreenshotStrategy strategy) : base(strategy)
        {
        }

        public override Bitmap MakeScreenshot(IWebDriver driver)
        {
            return CombineScreenshots(driver);
        }

        private void WaitAfterScrolling()
        {
            if (_waitAfterScroll == default) _waitAfterScroll = TimeSpan.FromMilliseconds(50);
            Thread.Sleep(_waitAfterScroll);
        }

        private Bitmap CombineScreenshots(IWebDriver driver)
        {
            var totalHeight = driver.GetHeight(SizesHelper.Entity.Document);
            var totalWidth = driver.GetWidth(SizesHelper.Entity.Document);
            var windowHeight = driver.GetHeight(SizesHelper.Entity.Window);
            var totalScrolls = totalHeight / windowHeight;
            var footer = totalHeight - windowHeight * totalScrolls;

            var combinedImage = new Bitmap(totalWidth, totalHeight);
            totalHeight = 0;
            using (var g = Graphics.FromImage(combinedImage))
            {
                for (var i = 0; i < totalScrolls; i++)
                {
                    driver.ExecuteJavaScript("scrollTo(0, arguments[0])", windowHeight * i);
                    WaitAfterScrolling();
                    var screenshot = Strategy.MakeScreenshot(driver);
                    if (i == 0)
                    {
                        g.DrawImage(screenshot, 0, 0);
                        _firstScreenshotHeight = screenshot.Height;
                    }
                    else
                    {
                        totalHeight = totalHeight + screenshot.Height;
                        g.DrawImage(screenshot, 0, totalHeight);
                    }
                }

                totalHeight = totalHeight + _firstScreenshotHeight;

                if (footer > 0)
                {
                    driver.ExecuteJavaScript("scrollTo(0, document.body.scrollHeight)");
                    WaitAfterScrolling();
                    var screenshot = Strategy.MakeScreenshot(driver);
                    var footerImage = screenshot.Clone(new Rectangle(0, screenshot.Height - footer, totalWidth, footer),
                        screenshot.PixelFormat);
                    totalHeight = totalHeight + footer;
                    g.DrawImage(footerImage, 0, totalHeight - footer);
                }
            }

            return combinedImage.Clone(new Rectangle(0, 0, totalWidth, totalHeight), combinedImage.PixelFormat);
        }

        public VerticalCombineDecorator SetWaitAfterScrolling(TimeSpan timeSpan)
        {
            _waitAfterScroll = timeSpan;
            return this;
        }
    }
}