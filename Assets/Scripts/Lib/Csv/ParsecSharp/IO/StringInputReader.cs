using System;
using Lib.Csv.ParsecSharp.Helpers;

namespace Lib.Csv.ParsecSharp.IO
{
   public class StringInputReader : AbstractInputReader
   {
      private String input;
      private int length;

      public StringInputReader(String input)
      {
         Throw.IfNull(input, "input");
         
         this.input = input;
         this.length = input.Length;
      }

      override public char Read()
      {
         if (EndOfStream)
            return '\0';

         char c = input[this.offset++];

         UpdatePosition(c);

         return c;
      }

      override public void Seek(Position position)
      {
         Throw.IfNull(position, "position");

         UpdatePosition(position);
      }

      override public bool EndOfStream
      {
         get { return this.offset == this.length; }
      }
   }
}
