using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE.Properties;

// ReSharper disable InconsistentNaming

namespace WDSE.Helpers
{
    public static class JQueryHelper
    {
        internal static void CheckJQueryOnPage(this IWebDriver driver)
        {
            var script = Resources.SetJQuery;
            try
            {
                _ = driver.ExecuteJavaScript<long>("return $(document).outerHeight()");
            }
            catch (WebDriverException)
            {
                driver.ExecuteJavaScript(script);
                var sw = new Stopwatch();
                sw.Start();
                do
                {
                    try
                    {
                        _ = driver.ExecuteJavaScript<long>("return $(document).outerHeight()");
                        return;
                    }
                    catch (WebDriverException) 
                    {
                        Thread.Sleep(10);
                    }
                    finally
                    {
                        sw.Stop();
                    }
                } while (sw.Elapsed.TotalSeconds <= 5);
            }
        }

        internal static bool IsElementPartialInViewPort(this IWebDriver driver, int y, int elementBottom)
        {
            var result = driver.ExecuteJavaScript<bool>(Resources.GetElementPartialVisibleState, y, elementBottom);
            return result;
        }

        internal static bool IsElementInViewPort(this IWebDriver driver, int y, int elementBottom)
        {
            var result = driver.ExecuteJavaScript<bool>(Resources.GetElementVisibleState, y, elementBottom);
            return result;
        }

        internal static void ScrollToElement(this IWebDriver driver, By by)
        {
            var element = driver.GetElementFromDOM(by, true);
            driver.ExecuteJavaScript(Resources.ScrollToElement, element);
        }

        internal static IWebElement GetElementFromDOM(this IWebDriver driver, By by, bool throwEx = false)
        {
            try
            {
                var ele = driver.FindElement(by);
                return ele;
            }
            catch (NoSuchElementException)
            {
                if (throwEx) throw;
                return null;
            }
        }

        internal static IEnumerable<IWebElement> GetElementsFromDOM(this IWebDriver driver, By by, bool throwEx = false)
        {
            try
            {
                var ele = driver.FindElements(by);
                return ele;
            }
            catch (NoSuchElementException)
            {
                if (throwEx) throw;
                return null;
            }
        }

        internal static void SetElementHidden(this IWebDriver driver, IWebElement element)
        {
            driver.ExecuteJavaScript(Resources.HideElementFromDOM, element);
        }


        internal static void SetElementVisible(this IWebDriver driver, IWebElement element)
        {
            driver.ExecuteJavaScript(Resources.ShowElementInDOM, element);
        }

        internal static string GetElementAbsoluteXPath(this IWebDriver driver, IWebElement element)
        {
            return driver.ExecuteJavaScript<string>(Resources.GetElementAbsoluteXPath, element);
        }

        internal static void ShowScrollBar(this IWebDriver driver, IWebElement element, string value)
        {
            driver.ExecuteJavaScript(Resources.ShowScrollBar, element, value);
        }

        internal static List<IWebElement> GetAllElementsWithScrollbars(this IWebDriver driver)
        {
            IReadOnlyCollection<IWebElement> arr = new List<IWebElement>();
            try
            {
                arr = driver.ExecuteJavaScript<IReadOnlyCollection<IWebElement>>(Resources
                    .GetAllElementsWithScrollBars);
            }
            catch (WebDriverException ex) when (ex.Message.Contains("Script returned a value"))
            {
                // nothing to do, elements with scrollbar not exists
            }

            return arr?.ToList();
        }

        internal static void HideScrollBar(this IWebDriver driver, IWebElement element)
        {
            driver.ExecuteJavaScript(Resources.RemoveScrollBar, element);
        }


        internal static IWebElement GetElementWithActiveScrollBar(this IWebDriver driver)
        {
            var allElementsWithScrollbar = driver.GetAllElementsWithScrollbars();
            allElementsWithScrollbar = allElementsWithScrollbar.Where(o => o.Displayed).ToList();
            if (allElementsWithScrollbar.Count == 0) return driver.GetDocumentScrollingElement();
            var element =
                driver.ExecuteJavaScript<IWebElement>(Resources.GetElementWithActiveScrollbar,
                    allElementsWithScrollbar);
            if (element == null || element.TagName.ToLower() == "body" ||
                element.TagName.ToLower() == "html") element = driver.GetDocumentScrollingElement();
            return element;
        }

        internal static IWebElement GetDocumentScrollingElement(this IWebDriver driver)
        {
            return driver.ExecuteJavaScript<IWebElement>("return document.scrollingElement");
        }

        internal static int GetElementScrollBarHeight(this IWebDriver driver, IWebElement element)
        {
            return int.Parse(driver
                .ExecuteJavaScript<long>("return arguments[0].scrollHeight", element).ToString());
        }

        internal static void ScrollTo(this IWebDriver driver, IWebElement element, int position)
        {
            driver.ExecuteJavaScript("$(arguments[0]).scrollTop(arguments[1])", element, position);
        }

        internal static int GetCurrentScrollLocation(this IWebDriver driver, IWebElement element)
        {
            var value = driver.ExecuteJavaScript<long>("return $(arguments[0]).scrollTop()", element);
            return (int) value;
        }
    }
}