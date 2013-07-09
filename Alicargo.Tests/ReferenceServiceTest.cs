﻿using Alicargo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Alicargo.Core.Repositories;
using Alicargo.Core.Models;
using System.Collections.Generic;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for ReferenceServiceTest and is intended
    ///to contain all ReferenceServiceTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ReferenceServiceTest
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
		///A test for ReferenceService Constructor
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void ReferenceServiceConstructorTest()
		{
			IReferenceRepository referenceRepository = null; // TODO: Initialize to an appropriate value
			IApplicationRepository applicationRepository = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ReferenceService target = new ReferenceService(referenceRepository, applicationRepository, unitOfWork);
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
			IReferenceRepository referenceRepository = null; // TODO: Initialize to an appropriate value
			IApplicationRepository applicationRepository = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ReferenceService target = new ReferenceService(referenceRepository, applicationRepository, unitOfWork); // TODO: Initialize to an appropriate value
			Reference model = null; // TODO: Initialize to an appropriate value
			target.Create(model);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}

		/// <summary>
		///A test for SetReference
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void SetReferenceTest()
		{
			IReferenceRepository referenceRepository = null; // TODO: Initialize to an appropriate value
			IApplicationRepository applicationRepository = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ReferenceService target = new ReferenceService(referenceRepository, applicationRepository, unitOfWork); // TODO: Initialize to an appropriate value
			long id = 0; // TODO: Initialize to an appropriate value
			string referenceBill = string.Empty; // TODO: Initialize to an appropriate value
			target.SetReference(id, referenceBill);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
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
			IReferenceRepository referenceRepository = null; // TODO: Initialize to an appropriate value
			IApplicationRepository applicationRepository = null; // TODO: Initialize to an appropriate value
			IUnitOfWork unitOfWork = null; // TODO: Initialize to an appropriate value
			ReferenceService target = new ReferenceService(referenceRepository, applicationRepository, unitOfWork); // TODO: Initialize to an appropriate value
			Dictionary<string, string> expected = null; // TODO: Initialize to an appropriate value
			Dictionary<string, string> actual;
			actual = target.ToDictionary();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
