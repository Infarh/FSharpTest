using System;
using System.Collections.Generic;
using System.Threading.Tasks;
// ReSharper disable MemberCanBeMadeStatic.Local

namespace FSharpTest.Servcies.SignalRStab
{
    internal abstract class Client : SignalRBase
    {
        private readonly Dictionary<Guid, string> _Connections = new Dictionary<Guid, string>();

        public override Task OnConnected()
        {
            var connection_id = new Guid(Context.ConnectionId);
            var user = Context.User;

            if (!_Connections.ContainsKey(connection_id))
            {
                var user_name = user.Identity!.Name;
                RegisterUserConnection(connection_id, user_name);
                _Connections.Add(connection_id, user_name);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            var connection_id = new Guid(Context.ConnectionId);

            if (_Connections.TryGetValue(connection_id, out var user_name))
            {
                DeregisterUserConnection(connection_id, user_name);
                _Connections.Remove(connection_id);
            }

            return base.OnDisconnected();
        }


        protected abstract void RegisterUserConnection(Guid ConnectionId, string UserName);

        protected abstract void DeregisterUserConnection(Guid ConnectionId, string UserName);
    }
}
