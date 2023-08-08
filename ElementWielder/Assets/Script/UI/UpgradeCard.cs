using Core;
using System;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;

namespace UI
{
    public class UpgradeCard : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _element;
        [SerializeField] private Text _description;
        [SerializeField] private Image _background;

        [SerializeField] private ElementTypeScriptable _elementTypeScriptable;

        private IndexedUpgrade _indexedUpgrade;
        [SerializeField] private UpgradeUI _upgradeUI;

        public void SetCard(ElementType element, IndexedUpgrade indexedUpgrade)
        {
            _name.text = indexedUpgrade.upgradeSeries.upgradeName;

            _element.text = element.ToString();

            _background.material = _elementTypeScriptable.GetMaterialByElement(element);

            _indexedUpgrade = indexedUpgrade;

            _description.text = "";

            Upgrade upgrade = indexedUpgrade.GetUpgrade();

            // Prefab
            if (upgrade.isPrefabUpgrade)
            {
                _description.text += "Change attack";
            }

            // Attack
            if (upgrade.isAttackUpgrade) 
            {
                if (upgrade.attackUpgrade.atkAddBonus > 0)
                    _description.text += Environment.NewLine + "atk +" + upgrade.attackUpgrade.atkAddBonus;

                if (upgrade.attackUpgrade.atkMultBonus > 0)
                {
                    if (upgrade.attackUpgrade.atkMultBonus > 1f)
                        _description.text += Environment.NewLine + "atk +" + ((1f - upgrade.attackUpgrade.atkMultBonus) * 100) + "%";
                    else if (upgrade.attackUpgrade.atkMultBonus < 1f)
                        _description.text += Environment.NewLine + "atk -" + ((1f - upgrade.attackUpgrade.atkMultBonus) * 100) + "%";
                }
            }

            // Cooldown
            if (upgrade.isCooldownUpgrade)
            {
                if (upgrade.cooldownUpgrade.cooldownMultBonus < 1f)
                    _description.text += Environment.NewLine + "cooldown -" + ((1f - upgrade.cooldownUpgrade.cooldownMultBonus) * 100) + "%";
                else if (upgrade.cooldownUpgrade.cooldownMultBonus > 1f)
                    _description.text += Environment.NewLine + "cooldown +" + ((1f - upgrade.cooldownUpgrade.cooldownMultBonus) * 100) + "%";
            }
        }

        public void ChooseUpgrade()
        {
            _upgradeUI.ChooseAnUpgrade(_indexedUpgrade);
        }
    }
}