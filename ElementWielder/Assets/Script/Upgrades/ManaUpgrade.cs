using Core;
using System;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class ManaUpgrade
    {
        [SerializeField] private ElementType _element;
        [SerializeField] private int _maxManaBonus;
        [SerializeField] private float _maxManaPercentMultiplier;
        [SerializeField] private float _currentManaPercentMultiplier;

        public ElementType element { get { return _element; } set { _element = value; } }
        public int maxManaBonus { get { return _maxManaBonus; } private set { } }
        public float maxManaPercentMultiplier { get { return _maxManaPercentMultiplier; } private set { } }
        public float currentManaPercentMultiplier { get { return _currentManaPercentMultiplier; } private set { } }
    }
}