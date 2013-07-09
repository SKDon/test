using Alicargo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Alicargo.Services;
using T4MVC;
using System.Web.Mvc;
using Alicargo.Core.Models;
using Alicargo.ViewModels;
using System.Collections.Generic;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for ApplicationControllerTest and is intended
    ///to contain all ApplicationControllerTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ApplicationControllerTest
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
		///A test for ApplicationController Constructor
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ApplicationControllerConstructorTest()
		{
			IApplicationService applicationService = null; // TODO: Initialize to an appropriate value
			IStateService stateService = null; // TODO: Initialize to an appropriate value
			IClientService clientService = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(applicationService, stateService, clientService);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for Close
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void CloseTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ActionResult expected = null; // TODO: Initialize to an appropriate value
			ActionResult actual;
			actual = target.Close();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Close
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void CloseTest1()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			long id = 0; // TODO: Initialize to an appropriate value
			ActionResult expected = null; // TODO: Initialize to an appropriate value
			ActionResult actual;
			actual = target.Close(id);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Create
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void CreateTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ViewResult expected = null; // TODO: Initialize to an appropriate value
			ViewResult actual;
			actual = target.Create();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Create
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void CreateTest1()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			Nullable<long> clientId = new Nullable<long>(); // TODO: Initialize to an appropriate value
			ViewResult expected = null; // TODO: Initialize to an appropriate value
			ViewResult actual;
			actual = target.Create(clientId);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Create
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void CreateTest2()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			Nullable<long> clientId = new Nullable<long>(); // TODO: Initialize to an appropriate value
			Application model = null; // TODO: Initialize to an appropriate value
			CarrierSelectModel carrierSelectModel = null; // TODO: Initialize to an appropriate value
			ActionResult expected = null; // TODO: Initialize to an appropriate value
			ActionResult actual;
			actual = target.Create(clientId, model, carrierSelectModel);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Edit
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void EditTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ViewResult expected = null; // TODO: Initialize to an appropriate value
			ViewResult actual;
			actual = target.Edit();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Edit
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void EditTest1()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			long id = 0; // TODO: Initialize to an appropriate value
			ViewResult expected = null; // TODO: Initialize to an appropriate value
			ViewResult actual;
			actual = target.Edit(id);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Edit
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void EditTest2()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			long id = 0; // TODO: Initialize to an appropriate value
			Application model = null; // TODO: Initialize to an appropriate value
			CarrierSelectModel carrierSelectModel = null; // TODO: Initialize to an appropriate value
			ActionResult expected = null; // TODO: Initialize to an appropriate value
			ActionResult actual;
			actual = target.Edit(id, model, carrierSelectModel);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Index
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void IndexTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ViewResult expected = null; // TODO: Initialize to an appropriate value
			ViewResult actual;
			actual = target.Index();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Invoice
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void InvoiceTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			FileResult expected = null; // TODO: Initialize to an appropriate value
			FileResult actual;
			actual = target.Invoice();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Invoice
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void InvoiceTest1()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			long id = 0; // TODO: Initialize to an appropriate value
			FileResult expected = null; // TODO: Initialize to an appropriate value
			FileResult actual;
			actual = target.Invoice(id);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for List
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ListTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			JsonResult expected = null; // TODO: Initialize to an appropriate value
			JsonResult actual;
			actual = target.List();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for List
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ListTest1()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			int take = 0; // TODO: Initialize to an appropriate value
			int skip = 0; // TODO: Initialize to an appropriate value
			int page = 0; // TODO: Initialize to an appropriate value
			int pageSize = 0; // TODO: Initialize to an appropriate value
			Dictionary<string, string>[] group = null; // TODO: Initialize to an appropriate value
			JsonResult expected = null; // TODO: Initialize to an appropriate value
			JsonResult actual;
			actual = target.List(take, skip, page, pageSize, group);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for SetState
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void SetStateTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ActionResult expected = null; // TODO: Initialize to an appropriate value
			ActionResult actual;
			actual = target.SetState();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for SetState
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void SetStateTest1()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			long id = 0; // TODO: Initialize to an appropriate value
			long stateId = 0; // TODO: Initialize to an appropriate value
			ActionResult expected = null; // TODO: Initialize to an appropriate value
			ActionResult actual;
			actual = target.SetState(id, stateId);
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
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ApplicationController.ActionNamesClass actual;
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
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ApplicationController actual;
			actual = target.Actions;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for CloseParams
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void CloseParamsTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ApplicationController.ActionParamsClass_Close actual;
			actual = target.CloseParams;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for CreateParams
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void CreateParamsTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ApplicationController.ActionParamsClass_Create actual;
			actual = target.CreateParams;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for EditParams
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void EditParamsTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ApplicationController.ActionParamsClass_Edit actual;
			actual = target.EditParams;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for InvoiceParams
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void InvoiceParamsTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ApplicationController.ActionParamsClass_Invoice actual;
			actual = target.InvoiceParams;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for ListParams
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ListParamsTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ApplicationController.ActionParamsClass_List actual;
			actual = target.ListParams;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for SetStateParams
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void SetStateParamsTest()
		{
			Dummy d = null; // TODO: Initialize to an appropriate value
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ApplicationController.ActionParamsClass_SetState actual;
			actual = target.SetStateParams;
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
			ApplicationController target = new ApplicationController(d); // TODO: Initialize to an appropriate value
			ApplicationController.ViewsClass actual;
			actual = target.Views;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
