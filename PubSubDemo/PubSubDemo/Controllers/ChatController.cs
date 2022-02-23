using Azure.Messaging.WebPubSub;
using Microsoft.Azure.WebPubSub.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EntityLayer;
using BusinessLogicLayer;

namespace PubSubDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        ILogger<ChatController> _logger;
        public ChatController(ILogger<ChatController> logger)
        {
            _logger = logger;
        }
        [Route("get/uri")]
        [HttpGet]
        public async Task<ActionResult> GetUri(string Username)
        {
            var connectionString = Environment.GetEnvironmentVariable("PUBSUB_ENDPOINT_BS");
            var hub = "chat";
            var serviceClient = new WebPubSubServiceClient(connectionString, hub);
            var url = await serviceClient.GetClientAccessUriAsync(userId: Username);
            var absUrl = url.AbsoluteUri;
            return Ok(new { Message = "Uri Fetch Successful", PubSubUri = absUrl, Status = "Ok" });
        }
        [Route("send/to/all")]
        [HttpPost]
        public async Task<ActionResult> SendToAll([FromForm]Message msg)
        {
            var connectionString = Environment.GetEnvironmentVariable("PUBSUB_ENDPOINT_BS");
            var hub = "chat";
            var serviceClient = new WebPubSubServiceClient(connectionString, hub);
            await serviceClient.SendToAllAsync($"[{msg.Sender}] {msg.Text}");
            return Ok(new { Message = "Sent To All", Code = "200", Status = "Ok" });
        }
        [Route("send/to/user")]
        [HttpPost]
        public async Task<ActionResult> SendToUser([FromForm] Message msg)
        {
            var connectionString = Environment.GetEnvironmentVariable("PUBSUB_ENDPOINT_BS");
            var hub = "chat";
            var serviceClient = new WebPubSubServiceClient(connectionString, hub);
            await serviceClient.SendToUserAsync(userId: msg.Receiver, $"[{msg.Sender}] {msg.Text}");
            //Add Message To CosmosDb
            await MessageLogic.AddMessageAsync(msg);
            return Ok(new { Message = $"Sent To {msg.Receiver}", Code = "200", Status = "Ok" });
        }
        /// <summary>
        /// AddUserToGroup is actually a quick way to add current connections for this user to the group
        /// which means, if a connection for userA connects after the AddUserToGroup(GroupA) call,
        /// this new connection is not joined to GroupA automatically. So actually the user can have a situation
        /// where some connections for userA belong to groupA while other connections for userA are not.
        /// </summary>
        [Route("add/user/to/group")]
        [HttpGet]
        public async Task<ActionResult> AddUserToGroup(string username, string groupname)
        {
            var connectionString = Environment.GetEnvironmentVariable("PUBSUB_ENDPOINT_BS");
            var hub = "chat";
            var serviceClient = new WebPubSubServiceClient(connectionString, hub);
            await serviceClient.AddUserToGroupAsync(group: groupname, userId: username);
            return Ok(new { Message = $"Added {username} To {groupname}", Code = "200", Status = "Ok" });
        }
        [Route("add/connection/to/group")]
        [HttpGet]
        public async Task<ActionResult> AddConnectionToGroup(string connectionid, string groupname)
        {
            var connectionString = Environment.GetEnvironmentVariable("PUBSUB_ENDPOINT_BS");
            var hub = "chat";
            var serviceClient = new WebPubSubServiceClient(connectionString, hub);
            await serviceClient.AddConnectionToGroupAsync(group: groupname, connectionId: connectionid);
            return Ok(new { Message = $"Added Connection To {groupname}", Code = "200", Status = "Ok" });
        }
        [Route("send/to/group")]
        [HttpPost]
        public async Task<ActionResult> SendToGroup([FromForm] Message msg, string groupname)
        {
            var connectionString = Environment.GetEnvironmentVariable("PUBSUB_ENDPOINT_BS");
            var hub = "chat";
            var serviceClient = new WebPubSubServiceClient(connectionString, hub);
            await serviceClient.SendToGroupAsync(group: groupname, $"[{msg.Sender}] {msg.Text}");
            return Ok(new { Message = $"Sent To {msg.Receiver}", Code = "200", Status = "Ok" });
        }
        //Get Messages Stored in DB
        [Route("get/chats")]
        [HttpGet]
        public async Task<ActionResult<Message>> GetAll(string sender, string receiver)
        {
            var msgs = await MessageLogic.GetMessagesAsync(sender, receiver);
            return Ok(new { Code = "200", Status = "Ok", Messages = msgs});
        }
    }
}
