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

        private Dictionary<Upgrade, (ElementType, int)> _proposedUpgrade;

        private void OnEnable()
        {
            _proposedUpgrade = new Dictionary<Upgrade, (ElementType, int)>();

            foreach (UpgradeCard card in _upgradeCardList)
            {
                ElementType element;
                int index;
                (element, index) = _upgradeManager.GetRandomUpgradeIndex();

                Upgrade upgrade = _upgradeManager.GetUpgrade(element, index);

                while (_proposedUpgrade.ContainsValue((element, index)))
                {
                    (element, index) = _upgradeManager.GetRandomUpgradeIndex();

                    upgrade = _upgradeManager.GetUpgrade(element, index);
                }              

                card.SetCard(element, upgrade);

                _proposedUpgrade.Add(upgrade, (element, index));
            }
        }
        public void ChooseAnUpgrade(Upgrade upgrade)
        {
            ElementType element;
            int index;
            (element, index) = _proposedUpgrade[upgrade];

            _upgradeManager.ApplyUpgrade(element, index);
        }
    }
}