using System;
using Lib.Csv.ParsecSharp.Either;
using Lib.Csv.ParsecSharp.Helpers;
using Lib.Csv.ParsecSharp.IO;

namespace Lib.Csv.ParsecSharp.Parsers
{
   internal class SelectParser<TSource, TResult> : IParser<TResult>
   {
      private IParser<TSource> parser;
      private Func<TSource, TResult> func;

      public SelectParser(IParser<TSource> parser, Func<TSource, TResult> func)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(func, "func");

         this.parser = parser;
         this.func = func;
      }

      public IEither<TResult, ParseError> Parse(IInputReader input)
      {
         var result = this.parser.Parse(input);
         if (result.IsError)
            return ParseResult.Error<TResult>(result.FromError());

         return ParseResult.Success(this.func(result.FromSuccess()));
      }
   }
}
