using NUnit.Framework;

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
            //Driver.Navigate().GoToUrl("https://www.nordstromrack.com");
            //var sm = new VerticalCombineDecorator(new ScreenshotMaker());

            //var screenshot = Driver.TakeScreenshot(sm);
        }
    }
#endif
}