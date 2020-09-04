﻿using System;
using Lib.Csv.ParsecSharp.Either;
using Lib.Csv.ParsecSharp.Helpers;
using Lib.Csv.ParsecSharp.IO;

namespace Lib.Csv.ParsecSharp.Parsers
{
   internal class LabeledParser<T> : IParser<T>
   {
      private IParser<T> parser;
      private Func<string> msgFunc;

      public LabeledParser(IParser<T> parser, Func<string> msgFunc)
      {
         Throw.IfNull(parser, "parser");
         Throw.IfNull(msgFunc, "msgFunc");

         this.parser = parser;
         this.msgFunc = msgFunc;
      }

      public IEither<T, ParseError> Parse(IInputReader input)
      {
         var result = this.parser.Parse(input);
         if (result.IsSuccess)
            return result;

         return ParseResult.Error<T>(input, msgFunc, result.FromError());
      }
   }
}
