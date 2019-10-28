using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Configuration;

namespace Neoris.TAF.Core
{
    public static class DriverHelper
    {
        public static IWebDriver FactoryDriver()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["DriversPath"];
            var type = ConfigurationManager.AppSettings["WebDriverType"];
            var headdless = ConfigurationManager.AppSettings["headdless"];
            switch (type)
            {
                case "FireFox":
                    return new FirefoxDriver();
                case "IExplorer":
                    return new InternetExplorerDriver(path);
                case "Chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("start-maximized");
                    if (Boolean.Parse(headdless))
                    {
                        options.AddArgument("headless");
                        options.AddArgument("window-size=1920,1080");
                    }
                    return new ChromeDriver(path,options);
                default:
                    throw new Exception("El driver no existe. Ingrese uno de los siguientes: FireFox, IExplorer, Chrome");
            }
        }
    }
}
