using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using WDSE;
using WDSE.Decorators;
using WDSE.Decorators.CuttingStrategies;
using WDSE.ScreenshotMaker;

namespace WDSETests.Debugging
{
#if DEBUG
    [TestFixture(TestName = "DEBUG SUITE")]
    [NonParallelizable]
    public class DebuggingTests : TestsInit
    {
        [Test]
        public void Debugging()
        {
            Driver.Manage().Window.Maximize();
            Driver.Url = "https://edition.cnn.com/entertainment";
            var scMaker = new ScreenshotMaker();
            var cutFooterDecorator = new CutterDecorator(scMaker);
            cutFooterDecorator.SetCuttingStrategy(new CutElementHeightOnEntireWidthThenCombine(By.Id("footer")));
            var vcd = new VerticalCombineDecorator(cutFooterDecorator);
            var screenArrBytes = Driver.TakeScreenshot(vcd);
        }
    }
#endif
}