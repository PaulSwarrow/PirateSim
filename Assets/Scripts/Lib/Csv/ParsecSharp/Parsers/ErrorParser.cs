using Lib.Csv.ParsecSharp.Either;
using Lib.Csv.ParsecSharp.IO;

namespace Lib.Csv.ParsecSharp.Parsers
{
   internal class ErrorParser<T> : IParser<T>
   {
      private string message;
      public ErrorParser(string message)
      {
         this.message = message;
      }

      public IEither<T, ParseError> Parse(IInputReader input)
      {
         return ParseResult.Error<T>(input, this.message);
      }
   }
}
