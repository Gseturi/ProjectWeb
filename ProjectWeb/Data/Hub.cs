using Microsoft.AspNetCore.SignalR;
using ProjectWeb.Data;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace ProjectWeb.Data
{
    public class ChatHub:Hub
    {
        ApplicationDbContext _Context;
        ApplicationUser _User;
        public ChatHub(ApplicationDbContext Context) { 
            
             _Context = Context;
             
                
        
        
        }

      
        public Task SendMessege(string user,string messege)
        {   
            
            return Clients.All.SendAsync("ReciveMessege",user, messege);
           
        }

        public Task SendFriendRequest(ApplicationUser USer, string friend) {


           // return Clients.AllExcept();
           
        }
    }
}
public class ChatHubb : Hub
{
    private readonly ApplicationDbContext _context;
    private static readonly ConcurrentDictionary<string, UserConnection> _userConnections = new ConcurrentDictionary<string, UserConnection>();

    public ChatHubb(ApplicationDbContext context)
    {
        _context = context;
    }

    public override Task OnConnectedAsync()
    {
        // Get the user ID from the context (assuming you have a way to identify the user)
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            var userConnection = new UserConnection
            {
                ConnectionId = Context.ConnectionId,
                UserName = Context.User?.Identity?.Name
            };
            _userConnections[userId] = userConnection;
        }
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        // Remove the user ID and connection ID mapping when the user disconnects
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            _userConnections.TryRemove(userId, out _);
        }
        return base.OnDisconnectedAsync(exception);
    }

    public Task SendMessage(string user, string message)
    {
        return Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task SendFriendRequest(string userId, string friendId)
    {
        // Find the connection ID of the user with the specified friendId
        if (_userConnections.TryGetValue(friendId, out UserConnection friendConnection))
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                await Clients.Client(friendConnection.ConnectionId).SendAsync("ReceiveFriendRequest", user.UserName, userId);
            }
        }
        else
        {
            // Handle the case where the friend is not connected
            // You might want to store the request and notify them when they connect
        }
    }
}

public class UserConnection
{
    public string ConnectionId { get; set; }
    public string UserName { get; set; }
}