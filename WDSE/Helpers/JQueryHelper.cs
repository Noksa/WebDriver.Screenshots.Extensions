using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE.Properties;

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
    }
}