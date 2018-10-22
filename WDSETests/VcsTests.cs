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

namespace WDSETests
{
    [TestFixture(TestName = "VerticalCombiningStrategy tests")]
    [Parallelizable(ParallelScope.All)]
    public class VcsTests : AllureReport
    {

        private readonly string _pagePath = Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(VcsTests)).Location),
            "Resources/VeryLongScrollPage.html");
        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            KillDriver();
            new DriverManager().SetUpDriver(new ChromeConfig());
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }

        [ThreadStatic] private static IWebDriver _driver;


        private void KillDriver()
        {
            var processes = Process.GetProcessesByName("chromedriver");

            foreach (var process in processes) process.Kill();
        }

        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TestVcsImage1920x1080()
        {
            _driver.Manage().Window.Size = new Size(1920, 1080);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(), new MagickImage(Resources.VcsImageShouldBe1920x1080)));
        }

        [Parallelizable(ParallelScope.Self)]
        [Test]
        public void TestVcsImage1280x720()
        {
            _driver.Manage().Window.Size = new Size(1280, 720);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(), new MagickImage(Resources.VcsImageShouldBe1280x720)));
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void TestBasicImage1920x1080()
        {
            _driver.Manage().Window.Size = new Size(1920, 1080);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new ScreenshotMaker());

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(), new MagickImage(Resources.BasicImageShouldBe1920x1080)));
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void TestBasicImage1280x720()
        {
            _driver.Manage().Window.Size = new Size(1280, 720);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new ScreenshotMaker());

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(), new MagickImage(Resources.BasicImageShouldBe1280x720)));
        }


        [TestCase(1920, 1080, 200)]
        [TestCase(1920, 1080, 0)]
        [TestCase(1280, 720, 200)]
        [TestCase(1280, 720, 0)]
        [Parallelizable(ParallelScope.Self)]
        public void TestCutHeadImage(int width, int height, int cutSize)
        {
            _driver.Manage().Window.Size = new Size(width, height);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new HeadCutDecorator(new ScreenshotMaker()).SetHead(cutSize));
            AllureLifecycle.Instance.AddAttachment("Screen", AllureLifecycle.AttachFormat.ImagePng, arr);
        }

        [TestCase(1920, 1080, 200)]
        [TestCase(1920, 1080, 0)]
        [TestCase(1280, 720, 200)]
        [TestCase(1280, 720, 0)]
        [Parallelizable(ParallelScope.Self)]
        public void TestCutFooterImage(int width, int height, int cutSize)
        {
            _driver.Manage().Window.Size = new Size(width, height);
            _driver.Navigate().GoToUrl(_pagePath);
            var arr = _driver.TakeScreenshot(new FooterCutDecorator(new ScreenshotMaker()).SetFooter(cutSize));
            AllureLifecycle.Instance.AddAttachment("Screen", AllureLifecycle.AttachFormat.ImagePng, arr);
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
    }
}