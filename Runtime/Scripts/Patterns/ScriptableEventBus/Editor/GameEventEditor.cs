using CommonDan;
using UnityEditor;
using UnityEngine;

namespace CommonDan
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Raise")) ((GameEvent)target).Raise();

        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(IntGameEvent))]
    public class IntGameEventEditor : Editor
    {
        private int _testValue;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUI.enabled = Application.isPlaying;
            _testValue = EditorGUILayout.IntField("Test Value", _testValue);
            if (GUILayout.Button("Raise")) ((IntGameEvent)target).Raise(_testValue);
        }
    }
#endif
}