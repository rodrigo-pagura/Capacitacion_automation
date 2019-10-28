using System;
using System.Linq;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Globalization;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Neoris.TAF.Core
{
    public class ReportHelper
    {
        private static String filePath = ConfigurationManager.AppSettings["HtmlReportsPath"];
        private static String fileName = String.Format("Reporte {0}.html", DateTime.Now.ToString("dd'-'MM'-'yyyy HHmm"));
        private static string ReporteImgPath = @"img\";
        private static String imgPath = $@"{filePath}{fileName}\{ReporteImgPath}";
        private static readonly ExtentHtmlReporter htmlReport = new ExtentHtmlReporter($@"{filePath}{fileName}\{fileName}");
        private static readonly ExtentReports _instance = new ExtentReports();
        public TestContext TestContext { get; set; }
        List<List<string>> imagesAndDetails = new List<List<string>>();

        public ReportHelper()
        {                        
            _instance.AttachReporter(htmlReport);
        }

        protected ExtentReports ExtentReport
        {
            get 
            {
                return _instance; 
            }
        }

        protected ExtentTest test;

        public void Start(TestContext testContext, string TestName = "")
        {
            if (!string.IsNullOrEmpty(TestName))
                test = ExtentReport.CreateTest(testContext.TestName + " - " + TestName);
            else
                test = ExtentReport.CreateTest(testContext.TestName);
        }

        public void GenerateReport(TestContext testContext)
        {
            this.ConfigureDirectories();
            var status = testContext.CurrentTestOutcome;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-ES");

            Status logstatus;

            switch (status)
            {
                case UnitTestOutcome.Failed:
                    logstatus = Status.Fail;
                    break;
                case UnitTestOutcome.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            var testClassName = testContext.FullyQualifiedTestClassName.Split('.').Last();
            test.AssignCategory(testClassName);
            test.Log(logstatus, "El test ha finalizado con estado " + logstatus);

            foreach (var item in imagesAndDetails)
            {
                test.Log(Status.Info, item[1], MediaEntityBuilder.CreateScreenCaptureFromPath(ReporteImgPath + item[0]).Build());
            }

            if (logstatus == Status.Fail || logstatus == Status.Warning)
            {
                try
                {
                    var _fileName = String.Format("errorTest_{0}_{1}", testContext.TestName, DateTime.Now.ToString("yyyyMMdd_HHmm"));
                    Browser.PrintScreen(_fileName, ScreenshotImageFormat.Jpeg, imgPath);
                    var file = ReporteImgPath + _fileName + "." + ScreenshotImageFormat.Jpeg;
                    test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(file));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
            ExtentReport.Flush();
        }

        private void ConfigureDirectories()
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (!Directory.Exists(imgPath))
            {
                Directory.CreateDirectory(imgPath);
            }
        }

        /// <summary>
        /// Agrega una captura de pantalla del aplicativo del momento.
        /// Opcionalmente se puede agregar un detalle que describa a la acción de la imagen.
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="details"></param>
        public void AddScreenCaptureToStep(string imageName , string details = "")
        {
            ConfigureDirectories();
            Browser.PrintScreen(imageName, ScreenshotImageFormat.Jpeg, imgPath);

            List<string> imgDetails = new List<string>();
            imgDetails.Add(imageName + ".jpeg");
            imgDetails.Add(details);
            imagesAndDetails.Add(imgDetails);
        }

        [TestInitialize]
        public void Initialize()
        {
            Browser.Initializes();
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.GenerateReport(TestContext);
            Browser.Quit();
        }
    }
}
