using System.Reflection;
using System.Web;

namespace Alicargo
{
	public /*sealed*/ class MvcApplication : HttpApplication
	{
		public static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		public void Application_Start()
		{
			Startup.ApplicationStart();
		}
	}
}