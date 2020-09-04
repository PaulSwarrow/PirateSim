using System;
using System.Collections.Generic;
using Lib.UnityQuickTools.Collections;

namespace Lib.Csv
{
    public class CsvTemplateModel : CsvModel
    {
        private string[] templateValues;
        
        public CsvTemplateModel(string[] header, string[] templateValues) : base(header)
        {
            this.templateValues = (string[]) templateValues.Clone();
        }

        public void AddVariant(Dictionary<string, string> overrideMap)
        {
            var newValues = (string[]) templateValues.Clone();
            var valuesCount = newValues.Length;
            
            foreach (var kvp in overrideMap)
            {
                var fieldIdx = GetFieldIdx(kvp.Key);
                if (fieldIdx >= 0 && fieldIdx < valuesCount)
                {
                    newValues[fieldIdx] = kvp.Value;
                }
            }
            
            Lines.Add(newValues);
        }

        public static bool TryCreateByField(CsvModel sourceModel, string fieldName, Func<string, bool> pred,
            out CsvTemplateModel result)
        {
            result = null;
            
            var fieldIdx = sourceModel.GetFieldIdx(fieldName);
            if (fieldIdx < 0)
            {
                return false;
            }

            if (sourceModel.Lines.TryFind(values => pred.Invoke(values[fieldIdx]), out var templateValues))
            {
                result = new CsvTemplateModel(sourceModel.Header, templateValues);
                return true;
            }

            return false;
        }
    }
}