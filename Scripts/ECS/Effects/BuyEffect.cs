
using System;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Buy")]
    public class BuyEffect : EcsEffect
    {
        [SerializeField] private Profile.CurrencyType currency;
        [SerializeField] private int cost;

        public UnityEvent onSuccessBuy;
        public UnityEvent onUnsuccessfulBuy;
        
        public void TryBuy()
        {
            switch (currency)
            {
                case Profile.CurrencyType.Soft:
                    if (OneLabConfiguration.Profile.Soft >= cost)
                    {
                        OneLabConfiguration.Profile.SetSoft(OneLabConfiguration.Profile.Soft - cost);
                        onSuccessBuy?.Invoke();
                    }
                    else onUnsuccessfulBuy?.Invoke();
                    break;
                case Profile.CurrencyType.Hard:
                    if (OneLabConfiguration.Profile.Hard >= cost)
                    {
                        OneLabConfiguration.Profile.SetHard(OneLabConfiguration.Profile.Hard - cost);
                        onSuccessBuy?.Invoke();
                    }
                    else onUnsuccessfulBuy?.Invoke();
                    break;
                case Profile.CurrencyType.Points:
                    if (OneLabConfiguration.Profile.Points >= cost)
                    {
                        OneLabConfiguration.Profile.SetPoints(OneLabConfiguration.Profile.Points - cost);
                        onSuccessBuy?.Invoke();
                    }
                    else onUnsuccessfulBuy?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}