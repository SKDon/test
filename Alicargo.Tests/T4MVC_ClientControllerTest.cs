using Alicargo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;
using Alicargo.Core.Models;
using Alicargo.ViewModels;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for T4MVC_ClientControllerTest and is intended
    ///to contain all T4MVC_ClientControllerTest Unit Tests
    ///</summary>
	[TestClass()]
	public class T4MVC_ClientControllerTest
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
		///A test for T4MVC_ClientController Constructor
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void T4MVC_ClientControllerConstructorTest()
		{
			T4MVC_ClientController target = new T4MVC_ClientController();
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
			T4MVC_ClientController target = new T4MVC_ClientController(); // TODO: Initialize to an appropriate value
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
			T4MVC_ClientController target = new T4MVC_ClientController(); // TODO: Initialize to an appropriate value
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
			T4MVC_ClientController target = new T4MVC_ClientController(); // TODO: Initialize to an appropriate value
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
		public void EditTest1()
		{
			T4MVC_ClientController target = new T4MVC_ClientController(); // TODO: Initialize to an appropriate value
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
			T4MVC_ClientController target = new T4MVC_ClientController(); // TODO: Initialize to an appropriate value
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
			T4MVC_ClientController target = new T4MVC_ClientController(); // TODO: Initialize to an appropriate value
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
	}
}
