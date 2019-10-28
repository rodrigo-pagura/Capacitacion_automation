using Neoris.TAF.Core;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.ProjectToTest
{
    public class HomePage : PageBase
    {
        [FindsBy(How = How.Name, Using = "q")]
        private IWebElement searchBox;

        public HomePage() : base("MyFirstPage", "https://www.google.com.ar")
        {

        }

        public void BuscarGoogle()
        {
            searchBox.SendKeys("Selenium");
            searchBox.SendKeys(Keys.Enter);            
        }
    }
}
