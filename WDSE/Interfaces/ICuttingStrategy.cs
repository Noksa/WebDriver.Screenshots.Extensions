using ImageMagick;
using OpenQA.Selenium;

namespace WDSE.Interfaces
{
    public interface ICuttingStrategy
    {
        IMagickImage Cut(IWebDriver driver, IMagickImage magickImage);
    }
}