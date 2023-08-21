using System;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class PrefabUpgrade
    {
        [SerializeField] private GameObject _attackPrefab;
        public GameObject attackPrefab { get { return _attackPrefab; } private set { } }
    }
}