using DubUrl.Extensions.DependencyInjection;
using DubUrl.Querying;
using DubUrl.Registering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Engine;
using Tseesecake.Modeling;
using Tseesecake.Testing.Engine;

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

        public class ConsoleLogger : IQueryLogger
        {
            public void Log(string message) => Console.WriteLine(message);
        }

        protected void SetupEngine(Type[] engines)
        {
            var options = new DubUrlServiceOptions();
            var services = new ServiceCollection()
               .AddSingleton(EmptyDubUrlConfiguration)
               .AddDubUrl(options)
               .WithQueryLogger(new ConsoleLogger())
               .AddSingleton(new ArrangerCollectionProvider());

            foreach (var engine in engines)
            {
                var parameters = engine == typeof(DmlEngine)
                                    ? new object[] { ConnectionString }
                                    : new object[] { ConnectionString, new Timeseries[] { DmlStatementDefinition.WindEnergy },  };
                services = services.AddTransient(engine, provider => ActivatorUtilities.CreateInstance(provider
                    , engine, parameters));
            }
                
            Provider = services.BuildServiceProvider();

            new ProviderFactoriesRegistrator().Register();
        }
    }
}