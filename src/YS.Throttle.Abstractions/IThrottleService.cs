using System.Threading.Tasks;

namespace YS.Throttle
{
    public interface IThrottleService
    {
        
        Task<bool> ShouldPass(ThrottleCode throttleCode, ThrottleValue throttleValue);
    }
}
