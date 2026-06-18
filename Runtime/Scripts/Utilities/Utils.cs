using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace CommonDan
{
    public static class Utils
    {
        /// <summary>
        /// Gets the mouse position using the new Unity input system
        /// </summary>
        /// <param name="customDepth">Custom Depth is used to avoid issues when using screentoworld</param>
        /// <returns></returns>
        public static Vector3 GetMousePos(float customDepth = 10)
        {
            Vector3 pos = Mouse.current.position.ReadValue();
            pos.z = customDepth;
            return pos;
        }
    
        /// <summary>
        /// Converts a camera using a render texture screen's position to world
        /// </summary>
        /// <param name="cam">Render texture camera</param>
        /// <param name="image">Render Texture</param>
        /// <param name="screenPos">Screen position</param>
        /// <returns>World Position</returns>
        ///TODO dosent work :(
        public static Vector3 RenderTextureCameraScreenToWorld(this Camera cam, RawImage image, Vector2 screenPos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                image.rectTransform,
                screenPos,
                Camera.main,
                out Vector2 localPoint
            );

            //convert to UV space
            Rect rect = image.rectTransform.rect;
            Vector2 uv = new Vector2(
                (localPoint.x - rect.x) / rect.width,
                (localPoint.y - rect.y) / rect.height
            );

            // check if position is inside the render texture
            if (uv.x < 0 || uv.x > 1 || uv.y < 0 || uv.y > 1) return new Vector3(0,0,0);

            // convert UV to screen point on render texture camera
            Vector2 rtScreenPoint = new Vector2(
                uv.x * cam.targetTexture.width,
                uv.y * cam.targetTexture.height
            );
        
            Vector3 worldPoint = cam.ScreenToWorldPoint(
                new Vector3(rtScreenPoint.x, rtScreenPoint.y, cam.nearClipPlane)
            );
        
            return worldPoint;
        }
    }
}
