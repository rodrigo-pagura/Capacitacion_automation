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
        public static IWebDriver SwitchToFrame(String frameName)
        {
            return Browser.SwitchTo().Frame(frameName);
        }

        public static IWebDriver SwitchToFrameElement(IWebElement frameElement)
        {
            return Browser.SwitchTo().Frame(frameElement);
        }

        public static IWebDriver SwitchToDefaultFrame()
        {
            return Browser.SwitchTo().DefaultContent();
        }
    }
}
