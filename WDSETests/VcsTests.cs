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
using WDSE.Decorators.CuttingStrategies;
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

        private readonly string _pagePathWithHr = Path.Combine(
            Path.GetDirectoryName(Assembly.GetAssembly(typeof(VcsTests)).Location) ??
            throw new InvalidOperationException(),
            "Resources/PageWithElements.html");

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
            _driver.Manage().Window.Size = new Size(1920, 1080);
            _driver.Navigate().GoToUrl("http://russian.rt.com");
            var ele = _driver.FindElement(By.ClassName(
                "header__content"));
            var vcs = new VerticalCombineDecorator(new CutterDecorator(new ScreenshotMaker()).SetCuttingStrategy(new CutElementHeightOnEntireWidthThenCombine(ele)));
            var screen = _driver.TakeScreenshot(vcs);
            AllureLifecycle.Instance.AddAttachment("screen", AllureLifecycle.AttachFormat.ImagePng, screen);
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
        public void TestCutElementImageWithPixels1280x720()
        {
            _driver.Manage().Window.Size = new Size(1280, 720);
            _driver.Navigate().GoToUrl(_pagePathWithHr);
            var ele = _driver.FindElement(By.Id("hrId"));
            var arr = _driver.TakeScreenshot(new CutterDecorator(new ScreenshotMaker()).SetCuttingStrategy(new CutElementHeightOnEntireWidthThenCombine(ele)));

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.EleCuttingShouldBe1280x720)));
        }


        [Test]
        public void TestCutElementImageWithPixels1920x1080()
        {
            _driver.Manage().Window.Size = new Size(1920, 1080);
            _driver.Navigate().GoToUrl(_pagePathWithHr);
            var ele = _driver.FindElement(By.Id("hrId"));
            var arr = _driver.TakeScreenshot(new CutterDecorator(new ScreenshotMaker()).SetCuttingStrategy(new CutElementHeightOnEntireWidthThenCombine(ele)));

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.EleCuttingShouldBe1920x1080)));

        }

        [Test]
        public void TestCutElementWithVcsImageWithPixels1920x1080()
        {
            _driver.Manage().Window.Size = new Size(1920, 1080);
            _driver.Navigate().GoToUrl(_pagePathWithHr);
            var ele = _driver.FindElement(By.Id("hrId"));
            var arr = _driver.TakeScreenshot(new VerticalCombineDecorator(new CutterDecorator(new ScreenshotMaker()).SetCuttingStrategy(new CutElementHeightOnEntireWidthThenCombine(ele))));

            AllureLifecycle.Instance.AddAttachment("screen", AllureLifecycle.AttachFormat.ImagePng, arr);
            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.VcsEleCuttingShouldBe1920x1080)));
        }

        [Test]
        public void TestCutElementWithVcsImageWithPixels1280x720()
        {
            _driver.Manage().Window.Size = new Size(1280, 720);
            _driver.Navigate().GoToUrl(_pagePathWithHr);
            var ele = _driver.FindElement(By.Id("hrId"));
            var arr = _driver.TakeScreenshot(new VerticalCombineDecorator(new CutterDecorator(new ScreenshotMaker()).SetCuttingStrategy(new CutElementHeightOnEntireWidthThenCombine(ele))));

            AllureLifecycle.Instance.AddAttachment("screen", AllureLifecycle.AttachFormat.ImagePng, arr);
            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.VcsEleCuttingShouldBe1280x720)));
        }

        [Test]
        public void TestOnlyElementImage1280x720()
        {
            _driver.Manage().Window.Size = new Size(1280, 720);
            _driver.Navigate().GoToUrl(_pagePathWithHr);
            var ele = _driver.FindElement(By.Id("hrId"));
            var arr = _driver.TakeScreenshot(new OnlyElementDecorator(new ScreenshotMaker()).SetElement(ele));
            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.OnlyElementShouldBe1280x720)));
        }

        [Test]
        public void TestOnlyElementImage1920x1080()
        {
            _driver.Manage().Window.Size = new Size(1920, 1080);
            _driver.Navigate().GoToUrl(_pagePathWithHr);
            var ele = _driver.FindElement(By.Id("hrId"));
            var arr = _driver.TakeScreenshot(new OnlyElementDecorator(new ScreenshotMaker()).SetElement(ele));
            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.OnlyElementShouldBe1920x1080)));
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
            var scMaker = new ScreenshotMaker();
            var cutFooterDecorator = new CutterDecorator(scMaker);
            cutFooterDecorator.SetCuttingStrategy(
            new CutElementHeightOnEntireWidthThenCombine(_driver.FindElement(By.Id("footer"))));
            var vcd = new VerticalCombineDecorator(cutFooterDecorator);
            var screenArrBytes = _driver.TakeScreenshot(vcd);

            var arr = _driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));

            Assert.That(WdseImageComparer.Compare(arr.ToMagickImage(),
                new MagickImage(Resources.VcsImageShouldBe1920x1080)));
        }
    }
}