using Microsoft.AspNetCore.SignalR;
using SignalRNotify;
using System;

namespace ChangeMonitor.Helpers {
    public class SignalRDispatcher {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SignalRDispatcher(IHubContext<NotificationHub> hubContext) {
            _hubContext = hubContext;
        }

        public async void SendToAllClient(String message) {
            if (_hubContext.Clients != null) { 
                await _hubContext.Clients.All.SendAsync("Notify", message);
            }
        }
    }
}
