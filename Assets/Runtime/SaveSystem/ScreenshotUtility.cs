using System;
using UnityEngine;

namespace Runtime.SaveSystem
{
    public static class ScreenshotUtility
    {
        private const int width = 512;
        private const int height = 288;
        
        public static Texture2D Base64ToTexture(string base64)
        {
            if (string.IsNullOrEmpty(base64))
            {
                return null;
            }

            var bytes = Convert.FromBase64String(base64);
            var texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);

            return texture;
        }
        
        public static string CaptureScreenshotFromMainCameraWithoutUI()
        {
            var camera = Camera.main;
            
            return CaptureScreenshotWithTempCamera(camera!.transform.position, camera.transform.rotation);
        }
        
        private static string CaptureScreenshotAsBase64(Camera camera)
        {
            var renderTexture = new RenderTexture(width, height, 24);
            var currentRT = RenderTexture.active;

            var previousTarget = camera.targetTexture;

            try
            {
                camera.targetTexture = renderTexture;

                camera.Render();

                RenderTexture.active = renderTexture;

                var screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
                screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
                screenshot.Apply();

                var bytes = screenshot.EncodeToPNG();
                var base64 = Convert.ToBase64String(bytes);

                UnityEngine.Object.Destroy(screenshot);

                return base64;
            }
            finally
            {
                camera.targetTexture = previousTarget;
                RenderTexture.active = currentRT;

                renderTexture.Release();
                UnityEngine.Object.Destroy(renderTexture);
            }
        }

        private static string CaptureScreenshotWithTempCamera(Vector3 position, Quaternion rotation)
        {
            var tempCameraObj = new GameObject("TempScreenshotCamera");
            var tempCamera = tempCameraObj.AddComponent<Camera>();

            try
            {
                var camera = Camera.main;
                
                tempCamera.transform.position = position;
                tempCamera.transform.rotation = rotation;

                tempCamera.fieldOfView = camera!.fieldOfView;
                tempCamera.nearClipPlane = camera.nearClipPlane;
                tempCamera.farClipPlane = camera.farClipPlane;

                tempCamera.cullingMask = LayerMask.GetMask("Default");
                
                tempCamera.enabled = false;

                return CaptureScreenshotAsBase64(tempCamera);
            }
            finally
            {
                UnityEngine.Object.Destroy(tempCameraObj);
            }
        }
    }
}