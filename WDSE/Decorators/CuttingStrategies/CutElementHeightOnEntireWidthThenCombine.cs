using System.Drawing;
using ImageMagick;
using OpenQA.Selenium;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE.Decorators.CuttingStrategies
{
    public class CutElementHeightOnEntireWidthThenCombine : ICuttingStrategy
    {
        private readonly By _elementByToCut;

        /// <summary>
        /// </summary>
        /// <param name="by">How to find element.</param>
        public CutElementHeightOnEntireWidthThenCombine(By by)
        {
            _elementByToCut = by;
        }

        public IMagickImage Cut(IWebDriver driver, IMagickImage magickImage)
        {
            if (!driver.IsElementInViewPort(_elementByToCut)) return magickImage;
            var width = magickImage.Width;
            var height = magickImage.Height;
            var elementCoordinates = driver.GetElementCoordinates(_elementByToCut);
            if (elementCoordinates.y != 0)
                using (var collection = new MagickImageCollection())
                {
                    IMagickImage firstPart = null;
                    IMagickImage secondPart = null;
                    var firstRectangle = new Rectangle(0, 0, width, 0 + elementCoordinates.y);
                    var secondRectangle = new Rectangle(0, elementCoordinates.y + elementCoordinates.height, width,
                        height - elementCoordinates.y - elementCoordinates.height);
                    if (firstRectangle.Height >= 0) firstPart = magickImage.Clone(new MagickGeometry(firstRectangle));
                    if (secondRectangle.Height >= 0) secondPart = magickImage.Clone(new MagickGeometry(secondRectangle));
                    if (firstPart != null) collection.Add(firstPart);
                    if (secondPart != null) collection.Add(secondPart);
                    var overAllImage = collection.AppendVertically();
                    return new MagickImage(overAllImage);
                }

            var rectangle = new Rectangle(0, elementCoordinates.height, width, height - elementCoordinates.height);
            return magickImage.Clone(new MagickGeometry(rectangle));
        }
    }
}