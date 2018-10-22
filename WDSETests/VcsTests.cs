using System;
using System.Diagnostics;
using System.Drawing;
using Allure.Commons;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace WDSETests
{
    [TestFixture(TestName = "VerticalCombiningStrategy tests")]
    [Parallelizable(ParallelScope.All)]
    public class VcsTests : AllureReport
    {
        [SetUp]
        public void Setup()
        {
            KillDriver();
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
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

        //[TestCase(1920, 1080, 200)]
        //[TestCase(1920, 1080, 0)]
        //[TestCase(1280, 720, 200)]
        //[TestCase(1280, 720, 0)]
        //[NonParallelizable]
        //public void TestVcsImage(int width, int height, int cutSize)
        //{
        //    _driver.Manage().Window.Size = new Size(width, height);
        //    _driver.Navigate().GoToUrl(Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(VcsTests)).Location), "Resources/VeryLongScrollPage.html"));
        //    var arr = _driver.TakeScreenshot(new VerticalCombiningDecorator(new ScreenshotMaker()));
        //    AllureLifecycle.Instance.AddAttachment("Screen", AllureLifecycle.AttachFormat.ImagePng, arr);
        //}

        //[TestCase(1920, 1080, 0)]
        //[TestCase(1280, 720, 0)]
        //[NonParallelizable]
        //public void TestBasicImage(int width, int height, int cutSize)
        //{
        //    _driver.Manage().Window.Size = new Size(width, height);
        //    _driver.Navigate().GoToUrl(Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(VcsTests)).Location), "Resources/VeryLongScrollPage.html"));
        //    var arr = _driver.TakeScreenshot(new ScreenshotMaker());
        //    AllureLifecycle.Instance.AddAttachment("Screen", AllureLifecycle.AttachFormat.ImagePng, arr);
        //}

        //[TestCase(1920, 1080, 200)]
        //[TestCase(1920, 1080, 0)]
        //[TestCase(1280, 720, 200)]
        //[TestCase(1280, 720, 0)]
        //[NonParallelizable]
        //public void TestCutHeadImage(int width, int height, int cutSize)
        //{
        //    _driver.Manage().Window.Size = new Size(width, height);
        //    _driver.Navigate().GoToUrl(Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(VcsTests)).Location), "Resources/VeryLongScrollPage.html"));
        //    var arr = _driver.TakeScreenshot(new HeadCutterDecorator());
        //    AllureLifecycle.Instance.AddAttachment("Screen", AllureLifecycle.AttachFormat.ImagePng, arr);
        //}

        [Test]
        public void Debugging()
        {
            _driver.Manage().Window.Size = new Size(1920, 1080);
            _driver.Navigate().GoToUrl("http://software-testing.ru");
            var vcs = new VerticalCombiningDecorator(new FootCutterDecorator(new ScreenshotMaker()).SetFooter(100));
            var screen = _driver.TakeScreenshot(vcs);
            AllureLifecycle.Instance.AddAttachment("screen", AllureLifecycle.AttachFormat.ImagePng, screen);
        }
    }
}