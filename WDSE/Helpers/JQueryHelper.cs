﻿using System;
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

        internal static bool IsElementInViewPort(this IWebDriver driver, By by)
        {
            var element = driver.GetElementFromDOM(by);
            if (element == null) return false;
            var result = driver.ExecuteJavaScript<bool>(Resources.GetElementVisibleState, element);
            return result;
        }

        internal static bool IsElementInViewPort(this IWebDriver driver, IWebElement element)
        {
            var result = driver.ExecuteJavaScript<bool>(Resources.GetElementVisibleState, element);
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

        internal static void SetElementHidden(this IWebDriver driver, IWebElement element)
        {
            driver.ExecuteJavaScript(Resources.HideElementFromDOM, element);
        }

        internal static void SetElementVisible(this IWebDriver driver, IWebElement element)
        {
            driver.ExecuteJavaScript(Resources.ShowElementInDOM, element);
        }
    }
}