using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        private Dictionary<ElementType, List<IndexedUpgrade>> _possibleUpgrade;

        private Dictionary<ElementType, List<IndexedUpgrade>> _obtainedUpgrade;

        [SerializeField] private UpgradeListScriptable _upgradeList;

        private void Awake()
        {
            _possibleUpgrade = new Dictionary<ElementType, List<IndexedUpgrade>>();
            _obtainedUpgrade = new Dictionary<ElementType, List<IndexedUpgrade>>();

            foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
            {
                List<IndexedUpgrade> upgradeList = new List<IndexedUpgrade>();

                if (element == ElementType.None)
                {
                    foreach (UpgradeSeries upgrade in _upgradeList.basicUpgradeList)
                    {
                        IndexedUpgrade indexedUpgrade = new IndexedUpgrade(upgrade);
                        upgradeList.Add(indexedUpgrade);
                    }
                }
                else
                {
                    foreach (UpgradeSeries upgrade in _upgradeList.elementalUpgradeList)
                    {
                        IndexedUpgrade indexedUpgrade = new IndexedUpgrade(upgrade);
                        upgradeList.Add(indexedUpgrade);
                    }
                }

                foreach (UpgradeSeries upgrade in _upgradeList.prefabUpgradeList)
                {
                    IndexedUpgrade indexedUpgrade = new IndexedUpgrade(upgrade);
                    upgradeList.Add(indexedUpgrade);
                }

                _possibleUpgrade.Add(element, upgradeList);
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
            return _possibleUpgrade[element][index];
        }

        public void ApplyUpgrade(ElementType element, int index)
        {
            Debug.Log(GetUpgrade(element, index).upgradeSeries.name);
        }
    }
}