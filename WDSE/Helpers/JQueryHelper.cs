using System.Diagnostics;
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
            try
            {
                _ = driver.ExecuteJavaScript<long>("return $(document).height()");
            }
            catch (WebDriverException ex)
            {
                if (ex.Message.Contains("$ is not defined"))
                {
                    var script = Resources.SetJQuery;
                    driver.ExecuteJavaScript(script);
                }
                else
                {
                    throw;
                }

                var sw = new Stopwatch();
                sw.Start();
                do
                {
                    try
                    {
                        _ = driver.ExecuteJavaScript<long>("return $(document).height()");
                        return;
                    }
                    catch (WebDriverException)
                    {
                        if (ex.Message.Contains("$ is not defined"))
                            Thread.Sleep(10);
                        else throw;
                    }
                    finally
                    {
                        sw.Stop();
                    }
                } while (sw.Elapsed.TotalSeconds <= 15);
            }
        }

        internal static bool IsElementInViewPort(this IWebDriver driver, IWebElement element)
        {
            driver.IsElementExistsInDOM(element, true);
            var result = driver.ExecuteJavaScript<bool>(Resources.GetElementVisibleState, element);
            return result;
        }

        internal static void ScrollToElement(this IWebDriver driver, IWebElement element)
        {
            driver.IsElementExistsInDOM(element, true);
            var script = Resources.ScrollToElement;
            driver.ExecuteJavaScript(script, element);
        }

        internal static bool IsElementExistsInDOM(this IWebDriver driver, IWebElement element, bool throwExIfNot = false)
        {
            var result = driver.ExecuteJavaScript<bool>(Resources.CheckElementExists, element);
            if (throwExIfNot && !result)
            {
                throw new NoSuchElementException($"Cant find element {element} in DOM.");
            }
            return result;
        }
    }
}