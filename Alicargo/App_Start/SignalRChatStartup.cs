using Alicargo;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRChatStartup))]

namespace Alicargo
{
	public class SignalRChatStartup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
		}
	}
}