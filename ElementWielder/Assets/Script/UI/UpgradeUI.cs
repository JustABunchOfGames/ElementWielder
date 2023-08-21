using Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Upgrades;

namespace UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private List<UpgradeCard> _upgradeCardList;

        [SerializeField] private UpgradeManager _upgradeManager;

        private Dictionary<IndexedUpgrade,(ElementType, int)> _proposedUpgrade;

        private void OnEnable()
        {
            _proposedUpgrade = new Dictionary<IndexedUpgrade, (ElementType, int)>();

            foreach(UpgradeCard card in _upgradeCardList)
            {
                ElementType element;
                int index;
                (element, index) = _upgradeManager.GetRandomUpgradeIndex();

                IndexedUpgrade indexedUpgrade = _upgradeManager.GetUpgrade(element, index);

                while (_proposedUpgrade.ContainsKey(indexedUpgrade))
                {
                    (element, index) = _upgradeManager.GetRandomUpgradeIndex();

                    indexedUpgrade = _upgradeManager.GetUpgrade(element, index);
                }

                card.SetCard(element, indexedUpgrade);

                _proposedUpgrade.Add(indexedUpgrade,(element, index));
            }
        }

        public void ChooseAnUpgrade(IndexedUpgrade indexedUpgrade)
        {
            ElementType element;
            int index;
            (element, index) = _proposedUpgrade[indexedUpgrade];

            _upgradeManager.ApplyUpgrade(element, index);
        }
    }
}