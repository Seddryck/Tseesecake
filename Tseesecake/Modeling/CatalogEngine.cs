using DubUrl;
using Sprache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Arrangers;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Parsing.Meta;
using Tseesecake.Parsing.Query;

namespace Tseesecake.Modeling
{
    internal class CatalogEngine
    {
        public Timeseries[] Timeseries { get; set; }

        public CatalogEngine(Timeseries[] timeseries)
            => (Timeseries) = (timeseries);

        public IDataReader ExecuteReader(IShowStatement statement)
            => statement switch
            {
                ShowAllTimeseries all => ExecuteShowAllTimeseries(),
                ShowFieldsTimeseries fields => ExecuteShowFieldsTimeseries(fields.TimeseriesName),
                _ => throw new NotImplementedException()
            };

        public IDataReader ExecuteReader(string query)
        {
            var parser = TimeseriesMetaParser.Show;
            var statement = parser.Parse(query);
            return ExecuteReader(statement);
        }

        protected virtual IDataReader ExecuteShowAllTimeseries()
        {
            var table = new DataTable();
            table.Columns.Add("TimeseriesName", typeof(string));
            foreach (var ts in Timeseries)
            {
                var row = table.NewRow();
                row["TimeseriesName"] = ts.Name;
                table.Rows.Add(row);
            }
            table.AcceptChanges();
            return table.CreateDataReader();
        }

        protected virtual IDataReader ExecuteShowFieldsTimeseries(string timeseries)
        {
            var table = new DataTable();
            var metas = new[] { "TimeseriesName", "ColumnName", "Position", "FamilyType", "DataType"};
            foreach (var meta in metas)
                table.Columns.Add(meta, meta=="Position" ? typeof(int) : typeof(string));

            var ts = Timeseries.SingleOrDefault(x => x.Name == timeseries);
            if (ts is not null)
            {
                var pos = 1;
                foreach (var column in ts.Columns)
                {
                    if (column is not Column col)
                        throw new InvalidOperationException();

                    var row = table.NewRow();
                    row["TimeseriesName"] = ts.Name;
                    row["ColumnName"] = column.Name;
                    row["Position"] = pos;
                    row["FamilyType"] = col.Family;
                    row["DataType"] = col.DbType;
                    table.Rows.Add(row);
                    pos += 1;
                }
            }
            table.AcceptChanges();
            return table.CreateDataReader();
        }
    }
}
