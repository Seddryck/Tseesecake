using DubUrl.Extensions.DependencyInjection;
using DubUrl.Registering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.QA
{
    public abstract class BaseQATest
    {
        public abstract string ConnectionString { get; }

        private ServiceProvider? provider;
        protected ServiceProvider Provider
        {
            get => provider ?? throw new InvalidOperationException();
            set => provider = value;
        }

        protected static IConfiguration EmptyDubUrlConfiguration
        {
            get
            {
                var builder = new ConfigurationBuilder().AddInMemoryCollection();
                return builder.Build();
            }
        }

        protected void SetupEngine(Type[] engines)
        {
            var options = new DubUrlServiceOptions();
            var services = new ServiceCollection()
               .AddSingleton(EmptyDubUrlConfiguration)
               .AddDubUrl(options);

            foreach (var engine in engines)
                services = services.AddTransient(engine, provider => ActivatorUtilities.CreateInstance(provider
                    , engine, new[] { ConnectionString }));
            Provider = services.BuildServiceProvider();

            new ProviderFactoriesRegistrator().Register();
        }
    }
}