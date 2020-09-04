﻿using System;
using Lib.Csv.ParsecSharp.Either;
using Lib.Csv.ParsecSharp.IO;

namespace Lib.Csv.ParsecSharp
{
   public static class ParseResult
   {
      public static IEither<T, ParseError> Success<T>(T value)
      {
         return Either.Either.Success<T, ParseError>(value);
      }

      public static IEither<T, ParseError> Error<T>(ParseError error)
      {
         return Either.Either.Error<T, ParseError>(error);
      }

      public static IEither<T, ParseError> Error<T>(IInputReader input, string message)
      {
         return Error<T>(new ParseError(input.GetPosition(), message));
      }

      public static IEither<T, ParseError> Error<T>(IInputReader input, Func<string> messageFn)
      {
         return Error<T>(new ParseError(input.GetPosition(), messageFn));
      }

      public static IEither<T, ParseError> Error<T>(IInputReader input, Func<string> messageFn, ParseError innerError)
      {
         return Error<T>(new ParseError(input.GetPosition(), messageFn, innerError));
      }

      public static IEither<T, ParseError> UnexpectedValue<T>(IInputReader input, object value)
      {
         return Error<T>(new ParseError(input.GetPosition(), "Unexpected \"" + value.ToString() + "\""));
      }
   }
}
