﻿using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Parsing
{
    internal class Grammar
    {
        public static readonly Parser<string> Textual = Parse.Letter.AtLeastOnce().Text().Token();
        public static readonly Parser<string> DoubleQuotedTextual = Parse.CharExcept("\"").AtLeastOnce().Text().Contained(Parse.Char('\"'), Parse.Char('\"')).Token();
        public static readonly Parser<string> SingleQuotedTextual = Parse.CharExcept("\'").AtLeastOnce().Text().Contained(Parse.Char('\''), Parse.Char('\'')).Token();

        public static readonly Parser<string> Identifier = Textual.Or(DoubleQuotedTextual);

        public static readonly Parser<char> Terminator = Parse.Char(';').Token();
    }
}
