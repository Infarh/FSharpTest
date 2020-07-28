using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using FSharpLib;

namespace FSharpTest.Servcies.SignalRStab
{
    abstract class ClientWithAgents : SignalRBase
    {
        private readonly ConcurrentDictionary<Guid, string> _Connections = new ConcurrentDictionary<Guid, string>();

        public override Task OnConnected()
        {
            var connection_id = new Guid(Context.ConnectionId);
            var user = Context.User;
            var user_name = user.Identity!.Name;

            if (_Connections.TryAdd(connection_id, user_name))
                RegisterUserConnection(connection_id, user_name);

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            var connection_id = new Guid(Context.ConnectionId);

            if (_Connections.TryRemove(connection_id, out var user_name))
                DeregisterUserConnection(connection_id, user_name);

            return base.OnDisconnected();
        }

        protected abstract void RegisterUserConnection(Guid ConnectionId, string UserName);

        protected abstract void DeregisterUserConnection(Guid ConnectionId, string UserName);
    }
}