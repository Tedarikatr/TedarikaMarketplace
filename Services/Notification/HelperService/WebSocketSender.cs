using Microsoft.AspNetCore.SignalR;

namespace Services.Notification.HelperService
{
    public interface IWebSocketSender
    {
        Task SendWebSocketAsync(int userId, string message);
    }

    public class WebSocketSender : IWebSocketSender
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public WebSocketSender(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendWebSocketAsync(int userId, string message)
        {
            await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
}
