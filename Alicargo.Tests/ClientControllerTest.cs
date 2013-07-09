using Alicargo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Alicargo.Services;
using T4MVC;
using System.Web.Mvc;
using Alicargo.Core.Models;
using Alicargo.ViewModels;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for ClientControllerTest and is intended
    ///to contain all ClientControllerTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ClientControllerTest
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
		///A test for ClientController Constructor
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ClientControllerConstructorTest()
		{
			IClientService clientService = null; // TODO: Initialize to an appropriate value
			ClientController target = new ClientController(clientService);
			Assert.Inconclusive("TODO: Implement code to verify target");
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
			Client model = null; // TODO: Initialize to an appropriate value
			CarrierSelectModel carrierSelectModel = null; // TODO: Initialize to an appropriate value
			ActionResult expected = null; // TODO: Initialize to an appropriate value
			ActionResult actual;
			actual = target.Create(model, carrierSelectModel);
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
			ActionResult expected = null; // TODO: Initialize to an appropriate value
			ActionResult actual;
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
			Nullable<long> id = new Nullable<long>(); // TODO: Initialize to an appropriate value
			ActionResult expected = null; // TODO: Initialize to an appropriate value
			ActionResult actual;
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
			Client model = null; // TODO: Initialize to an appropriate value
			CarrierSelectModel carrierSelectModel = null; // TODO: Initialize to an appropriate value
			ActionResult expected = null; // TODO: Initialize to an appropriate value
			ActionResult actual;
			actual = target.Edit(model, carrierSelectModel);
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
			ViewResult expected = null; // TODO: Initialize to an appropriate value
			ViewResult actual;
			actual = target.Index();
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
			int take = 0; // TODO: Initialize to an appropriate value
			int skip = 0; // TODO: Initialize to an appropriate value
			int page = 0; // TODO: Initialize to an appropriate value
			int pageSize = 0; // TODO: Initialize to an appropriate value
			JsonResult expected = null; // TODO: Initialize to an appropriate value
			JsonResult actual;
			actual = target.List(take, skip, page, pageSize);
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
			ClientController.ActionNamesClass actual;
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
			ClientController actual;
			actual = target.Actions;
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
			ClientController.ActionParamsClass_Create actual;
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
			ClientController.ActionParamsClass_Edit actual;
			actual = target.EditParams;
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
			ClientController.ActionParamsClass_List actual;
			actual = target.ListParams;
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
			ClientController target = new ClientController(d); // TODO: Initialize to an appropriate value
			ClientController.ViewsClass actual;
			actual = target.Views;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
