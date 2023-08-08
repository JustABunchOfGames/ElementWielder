using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName ="Upgrades/Upgrade list")]
    public class UpgradeListScriptable : ScriptableObject
    {
        // Upgrade that change attacks prefab, usable by all element
        [field :SerializeField] public List<UpgradeSeries> prefabUpgradeList { get; private set; }

        // Upgrade available for all element
        [field :SerializeField] public List<UpgradeSeries> elementalUpgradeList { get; private set; }

        // Unique upgrades or only available on basic attack
        [field: SerializeField] public List<UpgradeSeries> basicUpgradeList { get; private set; }
    }
}