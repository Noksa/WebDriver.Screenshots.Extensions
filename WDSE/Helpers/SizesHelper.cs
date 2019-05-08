using System;
using System.Runtime.CompilerServices;
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
        internal static int GetCurrentScrolledBottom(this IWebDriver driver)
        {
            return int.Parse(driver.ExecuteJavaScript<long>("return $(window).scrollTop() + window.innerHeight;")
                .ToString());
        }

        internal static int GetHeight(this IWebDriver driver, Entity entity)
        {
            long height;
            switch (entity)
            {
                case Entity.Document:
                    height = driver.ExecuteJavaScript<long>($"return $({GetStrEntity(entity)}).height()");
                    break;
                case Entity.Window:
                    height = driver.ExecuteJavaScript<long>($"return {GetStrEntity(entity)}.innerHeight");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entity), entity, null);
            }

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

        internal static ElementCoords GetElementCoordinates(this IWebDriver driver, IWebElement element)
        {
            var w = driver.ExecuteJavaScript<string>(Resources.GetElementCoordinates, element);
            var json = JsonConvert.DeserializeObject<ElementCoords>(w);
            if (json.y >= 0)
            {
                json.bottom = json.y + json.height;
            }
            else
            {
                json.bottom = json.height + json.y;
            }
            

            return json;
        }

        internal static ElementCoords GetElementCoordinates(this IWebDriver driver, By by)
        {
            var element = driver.GetElementFromDOM(by);
            return GetElementCoordinates(driver, element);
        }

        internal enum Entity
        {
            Document,
            Window
        }
    }
}