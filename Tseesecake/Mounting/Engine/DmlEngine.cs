using DubUrl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Mounting.Engine;
using Tseesecake.Querying;

namespace Tseesecake.Engine
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
                cmd.CommandText = new DmlStatement(ts).Read(ConnectionUrl.Dialect, ConnectionUrl.Connectivity);
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
                cmd.CommandText = new ElementalQuery(query).Read(ConnectionUrl.Dialect, ConnectionUrl.Connectivity);
                return (T)(cmd.ExecuteScalar() ?? throw new NullReferenceException());
            }
            catch { throw; }
            finally { conn.Close(); }
        }
            
    }
}
