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
        public static IAlert Alert
        {
            get { return Browser.SwitchTo().Alert(); }
        }
    }
}
