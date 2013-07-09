﻿using Alicargo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Alicargo.Core.Repositories;
using Alicargo.Core.Security;
using Alicargo.ViewModels;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for AuthenticationServiceTest and is intended
    ///to contain all AuthenticationServiceTest Unit Tests
    ///</summary>
	[TestClass()]
	public class AuthenticationServiceTest
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
		///A test for AuthenticationService Constructor
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void AuthenticationServiceConstructorTest()
		{
			IUserRepository users = null; // TODO: Initialize to an appropriate value
			IPasswordConverter passwordConverter = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			AuthenticationService target = new AuthenticationService(users, passwordConverter, identity);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for Authenticate
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void AuthenticateTest()
		{
			IUserRepository users = null; // TODO: Initialize to an appropriate value
			IPasswordConverter passwordConverter = null; // TODO: Initialize to an appropriate value
			IIdentityService identity = null; // TODO: Initialize to an appropriate value
			AuthenticationService target = new AuthenticationService(users, passwordConverter, identity); // TODO: Initialize to an appropriate value
			SignIdModel user = null; // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.Authenticate(user);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
