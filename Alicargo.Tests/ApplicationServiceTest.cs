using Alicargo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Alicargo.Core.Repositories;
using Alicargo.Core.Models;
using Alicargo.ViewModels;
using Alicargo.Core.Helpers;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for ApplicationServiceTest and is intended
    ///to contain all ApplicationServiceTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ApplicationServiceTest
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
		///A test for ApplicationService Constructor
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ApplicationServiceConstructorTest()
		{
			IApplicationRepository applications = null; // TODO: Initialize to an appropriate value
			IClientService clientService = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			IStateService stateService = null; // TODO: Initialize to an appropriate value
			ITransitService transitService = null; // TODO: Initialize to an appropriate value
			ITransitRepository transitRepository = null; // TODO: Initialize to an appropriate value
			IReferenceService referenceService = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ApplicationService target = new ApplicationService(applications, clientService, identity, stateService, transitService, transitRepository, referenceService, unitOfWork);
			Assert.Inconclusive("TODO: Implement code to verify target");
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
			IApplicationRepository applications = null; // TODO: Initialize to an appropriate value
			IClientService clientService = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			IStateService stateService = null; // TODO: Initialize to an appropriate value
			ITransitService transitService = null; // TODO: Initialize to an appropriate value
			ITransitRepository transitRepository = null; // TODO: Initialize to an appropriate value
			IReferenceService referenceService = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ApplicationService target = new ApplicationService(applications, clientService, identity, stateService, transitService, transitRepository, referenceService, unitOfWork); // TODO: Initialize to an appropriate value
			Application model = null; // TODO: Initialize to an appropriate value
			CarrierSelectModel carrierSelectModel = null; // TODO: Initialize to an appropriate value
			target.Add(model, carrierSelectModel);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
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
			IApplicationRepository applications = null; // TODO: Initialize to an appropriate value
			IClientService clientService = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			IStateService stateService = null; // TODO: Initialize to an appropriate value
			ITransitService transitService = null; // TODO: Initialize to an appropriate value
			ITransitRepository transitRepository = null; // TODO: Initialize to an appropriate value
			IReferenceService referenceService = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ApplicationService target = new ApplicationService(applications, clientService, identity, stateService, transitService, transitRepository, referenceService, unitOfWork); // TODO: Initialize to an appropriate value
			long id = 0; // TODO: Initialize to an appropriate value
			Application expected = null; // TODO: Initialize to an appropriate value
			Application actual;
			actual = target.Get(id);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetApplicationIndexModel
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void GetApplicationIndexModelTest()
		{
			IApplicationRepository applications = null; // TODO: Initialize to an appropriate value
			IClientService clientService = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			IStateService stateService = null; // TODO: Initialize to an appropriate value
			ITransitService transitService = null; // TODO: Initialize to an appropriate value
			ITransitRepository transitRepository = null; // TODO: Initialize to an appropriate value
			IReferenceService referenceService = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ApplicationService target = new ApplicationService(applications, clientService, identity, stateService, transitService, transitRepository, referenceService, unitOfWork); // TODO: Initialize to an appropriate value
			ApplicationIndexModel expected = null; // TODO: Initialize to an appropriate value
			ApplicationIndexModel actual;
			actual = target.GetApplicationIndexModel();
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
			IApplicationRepository applications = null; // TODO: Initialize to an appropriate value
			IClientService clientService = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			IStateService stateService = null; // TODO: Initialize to an appropriate value
			ITransitService transitService = null; // TODO: Initialize to an appropriate value
			ITransitRepository transitRepository = null; // TODO: Initialize to an appropriate value
			IReferenceService referenceService = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ApplicationService target = new ApplicationService(applications, clientService, identity, stateService, transitService, transitRepository, referenceService, unitOfWork); // TODO: Initialize to an appropriate value
			int take = 0; // TODO: Initialize to an appropriate value
			int skip = 0; // TODO: Initialize to an appropriate value
			Order[] groups = null; // TODO: Initialize to an appropriate value
			ListCollection<Application> expected = null; // TODO: Initialize to an appropriate value
			ListCollection<Application> actual;
			actual = target.List(take, skip, groups);
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
			IApplicationRepository applications = null; // TODO: Initialize to an appropriate value
			IClientService clientService = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			IStateService stateService = null; // TODO: Initialize to an appropriate value
			ITransitService transitService = null; // TODO: Initialize to an appropriate value
			ITransitRepository transitRepository = null; // TODO: Initialize to an appropriate value
			IReferenceService referenceService = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ApplicationService target = new ApplicationService(applications, clientService, identity, stateService, transitService, transitRepository, referenceService, unitOfWork); // TODO: Initialize to an appropriate value
			Application model = null; // TODO: Initialize to an appropriate value
			CarrierSelectModel carrierSelectModel = null; // TODO: Initialize to an appropriate value
			target.Update(model, carrierSelectModel);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}
	}
}
