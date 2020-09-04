using System;

namespace Lib.Csv.ParsecSharp.Helpers
{
   public static class Throw
   {
      public static void IfNull(object value, string paramName)
      {
         if (value == null)
            throw new ArgumentNullException(paramName);
      }
   }
}
