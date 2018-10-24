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

        internal static ElementCoords GetElementCoordinates(this IWebDriver driver, By by)
        {
            var element = driver.GetElementFromDOM(by);
            var w = driver.ExecuteJavaScript<string>(Resources.GetElementCoordinates, element);
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