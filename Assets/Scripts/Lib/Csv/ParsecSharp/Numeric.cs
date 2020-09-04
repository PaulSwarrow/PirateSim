using System.Globalization;

namespace Lib.Csv.ParsecSharp
{
   /// <summary>
   /// Defines Parsers for numeric values.
   /// </summary>
   public static class Numeric
   {
      /// <summary>
      /// Parses an integer value.
      /// </summary>
      public static IParser<int> Int()
      {
         return from x in Chars.Satisfy(c => char.IsDigit(c) 
                                          || c == '-' 
                                          || c == '+').Many1()
                select int.Parse(x);
      }

      /// <summary>
      /// Parses a double value.
      /// </summary>
      public static IParser<double> Double()
      {
         return from x in Chars.Satisfy(c => char.IsDigit(c) 
                                          || c == '-' 
                                          || c == '+' 
                                          || c == '.').Many1()
                select double.Parse(x, CultureInfo.InvariantCulture);
      }
   }
}
