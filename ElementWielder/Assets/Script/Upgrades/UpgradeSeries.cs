using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName ="Upgrades/New Series")]
    public class UpgradeSeries : ScriptableObject
    {
        [Header("Just a reminder, not used in code")]
        [SerializeField] private ElementType type;

        [Header("Name for UI")]
        [SerializeField] private string _upgradeName;
        public string upgradeName { get { return _upgradeName; } private set { } }

        [SerializeField] private List<Upgrade> _upgrades = new List<Upgrade>();

        public Upgrade GetUpgrade(int index)
        {
            if (0 <= index && index < _upgrades.Count)
                return _upgrades[index];
            else
                return null;
        }

        public int GetMaxIndex()
        {
            return _upgrades.Count - 1;
        }

        public void PrefabElement(ElementType element)
        {
            foreach(Upgrade upgrade in _upgrades)
            {
                upgrade.SetElement(element);
            }
        }
    }
}