using Alicargo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Alicargo.ViewModels;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for ICarrierServiceTest and is intended
    ///to contain all ICarrierServiceTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ICarrierServiceTest
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


		internal virtual ICarrierService CreateICarrierService()
		{
			// TODO: Instantiate an appropriate concrete class.
			ICarrierService target = null;
			return target;
		}

		/// <summary>
		///A test for AddOrGetCarrier
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void AddOrGetCarrierTest()
		{
			ICarrierService target = CreateICarrierService(); // TODO: Initialize to an appropriate value
			CarrierSelectModel carrierSelectModel = null; // TODO: Initialize to an appropriate value
			Func<long> expected = null; // TODO: Initialize to an appropriate value
			Func<long> actual;
			actual = target.AddOrGetCarrier(carrierSelectModel);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Get
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void GetTest()
		{
			ICarrierService target = CreateICarrierService(); // TODO: Initialize to an appropriate value
			Nullable<long> selectedId = new Nullable<long>(); // TODO: Initialize to an appropriate value
			CarrierSelectModel expected = null; // TODO: Initialize to an appropriate value
			CarrierSelectModel actual;
			actual = target.Get(selectedId);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
