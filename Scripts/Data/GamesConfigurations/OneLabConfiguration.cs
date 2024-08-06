using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.Data.GamesConfigurations
{
    [CreateAssetMenu(fileName = "OneLabConfiguration", menuName = "Exerussus/Configs/OneLabConfiguration")]
    public class OneLabConfiguration : ScriptableObject
    {
        [SerializeField] private float tickDelay = 1f;
        [SerializeField] private DebugSettings debug;
        [SerializeField] private Systems systems;
        [SerializeField] private Profile profile;
        
        public DebugSettings Debug => debug;
        public Systems Systems => systems;
        public Profile Profile => profile;
        public float TickDelay => tickDelay;
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