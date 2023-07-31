using Core;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [Serializable]
    public class PlayerHealth
    {
        private int _maxHealth;

        private int _health;

        public static HealthChangedEvent healthChangedEvent = new HealthChangedEvent();
        public static PlayerDied deathEvent = new PlayerDied();

        public PlayerHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
            _health = _maxHealth;
        }

        public void Init()
        {
            healthChangedEvent.Invoke(_health, _maxHealth);
        }

        public void AddHealth(int health)
        {
            _health += health;

            if (_health > _maxHealth)
                _health = _maxHealth;

            healthChangedEvent.Invoke(_health, _maxHealth);
        }

        public void ReduceHealth(int health)
        {
            _health -= health;

            if (_health <= 0)
            {
                _health = 0;
                deathEvent.Invoke();
            }

            healthChangedEvent.Invoke(_health, _maxHealth);
        }

        public class HealthChangedEvent : UnityEvent<int, int> { }

        public class PlayerDied : UnityEvent { }
    }
}