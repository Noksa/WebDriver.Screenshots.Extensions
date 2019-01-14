using System.Drawing;
using NUnit.Framework;
using WDSE;
using WDSE.Decorators;
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
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Url = "https://edition.cnn.com/entertainment";
            var scMaker = new ScreenshotMaker();
            var vcd = new VerticalCombineDecorator(scMaker);
            var screenArrBytes = Driver.TakeScreenshot(vcd);
        }
    }
#endif
}