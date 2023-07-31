using Core;
using UnityEngine;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        public PlayerHealth health { get; private set; }

        [SerializeField] private int _maxMana;
        public PlayerMana mana { get; private set; }

        private void Awake()
        {
            health = new PlayerHealth(_maxHealth);
            mana = new PlayerMana(_maxMana);
        }

        private void Start()
        {
            mana.Init();
            health.Init();
        }
    }
}