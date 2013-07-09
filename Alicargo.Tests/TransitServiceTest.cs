using Alicargo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Alicargo.Core.Repositories;
using Alicargo.ViewModels;
using Alicargo.Core.Models;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for TransitServiceTest and is intended
    ///to contain all TransitServiceTest Unit Tests
    ///</summary>
	[TestClass()]
	public class TransitServiceTest
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
		///A test for TransitService Constructor
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void TransitServiceConstructorTest()
		{
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ICarrierService carriers = null; // TODO: Initialize to an appropriate value
			ITransitRepository transitRepository = null; // TODO: Initialize to an appropriate value
			TransitService target = new TransitService(unitOfWork, carriers, transitRepository);
			Assert.Inconclusive("TODO: Implement code to verify target");
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
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ICarrierService carriers = null; // TODO: Initialize to an appropriate value
			ITransitRepository transitRepository = null; // TODO: Initialize to an appropriate value
			TransitService target = new TransitService(unitOfWork, carriers, transitRepository); // TODO: Initialize to an appropriate value
			CarrierSelectModel carrierSelectModel = null; // TODO: Initialize to an appropriate value
			long expected = 0; // TODO: Initialize to an appropriate value
			long actual;
			actual = target.AddOrGetCarrier(carrierSelectModel);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for AddTransit
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void AddTransitTest()
		{
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ICarrierService carriers = null; // TODO: Initialize to an appropriate value
			ITransitRepository transitRepository = null; // TODO: Initialize to an appropriate value
			TransitService target = new TransitService(unitOfWork, carriers, transitRepository); // TODO: Initialize to an appropriate value
			ITransitData model = null; // TODO: Initialize to an appropriate value
			CarrierSelectModel carrierSelectModel = null; // TODO: Initialize to an appropriate value
			long expected = 0; // TODO: Initialize to an appropriate value
			long actual;
			actual = target.AddTransit(model, carrierSelectModel);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
