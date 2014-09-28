using Microsoft.AspNet.SignalR;

namespace Alicargo.Services.Chat
{
	public sealed class ChatHub : Hub
	{
		public void Send(string name, string message)
		{
			Clients.All.broadcastMessage(name, message);
		}
	}
}