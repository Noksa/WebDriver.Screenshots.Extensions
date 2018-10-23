using ImageMagick;
using OpenQA.Selenium;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    public abstract class BaseScreenshotDecorator : IScreenshotStrategy
    {
        #region Ctor

        protected BaseScreenshotDecorator(IScreenshotStrategy strategy)
        {
            Strategy = strategy;
        }

        #endregion


        #region Props

        /// <summary>
        /// Nested strategy of screenshoting.
        /// </summary>
        public IScreenshotStrategy Strategy { get; }

        #endregion


        #region Abstract member

        /// <summary>
        /// Method that determines how a screenshot will be processed or created.
        /// </summary>
        /// <param name="driver">Webdriver.</param>
        /// <returns></returns>
        public abstract IMagickImage MakeScreenshot(IWebDriver driver);

        #endregion
    }
}