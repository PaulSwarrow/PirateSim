using Lib.Csv.ParsecSharp.Either;
using Lib.Csv.ParsecSharp.IO;

namespace Lib.Csv.ParsecSharp.Parsers
{
   internal class EofParser<T> : IParser<T>
   {
      public IEither<T, ParseError> Parse(IInputReader input)
      {
         if (input.EndOfStream)
            return ParseResult.Success(default(T));

         return ParseResult.Error<T>(input, "Expected end of input");
      }
   }
}
