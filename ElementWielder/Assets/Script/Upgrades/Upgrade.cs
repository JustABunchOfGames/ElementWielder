using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class Upgrade
    {
        // Set element in all upgrade
        public void SetElement(ElementType element)
        {
            foreach (AttackUpgrade attackUpgrade in _attackUpgrades)
                attackUpgrade.element = element;

            foreach(CooldownUpgrade cooldownUpgrade in _cooldownUpgrades)
                cooldownUpgrade.element = element;
        }

        /*
         * Attack Upgrade
        */
        [SerializeField] private List<AttackUpgrade> _attackUpgrades;
        public List<AttackUpgrade> attackUpgrades { get { return _attackUpgrades; } private set { } }
        public bool isAttackUpgrade { get { return _attackUpgrades != null && _attackUpgrades.Count > 0; } private set { } }

        /*
         * Prefab Upgrade
        */
        [SerializeField] private PrefabUpgrade _prefabUpgrade;
        public PrefabUpgrade prefabUpgrade { get { return _prefabUpgrade; } private set { } }
        public bool isPrefabUpgrade { get { return _prefabUpgrade.attackPrefab != null; } private set { } }

        /*
         * Cooldown Upgrade
        */
        [SerializeField] private List<CooldownUpgrade> _cooldownUpgrades;
        public List<CooldownUpgrade> cooldownUpgrades { get { return _cooldownUpgrades; } private set { } }
        public bool isCooldownUpgrade { get { return _cooldownUpgrades != null && _cooldownUpgrades.Count > 0; } private set { } }

        /*
         * Mana Cost Upgrade
         */
        [SerializeField] private List<ManaCostUpgrade> _manaCostUpgrades;
        public List<ManaCostUpgrade > manaCostUpgrades { get { return _manaCostUpgrades; } private set { } }
        public bool isManaCostUpgrade { get { return _manaCostUpgrades!= null && _manaCostUpgrades.Count > 0; } private set { } }

        /*
         * Health Upgrade
         */
        [SerializeField] private HealthUpgrade _healthUpgrade;
        public HealthUpgrade healthUpgrade { get { return _healthUpgrade; } private set { } }
        public bool isHealthUpgrade { get { return _healthUpgrade != null && (_healthUpgrade.maxHealthBonus != 0 || _healthUpgrade.maxHealthPercentMultiplier != 0); } private set { } }

        /*
         * Mana Upgrade
         */
        [SerializeField] private List<ManaUpgrade> _manaUpgrades;
        public List<ManaUpgrade> manaUpgrades { get { return _manaUpgrades; } private set { } }
        public bool isManaUpgrade { get { return _manaUpgrades != null && _manaUpgrades.Count > 0; } private set { } }
    }
}