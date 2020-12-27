using System;
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
        
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            string functionCode = context.ImplementationMethod.GetFunctionCode();
            IAppContext appContext = context.ServiceProvider.GetRequiredService<IAppContext>();
            string contextValue =  appContext.GetValue(ContextKey)?.ToString();
            var  throttleCode = new ThrottleCode
            {
                 ContextKind = ContextKey,
                 FunctionCode = functionCode,
                 ContextValue = contextValue
            };
            var throttleValue = new ThrottleValue();
            
            var throttleService = context.ServiceProvider.GetRequiredService<IThrottleService>();
            if (await throttleService.ShouldPass(throttleCode, throttleValue))
            {
                
                await next.Invoke(context);
            }
            else
            {
                throw new ApplicationException("Throttle limited.");
                
            }
        }
        
    }
}
