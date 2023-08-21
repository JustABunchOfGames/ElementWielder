using Core;
using System;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class AttackUpgrade
    {
        [SerializeField] private ElementType _element;
        [SerializeField] private int _atkAddBonus;
        [SerializeField] private float _atkPercentMultiplier;

        public ElementType element { get { return _element; } set { _element = value; } }
        public int atkAddBonus { get { return _atkAddBonus; } private set { } }
        public float atkPercentMultiplier { get { return _atkPercentMultiplier; } private set { } }
    }
}