// ReSharper disable InconsistentNaming

using System.Drawing;
using Allure.Commons;
using ImageMagick;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace WDSETests.Debugging
{
    [TestFixture(TestName = "DEBUG SUITE")]
    [NonParallelizable]
    public class DebuggingTests : TestsInit
    {
        [Test]
        public void Debugging()
        {
            Driver.Manage().Window.Size = new Size(1280, 720);
            Driver.Navigate().GoToUrl("http://docker.com");

            var arr = new VerticalCombineDecorator(new ScreenshotMaker());

            var bytesArr = Driver.TakeScreenshot(arr);

            AllureLifecycle.Instance.AddAttachment("Screen", AllureLifecycle.AttachFormat.ImagePng, bytesArr);
        }
    }
}