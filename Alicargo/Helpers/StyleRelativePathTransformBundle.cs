using System.Web.Optimization;

namespace Alicargo.Helpers
{
	internal sealed class StyleRelativePathTransformBundle : Bundle
	{
		public StyleRelativePathTransformBundle(string virtualPath)
			: base(virtualPath, new StyleRelativePathTransform(), new CssMinify()) { }
	}
}