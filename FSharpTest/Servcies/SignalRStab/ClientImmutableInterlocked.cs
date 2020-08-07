using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace FSharpTest.Servcies.SignalRStab
{
    internal abstract class ClientImmutableInterlocked : SignalRBase
    {
        private ImmutableDictionary<Guid, string> _Connections = ImmutableDictionary<Guid, string>.Empty;

        public override Task OnConnected()
        {
            var connection_id = new Guid(Context.ConnectionId);
            var user = Context.User;
            var user_name = user.Identity!.Name;

            if (ImmutableInterlocked.TryAdd(ref _Connections, connection_id, user_name))
                RegisterUserConnection(connection_id, user_name);

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            var connection_id = new Guid(Context.ConnectionId);

            if (ImmutableInterlocked.TryRemove(ref _Connections, connection_id, out var user_name)) 
                DeregisterUserConnection(connection_id, user_name);

            return base.OnDisconnected();
        }

        protected abstract void RegisterUserConnection(Guid ConnectionId, string UserName);

        protected abstract void DeregisterUserConnection(Guid ConnectionId, string UserName);
    }
}