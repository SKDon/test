using Alicargo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Alicargo.Core.Repositories;
using Alicargo.Core.Enums;

namespace Alicargo.Tests
{
    
    
    /// <summary>
    ///This is a test class for IdentityServiceTest and is intended
    ///to contain all IdentityServiceTest Unit Tests
    ///</summary>
	[TestClass()]
	public class IdentityServiceTest
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
		///A test for IdentityService Constructor
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void IdentityServiceConstructorTest()
		{
			IUserRepository users = null; // TODO: Initialize to an appropriate value
			IdentityService target = new IdentityService(users);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for IsInRole
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void IsInRoleTest()
		{
			IUserRepository users = null; // TODO: Initialize to an appropriate value
			IdentityService target = new IdentityService(users); // TODO: Initialize to an appropriate value
			Role role = new Role(); // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IsInRole(role);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Id
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void IdTest()
		{
			IUserRepository users = null; // TODO: Initialize to an appropriate value
			IdentityService target = new IdentityService(users); // TODO: Initialize to an appropriate value
			Nullable<long> expected = new Nullable<long>(); // TODO: Initialize to an appropriate value
			Nullable<long> actual;
			target.Id = expected;
			actual = target.Id;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for IsAuthenticated
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void IsAuthenticatedTest()
		{
			IUserRepository users = null; // TODO: Initialize to an appropriate value
			IdentityService target = new IdentityService(users); // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IsAuthenticated;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for TwoLetterISOLanguageName
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://localhost:1893")]
		public void TwoLetterISOLanguageNameTest()
		{
			IUserRepository users = null; // TODO: Initialize to an appropriate value
			IdentityService target = new IdentityService(users); // TODO: Initialize to an appropriate value
			string actual;
			actual = target.TwoLetterISOLanguageName;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
