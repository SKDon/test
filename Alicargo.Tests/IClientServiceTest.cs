using Alicargo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Alicargo.Core.Models;
using Alicargo.ViewModels;
using System.Collections.Generic;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for IClientServiceTest and is intended
    ///to contain all IClientServiceTest Unit Tests
    ///</summary>
	[TestClass()]
	public class IClientServiceTest
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


		internal virtual IClientService CreateIClientService()
		{
			// TODO: Instantiate an appropriate concrete class.
			IClientService target = null;
			return target;
		}

		/// <summary>
		///A test for Add
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void AddTest()
		{
			IClientService target = CreateIClientService(); // TODO: Initialize to an appropriate value
			Client model = null; // TODO: Initialize to an appropriate value
			CarrierSelectModel carrierSelectModel = null; // TODO: Initialize to an appropriate value
			target.Add(model, carrierSelectModel);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}

		/// <summary>
		///A test for GetClient
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void GetClientTest()
		{
			IClientService target = CreateIClientService(); // TODO: Initialize to an appropriate value
			Nullable<long> id = new Nullable<long>(); // TODO: Initialize to an appropriate value
			Client expected = null; // TODO: Initialize to an appropriate value
			Client actual;
			actual = target.GetClient(id);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetList
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void GetListTest()
		{
			IClientService target = CreateIClientService(); // TODO: Initialize to an appropriate value
			int take = 0; // TODO: Initialize to an appropriate value
			int skip = 0; // TODO: Initialize to an appropriate value
			ListCollection<Client> expected = null; // TODO: Initialize to an appropriate value
			ListCollection<Client> actual;
			actual = target.GetList(take, skip);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for ToDictionary
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ToDictionaryTest()
		{
			IClientService target = CreateIClientService(); // TODO: Initialize to an appropriate value
			Dictionary<long, string> expected = null; // TODO: Initialize to an appropriate value
			Dictionary<long, string> actual;
			actual = target.ToDictionary();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Update
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void UpdateTest()
		{
			IClientService target = CreateIClientService(); // TODO: Initialize to an appropriate value
			Client model = null; // TODO: Initialize to an appropriate value
			CarrierSelectModel carrierSelectModel = null; // TODO: Initialize to an appropriate value
			target.Update(model, carrierSelectModel);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}
	}
}
