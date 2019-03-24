using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Hubs;

namespace SignalRDemo.Scheduler
{
    public class PingTask : IPeriodicTask
    {
        public int Interval => 1000;

        private readonly IHubContext<DemoHub> _demoHub;

        public PingTask(IHubContext<DemoHub> demoHub)
        {
            _demoHub = demoHub;
        }

        public void Execute()
        {
            _demoHub.Clients.All.SendAsync("Ping", "Ping");
        }
    }
}