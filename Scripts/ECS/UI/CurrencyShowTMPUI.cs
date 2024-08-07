
using System;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations;
using Plugins.Exerussus._1Lab.Scripts.Extensions;
using TMPro;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.UI
{
    [AddComponentMenu("1Lab/UI/CurrencyShowTMP")]
    public class CurrencyShowTMPUI : MonoSignalListener
    {
        [SerializeField] private Profile.CurrencyType currency;
        [SerializeField] private string prefix;
        [SerializeField] private string postfix;
        [SerializeField] private TMP_Text tmpText;
        [SerializeField, HideInInspector] private OneLabConfiguration oneLabConfiguration;
        
        public OneLabConfiguration OneLabConfiguration => oneLabConfiguration;
        
        private void OnEnable()
        {
            if (tmpText == null) return;

            switch (currency)
            {
                case Profile.CurrencyType.Soft:
                    OneLabConfiguration.Profile.OnSoftUpdate += UpdateText;
                    break;
                case Profile.CurrencyType.Hard:
                    OneLabConfiguration.Profile.OnHardUpdate += UpdateText;
                    break;
                case Profile.CurrencyType.Points:
                    OneLabConfiguration.Profile.OnPointsUpdate += UpdateText;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDisable()
        {
            if (tmpText == null) return;
            
            switch (currency)
            {
                case Profile.CurrencyType.Soft:
                    OneLabConfiguration.Profile.OnSoftUpdate -= UpdateText;
                    break;
                case Profile.CurrencyType.Hard:
                    OneLabConfiguration.Profile.OnHardUpdate -= UpdateText;
                    break;
                case Profile.CurrencyType.Points:
                    OneLabConfiguration.Profile.OnPointsUpdate -= UpdateText;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (oneLabConfiguration == null)
            {
                oneLabConfiguration = oneLabConfiguration.TryGetLabConfig();
            }

            if (tmpText == null) tmpText = GetComponent<TMP_Text>();
            
            if (tmpText != null)
            {
                switch (currency)
                {
                    case Profile.CurrencyType.Soft:
                        UpdateText(OneLabConfiguration.Profile.Soft);
                        break;
                    case Profile.CurrencyType.Hard:
                        UpdateText(OneLabConfiguration.Profile.Hard);
                        break;
                    case Profile.CurrencyType.Points:
                        UpdateText(OneLabConfiguration.Profile.Points);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void UpdateText(int value)
        {
            tmpText.text = $"{prefix}{value}{postfix}";
        }
    }
}