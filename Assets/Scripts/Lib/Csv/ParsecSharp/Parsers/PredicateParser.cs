using System;
using Lib.Csv.ParsecSharp.Either;
using Lib.Csv.ParsecSharp.Helpers;
using Lib.Csv.ParsecSharp.IO;

namespace Lib.Csv.ParsecSharp.Parsers
{
   internal class PredicateParser<T> : IParser<T>
   {
      private IParser<T> parser;

      public PredicateParser(IParser<T> parser, Predicate<T> predicate)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(predicate, "predicate");

         this.parser = parser;
         this.Predicate = predicate;
      }

      public Predicate<T> Predicate
      {
         get;
         set;
      }

      public IEither<T, ParseError> Parse(IInputReader input)
      {
         var result = this.parser.Parse(input);
         if (result.IsError)
            return result;

         T resultValue = result.FromSuccess();
         if (this.Predicate(resultValue))
            return result;

         return ParseResult.UnexpectedValue<T>(input, resultValue);
      }
   }
}
