using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neoris.TAF.Core
{
    public static class PageFactoryHelper
    {
        public static T InitElements<T>() where T : PageBase, new()
        {
            T page = new T();
            PageFactory.InitElements(Browser.Driver, page);
            return page;
        }
    }
}
