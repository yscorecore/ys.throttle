using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using YS.AppContext;
using YS.Knife;
using YS.Knife.Aop;

namespace YS.Throttle
{
    public class ThrottleAttribute: BaseAopAttribute
    {
        public string ContextKey { get; set; } = AppContextKeys.UserId;
        
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            string functionCode = context.ImplementationMethod.GetFunctionCode();
            IAppContext appContext = context.ServiceProvider.GetRequiredService<IAppContext>();
            object contextValue = appContext.GetValue(ContextKey);
            string throttleCode = $"{functionCode}/{ContextKey}/{contextValue}";
            return  Task.CompletedTask;
        }
        
    }
}
