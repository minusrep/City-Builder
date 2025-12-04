using System.Collections.Generic;
using UnityEngine;
using System;

namespace Runtime.Utilities
{
    public static class SerializationExtensions
    {
        public static string GetString(this Dictionary<string, object> dict, string key)
        {
            return (string)dict[key];
        }

        public static int GetInt(this Dictionary<string, object> dict, string key)
        {
            return Convert.ToInt32(dict[key]);
        }

        public static float GetFloat(this Dictionary<string, object> dict, string key)
        {
            return Convert.ToSingle(dict[key]);
        }
        
        public static long GetLong(this Dictionary<string, object> dict, string key)
        {
            return Convert.ToInt64(dict[key]);
        }

        public static bool GetBool(this Dictionary<string, object> dict, string key)
        {
            return Convert.ToBoolean(dict[key]);
        }
        
        public static List<object> GetList(this Dictionary<string, object> dict, string key)
        {
            return (List<object>)dict[key];
        }

        public static Dictionary<string, object> GetDict(this Dictionary<string, object> dict, string key)
        {
            return (Dictionary<string, object>)dict[key];
        }

        public static Vector2 GetVector2(this Dictionary<string, object> dict, string key)
        {
            var list = (List<object>)dict[key];
            return new Vector2(
                Convert.ToSingle(list[0]),
                Convert.ToSingle(list[1])
            );
        }

        public static Vector3 GetVector3(this Dictionary<string, object> dict, string key)
        {
            var list = (List<object>)dict[key];
            return new Vector3(
                Convert.ToSingle(list[0]),
                Convert.ToSingle(list[1]),
                Convert.ToSingle(list[2])
            );
        }

        public static void Set(this Dictionary<string, object> dict, string key, object value)
        {
            dict[key] = value;
        }

        public static List<object> ToList(this Vector2 v)
        {
            return new List<object> { v.x, v.y };
        }

        public static List<object> ToList(this Vector3 v)
        {
            return new List<object> { v.x, v.y, v.z };
        }
    }
}