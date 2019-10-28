using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Example.ProjectToTest;
using System.Threading;
using System.Drawing.Imaging;
using System.Globalization;
using Neoris.TAF.Core;

namespace Example.Test.Example._01.Pagina1
{
    [TestClass]
    public class HomePageTest
    {
        public TestContext TestContext { get; set; }
        private ReportHelper reportHelper = new ReportHelper();

        [TestInitialize]
        public void Initialize()
        {
            Pages.HomePage.Initializes();
            reportHelper.Start(TestContext);
        }

        [TestMethod]
        public void TestReport()
        {
            Pages.HomePage.GoTo();
            reportHelper.AddScreenCaptureToStep("imagen 1", "detalle de la imagen 1");
            var a = false;
            Assert.IsTrue(a);
        }

        [TestMethod]
        public void OpenPage()
        {
            decimal dec2 = Convert.ToDecimal("400.677,3245");
            string sdec2 = dec2.ToString("0.000", CultureInfo.InvariantCulture);
        }

        [TestMethod]
        public void EscribirTextoEnTextBoxNombre_TextoEscritoEnTexboxNombre() 
        {
            Pages.HomePage.GoTo();
            Pages.HomePage.BuscarGoogle();
            Pages.HomePage.Scroll();
        }

        [TestMethod]
        public void ContarCantidadDeLabels() 
        {
            Pages.HomePage.GoTo();
            reportHelper.AddScreenCaptureToStep("imagen 1", "detalle de la imagen 1");
            reportHelper.AddScreenCaptureToStep("imagen 2", "detalle de la imagen 2");
            //var cantidadLabels = Pages.HomePage.ContarCantidadDeLabels();
        }

        [TestCleanup]
        public void CleanUp()
        {
            reportHelper.GenerateReport(TestContext);
            Pages.HomePage.Quit();
        }

    }
}
