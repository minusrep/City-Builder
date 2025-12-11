using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runtime.Extensions
{
    public static class SerializationExtensions
    {
        public static string GetString(this Dictionary<string, object> dictionary, string key)
        {
            return (string)dictionary[key];
        }

        public static int GetInt(this Dictionary<string, object> dictionary, string key)
        {
            return Convert.ToInt32(dictionary[key]);
        }

        public static float GetFloat(this Dictionary<string, object> dictionary, string key)
        {
            return Convert.ToSingle(dictionary[key]);
        }
        
        public static long GetLong(this Dictionary<string, object> dictionary, string key)
        {
            return Convert.ToInt64(dictionary[key]);
        }

        public static bool GetBool(this Dictionary<string, object> dictionary, string key)
        {
            return Convert.ToBoolean(dictionary[key]);
        }
        
        public static List<T> GetList<T>(this Dictionary<string, object> dictionary, string key)
        {
            var list = new List<T>();
            foreach (var obj in dictionary.GetList(key))
            {
                list.Add((T)Convert.ChangeType(obj, typeof(T)));
            }
            return list;
        }

        public static Queue<T> GetQueue<T>(this Dictionary<string, object> dictionary, string key)
        {
            var list = dictionary.GetList(key);
            return new Queue<T>(list.Select(obj => (T)Convert.ChangeType(obj, typeof(T))));
        }

        public static Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(this Dictionary<string, object> dictionary, string key)
        {
            var result = new Dictionary<TKey, TValue>();

            foreach (var pair in dictionary.GetNode(key))
            {
                var typedKey = (TKey)Convert.ChangeType(pair.Key, typeof(TKey));
                var value = (TValue)Convert.ChangeType(pair.Value, typeof(TValue));

                result[typedKey] = value;
            }

            return result;
        }

        public static Dictionary<string, object> GetNode(this Dictionary<string, object> dictionary, string key)
        {
            return (Dictionary<string, object>)dictionary[key];
        }

        public static Vector2 GetVector2(this Dictionary<string, object> dictionary, string key)
        {
            var list = (List<object>)dictionary[key];
            return new Vector2(
                Convert.ToSingle(list[0]),
                Convert.ToSingle(list[1])
            );
        }

        public static Vector3 GetVector3(this Dictionary<string, object> dictionary, string key)
        {
            var list = (List<object>)dictionary[key];
            return new Vector3(
                Convert.ToSingle(list[0]),
                Convert.ToSingle(list[1]),
                Convert.ToSingle(list[2])
            );
        }
        
        public static Dictionary<string, object> ToJson<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            var result = new Dictionary<string, object>();
            foreach (var kv in dictionary)
            {
                result[kv.Key.ToString()] = kv.Value;
            }
            return result;
        }

        public static void Set(this Dictionary<string, object> dictionary, string key, object value)
        {
            dictionary[key] = value;
        }

        public static List<object> ToList(this Vector2 vector)
        {
            return new List<object> { vector.x, vector.y };
        }

        public static List<object> ToList(this Vector3 vector)
        {
            return new List<object> { vector.x, vector.y, vector.z };
        }
        
        private static List<object> GetList(this Dictionary<string, object> dictionary, string key)
        {
            return (List<object>)dictionary[key];
        }
    }
}