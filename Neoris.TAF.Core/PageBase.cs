using Neoris.TAF.Core.Enums;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Threading;

namespace Neoris.TAF.Core
{
    public abstract class PageBase
    {
        public String Title { get; set; }
        public String Page { get; set; }

        protected PageBase(String title, String page)
        {
            Title = title;
            Page = page;
        }

        public void Initializes(Boolean maximized = true)
        {
            Browser.Initializes(maximized);
        }

        public void Action(Navigation.NavigationActions action)
        {
            Browser.Action(action);
        }

        public void AceptarPopUp()
        {
            Thread.Sleep(2000);
            Browser.Alert.Accept();
        }

        public void CancelarPopUp()
        {
            Thread.Sleep(2000);
            Browser.Alert.Dismiss();
        }

        public void GoTo()
        {
            Browser.GoTo(Page);
        }

        public void Quit()
        {
            Browser.Quit();
        }

        public void PrintScreen(String fileName, ScreenshotImageFormat imageFormat, String path = null)
        {
            Browser.PrintScreen(fileName, imageFormat, path);
        }

        public IWebElement ExplicitWait(Int32 time, Func<IWebDriver, IWebElement> explicitWaitFunc)
        {
            return Browser.ExplicitWait(time, explicitWaitFunc);
        }

        public bool ExplicitWait(Int32 time, Func<IWebDriver, bool> explicitWaitFunc)
        {
            return Browser.ExplicitWait(time, explicitWaitFunc);
        }

        public static bool FindElementIfExists(By by)
        {
            return Browser.FindElementIfExists(by);
        }

        public void PressBtn(IWebElement boton)
        {
            Thread.Sleep(1000);
            boton.Click();
            Thread.Sleep(1000);
        }

        public void PressBtn(IWebElement boton1, IWebElement boton2)
        {
            boton1.Click();
            boton2.Click();
            Thread.Sleep(500);
        }

        public IWebElement Find(By objeto)
        {
            return ExplicitWait(10, x => x.FindElement(objeto));
        }

        public IWebElement FindNestedElement(By objeto1, By objeto2)
        {
            return (Browser.FindNestedElements(objeto1, objeto2));
        }

        public virtual void MoveToElement(IWebElement elem)
        {
            Browser.MoveToElement(elem);
        }

        public void DragAndDrop(IWebElement elem1, IWebElement elem2)
        {
            Browser.DragAndDrop(elem1, elem2);
        }

        public virtual void CompleteField(IWebElement element, String documento)
        {
            Thread.Sleep(1500);
            Browser.CompleteField(element, documento);

        }

        public virtual void JavaScript(String jav)
        {
            Browser.Javas(jav);
        }

        public static string PagesSource()
        {
            return Browser.CodigoPage();
        }

        public virtual void Scroll(string i = null)
        {
            Browser.Scroll(i);
        }

        #region Windows

        public List<String> GetAllWindows()
        {
            return Browser.GetAllWindows();
        }

        public void SwitchToWindowByTitle(String title)
        {
            Browser.SwitchToWindowByTitle(title);
        }

        public void SwitchToWindowByUrl(String url)
        {
            Browser.SwitchToWindowByUrl(url);
        }

        public void SwitchToDefaultWindow()
        {
            Browser.SwitchToDefaultContent();
        }

        #endregion

        #region Frames


        public static void SwitchToDefaultFrame()
        {
            Browser.SwitchToDefaultFrame();
        }

        public void SwitchToFrame(String frameName)
        {
            Browser.SwitchToFrame(frameName);
        }

        public void SwitchToFrameElement(IWebElement frameName)
        {
            Browser.SwitchToFrameElement(frameName);
        }


        #endregion

    }
}
