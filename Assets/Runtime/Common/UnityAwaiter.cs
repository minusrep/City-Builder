using System.Threading.Tasks;
using UnityEngine;

namespace Runtime.Common
 {
     public static class UnityAwaiter
     {
         public static async Task NextFrame()
         {
             await Task.Yield();
             await Task.Yield();
         }
 
         public static async Task EndOfFrame()
         {
             var frame = Time.frameCount;
             
             while (Time.frameCount == frame)
             {
                 await Task.Yield();
             }
         }
     }
 }