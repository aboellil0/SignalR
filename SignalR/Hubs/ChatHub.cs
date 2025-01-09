using Microsoft.AspNetCore.SignalR;
using SignalR.Models;
using System;

namespace SignalR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly SignalRContext _context;

        public ChatHub(SignalRContext context)
        {
            this._context = context;
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
                

                //boadcasting 
                Clients.Group(groupname).SendAsync("groupsend", name, groupname);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in JoinToGroup: {ex.Message}");
            }
        }

        [HubMethodName("sendtogroup")]
        public void SendToGroup(string groupname, string name,string message)
        {
            try
            {
                //send to group
                
                //
                Clients.Group(groupname).SendAsync("newgroupmessage", name, message);
            }
            catch (Exception ex)
            {
                ILogger logger = null;
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
