using Core;
using System;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class HealthUpgrade
    {
        [SerializeField] private int _maxHealthBonus;
        [SerializeField] private float _maxHealthPercentMultiplier;
        public int maxHealthBonus { get { return _maxHealthBonus; } private set { } }
        public float maxHealthPercentMultiplier { get { return _maxHealthPercentMultiplier; } private set { } }
    }
}