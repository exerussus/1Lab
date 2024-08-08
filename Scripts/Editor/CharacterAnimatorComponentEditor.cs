#if UNITY_EDITOR

using Exerussus._1Lab.Scripts.Data.GamesConfigurations;
using Exerussus._1Lab.Scripts.ECS.Components;
using UnityEditor;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.InspectorEditor
{

    [CustomEditor(typeof(CharacterAnimatorComponent))]
    public class CharacterAnimatorComponentEditor : Editor
    {
        private SerializedProperty _isFalling;
        private SerializedProperty _groundCollider2D;
        private SerializedProperty _isTouchingCollider;
        private OneLabConfiguration _oneLabConfiguration;
        
        private const string AssetConfigPath = "Assets/Configs/1Lab/OneLabConfiguration.asset";
        
        private void OnEnable()
        {
            _oneLabConfiguration = AssetDatabase.LoadAssetAtPath<OneLabConfiguration>(AssetConfigPath);
            _isFalling = serializedObject.FindProperty("isFalling");
            _groundCollider2D = serializedObject.FindProperty("groundCollider2D");
            _isTouchingCollider = serializedObject.FindProperty("isTouchingCollider");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            var autoRunProperty = serializedObject.FindProperty("autoRun");
            var spriteRenderer = serializedObject.FindProperty("spriteRenderer");
            var idle = serializedObject.FindProperty("idle");
            var fall = serializedObject.FindProperty("fall");
            var jump = serializedObject.FindProperty("jump");
            var run = serializedObject.FindProperty("run");
            var tags = serializedObject.FindProperty("tags");
            
            EditorGUILayout.PropertyField(autoRunProperty, new GUIContent(_oneLabConfiguration.LanguageLibrary.AutoRun));
            EditorGUILayout.PropertyField(spriteRenderer, new GUIContent("Sprite Renderer"));
            EditorGUILayout.PropertyField(idle, new GUIContent(_oneLabConfiguration.LanguageLibrary.Idle));
            EditorGUILayout.PropertyField(fall, new GUIContent(_oneLabConfiguration.LanguageLibrary.Fall));
            EditorGUILayout.PropertyField(jump, new GUIContent(_oneLabConfiguration.LanguageLibrary.Jump));
            EditorGUILayout.PropertyField(run, new GUIContent(_oneLabConfiguration.LanguageLibrary.Run));
            EditorGUILayout.PropertyField(tags, new GUIContent(_oneLabConfiguration.LanguageLibrary.Tags));

            EditorGUILayout.LabelField(_oneLabConfiguration.LanguageLibrary.IsFalling, 
                _isFalling.boolValue ? _oneLabConfiguration.LanguageLibrary.True : _oneLabConfiguration.LanguageLibrary.False);
            
            EditorGUILayout.LabelField(_oneLabConfiguration.LanguageLibrary.TouchCollider, 
                _isTouchingCollider.boolValue ? _groundCollider2D.objectReferenceValue.name : _oneLabConfiguration.LanguageLibrary.None);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif