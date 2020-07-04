using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FSharpTest.Servcies.SignalRStab
{
    abstract class SignalRBase
    {
        protected ConnectionContext Context { get; } = new ConnectionContext();

        public virtual Task OnConnected() => Task.CompletedTask;

        public virtual Task OnDisconnected() => Task.CompletedTask;
    }
}
