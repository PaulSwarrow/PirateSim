using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib.Csv
{
    public class CsvModel
    {
        public string[] Header { get; }
        
        public List<string[]> Lines { get; }

        public int FieldsCount => Header?.Length ?? 0;

        public bool IsInvalid { get; }
        
        public CsvModel(string source)
        {
            var table = CsvParser.Parse(source);
            if (table == null)
            {
                IsInvalid = true;
                return;
            }

            using (var linesEnumerator = table.GetEnumerator())
            {
                if (!linesEnumerator.MoveNext())
                    return;

                Header = GetValues(linesEnumerator.Current);

                Lines = new List<string[]>();
                while (linesEnumerator.MoveNext())
                    Lines.Add(GetValues(linesEnumerator.Current));
            }
        }

        public string[] GetValues(IEnumerable<string> rawValues)
        {
            return rawValues.Select(value => value.Replace(@"\44", ",")).ToArray();
        }

        protected CsvModel(string[] header)
        {
            Header = (string[]) header.Clone();
            Lines = new List<string[]>();
        }
        
        public int GetFieldIdx(string field)
        {
            return field != null ? Array.IndexOf(Header, field) : -1;
        }

        public string GetField(int index) => Header[index];

        public static Task<CsvModel> CreateAsync(string source) => Task.Run(() => new CsvModel(source));
    }
}