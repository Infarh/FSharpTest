#nullable enable
using System;
using System.Security.Principal;

namespace FSharpTest.Servcies.SignalRStab
{
    internal class ConnectionContext
    {
        private class PrincipalStab : IPrincipal
        {
            public bool IsInRole(string role) => true;

            public IIdentity? Identity { get; } = new Stab();
        }

        private class Stab : IIdentity
        {
            public string? AuthenticationType { get; } = "Test";
            public bool IsAuthenticated { get; } = true;
            public string? Name { get; } = "UserName";
        }

        public byte[] ConnectionId => Guid.NewGuid().ToByteArray();

        public IPrincipal User { get; } = new PrincipalStab();
    }
}
