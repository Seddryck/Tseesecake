using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Frames;

namespace Tseesecake.Parsing.Select
{
    internal class FrameParser
    {
        protected internal static readonly Parser<string> Range = Parse.IgnoreCase("Range").Text().Token();
        protected internal static readonly Parser<string> Rows = Parse.IgnoreCase("Rows").Text().Token();
        protected internal static readonly Parser<string> Between = Parse.IgnoreCase("Between").Text().Token();
        protected internal static readonly Parser<string> Preceding = Parse.IgnoreCase("Preceding").Text().Token();
        protected internal static readonly Parser<string> Following = Parse.IgnoreCase("Following").Text().Token();
        protected internal static readonly Parser<string> Unbounded = Parse.IgnoreCase("Unbounded").Text().Token();
        protected internal static readonly Parser<string> Current = Parse.IgnoreCase("Current").Text().Token();
        protected internal static readonly Parser<string> Row = Parse.IgnoreCase("Row").Text().Token();

        protected internal static Parser<Boundary> UnboundedPreceding =
            Unbounded.Then(_ => Preceding).Return(new UnboundedPreceding());
        
        protected internal static Parser<Boundary> UnboundedFollowing =
            Unbounded.Then(_ => Following).Return(new UnboundedFollowing());
        
        protected internal static Parser<Boundary> CurrentRow =
            Current.Then(_ => Row).Return(new CurrentRow());

        protected internal static Parser<Boundary> BoundaryPreceding = 
            from constant in ExpressionParser.Constant
            from _ in Preceding
            select new Preceding(constant);

        protected internal static Parser<Boundary> BoundaryFollowing =
            from constant in ExpressionParser.Constant
            from _ in Following
            select new Following(constant);

        protected internal static Parser<Boundary> BoundariesPreceding =
            UnboundedPreceding.Or(BoundaryPreceding).Or(CurrentRow);

        protected internal static Parser<Boundary> BoundariesFollowing =
            UnboundedFollowing.Or(BoundaryFollowing).Or(CurrentRow);

        protected internal static Parser<Boundary> Boundaries =
            BoundariesPreceding.Or(BoundariesFollowing);

        protected internal static Parser<IFrame> RangeBetween =
            from _ in Range.Then(_ => Between)
            from preceding in BoundariesPreceding
            from and in Keyword.And
            from following in BoundariesFollowing
            select new RangeBetween(preceding, following);

        protected internal static Parser<IFrame> RowsBetween =
            from _ in Rows.Then(_ => Between)
            from preceding in BoundariesPreceding
            from and in Keyword.And
            from following in BoundariesFollowing
            select new RowsBetween(preceding, following);

        protected internal static Parser<IFrame> RowsSingle =
            from _ in Rows
            from boundary in Boundaries
            select new RowsSingle(boundary);

        public static Parser<IFrame> Frame =
            RangeBetween.Or(RowsBetween).Or(RowsSingle);
    }
}
