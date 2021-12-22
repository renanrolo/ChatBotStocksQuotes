using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBotStocksQuotes.Api.Filters
{
    public class TokenFilter : IHubFilter
    {
        public async ValueTask<object> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
        {
            //string expClaim = invocationContext.Context.User.Claims.First(x => x.Type == "exp").Value;
            //DateTime ExpiryTime = UnixTimeStampToDateTime(Convert.ToDouble(expClaim));

            //if (ExpiryTime < DateTime.UtcNow)
            //{
            //    throw new HubException("Token Expired");
            //}
            //else
            //{
                return await next(invocationContext);
            //}
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
