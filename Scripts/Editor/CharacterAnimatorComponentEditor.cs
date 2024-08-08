#if UNITY_EDITOR

using Exerussus._1Lab.Scripts.ECS.Components;
using UnityEditor;

namespace Exerussus._1Lab.Scripts.InspectorEditor
{

    [CustomEditor(typeof(CharacterAnimatorComponent))]
    public class CharacterAnimatorComponentEditor : Editor
    {
        private SerializedProperty isFalling;
        private SerializedProperty groundCollider2D;
        private SerializedProperty isTouchingCollider;

        private void OnEnable()
        {
            isFalling = serializedObject.FindProperty("isFalling");
            groundCollider2D = serializedObject.FindProperty("groundCollider2D");
            isTouchingCollider = serializedObject.FindProperty("isTouchingCollider");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("autoRun"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("spriteRenderer"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("idle"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fall"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("jump"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("run"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tags"));

            EditorGUILayout.LabelField("Is Falling", isFalling.boolValue ? "True" : "False");
            EditorGUILayout.LabelField("Ground Collider", groundCollider2D.objectReferenceValue != null ? groundCollider2D.objectReferenceValue.name : "None");
            EditorGUILayout.LabelField("Is Touching Target Collider", isTouchingCollider.boolValue ? "True" : "False");

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif