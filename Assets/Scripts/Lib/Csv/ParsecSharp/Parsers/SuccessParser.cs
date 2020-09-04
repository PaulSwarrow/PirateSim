using Lib.Csv.ParsecSharp.Either;
using Lib.Csv.ParsecSharp.IO;

namespace Lib.Csv.ParsecSharp.Parsers
{
   internal class SuccessParser<T> : IParser<T>
   {
      private T value;

      public SuccessParser(T value)
      {
         this.value = value;
      }

      public IEither<T, ParseError> Parse(IInputReader input)
      {
         return ParseResult.Success(this.value);
      }
   }
}
