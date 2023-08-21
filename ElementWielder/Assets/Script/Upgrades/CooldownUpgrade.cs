using Core;
using System;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class CooldownUpgrade
    {
        [SerializeField] private ElementType _element;
        [SerializeField] private float _cooldownPercentBonus;

        public ElementType element { get { return _element; } set { _element = value; } }
        public float cooldownPercentBonus { get { return _cooldownPercentBonus; } private set { } }
    }
}