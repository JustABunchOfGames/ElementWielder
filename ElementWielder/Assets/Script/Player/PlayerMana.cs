using Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerMana
    {
        public class ManaValue
        {
            public int maxMana { get; private set; }
            private int _baseMaxMana;
            private int _maxManaBonus;
            private float _maxManaPercentMultiplier;

            public int currentMana { get; private set; }
            private float _currentManaPercentMultiplier;

            public ManaValue(int maxMana)
            {
                this.maxMana = maxMana;
                _baseMaxMana = maxMana;
                currentMana = 0;

                _maxManaBonus = 0;
                _maxManaPercentMultiplier = 100f;

                _currentManaPercentMultiplier = 100f;
            }

            public void AddMana(int value)
            {
                float mana = value * _currentManaPercentMultiplier / 100f;

                if (currentMana + mana > maxMana)
                    currentMana = maxMana;
                else
                    currentMana += (int)mana;
            }

            public bool UseMana(int value)
            {
                if (currentMana - value < 0)
                    return false;

                currentMana -= value;
                return true;
            }

            public void AddManaBonus(int maxManaBonus, float maxManaPercentMultiplier, float currentManaPercentMultiplier)
            {
                _currentManaPercentMultiplier += currentManaPercentMultiplier;

                _maxManaBonus += maxManaBonus;
                _maxManaPercentMultiplier += maxManaPercentMultiplier;


                float mana = (_baseMaxMana + _maxManaBonus) * _maxManaPercentMultiplier / 100f;
                maxMana = (int)mana;
            }
        }

        private int _maxMana;

        Dictionary<ElementType, ManaValue> _mana;

        public static ManaChangedEvent manaChanged = new ManaChangedEvent();

        public PlayerMana(int maxMana)
        {
            _maxMana = maxMana;

            _mana = new Dictionary<ElementType, ManaValue>();
        }

        public void Init()
        {
            foreach (ElementType element in Enum.GetValues(typeof(ElementType)))
            {
                _mana.Add(element, new ManaValue(_maxMana));

                InvokeManaEvent(element);
            }
        }

        public bool UseMana(ElementType element, int value)
        {
            bool manaUsed = _mana[element].UseMana(value);

            if (manaUsed)
                InvokeManaEvent(element);

            return manaUsed;
        }

        public void AddMana(ElementType element, int value)
        {
            _mana[element].AddMana(value);

            InvokeManaEvent(element);
        }

        private void InvokeManaEvent(ElementType element)
        {
            ManaValue manaValue = _mana[element];
            manaChanged.Invoke(element, manaValue.currentMana, manaValue.maxMana);
        }

        public void AddManaBonus(ElementType element, int maxManaBonus, float maxManaPercentMultiplier, float currentManaPercentMultiplier)
        {
            _mana[element].AddManaBonus(maxManaBonus, maxManaPercentMultiplier, currentManaPercentMultiplier);
            InvokeManaEvent(element);
        }

        public class ManaChangedEvent : UnityEvent<ElementType, int, int> { }
    }
}