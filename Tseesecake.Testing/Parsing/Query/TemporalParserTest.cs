﻿using DubUrl.Parsing;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Parsing.Query;
using Tseesecake.Querying.Expressions;
using Tseesecake.Querying.Filters;
using Tseesecake.Querying.Slicers;

namespace Tseesecake.Testing.Parsing.Query
{
    public class TemporalParserTest
    {
        [Test]
        [TestCase("second", TruncationTemporal.Second)]
        [TestCase("seconds", TruncationTemporal.Second)]
        [TestCase("MINUTE", TruncationTemporal.Minute)]
        [TestCase("HoURs", TruncationTemporal.Hour)]
        public void Parse_Truncation_CorrectValue(string text, TruncationTemporal expected)
            => Assert.That(TemporalParser.Truncation.Parse(text), Is.EqualTo(expected));

        [Test]
        [TestCase("day of week", CyclicTemporal.DayOfWeek)]
        [TestCase("Month Of Year", CyclicTemporal.MonthOfYear)]
        [TestCase("WEEK OF YEAR", CyclicTemporal.WeekOfYear)]
        public void Parse_Cyclic_CorrectValue(string text, CyclicTemporal expected)
            => Assert.That(TemporalParser.Cyclic.Parse(text), Is.EqualTo(expected));

        [Test]
        [TestCase("BUCKET Instant BY Month Of Year")]
        [TestCase("BUCKET Instant BY Month")]
        public void Parse_BucketByNamed_CorrectTemplate(string text)
            => Assert.That(TemporalParser.BucketBy.Parse(text), Is.TypeOf<Timestamp>());

        [Test]
        [TestCase("BUCKET BY Month Of Year")]
        [TestCase("BUCKET BY Month")]
        public void Parse_BucketByAnonymous_CorrectTemplate(string text)
            => Assert.That(TemporalParser.BucketBy.Parse(text), Is.TypeOf<AnonymousTimestamp>());

        [Test]
        [TestCase("BUCKET Instant BY Month Of Year", nameof(CyclicTemporalSlicer))]
        [TestCase("BUCKET Instant BY Month", nameof(TruncationTemporalSlicer))]
        [TestCase("BUCKET BY Month Of Year", nameof(CyclicTemporalSlicer))]
        [TestCase("BUCKET BY Month", nameof(TruncationTemporalSlicer))]
        public void Parse_SlicerAnonymous_CorrectTemplate(string text, string template)
            => Assert.That(TemporalParser.Slicer.Parse(text).Template, Is.EqualTo(template));
    }
}
