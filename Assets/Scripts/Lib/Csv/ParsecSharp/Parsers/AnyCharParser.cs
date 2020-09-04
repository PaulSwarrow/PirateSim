using Lib.Csv.ParsecSharp.Either;
using Lib.Csv.ParsecSharp.IO;

namespace Lib.Csv.ParsecSharp.Parsers
{
   internal class AnyCharParser : IParser<char>
   {
      public IEither<char, ParseError> Parse(IInputReader input)
      {
         if (input.EndOfStream)
            return ParseResult.Error<char>(input, "Unexpected end of input");

         char c = input.Read();
         return ParseResult.Success(c);
      }
   }
}
