
using System;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Currency")]
    public class CurrencyEffect : EcsEffect
    {
        [SerializeField] private Profile.CurrencyType currency;
        
        public void ChangeCurrency(int value)
        {
            switch (currency)
            {
                case Profile.CurrencyType.Soft:
                    OneLabConfiguration.Profile.SetSoft(OneLabConfiguration.Profile.Soft + value);
                    break;
                case Profile.CurrencyType.Hard:
                    OneLabConfiguration.Profile.SetHard(OneLabConfiguration.Profile.Hard + value);
                    break;
                case Profile.CurrencyType.Points:
                    OneLabConfiguration.Profile.SetPoints(OneLabConfiguration.Profile.Points + value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void SetCurrency(int value)
        {
            switch (currency)
            {
                case Profile.CurrencyType.Soft:
                    OneLabConfiguration.Profile.SetSoft(value);
                    break;
                case Profile.CurrencyType.Hard:
                    OneLabConfiguration.Profile.SetHard(value);
                    break;
                case Profile.CurrencyType.Points:
                    OneLabConfiguration.Profile.SetPoints(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}