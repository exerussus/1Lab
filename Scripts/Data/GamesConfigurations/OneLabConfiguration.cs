using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations.Luguages;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.Data.GamesConfigurations
{
    public class OneLabConfiguration : ScriptableObject
    {
        [SerializeField] private float tickDelay = 1f;
        [SerializeField] private DebugSettings debug;
        [SerializeField] private Systems systems;
        [SerializeField] private Profile profile;
        [SerializeField] private LanguageType language;
        private readonly LanguageLibrary _languageLibrary = new();
        
        public DebugSettings Debug => debug;
        public Systems Systems => systems;
        public Profile Profile => profile;
        public float TickDelay => tickDelay;

        public LanguageType Language
        {
            get => language;
            set
            {
                if (language != value) _languageLibrary.Initialize(value);
                language = value;
            }
        }
        
        public ILanguage LanguageLibrary
        {
            get
            {
                if (!_languageLibrary.isInitialized) _languageLibrary.Initialize(language);
                return _languageLibrary.Language;
            }
        }

        private void OnValidate()
        {
            _languageLibrary.Initialize(language);
        }
    }
    
    [Serializable]
    public class DebugSettings
    {
        public bool signalLogs;
    }   

    [Serializable]
    public class Systems
    {
        public DestroySystem.Settings destroy;
    }
}