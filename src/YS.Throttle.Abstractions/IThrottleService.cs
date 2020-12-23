namespace YS.Throttle
{
    public interface IThrottleService
    {
        bool ShouldPass(ThrottleCode throttleCode);
    }
}
