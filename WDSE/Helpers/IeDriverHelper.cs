using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE.Properties;

namespace WDSE.Helpers
{
    internal static class IeDriverHelper
    {
        private const string Script = "return document.scrollingElement";

        public static void CheckIeDriver(this IWebDriver driver)
        {
            if (driver is OpenQA.Selenium.IE.InternetExplorerDriver)
            {
                var scrollingElement = driver.ExecuteJavaScript<IWebElement>(Script);
                if (scrollingElement != null) return;
                driver.ExecuteJavaScript(Resources.ScrollingElement);
                var sw = Stopwatch.StartNew();
                while (sw.Elapsed.TotalSeconds <= 10)
                {
                    scrollingElement = driver.ExecuteJavaScript<IWebElement>(Script);
                    if (scrollingElement != null) return;
                }
                throw new Exception("Cant get scrolling element at document.");
            }
        }
    }
}