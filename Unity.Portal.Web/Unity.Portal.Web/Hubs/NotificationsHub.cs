using Microsoft.AspNetCore.SignalR;

namespace Unity.Portal.Web.Hubs
{
    public class NotificationsHub : Hub<IUserNotification>
    {
        public async Task SendMessage(string user, string message)
            => await Clients.All.ReceiveNotification(user, message);

        public async Task SendMessageToCaller(string user, string message)
            => await Clients.Caller.ReceiveNotification(user, message);

        public async Task SendMessageToGroup(string user, string message)
            => await Clients.Group("SignalR Users").ReceiveNotification(user, message);
    }
}