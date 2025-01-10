using Microsoft.AspNetCore.SignalR;
using SignalR.Models;
using System;

namespace SignalR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly SignalRContext _context;
        private readonly ILogger logger;

        public ChatHub(SignalRContext context,ILogger<ChatHub> logger)
        {
            this._context = context;
            this.logger = logger;
        }
        [HubMethodName("SendMessege")]
        public void Aboellil(string name, string message)
        {
            //save in DB 
            _context.Chats.Add(new Chat { UserName = name, Messege = message });
            _context.SaveChanges();

            //broadcasting to all online clints
            
            Clients.All.SendAsync("NewMessege", name, message).Wait();
        }
        public void GetAll()
        {

            var chat = _context.Chats.ToList();
            foreach (var chatItem in chat)
            {
                Clients.All.SendAsync("GetAllMesseges", chatItem.UserName, chatItem.Messege);
            }
        }
        [HubMethodName("joingroup")]
        public void JoinToGroup(string groupname,string name)
        {
            try
            {
                //add to group
                Groups.AddToGroupAsync(Context.ConnectionId, groupname);

                //save in database
                _context.Groups.Add(new Group { GroupName = groupname });

                //boadcasting 
                Clients.Group(groupname).SendAsync("groupsend", name, groupname);
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Error in JoinToGroup: {ex.Message}");
            }
        }

        [HubMethodName("sendtogroup")]
        public void SendToGroup(string groupname, string name,string message)
        {
            try
            {
                //Save in DB
                _context.Groups.FirstOrDefault(e => e.GroupName == groupname)
                    .Messages.Add(new GroupMessage { SinderName = groupname, Message = message });
                _context.SaveChanges() ;

                //bopadcasting
                Clients.Group(groupname).SendAsync("newgroupmessage",groupname, name, message);
            }
            catch (Exception ex)
            {
                logger.LogInformation($"################Error: {ex.Message}");
            }
        }

        [HubMethodName("getgroupmessages")]
        public void GetAllGroupMessages(string groupname)
        {
            try
            {
                var message = _context.Groups.FirstOrDefault(e=>e.GroupName == groupname).Messages.ToList();
                foreach (var groupMessag in message)
                {
                    Clients.Group(groupname).SendAsync("getgroupmessages",groupname,groupMessag.SinderName, groupMessag.Message);
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation($"################Error: {ex.Message}");
            }
        }
        public override Task OnConnectedAsync()
        {
            string conId = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

    }
}
