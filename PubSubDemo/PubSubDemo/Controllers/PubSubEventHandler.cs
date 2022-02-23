using Microsoft.Azure.WebPubSub.AspNetCore;
using Microsoft.Azure.WebPubSub.Common;

namespace PubSubDemo.Controllers
{
    sealed class PubSubEventHandler : WebPubSubHub
    {
        private readonly WebPubSubServiceClient<PubSubEventHandler> _serviceClient;

        public PubSubEventHandler(WebPubSubServiceClient<PubSubEventHandler> serviceClient)
        {
            _serviceClient = serviceClient;
        }
        public override async Task OnConnectedAsync(ConnectedEventRequest request)
        {
            await _serviceClient.SendToUserAsync(request.ConnectionContext.UserId, $"[EVENT] You are connected!");
        }

    }
}
