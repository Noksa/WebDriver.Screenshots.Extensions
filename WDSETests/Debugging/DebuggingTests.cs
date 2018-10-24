

// ReSharper disable InconsistentNaming

namespace WDSETests.Debugging
{
#if DEBUG
    [TestFixture(TestName = "DEBUG SUITE")]
    [NonParallelizable]
    public class DebuggingTests : TestsInit
    {

        [Test]
        public void TestRemoveElementsFromDOM1280x720()
        {
            Driver.Manage().Window.Size = new Size(1280, 720);
            Driver.Navigate().GoToUrl("http://docker.com");
            var screenMaker = new ScreenshotMaker();
            screenMaker.SetElementsToHide(new[] { By.Id("floatingContactButton") });
            var vcd = new VerticalCombineDecorator(screenMaker);
            Driver.TakeScreenshot(vcd);
        }
    }
#endif
}