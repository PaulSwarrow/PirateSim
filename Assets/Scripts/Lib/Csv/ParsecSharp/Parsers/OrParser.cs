using Lib.Csv.ParsecSharp.Either;
using Lib.Csv.ParsecSharp.Helpers;
using Lib.Csv.ParsecSharp.IO;

namespace Lib.Csv.ParsecSharp.Parsers
{
   internal class OrParser<T> : IParser<T>
   {
      private IParser<T> parserA;
      private IParser<T> parserB;

      public OrParser(IParser<T> parserA, IParser<T> parserB)
      {
         Throw.IfNull(parserA, "parserA");
         Throw.IfNull(parserB, "parserB");

         this.parserA = parserA;
         this.parserB = parserB;
      }

      public IEither<T, ParseError> Parse(IInputReader input)
      {
         Position position = input.GetPosition();

         var result = this.parserA.Parse(input);
         if (result.IsSuccess || input.GetPosition() != position)
            return result;

         return this.parserB.Parse(input);

      }
   }
}
