using System.Threading;
using System.Threading.Tasks;

namespace SignalRDemo.Scheduler
{
    public interface IPeriodicTask
    {
        int Interval{ get; }
        void Execute();
    }
}