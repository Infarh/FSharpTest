using System;
using System.Threading.Tasks;
using FSharpLib;

namespace FSharpTest.Servcies.SignalRStab
{
    internal abstract class ClientConcurrentDictionary : SignalRBase
    {
        private readonly AgentOnlineUsers __Connections = new AgentOnlineUsers();

        public override Task OnConnected()
        {
            var connection_id = new Guid(Context.ConnectionId);
            var user = Context.User;
            var user_name = user.Identity!.Name;

            __Connections.AddIfNotExists(connection_id, user_name);

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            var connection_id = new Guid(Context.ConnectionId);

            __Connections.RemoveIfNotExists(connection_id);

            return base.OnDisconnected();
        }

        protected abstract void RegisterUserConnection(Guid ConnectionId, string UserName);

        protected abstract void DeregisterUserConnection(Guid ConnectionId, string UserName);
    }
}