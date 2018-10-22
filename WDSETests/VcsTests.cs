using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using Allure.Commons;
using ImageMagick;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WDSE;
using WDSE.Compare;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;
using WDSETests.Properties;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

// ReSharper disable InconsistentNaming

namespace WDSETests
{
    [TestFixture(TestName = "VerticalCombiningStrategy tests")]
    [NonParallelizable]
    public class VcsTests : AllureReport
    {
        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("--headless");
            _driver = new ChromeDriver(chromeOptions);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }

        private readonly string _pagePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetAssembly(typeof(VcsTests)).Location) ??
            throw new InvalidOperationException(),
            "Resources/VeryLongScrollPage.html");

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            KillDriver();
            new DriverManager().SetUpDriver(new ChromeConfig());
        }

        [ThreadStatic] private static IWebDriver _driver;


        private void KillDriver()
        {
            var processes = Process.GetProcessesByName("chromedriver");

            foreach (var process in processes) process.Kill();
        }

        [Test]
        public void Debugging()
        {
            var diff = WdseImageComparer.CompareAndGetImage("C:\\diff1.png", "C:\\diff1.png");
            AllureLifecycle.Instance.AddAttachment("screen", AllureLifecycle.AttachFormat.ImagePng, diff);
            //_driver.Manage().Window.Size = new Size(1920, 1080);
            //_driver.Navigate().GoToUrl("http://docker.com");
            //var vcs = new VerticalCombineDecorator(new ScreenshotMaker());
            //var screen = _driver.TakeScreenshot(vcs);
            //AllureLifecycle.Instance.AddAttachment("screen", AllureLifecycle.AttachFormat.ImagePng, screen);
        }

        [Test]
        public void TestBasicImage1280x720()
        {
            _driver.Manage().Window.Size = new Size(1280, 720);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new ScreenshotMaker());

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.BasicImageShouldBe1280x720)));
        }

        [Test]
        public void TestBasicImage1920x1080()
        {
            _driver.Manage().Window.Size = new Size(1920, 1080);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new ScreenshotMaker());

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.BasicImageShouldBe1920x1080)));
        }

        [Test]
        public void TestCutFooterCutHeaderAndCombine1280x720()
        {
            _driver.Manage().Window.Size = new Size(1280, 720);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new VerticalCombineDecorator(
                    new HeadCutDecorator(new FooterCutDecorator(new ScreenshotMaker()).SetFooter(100)).SetHead(100))
                .SetWaitAfterScrolling(TimeSpan.FromMilliseconds(200)));

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.AllInOneShouldBe1280x720)));
        }

        [Test]
        public void TestCutFooterCutHeaderAndCombine1920x1080()
        {
            _driver.Manage().Window.Size = new Size(1920, 1080);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new VerticalCombineDecorator(
                    new HeadCutDecorator(new FooterCutDecorator(new ScreenshotMaker()).SetFooter(100)).SetHead(100))
                .SetWaitAfterScrolling(TimeSpan.FromMilliseconds(200)));

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.AllInOneShouldBe1920x1080)));
        }


        [Test]
        public void TestCutFooterImageWithPixels1280x720()
        {
            _driver.Manage().Window.Size = new Size(1280, 720);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new FooterCutDecorator(new ScreenshotMaker()).SetFooter(150));
            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.FooterCutImageShouldBe1280x720)));
        }

        [Test]
        public void TestCutFooterImageWithPixels1920x1080()
        {
            _driver.Manage().Window.Size = new Size(1920, 1080);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new FooterCutDecorator(new ScreenshotMaker()).SetFooter(150));
            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.FooterCutImageShouldBe1920x1080)));
        }

        [Test]
        public void TestCutHeadImageWithPixels1280x720()
        {
            _driver.Manage().Window.Size = new Size(1280, 720);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new HeadCutDecorator(new ScreenshotMaker()).SetHead(100));

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.HeadCutImageShouldBe1280x720)));
        }


        [Test]
        public void TestCutHeadImageWithPixels1920x1080()
        {
            _driver.Manage().Window.Size = new Size(1920, 1080);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new HeadCutDecorator(new ScreenshotMaker()).SetHead(100));

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.HeadCutImageShouldBe1920x1080)));
        }

        [Test]
        public void TestVcsImage1280x720()
        {
            _driver.Manage().Window.Size = new Size(1280, 720);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.VcsImageShouldBe1280x720)));
        }

        [Test]
        public void TestVcsImage1920x1080()
        {
            _driver.Manage().Window.Size = new Size(1920, 1080);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.VcsImageShouldBe1920x1080)));
        }
    }
}