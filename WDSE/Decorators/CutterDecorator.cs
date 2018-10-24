using System;
using ImageMagick;
using OpenQA.Selenium;
using WDSE.Interfaces;

namespace WDSE.Decorators
{
    public class CutterDecorator : BaseScreenshotDecorator
    {
        #region Private fields

        private ICuttingStrategy _cuttingStrategy;

        #endregion

        #region Ctor

        public CutterDecorator(IScreenshotStrategy strategy) : base(strategy)
        {
        }

        #endregion

        #region Override

        public override IMagickImage MakeScreenshot(IWebDriver driver)
        {
            return Cut(driver, Strategy.MakeScreenshot(driver));
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets cutting strategy for specify how and what be cutting from screenshot.
        /// </summary>
        /// <param name="cuttingStrategy">Cutting strategy</param>
        /// <returns></returns>
        public CutterDecorator SetCuttingStrategy(ICuttingStrategy cuttingStrategy)
        {
            _cuttingStrategy = cuttingStrategy;
            return this;
        }

        #endregion

        #region Privates

        private IMagickImage Cut(IWebDriver driver, IMagickImage magickImage)
        {
            if (_cuttingStrategy == null)
                throw new ArgumentNullException(
                    $"Before using {nameof(CutterDecorator)}, you must specify which cutting strategy to use by calling {nameof(SetCuttingStrategy)} method.");
            return _cuttingStrategy.Cut(driver, magickImage);
        }

        #endregion
    }
}