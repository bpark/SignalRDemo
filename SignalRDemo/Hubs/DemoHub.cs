using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.Hubs
{
    public class DemoHub: Hub
    {
        public void Connect(string message)
        {
            Clients.Caller.SendAsync("Ping", "Welcome");
        }    
    }
}