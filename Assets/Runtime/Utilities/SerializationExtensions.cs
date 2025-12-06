using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Runtime.Utilities
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

        public static List<object> GetList(this Dictionary<string, object> dictionary, string key)
        {
            var value = dictionary[key];
            return value switch
            {
                List<object> list => list,
                Array array => array.Cast<object>().ToList(),
                _ => null
            };
        }
        
        public static List<T> GetList<T>(this Dictionary<string, object> dictionary, string key)
        {
            var list = new List<T>();
            foreach (var obj in dictionary.GetList(key))
            {
                list.Add((T)obj);
            }
            return list;
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
    }
}