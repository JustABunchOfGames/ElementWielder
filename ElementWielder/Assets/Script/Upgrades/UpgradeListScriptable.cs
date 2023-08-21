using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName ="Upgrades/Upgrade list")]
    public class UpgradeListScriptable : ScriptableObject
    {
        // Upgrade that change attacks prefab, usable by all element
        [field :SerializeField] public List<UpgradeSeries> prefabUpgradeList { get; private set; }

        // Upgrade by element
        [field: SerializeField] public List<ElementalUpgradeList> elementalUpgradeList { get; private set; }

        public List<UpgradeSeries> GetElementalUpgrade(ElementType type)
        {
            foreach(ElementalUpgradeList list in elementalUpgradeList)
            {
                if (list.type == type)
                    return list.upgradeList;
            }
            return null;
        }

        [Serializable]
        public class ElementalUpgradeList
        {
            public ElementType type;
            public List<UpgradeSeries> upgradeList;
        }
    }
}