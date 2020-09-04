using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lib.Csv
{
    public class CsvReader
    {
        

        public static List<Dictionary<string, object>> ReadFromFile (string file)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(file);
            return Read(textAsset.text);
        }
        
        public static List<Dictionary<string, object>> Read(string text) {
            var list = new List<Dictionary<string, object>> ();
            
            var model = new CsvModel(text);
            if (model.IsInvalid) return list;

            foreach (var values in model.Lines) {
                if (values.Length == 0) continue;

                var entry = new Dictionary<string, object> ();
                for (var j = 0; j < model.FieldsCount && j < values.Length; j++) {
                    string value = values [j];
                    value = value.TrimStart ('\"').TrimEnd ('\"');//.Replace ("\\", "");
//                    if (value.Contains(',')) 
//                        value = value.Replace(',', '.');
                    
                    object finalvalue = value;
                    int n;
                    float f;
                    if (int.TryParse (value, out n)) {
                        finalvalue = n;
                    } else if (float.TryParse (value, out f)) {
                        finalvalue = f;
                    }
                    entry [model.GetField(j)] = finalvalue;
                }
                list.Add (entry);
            }
            return list;
        }
        
        public static List<List<T>> ReadAsTable<T>(string text, CsvReadTableParams<T> parameters = null) {
            var list = new List<List<T>> ();
            
            var model = new CsvModel(text);
            if (model.IsInvalid) return list;
            if (parameters == null) parameters = new CsvReadTableParams<T>();

            foreach (var values in model.Lines) {
                if (values.Length == 0) continue;

                var entry = new List<T> ();
                for (var j = parameters.excludeFirstColumn ? 1 : 0; j < model.FieldsCount && j < values.Length; j++) {
                    var value = values [j];
                    value = value.TrimStart ('\"').TrimEnd ('\"');//.Replace ("\\", "");
//                    if (value.Contains(',')) 
//                        value = value.Replace(',', '.');

                    if (parameters.converter == null)
                    {
                        entry.Add((T) Convert(value, typeof(T)));
                    }
                    else
                    {
                        entry.Add(parameters.converter.Invoke(value));
                    }
                }
                list.Add (entry);
            }
            return list;
        }
        
        public static Dictionary<string, T> ReadAsMap<T>(string text, CsvReadMapParams parameters = null) where T : new()
        {
            var map = new Dictionary<string, T> ();
            
            var model = new CsvModel(text);
            if (model.IsInvalid) return map;
            if (parameters == null) parameters = new CsvReadMapParams();

            foreach (var values in model.Lines) {
                if (values.Length == 0) continue;
                if (values.All(string.IsNullOrEmpty)) continue;


                var entry = new T();
                string key = null;

                var type = entry.GetType ();
                for (var j = 0; j < model.FieldsCount && j < values.Length; j++) {
                    var value = values [j].TrimStart ('\"').TrimEnd ('\"');
                    var fieldName = model.GetField(j);
                    if (fieldName == parameters.keyField)
                    {
                        key = value;
                    }
                    var field = type.GetField (fieldName);
                    var prop = field != null ? null : type.GetProperty (fieldName);
                    if (field != null) {
                        field.SetValue (entry, Convert (value, field.FieldType));
                    } else if (prop != null) {
                        prop.SetValue (entry, Convert (value, prop.PropertyType), null);
                    } else {
//                        Debug.Log ("Unknown props \"" + header [j] + "\" in " + type.FullName);
                    }
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    map.Add(key, entry);
                }
                
            }
            return map;
        }
        
        public static List<T> ReadFromFile<T> (string file, Func<string, T> constructor = null, string typeField = null) where T : new()
        { 
//            Object textAsset = Resources.Load(file);
            /*if (textAsset)
            {
                Read<T>(textAsset.text, constructor, typeField);
            }*/
            return new List<T>();
        }

        public static List<T> Read<T>(string text, Func<string, T> constructor = null, string typeField = null)
            where T : new()
        {
            var model = new CsvModel(text);
            return model.IsInvalid ? new List<T>() : Read(model, constructor, typeField);
        }

        public static Task<List<T>> ReadAsync<T>(string text, Func<string, T> constructor = null,
            string typeField = null) where T : new()
        {
            if (typeof(Object).IsAssignableFrom(typeof(T)))
                throw new ArgumentException(
                    "Can't perform async operation on the object inherited from UnityEngine.Object");
            
            return Task.Run(() => Read(text, constructor, typeField));
        }
        
        public static List<T> Read<T> (CsvModel model, Func<string, T> constructor = null, string typeField = null) where T : new()
        {
            try
            {
                var list = new List<T>();
                if (model.IsInvalid) return list;

                var typeIdx = model.GetFieldIdx(typeField);

//            string [] prev = new string [header.Length];
                string typeValue = null;
                T entry;

                foreach (var values in model.Lines)
                {
//                Debug.Log(lines[i]);
                    if (values.Length == 0) continue;
                    if (values.All(string.IsNullOrEmpty)) continue;

                    if (constructor != null)
                    {
                        if (values.Length > typeIdx && typeIdx >= 0 && values[typeIdx].Length > 0)
                            typeValue = values[typeIdx];
                        else
                        {
//                        Debug.Log("header: " + string.Join(",", header) + "\nvalues: " + string.Join(",", values));
                        }

                        entry = constructor(typeValue);
                    }
                    else
                    {
                        entry = new T();
                    }

                    var type = entry.GetType();
                    for (var j = 0; j < model.FieldsCount && j < values.Length; j++)
                    {
                        string value = values[j];
//                    if (value.Length <= 0 && prev [j] != null) value = prev [j];
//                    else prev [j] = value;

                        value = value.TrimStart('\"').TrimEnd('\"'); //.Replace ("\\", "");
//                    if (value.Contains(',')) 
//                        value = value.Replace(',', '.');

                        var fieldName = model.GetField(j);
                        var field = type.GetField(fieldName);
                        var prop = field != null ? null : type.GetProperty(fieldName);
                        if (field != null)
                        {
                            field.SetValue(entry, Convert(value, field.FieldType));
                        }
                        else if (prop != null)
                        {
                            prop.SetValue(entry, Convert(value, prop.PropertyType), null);
                        }
                        else
                        {
//                        Debug.Log ("Unknown props \"" + header [j] + "\" in " + type.FullName);
                        }
                    }

                    list.Add(entry);
                }

                return list;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }

        public static object Convert (string value, Type type)
        {
            if (type == typeof(string)) 
                return value;
            
            if (type.IsValueType && value == "")
            {
                if (type == typeof(bool)) 
                    return false;
                
                value = "0";
            }
            
            try
            {
                return type.IsEnum ? Enum.Parse(type, value) : System.Convert.ChangeType(value, type, new CultureInfo("en-US"));
            }
            catch
            {
                Debug.LogError("Convert Error: " + value + " => type " + type.Name);
                return value;
            }
        }
    }
}
