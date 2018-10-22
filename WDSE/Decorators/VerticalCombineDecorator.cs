﻿using System;
using System.Threading;
using ImageMagick;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    public class VerticalCombineDecorator : BaseScreenshotDecorator
    {
        private TimeSpan _waitAfterScroll;

        public VerticalCombineDecorator(IScreenshotStrategy strategy) : base(strategy)
        {
        }

        public override IMagickImage MakeScreenshot(IWebDriver driver)
        {
            return CombineScreenshots(driver);
        }

        private void WaitAfterScrolling()
        {
            if (_waitAfterScroll == default) _waitAfterScroll = TimeSpan.FromMilliseconds(50);
            Thread.Sleep(_waitAfterScroll);
        }

        private IMagickImage CombineScreenshots(IWebDriver driver)
        {
            var totalHeight = driver.GetHeight(SizesHelper.Entity.Document);
            var totalWidth = driver.GetWidth(SizesHelper.Entity.Document);
            var windowHeight = driver.GetHeight(SizesHelper.Entity.Window);
            var totalScrolls = totalHeight / windowHeight;
            var footer = totalHeight - windowHeight * totalScrolls;

            using (var imagesCollection = new MagickImageCollection())
            {
                for (var i = 0; i < totalScrolls; i++)
                {
                    driver.ExecuteJavaScript("scrollTo(0, arguments[0])", windowHeight * i);
                    WaitAfterScrolling();
                    var screenshot = new MagickImage(Strategy.MakeScreenshot(driver));
                    imagesCollection.Add(screenshot);
                }

                if (footer > 0)
                {
                    driver.ExecuteJavaScript("scrollTo(0, document.body.scrollHeight)");
                    WaitAfterScrolling();
                    var screenshot = new MagickImage(Strategy.MakeScreenshot(driver));
                    var footerImage = screenshot.Clone(0, screenshot.Height - footer, totalWidth, footer);
                    footerImage.ToBitmap().Save(@"C:\footer.png");
                    imagesCollection.Add(footerImage);
                }

                var overAllImage = imagesCollection.AppendVertically();
                return overAllImage;
            }
        }

        public VerticalCombineDecorator SetWaitAfterScrolling(TimeSpan timeSpan)
        {
            _waitAfterScroll = timeSpan;
            return this;
        }
    }
}