using System.Globalization;
using Alicargo.Jobs.ApplicationEvents.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Jobs.Tests.Events.Helpers
{
	[TestClass]
	public class TextBulderHelperGetTextTests
	{
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
		}

		[TestMethod]
		public void Test_GetText()
		{
			var value = _fixture.Create<string>();
			var text = TextBulderHelper.GetText(CultureInfo.CurrentCulture, "Text: {0}.", value);

			text.ShouldBeEquivalentTo("Text: " + value + ".");
		}

		[TestMethod]
		public void Test_BadFromat()
		{
			var value = _fixture.Create<string>();
			const string format = "Text: {0.";
			var text = TextBulderHelper.GetText(CultureInfo.CurrentCulture, format, value);

			text.ShouldBeEquivalentTo(format + value);
		}

		[TestMethod]
		public void Test_NullFromat()
		{
			var value = _fixture.Create<string>();
			var text = TextBulderHelper.GetText(CultureInfo.CurrentCulture, null, value);

			text.ShouldBeEquivalentTo(value);
		}

		[TestMethod]
		public void Test_NullValue()
		{
			var text = TextBulderHelper.GetText(CultureInfo.CurrentCulture, _fixture.Create<string>(), null);

			text.ShouldBeEquivalentTo(string.Empty);
		}
	}
}