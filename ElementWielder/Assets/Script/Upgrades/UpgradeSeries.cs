using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(menuName ="Upgrades/New Series")]
    public class UpgradeSeries : ScriptableObject
    {
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
    }
}