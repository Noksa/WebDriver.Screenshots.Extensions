using System.Drawing;
using ImageMagick;
using OpenQA.Selenium;
using WDSE.Helpers;
using WDSE.Interfaces;

namespace WDSE.Decorators.CuttingStrategies
{
    public class CutElementHeightOnEntireWidthThenCombine : ICuttingStrategy
    {
        private readonly IWebElement _elementToCut;

        public CutElementHeightOnEntireWidthThenCombine(IWebElement ele)
        {
            _elementToCut = ele;
        }

        public IMagickImage Cut(IWebDriver driver, IMagickImage magickImage)
        {
            if (!driver.IsElementInViewPort(_elementToCut)) return magickImage;
            var width = magickImage.Width;
            var height = magickImage.Height;
            var headElementCoords = driver.GetElementCoordinates(_elementToCut);
            if (headElementCoords.y != 0)
            {
                using (var collection = new MagickImageCollection())
                {
                    var firstRectangle = new Rectangle(0, 0, width, 0 + headElementCoords.y);
                    var secondRectangle = new Rectangle(0, headElementCoords.y + headElementCoords.height, width,
                        height - headElementCoords.y - headElementCoords.height);
                    var firstpart = magickImage.Clone(new MagickGeometry(firstRectangle));
                    var secondpart = magickImage.Clone(new MagickGeometry(secondRectangle));
                    collection.Add(firstpart);
                    collection.Add(secondpart);
                    var overAllImage = collection.AppendVertically();
                    return new MagickImage(overAllImage);
                }
            }

            var rectangle = new Rectangle(0, headElementCoords.height, width, height - headElementCoords.height);
            return magickImage.Clone(new MagickGeometry(rectangle));
        }

    }
}
