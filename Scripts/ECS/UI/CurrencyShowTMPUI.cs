
using System;
using Source.Scripts.Data.GamesConfigurations;
using Source.Scripts.SignalSystem;
using TMPro;
using UnityEngine;

namespace Plugins._1Lab.Scripts.ECS.UI
{
    [AddComponentMenu("1Lab/UI/CurrencyShowTMP")]
    public class CurrencyShowTMPUI : MonoSignalListener
    {
        [SerializeField] private Profile.CurrencyType currency;
        [SerializeField] private string prefix;
        [SerializeField] private string postfix;
        [SerializeField] private TMP_Text tmpText;
        
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