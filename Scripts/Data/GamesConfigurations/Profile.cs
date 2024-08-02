
using System;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.Data.GamesConfigurations
{
    [Serializable]
    public class Profile
    {
        [SerializeField] private int points;
        [SerializeField] private int soft;
        [SerializeField] private int hard;

        public event Action<int> OnPointsUpdate;
        public event Action<int> OnHardUpdate;
        public event Action<int> OnSoftUpdate;

        public int Points => points;

        public int Soft => soft;

        public int Hard => hard;

        public void SetPoints(int value)
        {
            points = value;
            OnPointsUpdate?.Invoke(points);
        }
        
        public void SetSoft(int value)
        {
            soft = value;
            OnSoftUpdate?.Invoke(soft);
        }
        
        public void SetHard(int value)
        {
            hard = value;
            OnHardUpdate?.Invoke(hard);
        }

        public enum CurrencyType
        {
            Soft, 
            Hard, 
            Points
        }
    }
}