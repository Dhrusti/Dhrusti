using DTO;
using Microsoft.AspNetCore.SignalR;

namespace WaltCapitalManagementWebAPI.Hubs
{
    //public class ChatHub : Hub<IChatClient>
    //{
    //    public async Task SendMessage(ChatMessage message)
    //    {
    //        await Clients.All.ReceiveMessage(message);
    //    }
    //}

    public class ChatHub : Hub
    {
        protected IHubContext<ChatHub> _context;

        public ChatHub(IHubContext<ChatHub> context)
        {
            _context = context;
        }
        public async Task SendMessage(ChatMessage message)
        {
            var data = _context.Clients;

            //string logFileName = System._commonHelper.GetCurrentDateTime().ToString("dd/MM/yyyy").Replace('/', '_').ToString() + ".log";
            //var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Logs");
            //var exists = Directory.Exists(filePath);
            //if (!exists)
            //{
            //    Directory.CreateDirectory(filePath);
            //}
            //filePath = Path.Combine(filePath, logFileName);
            //using (StreamWriter writer = new StreamWriter(filePath, true))
            //{
            //    text = _commonHelper.GetCurrentDateTime().ToString() + " : " + text;
            //    writer.WriteLine(string.Format(text, _commonHelper.GetCurrentDateTime().ToString("dd/MM/yyyy hh:mm:ss tt")));
            //    writer.Close();
            //}

            await _context.Clients.All.SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            var data = _context.Clients;
            await base.OnConnectedAsync();
            Console.WriteLine("connected!! " + Context.ConnectionId);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var data = _context.Clients;
            await base.OnDisconnectedAsync(exception);
            Console.WriteLine("disconnected!!");
        }
    }
    /*public class ChatHub : Hub<IChatClient>
    { }*/
    /*public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        //private readonly string _botUser;

        //private readonly WaltCapitalDBContext _dbContext;
        //public ChatHub()
        //{

        //}


        //public void InsertTask(TaskViewModel task)
        //{
        //    Clients.Caller.onInsertTask(task, false);
        //    Clients.Others.onInsertTask(task, true);
        //}
    }*/
}


