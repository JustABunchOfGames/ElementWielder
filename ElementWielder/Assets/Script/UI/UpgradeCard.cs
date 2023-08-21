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

            Upgrade upgrade = indexedUpgrade.GetLatestUpgrade();

            // Prefab
            if (upgrade.isPrefabUpgrade)
            {
                _description.text += "Change attack";
            }

            // Attack
            if (upgrade.isAttackUpgrade)
            {
                foreach (AttackUpgrade attackUpgrade in upgrade.attackUpgrades)
                {
                    if (attackUpgrade.atkAddBonus != 0)
                        _description.text += Environment.NewLine + attackUpgrade.element.ToString() +
                            " atk " + (attackUpgrade.atkAddBonus > 0 ? "+" : "") + attackUpgrade.atkAddBonus;

                    if (attackUpgrade.atkPercentMultiplier != 0)
                        _description.text += Environment.NewLine + attackUpgrade.element.ToString() +
                            " atk " + (attackUpgrade.atkPercentMultiplier > 0? "+" : "") + attackUpgrade.atkPercentMultiplier + "%";
                    
                }
            }

            // Cooldown
            if (upgrade.isCooldownUpgrade)
            {
                foreach (CooldownUpgrade cooldownUpgrade in upgrade.cooldownUpgrades)
                {
                    _description.text += Environment.NewLine + cooldownUpgrade.element.ToString() +
                        " cooldown " + (cooldownUpgrade.cooldownPercentBonus > 0 ? "+" : "") + cooldownUpgrade.cooldownPercentBonus + "%";
                }
            }

            // ManaCost
            if (upgrade.isManaCostUpgrade)
            {
                foreach(ManaCostUpgrade manaCostUpgrade in upgrade.manaCostUpgrades)
                {
                    if (manaCostUpgrade.manaCostBonus != 0)
                        _description.text += Environment.NewLine + manaCostUpgrade.element.ToString() +
                            " mana cost " + (manaCostUpgrade.manaCostBonus > 0 ? "+" : "") + manaCostUpgrade.manaCostBonus;

                    if (manaCostUpgrade.manaCostPercentMultiplier != 0)
                        _description.text += Environment.NewLine + manaCostUpgrade.element.ToString() +
                            " mana cost " + (manaCostUpgrade.manaCostPercentMultiplier > 0 ? "+" : "") + manaCostUpgrade.manaCostPercentMultiplier + "%";
                }
            }

            // Player Health
            if (upgrade.isHealthUpgrade)
            {
                if (upgrade.healthUpgrade.maxHealthBonus != 0)
                    _description.text += Environment.NewLine +
                        " health " + (upgrade.healthUpgrade.maxHealthBonus > 0 ? "+" : "") + upgrade.healthUpgrade.maxHealthBonus;

                if (upgrade.healthUpgrade.maxHealthPercentMultiplier != 0)
                    _description.text += Environment.NewLine +
                        " health " + (upgrade.healthUpgrade.maxHealthPercentMultiplier > 0 ? "+" : "") + upgrade.healthUpgrade.maxHealthPercentMultiplier + "%";
            }

            // Player Mana
            if (upgrade.isManaUpgrade)
            {
                foreach (ManaUpgrade manaUpgrade in upgrade.manaUpgrades)
                {
                    if (manaUpgrade.maxManaBonus != 0)
                        _description.text += Environment.NewLine + manaUpgrade.element.ToString() +
                            " max mana " + (manaUpgrade.maxManaBonus > 0 ? "+" : "") + manaUpgrade.maxManaBonus;

                    if (manaUpgrade.maxManaPercentMultiplier != 0)
                        _description.text += Environment.NewLine + manaUpgrade.element.ToString() +
                            " max mana " + (manaUpgrade.maxManaPercentMultiplier > 0 ? "+" : "") + manaUpgrade.maxManaPercentMultiplier + "%";

                    if (manaUpgrade.currentManaPercentMultiplier != 0)
                        _description.text += Environment.NewLine + manaUpgrade.element.ToString() +
                            " mana gain " + (manaUpgrade.currentManaPercentMultiplier > 0 ? "+" : "") + manaUpgrade.currentManaPercentMultiplier + "%";
                }
            }
        }

        public void ChooseUpgrade()
        {
            _upgradeUI.ChooseAnUpgrade(_indexedUpgrade);
        }
    }
}