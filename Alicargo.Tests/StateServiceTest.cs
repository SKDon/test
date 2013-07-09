using Alicargo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Alicargo.Core.Repositories;
using Alicargo.Core.Models;
using System.Collections.Generic;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for StateServiceTest and is intended
    ///to contain all StateServiceTest Unit Tests
    ///</summary>
	[TestClass()]
	public class StateServiceTest
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
		///A test for StateService Constructor
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void StateServiceConstructorTest()
		{
			IStateRepository states = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			IApplicationRepository applications = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			StateService target = new StateService(states, identity, applications, unitOfWork);
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
			IStateRepository states = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			IApplicationRepository applications = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			StateService target = new StateService(states, identity, applications, unitOfWork); // TODO: Initialize to an appropriate value
			long applicationId = 0; // TODO: Initialize to an appropriate value
			target.Close(applicationId);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}

		/// <summary>
		///A test for GetAvailableStates
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void GetAvailableStatesTest()
		{
			IStateRepository states = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			IApplicationRepository applications = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			StateService target = new StateService(states, identity, applications, unitOfWork); // TODO: Initialize to an appropriate value
			long[] expected = null; // TODO: Initialize to an appropriate value
			long[] actual;
			actual = target.GetAvailableStates();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetDefaultState
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void GetDefaultStateTest()
		{
			IStateRepository states = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			IApplicationRepository applications = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			StateService target = new StateService(states, identity, applications, unitOfWork); // TODO: Initialize to an appropriate value
			State expected = null; // TODO: Initialize to an appropriate value
			State actual;
			actual = target.GetDefaultState();
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
			IStateRepository states = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			IApplicationRepository applications = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			StateService target = new StateService(states, identity, applications, unitOfWork); // TODO: Initialize to an appropriate value
			long applicationId = 0; // TODO: Initialize to an appropriate value
			long stateId = 0; // TODO: Initialize to an appropriate value
			target.SetState(applicationId, stateId);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}

		/// <summary>
		///A test for ToLocalizedDictionary
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ToLocalizedDictionaryTest()
		{
			IStateRepository states = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			IApplicationRepository applications = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			StateService target = new StateService(states, identity, applications, unitOfWork); // TODO: Initialize to an appropriate value
			IEnumerable<long> stateIds = null; // TODO: Initialize to an appropriate value
			Dictionary<long, string> expected = null; // TODO: Initialize to an appropriate value
			Dictionary<long, string> actual;
			actual = target.ToLocalizedDictionary(stateIds);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
