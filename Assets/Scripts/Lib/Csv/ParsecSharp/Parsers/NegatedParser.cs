using Lib.Csv.ParsecSharp.Either;
using Lib.Csv.ParsecSharp.Helpers;
using Lib.Csv.ParsecSharp.IO;

namespace Lib.Csv.ParsecSharp.Parsers
{
   internal class NegatedParser<T> : IParser<Unit>
   {
      private IParser<T> parser;

      public NegatedParser(IParser<T> parser)
      {
         Throw.IfNull(parser, "parser");

         this.parser = parser;
      }

      public IEither<Unit, ParseError> Parse(IInputReader input)
      {
         var result = this.parser.Parse(input);
         if (result.IsSuccess)
            return ParseResult.UnexpectedValue<Unit>(input, result.FromSuccess());

         return ParseResult.Success(Unit.Instance);
      }
   }
}
