using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using AlgorithmsLib;

namespace FSharpTest.Servcies.SignalRStab
{
    internal abstract class ClientWithAtom : SignalRBase
    {
        private readonly Atom<ImmutableDictionary<Guid, string>> _Connections =
            new Atom<ImmutableDictionary<Guid, string>>(ImmutableDictionary<Guid, string>.Empty);

        public override Task OnConnected()
        {
            var connection_id = new Guid(Context.ConnectionId);
            var user = Context.User;
            var user_name = user.Identity!.Name;

            var current_conn = _Connections.Value;
            if (_Connections.Swap(conn => conn.ContainsKey(connection_id) ? conn : conn.Add(connection_id, user_name)) != current_conn)
                RegisterUserConnection(connection_id, user_name);

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            var connection_id = new Guid(Context.ConnectionId);

            var current_conn = _Connections.Value;
            if (current_conn.TryGetValue(connection_id, out var user_name))
            {
                DeregisterUserConnection(connection_id, user_name);
                _Connections.Swap(conn => conn.ContainsKey(connection_id) ? conn.Remove(connection_id) : conn);
            }

            return base.OnDisconnected();
        }


        protected abstract void RegisterUserConnection(Guid ConnectionId, string UserName);

        protected abstract void DeregisterUserConnection(Guid ConnectionId, string UserName);
    }
}