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
            var elementCoordinates = driver.GetElementCoordinates(_elementByToCut);
            if (!driver.IsElementInViewPort(elementCoordinates.y, elementCoordinates.bottom)) return magickImage;
            var width = magickImage.Width;
            var height = magickImage.Height;
            if (elementCoordinates.y != 0)
                using (var collection = new MagickImageCollection())
                {
                    IMagickImage firstPart = null;
                    IMagickImage secondPart = null;
                    var heightT = 0 + elementCoordinates.y;
                    var firstRectangle = new Rectangle(0, 0, width, heightT);
                    var secondRectangle = new Rectangle(0, elementCoordinates.y + elementCoordinates.height, width,
                        height - elementCoordinates.y - elementCoordinates.height);
                    if (elementCoordinates.y <= 0) firstPart = null;
                    else firstPart = magickImage.Clone(new MagickGeometry(firstRectangle));
                    if (elementCoordinates.bottom > magickImage.Height) secondPart = null;
                    else secondPart = magickImage.Clone(new MagickGeometry(secondRectangle));
                    if (firstPart != null) collection.Add(firstPart);
                    if (secondPart != null) collection.Add(secondPart);
                    var overAllImage = collection.Count == 0 ? null : collection.AppendVertically();
                    return overAllImage == null ? null : new MagickImage(overAllImage);
                }

            var rectangle = new Rectangle(0, elementCoordinates.height, width, height - elementCoordinates.height);
            return magickImage.Clone(new MagickGeometry(rectangle));
        }
    }
}