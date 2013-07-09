using Alicargo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Alicargo.Services;
using T4MVC;
using System.Web.Mvc;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for CarrierControllerTest and is intended
    ///to contain all CarrierControllerTest Unit Tests
    ///</summary>
	[TestClass()]
	public class CarrierControllerTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for CarrierController Constructor
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void CarrierControllerConstructorTest()
		{
			ICarrierService carriers = null; // TODO: Initialize to an appropriate value
			CarrierController target = new CarrierController(carriers);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for Select
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void SelectTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			CarrierController target = new CarrierController(d); // TODO: Initialize to an appropriate value
			PartialViewResult expected = null; // TODO: Initialize to an appropriate value
			PartialViewResult actual;
			actual = target.Select();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Select
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void SelectTest1()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			CarrierController target = new CarrierController(d); // TODO: Initialize to an appropriate value
			Nullable<long> selectedId = new Nullable<long>(); // TODO: Initialize to an appropriate value
			PartialViewResult expected = null; // TODO: Initialize to an appropriate value
			PartialViewResult actual;
			actual = target.Select(selectedId);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for ActionNames
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ActionNamesTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			CarrierController target = new CarrierController(d); // TODO: Initialize to an appropriate value
			CarrierController.ActionNamesClass actual;
			actual = target.ActionNames;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Actions
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ActionsTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			CarrierController target = new CarrierController(d); // TODO: Initialize to an appropriate value
			CarrierController actual;
			actual = target.Actions;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for SelectParams
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void SelectParamsTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			CarrierController target = new CarrierController(d); // TODO: Initialize to an appropriate value
			CarrierController.ActionParamsClass_Select actual;
			actual = target.SelectParams;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Views
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ViewsTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			CarrierController target = new CarrierController(d); // TODO: Initialize to an appropriate value
			CarrierController.ViewsClass actual;
			actual = target.Views;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
