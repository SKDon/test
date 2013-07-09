using Alicargo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for ApplicationController_ViewsClassTest and is intended
    ///to contain all ApplicationController_ViewsClassTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ApplicationController_ViewsClassTest
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
		///A test for ViewsClass Constructor
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ApplicationController_ViewsClassConstructorTest()
		{
			ApplicationController.ViewsClass target = new ApplicationController.ViewsClass();
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for ViewNames
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ViewNamesTest()
		{
			ApplicationController.ViewsClass target = new ApplicationController.ViewsClass(); // TODO: Initialize to an appropriate value
			ApplicationController.ViewsClass._ViewNamesClass actual;
			actual = target.ViewNames;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
