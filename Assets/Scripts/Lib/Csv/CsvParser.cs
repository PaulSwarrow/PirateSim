using System.Collections.Generic;
using Lib.Csv.ParsecSharp;

namespace Lib.Csv
{
    public static class CsvParser
    {
        private const char _Separator = ',';

        private static readonly IParser<char> ValueSeparator = 
            Chars.Char(_Separator);

        private static readonly IParser<char> LineSeparator =
            Chars.EndOfLine();

        private static readonly IParser<string> UnquotedValue = 
            Chars.NoneOf(_Separator, '\r', '\n').Many();

        private static readonly IParser<char> Quote = 
            Chars.Char('"');

        private static readonly IParser<char> EscapedQuote =
            Quote.FollowedBy(Quote).Try();

        private static readonly IParser<string> QuotedValue =
            (EscapedQuote.Or(Chars.Not('"'))).Many()
            .Between(Quote);

        private static readonly IParser<string> Value =
            QuotedValue.Or(UnquotedValue);


        private static readonly IParser<IEnumerable<string>> Line =
            Value.SeparatedBy(ValueSeparator);


        private static readonly IParser<IEnumerable<IEnumerable<string>>> Lines =
            Line.SeparatedBy(LineSeparator);

        public static IEnumerable<IEnumerable<string>> Parse(string source)
        {
            var result = Lines.Parse(source);
            return result.IsError ? null : result.FromSuccess();
        }
    }
}