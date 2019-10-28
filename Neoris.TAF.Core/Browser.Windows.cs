using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neoris.TAF.Core
{
    internal static partial class Browser
    {
        /// <summary>
        /// Cambia el foco a la primer Window cuya URL contenga el parámetro url recibido.
        /// </summary>
        public static void SwitchToWindowByUrl(String url)
        {
            var windows = Browser.GetAllWindows();
            foreach (var window in windows)
            {
                Boolean esVentanaSolicitada = Browser.SwitchTo().Window(window).Url.Contains(url);
                if (esVentanaSolicitada)
                {
                    Browser.SwitchTo().Window(window);
                    break;
                }
            }
        }

        public static void SwitchToDefaultContent()
        {
            Browser.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Cambia el foco a la primer Window cuyo TITLE contenga el parámetro title recibido.
        /// </summary>
        public static void SwitchToWindowByTitle(String title)
        {
            var windows = Browser.GetAllWindows();
            foreach (var window in windows)
            {
                Boolean esVentanaSolicitada = Browser.SwitchTo().Window(window).Title.Contains(title);
                if (esVentanaSolicitada)
                {
                    Browser.SwitchTo().Window(window);
                    break;
                }
            }
        }

        public static List<String> GetAllWindows()
        {
            return webDriver.WindowHandles.ToList();
        }

        public static String CurrentWindow()
        {
            return webDriver.CurrentWindowHandle;
        }

        /// <summary>
        /// Encapsulamos funcionalidad en clase privada para consumir desde Frames, Alerts o Windows.-
        /// </summary>
        private static ITargetLocator SwitchTo()
        {
            return webDriver.SwitchTo();
        }
    }
}
