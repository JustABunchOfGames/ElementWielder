using System;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class Upgrade
    {
        /*
         * Attack Upgrade
        */
        [Serializable]
        public class AttackUpgrade
        {
            [SerializeField] private int _atkAddBonus;
            [SerializeField] private float _atkMultBonus;
            public int atkAddBonus { get { return _atkAddBonus; } private set { } }
            public float atkMultBonus { get { return _atkMultBonus; } private set { } }
        }

        [SerializeField] private AttackUpgrade _attackUpgrade;
        public AttackUpgrade attackUpgrade { get { return _attackUpgrade; } private set { } }
        public bool isAttackUpgrade { get { return _attackUpgrade.atkAddBonus != 0 || _attackUpgrade.atkMultBonus != 0; } private set { } }

        /*
         * Prefab Upgrade
        */

        [Serializable]
        public class PrefabUpgrade
        {
            [SerializeField] private GameObject _attackPrefab;
            public GameObject attackPrefab { get { return _attackPrefab; } private set { } }
        }

        [SerializeField] private PrefabUpgrade _prefabUpgrade;
        public PrefabUpgrade prefabUpgrade { get { return _prefabUpgrade; } private set { } }
        public bool isPrefabUpgrade { get { return _prefabUpgrade.attackPrefab != null; } private set { } }

        /*
         * Cooldown Upgrade
        */
        [Serializable]
        public class CooldownUpgrade
        {
            [SerializeField] private float _cooldownMultBonus;
            public float cooldownMultBonus { get { return _cooldownMultBonus; } private set { } }
        }

        [SerializeField] private CooldownUpgrade _cooldownUpgrade;
        public CooldownUpgrade cooldownUpgrade { get { return _cooldownUpgrade; } private set { } }
        public bool isCooldownUpgrade { get { return _cooldownUpgrade.cooldownMultBonus != 0; } private set { } }
    }
}