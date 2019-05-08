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
            var value = driver.ExecuteJavaScript<object>("return $(window).scrollTop() + window.innerHeight;").ToString();
            return int.Parse(value);
        }

        internal static int GetHeight(this IWebDriver driver, Entity entity)
        {
            string value;
            switch (entity)
            {
                case Entity.Document:
                    value = driver.ExecuteJavaScript<object>($"return $({GetStrEntity(entity)}).height()").ToString();
                    break;
                case Entity.Window:
                    value = driver.ExecuteJavaScript<object>($"return {GetStrEntity(entity)}.innerHeight").ToString();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entity), entity, null);
            }

            return int.Parse(value);
        }

        internal static int GetWidth(this IWebDriver driver, Entity entity)
        {
            var width = driver.ExecuteJavaScript<object>($"return $({GetStrEntity(entity)}).width()").ToString();
            return int.Parse(width);
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