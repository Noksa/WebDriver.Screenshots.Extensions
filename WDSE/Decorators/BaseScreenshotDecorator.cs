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
            NestedStrategy = strategy;
        }

        #endregion


        #region Props

        /// <summary>
        ///     Nested strategy, if have. Else is null.
        /// </summary>
        public IScreenshotStrategy NestedStrategy { get; }

        #endregion


        #region Abstract member

        public abstract IMagickImage MakeScreenshot(IWebDriver driver);

        #endregion
    }
}