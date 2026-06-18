using UnityEngine;

namespace CommonDan
{
    public static class VectorExtensions
    {
        public static Vector2 ToVector2(this Vector2Int v)
        {
            return new Vector2(v.x, v.y);
        }
        public static Vector2Int ToVector2Int(this Vector2 v)
        {
            return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        }
        public static Vector3 ToVector3(this Vector3Int v)
        {
            return new Vector3(v.x, v.y, v.z);
        }
        public static Vector3Int ToVector3Int(this Vector3 v)
        {
            return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
        }

        public static Vector3Int ToVector3Int(this Vector2Int v)
        {
            return new Vector3Int(v.x, v.y, 0);
        }
    }
}