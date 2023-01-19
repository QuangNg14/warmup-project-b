using System.Threading.Channels;
using Microsoft.AspNetCore.SignalR;
using Models;
using Warmup_teamb_backend.Services;
namespace Warmup_teamb_backend.Hubs
{
    public class ChatHub : Hub
    {
        private readonly DatabaseService _dbService;
        //All the following functions are for groups
        public ChatHub(
            DatabaseService dbService
            )
        {
            _dbService = dbService;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinRoom(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            Room newRoom = new Room
            {
                Name = room
            };
            await _dbService.CreateAsync(newRoom);
            await Clients.Group(room).SendAsync("AddedToRoom", "Warmup Project Server", $"Category {room} just got another subscribe");

        }

        public async Task SendRoomMessage(string room, string message)
        {
            CustomData messageData = new CustomData
            {
                Message = message,
                CreatedAt = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds,
                CategoryType = room
            };

            await Clients.Group(room).SendAsync("ReceiveMessage", messageData.Message, messageData.CreatedAt, messageData.CategoryType);
            await _dbService.CreateAsync(messageData);
        }

        public Task LeaveRoom(string room)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
        }

        public async Task AddCategory(string category)
        {
            //Add new category to DB
            await Clients.All.SendAsync("NewCategory", category);
        }

        public async Task GetCategories()
        {
            //Get all categories from DB
            string[] categories = { "categoryOne", "categoryTwo", "categoryThree" };
            await Clients.All.SendAsync("GetCategories", categories);
        }

        public async Task RemoveCategory(string category)
        {
            //Get all messages from db
            await Clients.All.SendAsync("RemovedCategory", category);
        }
    }
}