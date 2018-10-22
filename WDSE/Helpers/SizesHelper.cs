using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using WDSE.Json;
using WDSE.Properties;

[assembly: InternalsVisibleTo("WDSETests")]

namespace WDSE.Helpers
{
    internal static class SizesHelper
    {
        internal static int GetHeight(this IWebDriver driver, Entity entity)
        {
            var height = driver.ExecuteJavaScript<long>($"return $({GetStrEntity(entity)}).height()");
            return int.Parse(height.ToString());
        }

        internal static int GetWidth(this IWebDriver driver, Entity entity)
        {
            var height = driver.ExecuteJavaScript<long>($"return $({GetStrEntity(entity)}).width()");
            return int.Parse(height.ToString());
        }

        private static string GetStrEntity(Entity entity)
        {
            switch (entity)
            {
                case Entity.Document:
                    return "document";
                case Entity.Window:
                    return "window";
                default:
                    throw new ArgumentOutOfRangeException(nameof(entity), entity, null);
            }
        }

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

        internal static ElementCoords GetElementCoordinates(this IWebDriver driver, IWebElement element)
        {
            var script = Resources.GetElementCoordinates;
            var w = driver.ExecuteJavaScript<string>(script, element);
            var json = JsonConvert.DeserializeObject<ElementCoords>(w);
            return json;
        }

        internal enum Entity
        {
            Document,
            Window
        }
    }
}