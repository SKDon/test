using System.Globalization;
using Alicargo.Jobs.Helpers;
using Alicargo.Utilities.Localization;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Jobs.Tests.Helpers
{
	[TestClass]
	public class TextBuilderHelperHelperGetTextTests
	{
		private Fixture _fixture;
		private readonly CultureInfo _currentCulture = CultureProvider.GetCultureInfo();

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
		}

		[TestMethod]
		public void Test_GetText()
		{
			var value = _fixture.Create<string>();
			var text = TextBuilderHelper.GetText(_currentCulture, "Text: {0}.", value);

			text.ShouldBeEquivalentTo("Text: " + value + ".");
		}

		[TestMethod]
		public void Test_BadFromat()
		{
			var value = _fixture.Create<string>();
			const string format = "Text: {0.";
			var text = TextBuilderHelper.GetText(_currentCulture, format, value);

			text.ShouldBeEquivalentTo(format + value);
		}

		[TestMethod]
		public void Test_NullFromat()
		{
			var value = _fixture.Create<string>();
			var text = TextBuilderHelper.GetText(_currentCulture, null, value);

			text.ShouldBeEquivalentTo(value);
		}

		[TestMethod]
		public void Test_NullValue()
		{
			var text = TextBuilderHelper.GetText(_currentCulture, _fixture.Create<string>(), null);

			text.ShouldBeEquivalentTo(string.Empty);
		}
	}
}