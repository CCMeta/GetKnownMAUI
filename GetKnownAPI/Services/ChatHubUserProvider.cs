using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;

namespace GetKnownAPI.Services
{
    public class ChatHubUserProvider : IUserIdProvider
    {
        private readonly SessionService _sessionService;

        public ChatHubUserProvider(SessionService sessionService)
        {
            this._sessionService = sessionService;
        }

        public virtual string GetUserId(HubConnectionContext connection)
        {
            var context = GetHttpContextExtensions.GetHttpContext(connection);
            if (context == null)
                return "";
            context.Request.Headers.TryGetValue("Authorization", out var token);
            _sessionService.Sessions.TryGetValue(token, out int uid);
            return uid.ToString();
        }
    }
}
