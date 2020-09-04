using System;

namespace Lib.Csv
{
    public class CsvReadTableParams<T>
    {
        public bool excludeFirstColumn;
        public Func<string, T> converter;
    }

    public class CsvReadMapParams
    {
        public string keyField;
    }
}