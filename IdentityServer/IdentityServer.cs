using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using Owin;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.InMemory;

namespace IdSrv
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new IdentityServerOptions
            {
                Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(new List<InMemoryUser>()),

                RequireSsl = false
            };

            app.UseIdentityServer(options);
        }
    }
    static class Scopes
    {
        public static List<Scope> Get()
        {
            return new List<Scope>
        {
            new Scope
            {
                Name = "api1"
            }
        };
        }
    }

    static class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
        {
           // no human involved
            new Client
            {
                ClientName = "Silicon-only Client",
                ClientId = "silicon",
                Enabled = true,
                AccessTokenType = AccessTokenType.Reference,

                Flow = Flows.ClientCredentials,

                ClientSecrets = new List<Secret>
                {
                    new Secret("F621F470-9731-4A25-80EF-67A6F7C5F4B8".Sha256())
                },

                AllowedScopes = new List<string>
                {
                    "api1"
                }
            }
        };
        }
    }

}
