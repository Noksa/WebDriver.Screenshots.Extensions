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
            if (!driver.IsElementPartialInViewPort(elementCoordinates.y, elementCoordinates.bottom)) return magickImage;
            var width = magickImage.Width;
            var height = magickImage.Height;
                using (var collection = new MagickImageCollection())
                {
                    var heightT = 0 + elementCoordinates.y;
                    if (heightT < 0 && height < elementCoordinates.bottom) return null;
                    if (heightT < 0) heightT = elementCoordinates.bottom;
                    var firstRectangle = new Rectangle(0, 0, width, heightT);
                    var secondRectangle = new Rectangle(0, elementCoordinates.y + elementCoordinates.height, width,
                        magickImage.Height - elementCoordinates.y - elementCoordinates.height);
                    var firstPart = elementCoordinates.y <= 0
                        ? null
                        : magickImage.Clone(firstRectangle.ToMagickGeometry());
                    var secondPart = elementCoordinates.bottom > height
                        ? null
                        : magickImage.Clone(secondRectangle.ToMagickGeometry());
                    if (firstPart != null) collection.Add(firstPart);
                    if (secondPart != null) collection.Add(secondPart);
                    if (secondPart == null)
                    {

                    }
                    var overAllImage = collection.Count == 0 ? null : collection.AppendVertically();
                    return overAllImage == null ? null : new MagickImage(overAllImage);
                }

            var rectangle = new Rectangle(0, elementCoordinates.height, width, height - elementCoordinates.height);
            return magickImage.Clone(rectangle.ToMagickGeometry());
        }
    }
}