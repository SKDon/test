using Alicargo;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRChat))]

namespace Alicargo
{
	public class SignalRChat
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
		}
	}
}