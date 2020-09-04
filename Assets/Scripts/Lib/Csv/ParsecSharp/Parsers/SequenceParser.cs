using System.Collections.Generic;
using System.Linq;
using Lib.Csv.ParsecSharp.Either;
using Lib.Csv.ParsecSharp.Helpers;
using Lib.Csv.ParsecSharp.IO;

namespace Lib.Csv.ParsecSharp.Parsers
{
   internal class SequenceParser<T> : IParser<IEnumerable<T>>
   {
      private IEnumerable<IParser<T>> parsers;

      public SequenceParser(IEnumerable<IParser<T>> parsers)
      {
         Throw.IfNull(parsers, "parsers");

         this.parsers = parsers;
      }

      public IEither<IEnumerable<T>, ParseError> Parse(IInputReader input)
      {
         List<T> acc = new List<T>(parsers.Count());

         foreach (IParser<T> parser in parsers)
         {
            IEither<T, ParseError> result = parser.Parse(input);
            if (result.IsSuccess)
               acc.Add(result.FromSuccess());
            else
               return ParseResult.Error<IEnumerable<T>>(result.FromError());
         }

         return ParseResult.Success(acc);
      }
   }
}
