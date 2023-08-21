using Attacks;
using Core;
using Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        private Dictionary<ElementType, List<IndexedUpgrade>> _possibleUpgrade;

        private Dictionary<ElementType, List<IndexedUpgrade>> _obtainedUpgrade;

        [Header("List of all upgrades")]
        [SerializeField] private UpgradeListScriptable _upgradeList;
        [Header("Manager to apply attack upgrades")]
        [SerializeField] private AttackManager _attackManager;
        [Header("Player to apply health/mana upgrades")]
        [SerializeField] private PlayerData _player;

        public static UpgradeDoneEvent upgradeDoneEvent = new UpgradeDoneEvent();

        private void Awake()
        {
            _possibleUpgrade = new Dictionary<ElementType, List<IndexedUpgrade>>();
            _obtainedUpgrade = new Dictionary<ElementType, List<IndexedUpgrade>>();

            foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
            {
                List<IndexedUpgrade> upgradeList = new List<IndexedUpgrade>();

                foreach (UpgradeSeries upgrade in _upgradeList.GetElementalUpgrade(element))
                {
                    IndexedUpgrade indexedUpgrade = new IndexedUpgrade(upgrade);
                    upgradeList.Add(indexedUpgrade);
                }

                foreach (UpgradeSeries upgrade in _upgradeList.prefabUpgradeList)
                {
                    IndexedUpgrade indexedUpgrade = new IndexedUpgrade(upgrade);
                    upgradeList.Add(indexedUpgrade);
                }

                _possibleUpgrade.Add(element, upgradeList);

                _obtainedUpgrade.Add(element, new List<IndexedUpgrade>());
            }
        }

        public (ElementType, int) GetRandomUpgradeIndex()
        {
            ElementType element = (ElementType)UnityEngine.Random.Range(0, 5);

            int random = (int)(UnityEngine.Random.value * _possibleUpgrade[element].Count);

            return (element, random);
        }

        public IndexedUpgrade GetUpgrade(ElementType element, int index)
        {
            // If prefab, upgrade element should follow the series element
            if (_possibleUpgrade[element][index].GetLatestUpgrade().isPrefabUpgrade)
            {
                _possibleUpgrade[element][index].upgradeSeries.PrefabElement(element);
            }

            return _possibleUpgrade[element][index];
        }

        public void ApplyUpgrade(ElementType element, int index)
        {
            IndexedUpgrade newUpgrade = SaveNewUpgrade(element, index);

            IncrementPossibleUpgrade(element, index);

            // Bonuses actualized, apply it :
            Upgrade upgrade = newUpgrade.GetLatestUpgrade();

            // Change prefab of attack
            if (upgrade.isPrefabUpgrade)
            {
                _attackManager.attacks[element].SetPrefab(upgrade.prefabUpgrade.attackPrefab);
            }

            // Change attack bonuses
            if (upgrade.isAttackUpgrade)
            {
                foreach (AttackUpgrade attackUpgrade in upgrade.attackUpgrades)
                    _attackManager.attacks[attackUpgrade.element].AddAttackBonus(attackUpgrade.atkAddBonus, attackUpgrade.atkPercentMultiplier);
            }

            // Change cooldown
            if (upgrade.isCooldownUpgrade)
            {
                foreach (CooldownUpgrade cooldownUpgrade in upgrade.cooldownUpgrades)
                    _attackManager.attacksCooldowns[cooldownUpgrade.element].ApplyCooldownUpgrade(cooldownUpgrade.cooldownPercentBonus);
            }

            // Change Mana Cost
            if (upgrade.isManaCostUpgrade)
            {
                foreach (ManaCostUpgrade manaCostUpgrade in upgrade.manaCostUpgrades)
                    _attackManager.attacks[manaCostUpgrade.element].AddManaCostBonus(manaCostUpgrade.manaCostBonus, manaCostUpgrade.manaCostPercentMultiplier);
            }

            // Change Player Health
            if (upgrade.isHealthUpgrade)
            {
                _player.health.AddHealthBonus(upgrade.healthUpgrade.maxHealthBonus, upgrade.healthUpgrade.maxHealthPercentMultiplier);
            }

            // Change Player Mana
            if (upgrade.isManaUpgrade)
            {
                foreach(ManaUpgrade manaUpgrade in upgrade.manaUpgrades)
                {
                    _player.mana.AddManaBonus(manaUpgrade.element, manaUpgrade.maxManaBonus, manaUpgrade.maxManaPercentMultiplier, manaUpgrade.currentManaPercentMultiplier);
                }
            }

            // Stating that the upgrades are applied
            // Used in StageUI to go onto next stage
            upgradeDoneEvent.Invoke();
        }

        private IndexedUpgrade SaveNewUpgrade(ElementType element, int index)
        {
            IndexedUpgrade newUpgrade = new IndexedUpgrade(_possibleUpgrade[element][index].upgradeSeries);

            // Searching if the upgradeSeries is already obtained
            // If it's obtained, increment it

            foreach (IndexedUpgrade indexedUpgrade in _obtainedUpgrade[element])
            {
                if (indexedUpgrade.upgradeSeries.upgradeName == newUpgrade.upgradeSeries.upgradeName)
                {
                    indexedUpgrade.IncrementIndex();

                    return indexedUpgrade;
                }
            }

            // If it isn't obtained, save it
            _obtainedUpgrade[element].Add(newUpgrade);

            return newUpgrade;
        }

        private void IncrementPossibleUpgrade(ElementType element, int index)
        {
            bool isPrefabUpgrade = _possibleUpgrade[element][index].GetLatestUpgrade().isPrefabUpgrade;

            // Don't propose another prefab upgrade if one is already selected
            if (isPrefabUpgrade)
            {
                string upgradeName = _possibleUpgrade[element][index].upgradeSeries.upgradeName;

                // Remove all other prefab upgrade for this element
                // All prefab upgrade are at the end of the list
                int count = _possibleUpgrade[element].Count;
                while (count > 0 && isPrefabUpgrade)
                {
                    count--;

                    isPrefabUpgrade = _possibleUpgrade[element][count].GetLatestUpgrade().isPrefabUpgrade;

                    if (isPrefabUpgrade && _possibleUpgrade[element][count].upgradeSeries.upgradeName != upgradeName)
                    {
                        _possibleUpgrade[element].RemoveAt(count);
                    }
                }
                // Last upgrade is the remaining prefab upgrade
                index = _possibleUpgrade[element].Count - 1;
            }

            // If the upgrade is at max, don't propose it again
            if (_possibleUpgrade[element][index].isMaxed())
                _possibleUpgrade[element].RemoveAt(index);
            // Else, propose the next bonus
            else
                _possibleUpgrade[element][index].IncrementIndex();
        }

        public class UpgradeDoneEvent : UnityEvent { }
    }
}