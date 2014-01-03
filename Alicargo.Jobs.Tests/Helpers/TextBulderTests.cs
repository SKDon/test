using System.Linq;
using System.Text;
using Alicargo.Contracts.Enums;
using Alicargo.Jobs.ApplicationEvents.Entities;
using Alicargo.Jobs.Helpers;
using Alicargo.Jobs.Helpers.Abstract;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Jobs.Tests.Helpers
{
	[TestClass]
	public class TextBulderTests
	{
		private TextBuilder<TextLocalizedData> _builder;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
			_builder = new TextBuilder<TextLocalizedData>();
		}

		[TestMethod]
		public void Test_GetText()
		{
			var properties = typeof (TextLocalizedData).GetProperties().Where(x => x.PropertyType == typeof (string)).ToArray();
			var sb = new StringBuilder();
			foreach (var property in properties)
			{
				sb.AppendFormat("{{{0} [{0}: {{0}}]}}", property.Name).AppendLine();
			}
			sb.Append("{DisplayNumber[Number: \"{0}\"]}").AppendLine();
			sb.Append("{DisplayNumber}").AppendLine();

			var text = _builder.GetText(sb.ToString(), TwoLetterISOLanguageName.Russian, _fixture.Create<TextLocalizedData>());

			text.Should().NotContain("{").And.NotContain("}");
		}
	}
}