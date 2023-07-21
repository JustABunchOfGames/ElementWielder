
using Core;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerMana
    {
        Dictionary<ElementType, int> _mana;

        private int _maxMana = 100;

        private ManaUI _manaUI;

        public PlayerMana(ManaUI ui)
        {
            _manaUI = ui;

            _mana = new Dictionary<ElementType, int>();

            foreach(ElementType type in Enum.GetValues(typeof(ElementType)))
            {
                _mana.Add(type, 0);
                _manaUI.UpdateManaUI(type, _mana[type], _maxMana);
            }
        }

        public bool UseMana(ElementType type, int value)
        {
            if (_mana[type] < value)
                return false;

            _mana[type] -= value;

            _manaUI.UpdateManaUI(type, _mana[type], _maxMana);

            return true;
        }

        public void AddMana(ElementType type, int value)
        {
            _mana[type] += value;

            if (_mana[type] > _maxMana)
            {
                _mana[type] = _maxMana;
            }

            Debug.Log("AddMana : " + value);

            _manaUI.UpdateManaUI(type, _mana[type], _maxMana);
        }
    }
}