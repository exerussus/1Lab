﻿
using Exerussus._1Extensions;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    public abstract class EcsEffect : MonoSignalListener
    {
        [SerializeField] private bool activated = true;
        [SerializeField, HideInInspector] private OneLabConfiguration oneLabConfiguration;
        
        public OneLabConfiguration OneLabConfiguration => oneLabConfiguration;
        public bool Activated => activated;

        public void SetActivated(bool isActivated)
        {
            activated = isActivated;
        }
        
        public void Activate()
        {
            activated = true;
        }
        
        public void Deactivate()
        {
            activated = false;
        }

        protected override void OnValidate()
        {
            base.OnValidate(); 
            oneLabConfiguration = ConfigLoader.GetOrCreate<OneLabConfiguration>("1Lab");
        }
    }
}