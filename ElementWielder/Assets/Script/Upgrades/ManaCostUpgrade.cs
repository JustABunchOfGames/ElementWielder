using Core;
using System;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class ManaCostUpgrade
    {
        [SerializeField] private ElementType _element;
        [SerializeField] private int _manaCostBonus;
        [SerializeField] private float _manaCostPercentMultiplier;

        public ElementType element { get { return _element; } set { _element = value; } }
        public int manaCostBonus { get { return _manaCostBonus; } private set { } }
        public float manaCostPercentMultiplier { get { return _manaCostPercentMultiplier; } private set { } }
    }
}