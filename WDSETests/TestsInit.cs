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
using OpenQA.Selenium.IE;
using WDSE;
using WDSE.Compare;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace WDSETests
{
    public abstract class TestsInit : AllureReport
    {
        [ThreadStatic] private static IWebDriver _driver;

        protected readonly string PagePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetAssembly(typeof(VerticalCombineTests.VerticalCombineTests)).Location) ??
            throw new InvalidOperationException(),
            "Resources\\VeryLongScrollPage.html");

        protected readonly string PagePath5Elements = Path.Combine(
            Path.GetDirectoryName(Assembly.GetAssembly(typeof(VerticalCombineTests.VerticalCombineTests)).Location) ??
            throw new InvalidOperationException(),
            "Resources\\PageWithFiveElements.html");

        protected readonly string PagePathWithHr = Path.Combine(
            Path.GetDirectoryName(Assembly.GetAssembly(typeof(VerticalCombineTests.VerticalCombineTests)).Location) ??
            throw new InvalidOperationException(),
            "Resources\\PageWithElements.html");

        protected static IWebDriver Driver => _driver;

        [SetUp]
        public void Setup()
        {
            //var ieOptions = new InternetExplorerOptions();
            //var chromeOptions = new ChromeOptions();
            //_driver = new InternetExplorerDriver();
            _driver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            KillDriver();
            new DriverManager().SetUpDriver(new ChromeConfig());
        }

        private void KillDriver()
        {
            var processes = Process.GetProcessesByName("chromedriver");

            foreach (var process in processes) process.Kill();
        }

        protected void CompareAndTest(byte[] actual, Bitmap expected)
        {
            var image = WdseImageComparer.CompareAndGetImageIfNotSame(actual.ToMagickImage(),
                new MagickImage(expected));
            if (image == null) return;
            AllureLifecycle.Instance.AddAttachment("Difference", AllureLifecycle.AttachFormat.ImagePng, image);
            AllureLifecycle.Instance.AddAttachment("Actual", AllureLifecycle.AttachFormat.ImagePng, actual);
            AllureLifecycle.Instance.AddAttachment("Expected", AllureLifecycle.AttachFormat.ImagePng,
                new MagickImage(expected).ToByteArray());
            throw new AssertionException("Test failed");
        }
    }
}