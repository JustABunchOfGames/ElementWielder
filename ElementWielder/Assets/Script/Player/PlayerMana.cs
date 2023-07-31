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

            public int currentMana { get; private set; }

            public ManaValue(int maxMana)
            {
                this.maxMana = maxMana;
            }

            public void AddMana(int value)
            {
                if (currentMana + value > maxMana)
                    currentMana = maxMana;
                else
                    currentMana += value;
            }

            public bool UseMana(int value)
            {
                if (currentMana - value < 0)
                    return false;

                currentMana -= value;
                return true;
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
            foreach (ElementType type in Enum.GetValues(typeof(ElementType)))
            {
                _mana.Add(type, new ManaValue(_maxMana));

                InvokeManaEvent(type);
            }
        }

        public bool UseMana(ElementType type, int value)
        {
            bool manaUsed = _mana[type].UseMana(value);

            if (manaUsed)
                InvokeManaEvent(type);

            return manaUsed;
        }

        public void AddMana(ElementType type, int value)
        {
            _mana[type].AddMana(value);

            InvokeManaEvent(type);
        }

        private void InvokeManaEvent(ElementType type)
        {
            if (manaChanged == null)
                return;

            ManaValue manaValue = _mana[type];
            manaChanged.Invoke(type, manaValue.currentMana, manaValue.maxMana);
        }

        public class ManaChangedEvent : UnityEvent<ElementType, int, int> { }
    }
}