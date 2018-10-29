using System.Collections.Generic;
using System.Linq;
using ImageMagick;
using OpenQA.Selenium;
using WDSE.Decorators;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE
{
    public static class ExtensionMethods
    {
        /// <summary>
        ///     Makes a screenshot of the browser and performs additional actions if decorators are used.
        ///     <para></para>
        ///     For simple screenshot use strategy: new ScreenshotMaker()
        ///     <para></para>
        ///     More info can be founded at github page: https://github.com/Noksa/WebDriver.Screenshots.Extensions
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static byte[] TakeScreenshot(this IWebDriver driver, IScreenshotStrategy strategy)
        {
            driver.CheckJQueryOnPage();
            ScreenshotMaker.ScreenshotMaker screenshotMaker = null;
            switch (strategy)
            {
                case ScreenshotMaker.ScreenshotMaker sm:
                    screenshotMaker = sm;
                    break;
                case BaseScreenshotDecorator baseDecorator:
                    screenshotMaker = GetScrenshotMakerStrategy(baseDecorator);
                    break;
            }

            var magickImage = strategy.MakeScreenshot(driver);
            screenshotMaker?.RestoreAll();
            return magickImage.ToByteArray();
        }

        /// <summary>
        ///     Bytes array to IMagickImage.
        /// </summary>
        /// <param name="arr">Bytes array.</param>
        /// <returns></returns>
        public static IMagickImage ToMagickImage(this byte[] arr)
        {
            return new MagickImage(arr);
        }

        private static IEnumerable<IScreenshotStrategy> GetNestedStrategies(BaseScreenshotDecorator strategy)
        {
            var nestedStrategy = strategy.NestedStrategy;
            if (nestedStrategy == null) yield break;
            do
            {
                yield return nestedStrategy;
                strategy = nestedStrategy as BaseScreenshotDecorator;
                nestedStrategy = strategy?.NestedStrategy;
            } while (nestedStrategy != null);
        }

        internal static ScreenshotMaker.ScreenshotMaker GetScrenshotMakerStrategy(BaseScreenshotDecorator strategy)
        {
            var nestedStrategies = GetNestedStrategies(strategy).ToList();
            if (nestedStrategies.Any())
            {
                var sm = nestedStrategies.FirstOrDefault(q =>
                    q.GetType() == typeof(ScreenshotMaker.ScreenshotMaker));
                if (sm != null)
                {
                    return sm as ScreenshotMaker.ScreenshotMaker;
                }
            }

            return null;
        }
    }
}