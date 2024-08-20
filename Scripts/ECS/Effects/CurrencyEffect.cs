
using System;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Currency")]
    public class CurrencyEffect : EcsEffect
    {
        [SerializeField] private Profile.CurrencyType currency;
        
        public void AddCurrency(int value)
        {
            switch (currency)
            {
                case Profile.CurrencyType.Soft:
                    OneLabConfiguration.Profile.SetSoft(AddPref(OneLabConstants.Currency.Soft, value));
                    break;
                case Profile.CurrencyType.Hard:
                    OneLabConfiguration.Profile.SetHard(AddPref(OneLabConstants.Currency.Hard, value));
                    break;
                case Profile.CurrencyType.Points:
                    OneLabConfiguration.Profile.SetPoints(AddPref(OneLabConstants.Currency.Points, value));
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
                    OneLabConfiguration.Profile.SetSoft(SetPref(OneLabConstants.Currency.Soft, value));
                    break;
                case Profile.CurrencyType.Hard:
                    OneLabConfiguration.Profile.SetHard(SetPref(OneLabConstants.Currency.Hard, value));
                    break;
                case Profile.CurrencyType.Points:
                    OneLabConfiguration.Profile.SetPoints(SetPref(OneLabConstants.Currency.Points, value));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int AddPref(string key, int value)
        {
            var currencyAmount = PlayerPrefs.GetInt(key);
            var result = currencyAmount + value;
            PlayerPrefs.SetInt(key, result);
            return result;
        }

        private int SetPref(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            return value;
        }
    }
}