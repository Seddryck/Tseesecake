using DubUrl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Engine.Statements;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;

namespace Tseesecake.Engine.Mounting
{
    public class DmlEngine
    {
        private ConnectionUrl ConnectionUrl { get; }

        public DmlEngine(ConnectionUrlFactory factory, string url)
            => ConnectionUrl = factory.Instantiate(url);

        public void Mount(Timeseries ts)
        {
            using var conn = ConnectionUrl.Open();
            try
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = new DmlCommand(ts).Read(ConnectionUrl.Dialect, ConnectionUrl.Connectivity);
                cmd.ExecuteNonQuery();
            }
            catch { throw; }
            finally { conn.Close(); }
        }

        public T ExecuteScalar<T>(SelectStatement query)
        {
            using var conn = ConnectionUrl.Open();
            try
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = new SelectCommand(query).Read(ConnectionUrl.Dialect, ConnectionUrl.Connectivity);
                return (T)(cmd.ExecuteScalar() ?? throw new NullReferenceException());
            }
            catch { throw; }
            finally { conn.Close(); }
        }
    }
}