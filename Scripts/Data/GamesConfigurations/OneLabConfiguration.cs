using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.Data.GamesConfigurations
{
    [CreateAssetMenu(fileName = "OneLabConfiguration", menuName = "Data/OneLabConfiguration")]
    public class OneLabConfiguration : ScriptableObject
    {
        [SerializeField] private float tickDelay = 1f;
        [SerializeField] private DebugSettings debug;
        [SerializeField] private Systems systems;
        [SerializeField] private Profile profile;
        [SerializeField] private SignalHandler signalHandler;
        
        public Signal Signal => signalHandler.Signal;
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