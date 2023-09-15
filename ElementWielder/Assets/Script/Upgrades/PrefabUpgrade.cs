using Attacks;
using System;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class PrefabUpgrade
    {
        [SerializeField] private AttackShape _attackPrefab;
        public AttackShape attackPrefab { get { return _attackPrefab; } private set { } }
    }
}