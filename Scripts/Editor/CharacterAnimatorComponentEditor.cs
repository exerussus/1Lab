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
        
        private const string IsFallingRu = "Падает";
        private const string IsFallingEn = "Is Falling";
        
        private const string TouchColliderRu = "Касающийся коллайдер";
        private const string TouchColliderEn = "Touching Target Collider";
        
        private const string TrueEn = "True";
        private const string TrueRu = "Да";
        
        private const string FalseEn = "False";
        private const string FalseRu = "Нет";
        
        private const string NoneEn = "None";
        private const string NoneRu = "Отсутствует";
        
        private const string AutoRunEn = "Auto Run";
        private const string AutoRunRu = "Автоматический запуск";
        
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
            
            SerializedProperty autoRunProperty = serializedObject.FindProperty("autoRun");
            EditorGUILayout.PropertyField(autoRunProperty, new GUIContent(_oneLabConfiguration.Language == OneLabConfiguration.LanguageType.En ? AutoRunEn : AutoRunRu));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("spriteRenderer"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("idle"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fall"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("jump"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("run"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tags"));

            EditorGUILayout.LabelField(_oneLabConfiguration.Language == OneLabConfiguration.LanguageType.En ? IsFallingEn : IsFallingRu, 
                _isFalling.boolValue ? 
                    _oneLabConfiguration.Language == OneLabConfiguration.LanguageType.En ? TrueEn : TrueRu : 
                    _oneLabConfiguration.Language == OneLabConfiguration.LanguageType.En ? FalseEn : FalseRu);
            
            EditorGUILayout.LabelField(_oneLabConfiguration.Language == OneLabConfiguration.LanguageType.En ? TouchColliderEn : TouchColliderRu, 
                _isTouchingCollider.boolValue ? _groundCollider2D.objectReferenceValue.name : _oneLabConfiguration.Language == OneLabConfiguration.LanguageType.En ? NoneEn : NoneRu);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif