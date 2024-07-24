using System;
using OneLab.Scripts.ECS.Core;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.Data.GamesConfigurations
{
    [CreateAssetMenu(fileName = "OneLabConfiguration", menuName = "Data/OneLabConfiguration")]
    public class OneLabConfiguration : ScriptableObject, IGameShareItem
    {
        [SerializeField] private float tickDelay = 1f;
        [SerializeField] private DebugSettings debug;
        [SerializeField] private Systems systems;
        [SerializeField] private Profile profile;
        public Signal Signal { get; } = new();

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