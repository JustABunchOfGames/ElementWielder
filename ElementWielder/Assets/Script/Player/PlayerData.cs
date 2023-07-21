using Core;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private int _hp;

        [Header("Mana")]
        [SerializeField] private ManaUI _manaUI;
        private PlayerMana _mana;

        private void Start()
        {
            _mana = new PlayerMana(_manaUI);
        }

        public void ReceiveMana(ElementType type, int value)
        {
            Debug.Log("ReceiveMana :" + value);
            _mana.AddMana(type, value);
        }
    }
}