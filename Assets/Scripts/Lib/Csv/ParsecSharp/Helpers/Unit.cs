namespace Lib.Csv.ParsecSharp.Helpers
{
   public sealed class Unit
   {
      private Unit() { }

      private static Unit _Instance = new Unit();

      public static Unit Instance
      {
         get
         {
            return _Instance;
         }
      }
   }
}
