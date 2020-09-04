using Lib.Csv.ParsecSharp.Either;
using Lib.Csv.ParsecSharp.IO;

namespace Lib.Csv.ParsecSharp
{
   public interface IParser<out T>
   {
      /// <summary>
      /// Runs the parser on the given input.
      /// </summary>
      IEither<T, ParseError> Parse(IInputReader input);
   }
}
